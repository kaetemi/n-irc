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

#if !NET

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace nIRC.UI
{
    partial class ChatOutputBox : RichTextBox
    {
        public ChatOutputBox()
        {
            appendTextDelegate = new ColorStringCallback(AppendText);
            base.ReadOnly = true;
        }

        /// <summary>
        /// The text to be appended with time tag and irc formatting.
        /// Handles calls from other threads just fine.
        /// Should not throw any exceptions in any case!
        /// </summary>
        /// <param name="color"></param>
        /// <param name="text"></param>
        public void AppendText(Color color, string text)
        {
            if (InvokeRequired) { this.Invoke(appendTextDelegate, new object[] { color, text }); return; }

            this.AppendText(string.Format("\r\n[{0}] {1}", DateTime.Now.ToString("HH:mm:ss"), text));
        }
        public ColorStringCallback appendTextDelegate;
    }
}

#endif
