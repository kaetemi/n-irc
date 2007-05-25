/*
 * .nIRC - an IRC client written in C# and IronPython.
 * Copyright (C) 2007  Jan Boon, 
 * Fonteinstraat 65, 1502 Lembeek, Belgium
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Sockets;

namespace nIRC.Irc
{
    class IrcLineReader : IDisposable
    {
        Socket socket;
        byte[] buffer = new byte[16384];
        int restLength = 0;

        public IrcLineReader(Socket socket)
        {
            this.socket = socket;
        }

        public void StartReceiving()
        {
            beginRead();
        }

        void beginRead()
        {
            try
            {
                socket.BeginReceive(buffer, restLength, 16384 - restLength, SocketFlags.None, endRead, null);
            }
            catch { }
        }

        void endRead(IAsyncResult ar)
        {
            try
            {
                SocketError error;
                int length = socket.EndReceive(ar, out error);
                if (length == 0) return;
                length = length + restLength;
                int begin = 0;
                for (int i = restLength; i < length; i++)
                {
                    switch (buffer[i])
                    {
                        case 0x0A:
                        case 0x0D:
                            int line_length = i - begin;
                            if (line_length == 0)
                            {
                                begin++;
                            }
                            else
                            {
                                LineReceived(buffer, begin, line_length);
                                begin = i + 1;
                            }
                            break;
                    }
                }
                restLength = length - begin;
                if (begin == 0) throw new Exception("Received line exceeding the intentional 16384 byte limit. According to the IRC specification, the maximum line lenght should be 512 bytes. This connection will be killed.");
                if (restLength != 0)
                    Buffer.BlockCopy(buffer, begin, buffer, 0, restLength);
                beginRead();
            }
            catch (Exception ex)
            {
                if (ConnectionException != null) ConnectionException(ex.Message);
            }
        }

        public event BufferCallback LineReceived;
        public event StringCallback ConnectionException;

        #region IDisposable Members

        public void Dispose()
        {
            this.LineReceived = null;
            this.ConnectionException = null;
        }

        #endregion
    }
}
