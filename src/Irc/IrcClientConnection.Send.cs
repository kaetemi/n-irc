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
    partial class IrcClientConnection
    {
        public void Msg_PASS(string password)
        {
            Msg("PASS :" + password);
        }

        public void Msg_NICK(string nickname)
        {
            Msg("NICK :" + nickname);
        }

        public void Msg_USER(string user, string realname)
        {
            Msg("USER " + user + " 8 * :" + realname);
        }

        public void Msg_OPER(string name, string password)
        {
            Msg("OPER " + name + " :" + password);
        }

        public void Msg_MODE(string nickname)
        {
            Msg("MODE " + nickname);
        }

        public void Msg_MODE(string nickname, string parameters)
        {
            Msg("MODE " + nickname + " :" + parameters);
        }

        public void Msg_SERVICE(string nickname, string distribution, string info)
        {
            Msg("SERVICE " + nickname + " * " + distribution + " 0 0 :" + info);
        }

        public void Msg_QUIT()
        {
            Msg("QUIT");
        }

        public void Msg_QUIT(string message)
        {
            Msg("QUIT :" + message);
        }

        public void Msg_SQUIT(string server, string comment)
        {
            Msg("SQUIT " + server + " :" + comment);
        }

        public void Msg_JOIN(string[] channels)
        {
            Msg("JOIN :" + string.Join(",", channels));
        }

        public void Msg_JOIN(string[] channels, string[] keys)
        {
            Msg("JOIN " + string.Join(",", channels) + " :" + string.Join(",", keys));
        }

        public void Msg_JOIN(string channel)
        {
            Msg("JOIN :" + channel);
        }

        public void Msg_JOIN(string channel, string key)
        {
            Msg("JOIN " + channel + " :" + key);
        }

        public void Msg_PART(string[] channels)
        {
            Msg("PART :" + string.Join(",", channels));
        }

        public void Msg_PART(string[] channels, string message)
        {
            Msg("PART " + string.Join(",", channels) + " :" + message);
        }

        public void Msg_PART(string channel)
        {
            Msg("PART :" + channel);
        }

        public void Msg_PART(string channel, string message)
        {
            Msg("PART " + channel + " :" + message);
        }

        public void Msg_JOIN_0()
        {
            Msg("JOIN 0");
        }

        public void Msg_MODE(string channel, string modes, params string[] modeparams)
        {
            Msg("MODE " + modes + " " + string.Join(" ", modeparams));
        }

        public void Msg_TOPIC(string channel)
        {
            Msg("TOPIC " + channel);
        }

        public void Msg_TOPIC(string channel, string topic)
        {
            Msg("TOPIC " + channel + " :" + topic);
        }

        public void Msg_TOPIC_0(string channel)
        {
            Msg("TOPIC " + channel + " :");
        }

        public void Msg_NAMES()
        {
            Msg("NAMES");
        }

        public void Msg_NAMES(string channel)
        {
            Msg("NAMES :" + channel);
        }

        public void Msg_NAMES(string[] channels)
        {
            Msg("NAMES :" + string.Join(",", channels));
        }

        public void Msg_NAMES(string channel, string target)
        {
            Msg("NAMES " + channel + " :" + target);
        }

        public void Msg_NAMES(string[] channels, string target)
        {
            Msg("NAMES " + string.Join(",", channels) + " :" + target);
        }

        public void Msg_LIST()
        {
            Msg("LIST");
        }

        public void Msg_LIST(string channel)
        {
            Msg("LIST :" + channel);
        }

        public void Msg_LIST(string[] channels)
        {
            Msg("LIST :" + string.Join(",", channels));
        }

        public void Msg_LIST(string channel, string target)
        {
            Msg("LIST " + channel + " :" + target);
        }

        public void Msg_LIST(string[] channels, string target)
        {
            Msg("LIST " + string.Join(",", channels) + " :" + target);
        }

        public void Msg_INVITE(string nickname, string channel)
        {
            Msg("INVITE " + nickname + " :" + channel);
        }

        public void Msg_KICK(string channel, string user)
        {
            Msg("KICK " + channel + " :" + user);
        }

        public void Msg_KICK(string channel, string[] users)
        {
            Msg("KICK " + channel + " :" + string.Join(",", users));
        }

        public void Msg_KICK(string[] channels, string[] users)
        {
            Msg("KICK " + string.Join(",", channels) + " :" + string.Join(",", users));
        }

        public void Msg_KICK(string channel, string user, string comment)
        {
            Msg("KICK " + channel + " " + user + " :" + comment);
        }

        public void Msg_KICK(string channel, string[] users, string comment)
        {
            Msg("KICK " + channel + " " + string.Join(",", users) + " :" + comment);
        }

        public void Msg_KICK(string[] channels, string[] users, string comment)
        {
            Msg("KICK " + string.Join(",", channels) + " " + string.Join(",", users) + " :" + comment);
        }

        public void Msg_PRIVMSG(string msgtarget, string text)
        {
            Msg("PRIVMSG " + msgtarget + " :" + text);
        }

        public void Msg_NOTICE(string msgtarget, string text)
        {
            Msg("NOTICE " + msgtarget + " :" + text);
        }

        public void Msg_MOTD()
        {
            Msg("MOTD");
        }

        public void Msg_MOTD(string target)
        {
            Msg("MOTD :" + target);
        }

        public void Msg_LUSERS()
        {
            Msg("LUSERS");
        }

        public void Msg_LUSERS(string mask)
        {
            Msg("LUSERS :" + mask);
        }

        public void Msg_LUSERS(string mask, string target)
        {
            Msg("LUSERS " + mask + " :" + target);
        }

        public void Msg_VERSION()
        {
            Msg("VERSION");
        }

        public void Msg_VERSION(string target)
        {
            Msg("VERSION :" + target);
        }

        public void Msg_STATS()
        {
            Msg("VERSION");
        }

        public void Msg_STATS(string query)
        {
            Msg("VERSION :" + query);
        }

        public void Msg_STATS(string query, string target)
        {
            Msg("VERSION " + query + " :" + target);
        }

        public void Msg_LINKS()
        {
            Msg("LINKS");
        }

        public void Msg_LINKS(string servermask)
        {
            Msg("LINKS :" + servermask);
        }

        public void Msg_LINKS(string remoteserver, string servermask)
        {
            Msg("LINKS " + remoteserver + " :" + servermask);
        }

        public void Msg_TIME()
        {
            Msg("TIME");
        }

        public void Msg_TIME(string target)
        {
            Msg("TIME :" + target);
        }

        public void Msg_CONNECT(string targetserver, string port)
        {
            Msg("CONNECT " + targetserver + " :" + port);
        }

        public void Msg_CONNECT(string targetserver, string port, string remoteserver)
        {
            Msg("CONNECT " + targetserver + " " + port + " :" + remoteserver);
        }

        public void Msg_TRACE()
        {
            Msg("TRACE");
        }

        public void Msg_TRACE(string target)
        {
            Msg("TRACE :" + target);
        }

        public void Msg_ADMIN()
        {
            Msg("ADMIN");
        }

        public void Msg_ADMIN(string target)
        {
            Msg("ADMIN :" + target);
        }

        public void Msg_INFO()
        {
            Msg("INFO");
        }

        public void Msg_INFO(string target)
        {
            Msg("INFO :" + target);
        }

        public void Msg_SERVLIST()
        {
            Msg("SERVLIST");
        }

        public void Msg_SERVLIST(string mask)
        {
            Msg("SERVLIST :" + mask);
        }

        public void Msg_SERVLIST(string mask, string type)
        {
            Msg("SERVLIST " + mask + " :" + type);
        }

        public void Msg_SQUERY(string servicename, string text)
        {
            Msg("SQUERY " + servicename + " :" + text);
        }

        public void Msg_WHO()
        {
            Msg("WHO");
        }

        public void Msg_WHO(string mask)
        {
            Msg("WHO :" + mask);
        }

        public void Msg_WHO_o(string mask)
        {
            Msg("WHO " + mask + " o");
        }

        public void Msg_WHOIS(string mask)
        {
            Msg("WHOIS :" + mask);
        }

        public void Msg_WHOIS(string[] masks)
        {
            Msg("WHOIS :" + string.Join(",", masks));
        }

        public void Msg_WHOIS(string target, string mask)
        {
            Msg("WHOIS " + target + " :" + mask);
        }

        public void Msg_WHOIS(string target, string[] masks)
        {
            Msg("WHOIS " + target + " :" + string.Join(",", masks));
        }

        public void Msg_WHOWAS(string nickname)
        {
            Msg("WHOWAS :" + nickname);
        }

        public void Msg_WHOWAS(string nickname, string count)
        {
            Msg("WHOWAS " + nickname + " :" + count);
        }

        public void Msg_WHOWAS(string nickname, string count, string target)
        {
            Msg("WHOWAS " + nickname + " " + count + " :" + target);
        }

        public void Msg_WHOWAS(string[] nicknames)
        {
            Msg("WHOWAS :" + string.Join(",", nicknames));
        }

        public void Msg_WHOWAS(string[] nicknames, string count)
        {
            Msg("WHOWAS " + string.Join(",", nicknames) + " :" + count);
        }

        public void Msg_WHOWAS(string[] nicknames, string count, string target)
        {
            Msg("WHOWAS " + string.Join(",", nicknames) + " " + count + " :" + target);
        }

        public void Msg_KILL(string nickname, string comment)
        {
            Msg("KILL " + nickname + " :" + comment);
        }

        public void Msg_PING(string server1)
        {
            Msg("PING :" + server1);
        }

        public void Msg_PING(string server1, string server2)
        {
            Msg("PING " + server1 + " :" + server2);
        }

        public void Msg_PONG(string server1)
        {
            Msg("PONG :" + server1);
        }

        public void Msg_PONG(string server1, string server2)
        {
            Msg("PONG " + server1 + " :" + server2);
        }

        public void Msg_ERROR(string errormessage)
        {
            Msg("ERROR :" + errormessage);
        }

        public void Msg_AWAY_0()
        {
            Msg("AWAY");
        }

        public void Msg_AWAY(string text)
        {
            Msg("AWAY :" + text);
        }

        public void Msg_REHASH()
        {
            Msg("REHASH");
        }

        public void Msg_DIE()
        {
            Msg("DIE");
        }

        public void Msg_RESTART()
        {
            Msg("RESTART");
        }

        public void Msg_SUMMON(string user)
        {
            Msg("SUMMON :" + user);
        }

        public void Msg_SUMMON(string user, string target)
        {
            Msg("SUMMON " + user + " :" + target);
        }

        public void Msg_SUMMON(string user, string target, string channel)
        {
            Msg("SUMMON " + user + " " + target + " :" + channel);
        }

        public void Msg_USERS()
        {
            Msg("USERS");
        }

        public void Msg_USERS(string target)
        {
            Msg("USERS :" + target);
        }

        public void Msg_WALLOPS(string text)
        {
            Msg("WALLOPS :" + text);
        }

        public void Msg_USERHOST(string nickname)
        {
            Msg("USERHOST :" + nickname);
        }

        public void Msg_USERHOST(string[] nicknames)
        {
            Msg("USERHOST " + string.Join(" ", nicknames));
        }

        public void Msg_ISON(string nickname)
        {
            Msg("ISON :" + nickname);
        }

        public void Msg_ISON(string[] nicknames)
        {
            Msg("ISON " + string.Join(" ", nicknames));
        }

        public void Msg_CTCP_REQUEST(string target, string cmd, string[] args)
        {
            Msg("PRIVMSG " + target + " :\x0001" + cmd + " " + string.Join(" ", args) + "\x0001");
        }

        public void Msg_CTCP_REQUEST(string target, string cmd, string args)
        {
            Msg("PRIVMSG " + target + " :\x0001" + cmd + " " + args + "\x0001");
        }

        public void Msg_CTCP_REQUEST(string target, string cmd)
        {
            Msg("PRIVMSG " + target + " :\x0001" + cmd + "\x0001");
        }

        public void Msg_CTCP_REQUEST_PING(string target, string ping)
        {
            Msg("PRIVMSG " + target + " :\x0001PING " + ping + "\x0001");
        }

        public void Msg_CTCP_REQUEST_VERSION(string target)
        {
            Msg("PRIVMSG " + target + " :\x0001VERSION\x0001");
        }

        public void Msg_CTCP_REQUEST_ACTION(string target, string action)
        {
            Msg("PRIVMSG " + target + " :\x0001ACTION " + action + "\x0001");
        }

        public void Msg_CTCP_REPLY(string target, string cmd, string[] args)
        {
            Msg("NOTICE " + target + " :\x0001" + cmd + " " + string.Join(" ", args) + "\x0001");
        }

        public void Msg_CTCP_REPLY(string target, string cmd, string args)
        {
            Msg("NOTICE " + target + " :\x0001" + cmd + " " + args + "\x0001");
        }

        public void Msg_CTCP_REPLY(string target, string cmd)
        {
            Msg("NOTICE " + target + " :\x0001" + cmd + "\x0001");
        }

        //public void Msg_CTCP_REPLY_PONG(string target, string pong)
        //{
        //    Msg("NOTICE " + target + " :\x01PONG " + pong + "\x01");
        //}

        public void Msg_CTCP_REPLY_PING(string target, string ping)
        {
            Msg("NOTICE " + target + " :\x0001PING " + ping + "\x0001");
        }

        static string CTCP_REPLY_VERSION = Program.Identifier + " " + Program.OS + " " + Program.Contact;
        public void Msg_CTCP_REPLY_VERSION(string target)
        {
            Msg_CTCP_REPLY_VERSION(target, CTCP_REPLY_VERSION);
        }

        public void Msg_CTCP_REPLY_VERSION(string target, string version)
        {
            Msg("NOTICE " + target + " :\x0001VERSION " + version + "\x0001");
        }

        private static string[] days // it's not my fault it begins with Sun...
            = new string[] { "Sun", "Mon", "Tue", "Wen", "Thu", "Fri", "Sat" };
        private static string[] months // begins with 1...
            = new string[] { null, "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul",
			    "Aug", "Sep", "Oct", "Nov", "Dec" };
        private static string getDateTime()
        {
            DateTime dt = DateTime.Now;
            DateTime dtu = dt.ToUniversalTime();
            string hdif = ((dt - dtu).Hours).ToString("00");
            if (hdif[0] != '-') hdif = '+' + hdif;
            hdif += "00";
            return days[(int)dt.DayOfWeek]
				+ ", "
				+ dt.Day.ToString("00")
				+ " "
				+ months[dt.Month]
				+ " "
				+ dt.Year.ToString("0000")
				+ " "
				+ dt.ToString("H:mm:ss")
				+ " "
                + hdif;
        }

        public void Msg_CTCP_REPLY_TIME(string target)
        {
            // [Kaetemi] TIME Thu May 17 16:57:57 2007
            // Wed, 11 Jun 1997 18:55 -0700
            // Tue, 15 Nov 1994 08:12:31 GMT
            Msg_CTCP_REPLY_TIME(target, getDateTime());
        }

        public void Msg_CTCP_REPLY_TIME(string target, string time)
        {
            Msg("NOTICE " + target + " :\x0001TIME " + time + "\x0001");
        }
    }
}
