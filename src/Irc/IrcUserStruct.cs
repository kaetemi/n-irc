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

namespace nIRC.Irc
{
    public struct IrcUser
    {
        public IrcUser(string source)
        {
            string _source = source;
            if (_source[0] == ':') _source = _source.Substring(1);
            if (_source.Contains("!"))
            {
                string[] sa1 = _source.Split("!".ToCharArray(), 2);
                Nickname = sa1[0];
                if (sa1[1].Contains("@"))
                {
                    string[] sa2 = sa1[1].Split("@".ToCharArray(), 2);
                    User = sa2[0];
                    Host = sa2[1];
                }
                else
                {
                    User = "";
                    Host = sa1[1];
                }
            }
            else
            {
                Nickname = _source;
                User = "";
                Host = "";
            }
            Original = _source;
        }

        public string Nickname;
        public string User;
        public string Host;
        public string Original;
    }
}
