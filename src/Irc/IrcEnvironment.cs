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
using System.Drawing;
using nIRC.UI;

namespace nIRC.Irc
{
    class IrcEnvironment : nIRC.UI.SubEnvironment
    {
        ChatWindow serverWindow;
        public IrcClientConnection connection;
        public Dictionary<string, IrcEnvChannel> envChannels = new Dictionary<string, IrcEnvChannel>();
        Dictionary<string, ChatWindow> queryWindows = new Dictionary<string, ChatWindow>();

        public IrcEnvironment()
            : base()
        {
            serverWindow = nIRC.UI.Environment.ShowChatWindow(this, "New IRC Connection", nIRC.UI.Environment.GROUP_RAWR, true, false, true);

            //nIRC.UI.Environment.ShowChatWindow(this, "Channel", 4, true, false, true);
            //nIRC.UI.Environment.ShowChatWindow(this, "Query", 5, true, false, true);
            //nIRC.UI.Environment.ShowChatWindow(this, "Channel", 4, true, false, true);
            //nIRC.UI.Environment.ShowChatWindow(this, "Query", 5, true, false, true);
            //nIRC.UI.Environment.ShowChatWindow(this, "Query", 5, true, false, true);
            //nIRC.UI.Environment.ShowChatWindow(this, "Channel", 4, true, false, true);

            serverWindow.ReadInput += new ChatwindowStringCallback(serverWindow_ReadInput);
            serverWindow.FormClosing += new System.Windows.Forms.FormClosingEventHandler(serverWindow_FormClosing);
            serverWindow.FormClosed += new System.Windows.Forms.FormClosedEventHandler(serverWindow_FormClosed);
        }

        void serverWindow_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            try { connection.Msg_QUIT(Program.Identifier); connection.Dispose(); }
            catch { }
        }

        void serverWindow_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            base.Close();
        }

        public override void Close()
        {
            serverWindow.Close();
        }

        void serverWindow_ReadInput(ChatWindow sender, string text)
        {
            userInput(null, null, text);
        }

        void channelWindow_ReadInput(ChatWindow sender, string text)
        {
            userInput(null, sender, text);
        }

        void queryWindow_ReadInput(ChatWindow sender, string text)
        {
            userInput(sender, null, text);
        }

        // add an "active" ChatWindow param
        void userInput(ChatWindow query, ChatWindow channel, string line)
        {
            if (line.Length == 0) return;
            if (line[0] == '/')
            {
                string command;
                string trail;
                string[] args;
                args = line.Split(" ".ToCharArray(), 2);
                command = args[0].Substring(1).ToUpperInvariant();
                if (args.Length == 2) { trail = args[1]; args = trail.Split(' '); }
                else { trail = ""; args = new string[0]; }
                if (query != null)
                {
                    switch (command)
                    {
                        case "MSG":
                            if (args.Length > 1)
                            {
                                if (args[0].ToLowerInvariant() == query.GetTag().ToLowerInvariant())
                                {
                                    trail = trail.Substring(args[0].Length + 1);
                                    connection.Msg_PRIVMSG(args[0], trail);
                                    query.WriteOutput(Color.Black, string.Format("<{0}> {1}", connection.NickName, trail));
                                    return;
                                }
                            }
                            break;
                        case "ME":
                            if (args.Length > 0)
                            {
                                connection.Msg_CTCP_REQUEST_ACTION(query.GetTag(), trail);
                                query.WriteOutput(Color.Purple, string.Format("* {0} {1}", connection.NickName, trail));
                                return;
                            }
                            break;
                    }
                }
                if (channel != null)
                {
                    switch (command)
                    {
                        case "MSG":
                            if (args.Length > 1)
                            {
                                trail = trail.Substring(args[0].Length + 1);
                                connection.Msg_PRIVMSG(args[0], trail);
                                channel.WriteOutput(Color.Black, string.Format("-> *{0}* {1}", args[0], trail));
                                // if a query window exists, print with <> there
                                if (queryWindows.ContainsKey(args[0])) // might have to do case insensitive dictionary
                                    queryWindows[args[0]].WriteOutput(Color.Black, string.Format("<{0}> {1}", connection.NickName, trail));
                                return;
                            }
                            break;
                        case "ME":
                            if (args.Length > 0)
                            {
                                connection.Msg_CTCP_REQUEST_ACTION(channel.GetTag(), trail);
                                channel.WriteOutput(Color.Purple, string.Format("* {0} {1}", connection.NickName, trail));
                                return;
                            }
                            break;
                        case "PART":
                            if (args.Length == 0) connection.Msg_PART(channel.GetTag());
                            else connection.Msg_PART(channel.GetTag(), trail);
                            return;
                    }
                }
                switch (command)
                {
                    case "SERVER":
                        if (args.Length == 0) LocalErrorMessage("No server specified, you are making no sense.");
                        else
                        {
                            if (connection != null)
                            {
                                LocalInfoMessage("Disconnecting existing server connection, just for your information.");
                                if (connection.socket != null) if (connection.socket.Connected)
                                        connection.Msg_QUIT(Program.Identifier);
                                connection.Close();
                            }
                            LocalInfoMessage(string.Format("Connecting to some new server, {0}.", trail));
                            if (args.Length == 1)
                            {
                                InitializeConnection(args[0], 6666);
                            }
                            else // we already know it's higher than 0! :)
                            {
                                InitializeConnection(args[0], Convert.ToInt32(args[1]));
                            }
                            UpdateButtonText();
                        }
                        return;
                    case "CTCP":
                        if (args.Length > 2) { connection.Msg_CTCP_REQUEST(args[0], args[1].ToUpperInvariant(), string.Join(" ", args, 2, args.Length - 2)); return; }
                        else if (args.Length > 1) { connection.Msg_CTCP_REQUEST(args[0], args[1].ToUpperInvariant()); return; }
                        break;
                    case "PING":
                        if (args.Length > 0)
                        {
                            connection.Msg_CTCP_REQUEST_PING(args[0], System.Environment.TickCount.ToString());
                            return;
                        }
                        break;
                    case "MSG":
                        if (args.Length > 1)
                        {
                            trail = trail.Substring(args[0].Length + 1);
                            connection.Msg_PRIVMSG(args[0], trail);
                            serverWindow.WriteOutput(Color.Black, string.Format("-> *{0}* {1}", args[0], trail));
                            // if a query window exists, print with <> there
                            if (queryWindows.ContainsKey(args[0])) // might have to do case insensitive dictionary
                                queryWindows[args[0]].WriteOutput(Color.Black, string.Format("<{0}> {1}", connection.NickName, trail));
                            return;
                        }
                        break;
                    case "TEST":
                        if (args.Length > 0)
                        {
                            serverWindow.WriteOutput(Color.Black, trail);
                            return;
                        }
                        break;
                }
                if (args.Length == 0) connection.Msg(command);
                else connection.Msg(command + " " + trail);
            }
            else
            {
                if (query != null)
                {
                    connection.Msg_PRIVMSG(query.GetTag(), line);
                    query.WriteOutput(Color.Black, string.Format("<{0}> {1}", connection.NickName, line));
                }
                else if (channel != null)
                {
                    connection.Msg_PRIVMSG(channel.GetTag(), line);
                    channel.WriteOutput(Color.Black, string.Format("<{0}> {1}", connection.NickName, line));
                }
                else LocalErrorMessage("Unable to send useless input to the server.");
            }
        }

        public void UpdateButtonText() // rename to UpdateTag()
        {
            serverWindow.SetTag(connection.ServerName + " " + connection.NickName);
        }

        public void InitializeConnection(string server, int port)
        {
            connection = new IrcClientConnection(server, port);
            connection.ConnectionException += new StringCallback(connection_ConnectionException);
            connection.UnknownLine += new StringCallback(connection_UnknownLine);
            connection.Received_PING += new StringCallback(connection_Received_PING);
            connection.Received_NOTICE += new IrcuserStringStringCallback(connection_Received_NOTICE);
            connection.Received_RPL += new StringStringCallback(connection_Received_RPL); // 372
            connection.Received_RPL_ENDOFMOTD += new StringCallback(connection_Received_RPL_ENDOFMOTD);
            connection.Received_RPL_AWAY += new StringStringCallback(connection_Received_RPL_AWAY);
            connection.Received_JOIN += new IrcuserStringCallback(connection_Received_JOIN);
            connection.Received_PART += new IrcuserStringStringCallback(connection_Received_PART);
            connection.Received_QUIT += new IrcuserStringCallback(connection_Received_QUIT);
            connection.Received_NICK += new IrcuserStringCallback(connection_Received_NICK);
            connection.Received_KICK += new IrcuserStringStringStringCallback(connection_Received_KICK);
            connection.Received_PRIVMSG += new IrcuserStringStringCallback(connection_Received_PRIVMSG);
            connection.Received_CTCP_REQUEST += new IrcuserStringStringCallback(connection_Received_CTCP_REQUEST);
            connection.Received_CTCP_REQUEST_ACTION += new IrcuserStringStringCallback(connection_Received_CTCP_REQUEST_ACTION);
            connection.Received_CTCP_REPLY += new IrcuserStringStringCallback(connection_Received_CTCP_REPLY);
            connection.Received_CTCP_REPLY_PING += new IrcuserStringStringCallback(connection_Received_CTCP_REPLY_PING);
            connection.NickNameChanged += new StringCallback(connection_NickNameChanged);
            connection.ServerNameChanged += new StringCallback(connection_ServerNameChanged);
            connection.Connected += new ObjectCallback(connection_Connected);
            connection.Connect();
        }

        void connection_Received_CTCP_REPLY_PING(IrcUser source, string target, string message)
        {
            try { serverWindow.WriteOutput(Color.Blue, string.Format("[{0}] PING {1} ms.", source.Nickname, System.Environment.TickCount - Convert.ToInt32(message, 10))); }
            catch { connection_Received_CTCP_REPLY(source, target, "PING " + message); }
        }

        void connection_Received_CTCP_REPLY(IrcUser source, string target, string message)
        {
            serverWindow.WriteOutput(Color.Blue, string.Format(target == connection.NickName ? "[{0}] {1}" : "[{0} -> {2}] {1}", source.Nickname, message, target));
        }

        void connection_Received_CTCP_REQUEST(IrcUser source, string target, string message)
        {
            serverWindow.WriteOutput(Color.Red, string.Format(target == connection.NickName ? "[{0}] {1}" : "[{0} -> {2}] {1}", source.Nickname, message, target));
        }

        void connection_Received_CTCP_REQUEST_ACTION(IrcUser source, string target, string message)
        {
            string _target = target.ToLower();
            if (envChannels.ContainsKey(_target))
            {
                envChannels[_target].Window.WriteOutput(Color.Purple, string.Format("* {0} {1}", source.Nickname, message));
            }
            else if (target == connection.NickName)
            {
                getQueryWindow(source.Nickname).WriteOutput(Color.Purple, string.Format("* {0} {1}", source.Nickname, message));
            }
            else
            {
                serverWindow.WriteOutput(Color.Purple, string.Format("[ACTION] <{0} -> {2}> {1}", source.Nickname, message, target));
            }
        }

        void connection_Received_PRIVMSG(IrcUser source, string target, string message)
        {
            string _target = target.ToLower();
            if (envChannels.ContainsKey(_target))
            {
                envChannels[_target].Window.WriteOutput(Color.Black, string.Format("<{0}> {1}", source.Nickname, message));
            }
            else if (target == connection.NickName)
            {
                getQueryWindow(source.Nickname).WriteOutput(Color.Black, string.Format("<{0}> {1}", source.Nickname, message));
            }
            else
            {
                serverWindow.WriteOutput(Color.Black, string.Format("<{0} -> {2}> {1}", source.Nickname, message, target));
            }
        }

        void connection_Received_QUIT(IrcUser source, string message)
        {
            string text = (string.IsNullOrEmpty(message))
                ? string.Format("* {0} ({1}) has quit IRC", source.Nickname, source.Original)
                : string.Format("* {0} ({1}) has quit IRC ({2})", source.Nickname, source.Original, message);
            serverWindow.WriteOutput(Color.DarkBlue, text);
            foreach (IrcEnvChannel channel in envChannels.Values) if (channel.HasUser(source.Nickname))
                    channel.Window.WriteOutput(Color.DarkBlue, text);
        }

        void connection_Received_NICK(IrcUser source, string target)
        {
            if (connection.NickName == target)
                serverWindow.WriteOutput(Color.Green, "* Your nick is now " + target);

            string text = string.Format("* {0} ({1}) is now known as {2}", source.Nickname, source.Original, target);

            foreach (IrcEnvChannel channel in envChannels.Values) if (channel.HasUser(source.Nickname))
                    channel.Window.WriteOutput(Color.Green, text);
        }

        void connection_Received_KICK(IrcUser source, string target, string name, string message)
        {
            ChatWindow output;
            string _target = target.ToLower();
            if (name == connection.NickName)
            {
                output = envChannels.ContainsKey(_target)
                    ? envChannels[_target].Window
                    : serverWindow;
                if (string.IsNullOrEmpty(message)) output.WriteOutput(Color.Blue, string.Format("* You were kicked from {0} by {1} ({2})", target, source.Original, message));
                else output.WriteOutput(Color.Blue, string.Format("* You were kicked from {0} by {1}", target, source.Original));
                return;
            }
            output = envChannels.ContainsKey(_target)
               ? envChannels[_target].Window
               : serverWindow;
            if (string.IsNullOrEmpty(message)) output.WriteOutput(Color.Green, string.Format("* {0} was kicked from {1} by {2} ({3})", name, target, source.Nickname, message));
            else output.WriteOutput(Color.Green, string.Format("* {0} was kicked from {1} by {2}", name, target, source.Nickname));
        }

        //* You were kicked from {0} by {1} ({2})
        //* {0} was kicked by {1} ({2})
        //* {0} sets mode: {1} <- in channel window for channel, in system window for self :)

        void connection_Received_PART(IrcUser source, string target, string message)
        {
            ChatWindow output;
            string _target = target.ToLower();
            if (source.Nickname == connection.NickName)
            {
                output = envChannels.ContainsKey(_target)
                    ? envChannels[_target].Window
                    : serverWindow;
                if (string.IsNullOrEmpty(message)) output.WriteOutput(Color.Blue, string.Format("* You have left {0}", target));
                else output.WriteOutput(Color.Blue, string.Format("* You have left {0} ({1})", target, message));
                return;
            }
            output = envChannels.ContainsKey(_target)
                ? envChannels[_target].Window
                : serverWindow;
            if (string.IsNullOrEmpty(message)) output.WriteOutput(Color.Green, string.Format("* {1} ({2}) has left {0}", target, source.Nickname, source.Original));
            else output.WriteOutput(Color.Green, string.Format("* {1} ({2}) has left {0} ({3})", target, source.Nickname, source.Original, message));
        }

        void connection_Received_JOIN(IrcUser source, string target)
        {
            string _target = target.ToLower();
            ChatWindow output;
            if (source.Nickname == connection.NickName)
            {
                if (envChannels.ContainsKey(_target))
                {
                    output = envChannels[_target].Window;
                }
                else
                {
                    IrcEnvChannel channel = new IrcEnvChannel(this, target);
                    output = channel.Window;
                    output.ReadInput += new ChatwindowStringCallback(channelWindow_ReadInput);
                }
                output.WriteOutput(Color.Blue, string.Format("* Now talking in {0}", target));
            }
            else
            {
                output = envChannels.ContainsKey(_target)
                    ? envChannels[_target].Window
                    : serverWindow;
                output.WriteOutput(Color.Green, string.Format("* {1} ({2}) has joined {0} ", target, source.Nickname, source.Original));
            }
        }

        ChatWindow getQueryWindow(string sourcename)
        {
            ChatWindow queryWindow;
            string _sourcename = sourcename.ToLower();
            if (queryWindows.ContainsKey(_sourcename)) queryWindow = queryWindows[_sourcename];
            else
            {
                queryWindow = nIRC.UI.Environment.ShowChatWindow(
                    this, sourcename, nIRC.UI.Environment.GROUP_QUERY, true, false, true);
                queryWindows.Add(_sourcename, queryWindow);
                queryWindow.WriteOutput(Color.Blue, string.Format("* You are in a private conversation with {0}.", sourcename));
                queryWindow.ReadInput += new ChatwindowStringCallback(queryWindow_ReadInput);
            }
            return queryWindow;
        }

        void connection_Received_RPL_AWAY(string data1, string data2)
        {
            serverWindow.WriteOutput(Color.DarkBlue, string.Format("[RPL_AWAY] <{0}> {1}", data1, data2));
        }

        void connection_Connected(object sender)
        {
            if (connection == sender)
            {
                connection.Msg_NICK("Nidev");
                connection.Msg_USER("n-irc", ".nIRC SVN Build Test");
            }
            else
            {
                LocalErrorMessage("Double server connection, disconnecting from one.");
                ((IrcClientConnection)sender).Dispose();
            }
        }

        void connection_Received_RPL_ENDOFMOTD(string data)
        {
            serverWindow.WriteOutput(Color.Black, "[RPL_ENDOFMOTD] " + data);
        }

        void connection_Received_RPL(string data1, string data2)
        {
            serverWindow.WriteOutput(Color.Black, string.Format("[{0}] {1}", data1, data2));
        }

        void connection_Received_PING(string data)
        {
            serverWindow.WriteOutput(Color.Green, "[PING] " + data);
        }

        void connection_Received_NOTICE(IrcUser source, string target, string message)
        {
            string user = "-" + source.Nickname + "-";
            if (target != connection.NickName) user += target + "-";
            serverWindow.WriteOutput(Color.DarkRed, user + " " + message);
        }

        void connection_ConnectionException(string data)
        {
            serverWindow.WriteOutput(Color.Red, data);
        }

        void connection_ReceivedRPL_MOTD(string data1, string data2)
        {
            serverWindow.WriteOutput(Color.Black, string.Format("[{0}] {1}", data1, data2));
        }

        void connection_ServerNameChanged(string data)
        {
            UpdateButtonText();
        }

        void connection_NickNameChanged(string data)
        {
            UpdateButtonText();
        }

        void connection_Received001(string data1, string data2)
        {
            serverWindow.WriteOutput(Color.Black, string.Format("[{0}] {1}", data1, data2));
        }

        void connection_Received002(string data1, string data2)
        {
            serverWindow.WriteOutput(Color.Black, string.Format("[{0}] {1}", data1, data2));
        }

        void connection_UnknownLine(string data)
        {
            serverWindow.WriteOutput(Color.Black, data);
        }

        public void RawSend(string message)
        {
            connection.Msg(message);
            LocalInfoMessage("SENDING: " + message);
        }

        // stuff from old code.
        public void LocalExceptionMessage(string message)
        {
            serverWindow.WriteOutput(Color.Red, message);
        }

        public void LocalErrorMessage(string message)
        {
            serverWindow.WriteOutput(Color.DarkRed, message);
        }

        public void LocalInfoMessage(string message)
        {
            serverWindow.WriteOutput(Color.Blue, message);
        }

        public void RemoteInfoMessage(string message)
        {
            serverWindow.WriteOutput(Color.Black, message);
        }
    }
}
