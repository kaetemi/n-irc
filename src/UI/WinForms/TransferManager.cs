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
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace nIRC.UI
{
    partial class TransferManager : Form
    {
        public ToolBarWindowButton BarButton;

        public TransferManager()
        {
            this.Text = "Transfers";
            this.AutoScroll = true;
#if NET
            this.VerticalScroll.Enabled = true;
            this.HorizontalScroll.Enabled = false;
#endif

            BarButton = new ToolBarWindowButton(this.Text, 0, this);
            BarButton.Click += new EventHandler(BarButton_Click);
            BarButton.Visible = false;
            //int index = nIRC.UI.Environment.Window.WindowBar.Items.Count - 1;
            //for (index = 0; index < nIRC.UI.Environment.Window.WindowBar.Items.Count; index++)
            //{
            //    if (nIRC.UI.Environment.Window.WindowBar.Items[index] is ToolStripSeparator) break;
            //}

            nIRC.UI.Environment.Window.WindowBar.Items.Add(BarButton);
        }

        void BarButton_Click(object sender, EventArgs e)
        {
#if NET
            // What's up with the flashing when maximized in Windows?!
            LockWindowUpdate(nIRC.UI.Environment.Window.Handle);
#endif
            this.Activate();
#if NET
            LockWindowUpdate(new IntPtr(0));
#endif
        }
                    
#if NET   
        [DllImport("user32.dll")]
        static extern bool LockWindowUpdate(IntPtr hWndLock);
#endif

        protected override void OnActivated(EventArgs e)
        {
            BarButton.Checked = true;
            base.OnActivated(e);
        }

        protected override void OnDeactivate(EventArgs e)
        {
            BarButton.Checked = false;
            base.OnDeactivate(e);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            // possible that an exception may exist when closing form, not sure
#if !NET
			if (this.Visible) { this.MdiParent = nIRC.UI.Environment.Window; }
#endif
			nIRC.UI.Environment.Window.View_Transfers.Checked = this.Visible;
            BarButton.Visible = this.Visible;
            base.OnVisibleChanged(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    e.Cancel = true;
                    this.Hide();
                    break;
                default:
                    BarButton.Dispose();
                    break;
            }
            base.OnFormClosing(e);
        }
    }
}
