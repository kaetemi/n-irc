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
using nIRC.UI;

namespace nIRC.Irc
{
    // at some point, this should be split up, and then the JOIN event should contain an IrcChannel parameter
    /// <summary>
    /// Handles channel user interface,
    /// mainly the userlist and modes.
    /// </summary>
    class IrcEnvChannel
    {
        // boolModes (moderated, etc)
        // listModes (bans, etc)
        public IrcEnvironment Environment;
        public ChatWindow Window;
        public string Data_CHANNEL;
        public string Data_ID;
        public string Data_TOPIC = "";
        public void SetTopic(string value) { Data_TOPIC = value; UpdateTitle(); }

        public IrcEnvChannel(IrcEnvironment environment, string channel)
        {
            Data_CHANNEL = channel;
            Data_ID = channel.ToLower();
            this.Environment = environment;
            this.Window = nIRC.UI.Environment.ShowChatWindow(environment, channel, nIRC.UI.Environment.GROUP_CHANNEL, true, true, true);
            this.Window.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Window_FormClosing);
            environment.envChannels.Add(Data_ID, this);
        }

        void Window_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            this.Environment.envChannels.Remove(Data_ID);
            switch(e.CloseReason)
            {
                case System.Windows.Forms.CloseReason.UserClosing:
                    this.Environment.connection.Msg_PART(Data_CHANNEL, Program.Identifier);
                break;
            }
        }

        /// <summary>
        /// Checks if the given nickname is present within this channel.
        /// </summary>
        /// <param name="nickname">Case Sensitive</param>
        /// <returns></returns>
        public bool HasUser(string nickname)
        {
            return true;
        }

        void UpdateTitle()
        {

        }
    }
}
