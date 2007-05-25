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
using System.Runtime.InteropServices;

namespace nIRC.UI
{
    partial class Window : Form
    {
        public ToolBarWindowButton BarButton;
        public SubEnvironment SubEnvironment;

        public Window(SubEnvironment owner, string tag, int group)
        {
            base.Name = tag;
            base.Text = tag;
            setTitle = new StringCallback(SetTitle);
            setTag = new StringCallback(SetTag);

            this.SubEnvironment = owner;
            owner.Windows.Add(this);

            BarButton = new ToolBarWindowButton(tag, group, this);
            BarButton.ImageIndex = group;
            BarButton.Click += new EventHandler(BarButton_Click);
            ToolStripItem item;
            int index = nIRC.UI.Environment.Window.WindowBar.Items.IndexOf(owner.ToolBarSeperator) - 1;
            while (index > -1)
            {
                item = nIRC.UI.Environment.Window.WindowBar.Items[index];
                if (!(item is ToolBarWindowButton)) break;
                if (((ToolBarWindowButton)item).Group <= group) break;
                index--;
            }
            
            nIRC.UI.Environment.Window.WindowBar.Items.Insert(index + 1,  BarButton);

            this.TopLevel = false;
            this.MdiParent = nIRC.UI.Environment.Window;
            this.ClientSize = new Size(500, 300);
            this.Show();
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
            base.OnActivated(e);
            BarButton.Checked = true;
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            BarButton.Checked = false;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!e.Cancel)
            {
                nIRC.UI.Environment.Window.WindowBar.Items.Remove(this.BarButton);
                this.SubEnvironment.Windows.Remove(this);
            }
            base.OnFormClosing(e);
        }

        public string GetTitle()
        {
            return base.Text;
        }

        StringCallback setTitle;
        public void SetTitle(string title)
        {
            if (InvokeRequired) { this.Invoke(setTitle, new object[] { title }); return; }
            base.Text = title;
        }


        public string GetTag()
        {
            return base.Name;
        }

        StringCallback setTag;
        public void SetTag(string tag)
        {
            if (InvokeRequired) { this.Invoke(setTag, new object[] { tag }); return; }
            base.Name = tag;
            BarButton.Text = tag;
        }
    }
}
