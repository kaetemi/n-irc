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
using System.Collections.ObjectModel;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace nIRC.Irc
{
    partial class IrcClientConnection : IDisposable
    {
        public Collection<string> Data_MOTD = new Collection<string>();
        public bool Data_MOTD_END = true;

        public Socket socket;
        string server;
        int port;
        IrcLineReader lineReader;
        string nickname = "";
        public string NickName
        {
            get { return nickname; }
            set { nickname = value; if (NickNameChanged != null) NickNameChanged(value); }
        }
        public event StringCallback NickNameChanged;
        string servername;
        public string ServerName
        {
            get { return servername; }
            set { servername = value; if (ServerNameChanged != null) ServerNameChanged(value); }
        }
        public event StringCallback ServerNameChanged;

        public IrcClientConnection(string server, int port)
        {
            this.server = server;
            this.servername = server;
            this.port = port;
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect()
        {
            socket.BeginConnect(server, port, endConnect, null);
        }

        void endConnect(IAsyncResult ar)
        {
            try
            {
                socket.EndConnect(ar);
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger, new LingerOption(true, System.UInt16.MaxValue));
                socket.NoDelay = true;
                lineReader = new IrcLineReader(socket);
                lineReader.LineReceived += new BufferCallback(lineReceived);
                lineReader.ConnectionException += new StringCallback(connectionException);
                lineReader.StartReceiving();
                if (Connected != null) Connected(this);
            }
            catch (Exception ex)
            {
                connectionException(ex.Message);
            }
        }

        void connectionException(string data)
        {
            if (ConnectionException != null) ConnectionException(data);
            this.Close();
        }

        void lineReceived(byte[] buffer, int from, int length)
        {
            try
            {
                string[] data = SplitByteString(buffer, from, length);
                int pi = 1;
                IrcUser source;
                string command = data[0];
                if (command[0] != ':') source = new IrcUser();
                else
                {
                    source = new IrcUser(command);
                    command = data[1];
                    pi = 2;
                }
                switch (command)
                {
                    case "PING":
                        if (data.Length - pi > 0)
                        {
                            Msg_PONG(data[pi]);
                            if (Received_PING != null) Received_PING(data[pi]);
                            else goto default;
                        }
                        else goto default;
                        break;
                    case "NOTICE": // INCOMPLETE
                        if (isCtcp(data[pi + 1])) ctcpReplyReceived(source, data[pi], getCtcp(data[pi + 1]));
                        else if (Received_NOTICE != null) Received_NOTICE(source, data[pi], data[pi + 1]);
                        else goto default;
                        break;
                    case "PRIVMSG": // CTCP NOT IMPLEMENTED
                        if (isCtcp(data[pi + 1])) ctcpRequestReceived(source, data[pi], getCtcp(data[pi + 1]));
                        else if (Received_PRIVMSG != null) Received_PRIVMSG(source, data[pi], data[pi + 1]);
                        else goto default;
                        break;
                    case "JOIN":
                        if (Received_JOIN != null) Received_JOIN(source, data[pi]);
                        else goto default;
                        break;
                    case "PART":
                        if (Received_PART != null)
                        {
                            if (data.Length > pi + 1) Received_PART(source, data[pi], data[pi + 1]);
                            else Received_PART(source, data[pi], null);
                        }
                        else goto default;
                        break;
                    case "QUIT":
                        if (Received_PART != null)
                        {
                            if (data.Length > pi) Received_QUIT(source, data[pi]);
                            else Received_QUIT(source, null);
                        }
                        else goto default;
                        break;
                    case "KICK":
                        if (Received_KICK != null)
                        {
                            if (data.Length > pi + 2) Received_KICK(source, data[pi], data[pi+1], data[pi + 2]);
                            else Received_KICK(source, data[pi], data[pi + 1], null);
                        }
                        else goto default;
                        break;
                    case "NICK":
                        if (source.Nickname == nickname)
                            this.NickName = data[pi];
                        if (Received_NICK != null) 
                            Received_NICK(source, data[pi]);
                        else goto default;
                        break;
                    case "001":
                        this.NickName = data[pi];
                        if (Received_RPL != null) Received_RPL("RPL_WELCOME", data[pi + 1]);
                        else goto default;
                        break;
                    case "002":
                        if (Received_RPL != null) Received_RPL("RPL_YOURHOST", data[pi + 1]);
                        else goto default;
                        break;
                    case "003":
                        if (Received_RPL != null) Received_RPL("RPL_CREATED", data[pi + 1]);
                        else goto default;
                        break;
                    case "004":
                        if (Received_RPL != null) Received_RPL("RPL_MYINFO", string.Join("; ", data, pi + 1, data.Length - pi - 1));
                        else goto default;
                        break;
                    case "005":
                        if (Received_RPL != null) Received_RPL("RPL_ISUPPORT", string.Join("; ", data, pi + 1, data.Length - pi - 1));
                        else goto default;
                        break;
                    case "015":
                        if (Received_RPL != null) Received_RPL("RPL_MAP", string.Join("; ", data, pi + 1, data.Length - pi - 1));
                        else goto default;
                        break;
                    case "016":
                        if (Received_RPL != null) Received_RPL("RPL_MAPMORE", string.Join("; ", data, pi + 1, data.Length - pi - 1));
                        else goto default;
                        break;
                    case "017":
                        if (Received_RPL != null) Received_RPL("RPL_MAPEND", string.Join("; ", data, pi + 1, data.Length - pi - 1));
                        else goto default;
                        break;
                    case "042":
                        if (Received_RPL != null) Received_RPL("RPL_YOURID", string.Join("; ", data, pi + 1, data.Length - pi - 1));
                        else goto default;
                        break;
                    case "302": //    RPL_USERHOST
                        if (Received_RPL != null) Received_RPL("RPL_USERHOST", data[pi + 1]);
                        else goto default;
                        break;
                    case "303": //    RPL_ISON
                        if (Received_RPL != null) Received_RPL("RPL_ISON", data[pi + 1]);
                        else goto default;
                        break;
                    case "301": //    RPL_AWAY -- own event
                        if (Received_RPL_AWAY != null) Received_RPL_AWAY(data[pi + 1], data[pi + 2]);
                        else goto default;
                        break;
                    case "305": //    RPL_UNAWAY -- one string
                        if (Received_RPL != null) Received_RPL("RPL_UNAWAY", data[pi + 1]);
                        else goto default;
                        break;
                    case "306": //    RPL_NOWAWAY -- one string
                        if (Received_RPL != null) Received_RPL("RPL_NOWAWAY", data[pi + 1]);
                        else goto default;
                        break;
                    case "375": // RPL_MOTDSTART
                        Data_MOTD_END = false; Data_MOTD.Clear();
                        if (Received_RPL != null) Received_RPL("RPL_MOTDSTART", data[pi + 1]);
                        else goto default;
                        break;
                    case "372": // RPL_MOTD
                        if (Data_MOTD_END) { Data_MOTD_END = false; Data_MOTD.Clear(); }
                        Data_MOTD.Add(data[pi + 1]);
                        if (Received_RPL != null) Received_RPL("RPL_MOTD", data[pi + 1]);
                        else goto default;
                        break;
                    case "376": // RPL_ENDOFMOTD <- SHOULD GET IT'S OWN EVENT!
                        Data_MOTD_END = true;
                        if (Received_RPL_ENDOFMOTD != null) Received_RPL_ENDOFMOTD(data[pi + 1]);
                        else goto default;
                        break;
                    //case "PRIVMSG": // UNFINISHED
                    //    string msgtarget = ConvertToString(line[index]);
                    //    index++;
                    //    string msgtext = ConvertToString(line[index]);
                    //    // index++; is not used anymore.
                    //    break;
                    default:
                        if (UnknownLine != null) UnknownLine(Encoding.UTF8.GetString(buffer, from, length));
                        break;
                }
                if (RawLine != null) RawLine(" <- " + Encoding.UTF8.GetString(buffer, from, length));
            }
            catch (Exception ex)
            {
                connectionException(ex.ToString() + "\r\n @ -> " + Encoding.UTF8.GetString(buffer, from, length));
            }
        }

        static bool isCtcp(string msgline)
        {
            return (msgline[0] == '\x01' && msgline[msgline.Length - 1] == '\x01');
        }

        static string getCtcp(string msgline)
        {
            return msgline.Substring(1, msgline.Length - 2);
        }

        private void ctcpRequestReceived(IrcUser source, string target, string line)
        {
            string[] args = line.Split(" ".ToCharArray(), 2);
            string command;
            string arg;
            if (args.Length == 0) { command = ""; arg = ""; }
            else if (args.Length == 1) { command = args[0]; arg = ""; args = new string[0]; }
            else { command = args[0]; arg = args[1]; args = arg.Split(' '); }
            switch (command)
            {
                case "VERSION":
                    Msg_CTCP_REPLY_VERSION(source.Nickname);
                    goto default;
                case "PING":
                    if (arg.Length < 17) { Msg_CTCP_REPLY_PING(source.Nickname, arg); }
                    goto default;
                case "TIME":
                    Msg_CTCP_REPLY_TIME(source.Nickname);
                    goto default;
                case "ACTION":
                    if (Received_CTCP_REQUEST_ACTION != null) Received_CTCP_REQUEST_ACTION(source, target, arg);
                    else goto default;
                    break;
                default:
                    if (Received_CTCP_REQUEST != null) Received_CTCP_REQUEST(source, target, line);
                    break;
            }
        }

        private void ctcpReplyReceived(IrcUser source, string target, string line)
        {
            string[] args = line.Split(" ".ToCharArray(), 2);
            string command;
            string arg;
            if (args.Length == 0) { command = ""; arg = ""; }
            else if (args.Length == 1) { command = args[0]; arg = ""; args = new string[0]; }
            else { command = args[0]; arg = args[1]; args = arg.Split(' '); }
            switch (command)
            {
                case "PING":
                case "PONG":
                    if (Received_CTCP_REPLY_PING != null) Received_CTCP_REPLY_PING(source, target, arg);
                    else goto default;
                    break;
                default:
                    if (Received_CTCP_REPLY != null) Received_CTCP_REPLY(source, target, line);
                    break;
            }
        }

        public void Msg(string line)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(line + "\r\n");
                socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, null, null);
                if (RawLine != null) RawLine(" -> " + line);
            }
            catch (Exception ex)
            {
                connectionException(ex.Message);
            }
        }

        public event ObjectCallback Connected;
        public event StringCallback ConnectionException;
        public event StringCallback RawLine;
        public event StringCallback UnknownLine;
        public event StringCallback Received_PING;
        public event IrcuserStringStringCallback Received_NOTICE;
        public event StringStringCallback Received_RPL;
        public event StringCallback Received_RPL_ENDOFMOTD;
        public event StringStringCallback Received_RPL_AWAY;
        public event IrcuserStringCallback Received_JOIN;
        public event IrcuserStringStringCallback Received_PART;
        public event IrcuserStringCallback Received_QUIT;
        public event IrcuserStringCallback Received_NICK;
        public event IrcuserStringStringStringCallback Received_KICK;
        public event IrcuserStringStringCallback Received_PRIVMSG;
        public event IrcuserStringStringCallback Received_CTCP_REQUEST;
        public event IrcuserStringStringCallback Received_CTCP_REQUEST_ACTION;
        public event IrcuserStringStringCallback Received_CTCP_REPLY;
        public event IrcuserStringStringCallback Received_CTCP_REPLY_PING;

        #region IDisposable Members

        /// <summary>
        /// Destroys all events and closes the connection.
        /// </summary>
        public void Dispose()
        {
            if (lineReader != null) lineReader.Dispose();
            this.Connected = null;
            this.ConnectionException = null;
            this.RawLine = null;
            this.UnknownLine = null;
            this.Received_PING = null;
            this.Received_NOTICE = null;
            this.Received_RPL = null;
            this.Received_RPL_ENDOFMOTD = null;
            this.Received_RPL_AWAY = null;
            this.Received_JOIN = null;
            this.Received_PART = null;
            this.Received_QUIT = null;
            this.Received_PRIVMSG = null;
            this.Received_CTCP_REQUEST = null;
            this.Received_CTCP_REQUEST_ACTION = null;
            this.Received_CTCP_REPLY = null;
            this.Received_CTCP_REPLY_PING = null;
            this.Close();
        }

        #endregion

        /// <summary>
        /// Closes the connection.
        /// </summary>
        public void Close()
        {
            if (socket == null) { if (ConnectionException != null) ConnectionException("There is no socket."); return; }
            if (!socket.Connected) { if (ConnectionException != null) ConnectionException("The socket is not connected."); return; }
            try { socket.Shutdown(SocketShutdown.Both); }
            catch (Exception ex) { if (ConnectionException != null) ConnectionException(ex.Message); }
            try { socket.Close(); }
            catch (Exception ex) { if (ConnectionException != null) ConnectionException(ex.Message); }
            if (ConnectionException != null) ConnectionException("Connection closed.");
        }

        public string[] SplitByteString(byte[] buffer, int from, int length)
        {
            string[] temp = new string[32]; // the limit's supposed to be 15 or something
            string[] result; // array with correct length created here when done;
            int index = 0; // position in temp array
            int to = length + from; // 'to' does not exist as an index.
            int begin = from;
            for (int i = from; i < to; i++)
            {
                if (buffer[i] == 0x20)
                {
                    if (index >= temp.Length)
                    { // just in case the server goes over or array length >_>
                        result = temp; // should be fine as long as the server doesnt spam spaces
                        temp = new string[result.Length * 2];
                        for (int j = 0; j < result.Length; j++) temp[j] = result[j];
                    }
                    temp[index] = Encoding.UTF8.GetString(buffer, begin, i - begin);
                    index++;
                    begin = i + 1;
                    if (buffer[begin] == 0x3A)
                    {
                        begin++;
                        break;
                    }
                }
            }
            if (index >= temp.Length)
            { // just in case the server goes over or array length >_>
                result = temp; // should be fine as long as the server doesnt spam spaces
                temp = new string[result.Length * 2];
                for (int j = 0; j < result.Length; j++) temp[j] = result[j];
            }
            temp[index] = Encoding.UTF8.GetString(buffer, begin, to - begin);
            index++;
            result = new string[index];
            for (int j = 0; j < index; j++) result[j] = temp[j];
            return result;
        }
    }
}
