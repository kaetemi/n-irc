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
using System.Windows.Forms;
using System.Drawing;

namespace nIRC.UI
{
    partial class ChatWindow : Window
    {
        public ChatWindow(SubEnvironment owner, string tag, int group, bool input, bool userlist, bool output)
            : base(owner, tag, group)
        {
            this.SuspendLayout();

            this.Font = new Font(FontFamily.GenericMonospace, 9f);
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;

            if (userlist && output)
            {
                uiUserlistBox = new ChatUserlistBox();
                uiOutputBox = new ChatOutputBox();
                uiUserlistBox.Dock = DockStyle.Fill;
                uiOutputBox.Dock = DockStyle.Fill;
                SplitContainer split = new SplitContainer();
                split.Dock = DockStyle.Fill;
                this.Controls.Add(split);
                this.ResumeLayout(false);
                this.PerformLayout();
                split.FixedPanel = FixedPanel.Panel2;
                split.SplitterDistance = split.Width - 125;
                this.SuspendLayout();
                split.Panel1.Controls.Add(uiOutputBox);
                split.Panel2.Controls.Add(uiUserlistBox);
            }
            else
            {
                if (userlist)
                {
                    uiUserlistBox = new ChatUserlistBox();
                    uiUserlistBox.Dock = DockStyle.Fill;
                    this.Controls.Add(uiUserlistBox);
                }
                if (output)
                {
                    uiOutputBox = new ChatOutputBox();
                    uiOutputBox.Dock = DockStyle.Fill;
                    this.BackColorChanged += new EventHandler(ChatWindow_BackColorChanged);
                    uiOutputBox.BackColor = Color.White;
                    uiOutputBox.ReadOnly = true;
                    this.Controls.Add(uiOutputBox);
                }
            }
            if (input)
            {
                uiInputBox = new ChatInputBox();
                uiInputBox.Dock = DockStyle.Bottom;
                uiInputBox.LineEntry += new StringCallback(uiInputBox_LineEntry); // += ReadInput;
                this.Activated += new EventHandler(ChatWindow_Activated);
                this.Controls.Add(uiInputBox);
            }

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        void ChatWindow_Activated(object sender, EventArgs e)
        {
            //BarButton.ForeColor = SystemColors.ControlText;
            uiInputBox.Focus();
        }

        void ChatWindow_BackColorChanged(object sender, EventArgs e)
        {
            uiOutputBox.BackColor = this.BackColor;
        }

        void uiInputBox_LineEntry(string text)
        {
            ReadInput(this, text);
        }

        public void WriteOutput(Color color, string text)
        {
            //if (MdiParent.ActiveMdiChild != this) BarButton.ForeColor = SystemColors.HotTrack;
            //need invoke to do this!
            //move invoke from textbox to here
            uiOutputBox.AppendText(color, text);
        }

        public event ChatwindowStringCallback ReadInput;

        public ChatOutputBox uiOutputBox;
        public ChatInputBox uiInputBox;
        public ChatUserlistBox uiUserlistBox;
    }
}
