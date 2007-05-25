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
using System.Windows.Forms;
using System.Drawing;

namespace nIRC.UI
{
    partial class ChatInputBox : TextBox
    {
        public ChatInputBox()
        {
            this.KeyDown += new KeyEventHandler(IrcInputBox_KeyDown);
        }

        public event StringCallback LineEntry;
        Collection<string> lineHistory = new Collection<string>();
        int lineIndex = 0;
        string lineCurrent = "";

        void IrcInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (LineEntry != null)
                    {
                        string text = this.Text;
                        LineEntry(text); // first thing to do is send it, important!
                        if (lineHistory.Contains(text)) lineHistory.Remove(text);
                        lineHistory.Add(text);
                        lineIndex = lineHistory.Count;
                        lineCurrent = "";
                        this.Clear();
#if NET // http://bugzilla.ximian.com/show_bug.cgi?id=81692
                        e.SuppressKeyPress = true;
#endif
                        e.Handled = true;
                        return;
                    }
                    break;
                case Keys.Up:
                    if (lineIndex > 0)
                    {
                        if (lineIndex == lineHistory.Count)
                        {
                            lineCurrent = this.Text;
                        }
                        lineIndex--;
                        this.Text = lineHistory[lineIndex];
                    }
                    break;
                case Keys.Down: 
                    lineIndex++;
                    if (lineIndex < lineHistory.Count)
                    {
                        this.Text = lineHistory[lineIndex];
                    }
                    else if (lineIndex == lineHistory.Count)
                    {
                        this.Text = lineCurrent;
                    }
                    else { lineIndex = lineHistory.Count; }
                    break;
            }
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.B: //bold
                        this.SelectedText = ("\x0002");
                        break;
                    case Keys.K: //color
                        if (e.Shift) new ColorSelection(this).ShowDialog();
                        else this.SelectedText = ("\x0003");
                        break;
                    case Keys.R: //reverse
                        this.SelectedText = ("\x0016");
                        break;
                    case Keys.U: //underline
                        this.SelectedText = ("\x001F");
                        break;
                    case Keys.O: //remove formatting
                        this.SelectedText = ("\x000F");
                        break;
                    case Keys.I: //italic
                        this.SelectedText = ("\x0006I\x0006");
                        break;
                    case Keys.S: //strikeout
                        this.SelectedText = ("\x0006S\x0006");
                        break;
                    case Keys.F: //format prefix
                        this.SelectedText = ("\x0006");
                        break;
                    default:
                        return;
                }
#if NET // http://bugzilla.ximian.com/show_bug.cgi?id=81692
                e.SuppressKeyPress = true;
#endif
                e.Handled = true;
            }
        }
    }
}
