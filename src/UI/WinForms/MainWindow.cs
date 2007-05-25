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
    class MainWindow : Form
    {
        public MainMenu MenuBar;
        public StatusBar StatusBar;
        public ToolStrip WindowBar;
        public MenuItem View_Transfers;

        public MainWindow()
        {
            this.SuspendLayout();

            MenuItem View_ToolBar = new MenuItem("&Tool Bar");
            View_ToolBar.Checked = true;
			MenuItem View_WindowBar = new MenuItem("&Window Bar", View_WindowBar_Click);
			View_WindowBar.Checked = true;
			MenuItem View_StatusBar = new MenuItem("&Status Bar", View_StatusBar_Click);
			View_StatusBar.Checked = true;
			View_Transfers = new MenuItem("Trans&fers", View_Transfers_Click);
			
            MenuBar = new MainMenu(new MenuItem[] {
                new MenuItem("&Chat", new MenuItem[] {   
                    new MenuItem("&Connection Wizard", Irc_ConnectionWizard_Click),
                    new MenuItem("&New Connection", Irc_NewConnection_Click),                    
                    new MenuItem("E&xit", Chat_Exit_Click) }),
                new MenuItem("&View", new MenuItem[] {   
                    View_WindowBar,
                    View_StatusBar,
                    View_Transfers }),
                new MenuItem("&Tools", new MenuItem[] {
                    new MenuItem("&Options", Tools_Options_Click) }),
                new MenuItem("&Windows", new MenuItem[] {
                    new MenuItem("&Cascade", Windows_Cascade_Click),
                    new MenuItem("Tile &Vertical", Windows_TileVertical_Click),
                    new MenuItem("Tile &Horizontal", Windows_TileHorizontal_Click),
                    // new MenuItem("C&lose All"),
                    new MenuItem("&Arrange Icons", Windows_ArrangeIcons_Click) }),
#if DEBUG
                new MenuItem("&Debug", new MenuItem[] {
                    new MenuItem("&ChatWindow", Debug_ChatWindow_Click) }),
#endif
                new MenuItem("&Help", new MenuItem[] {
                    new MenuItem("&About", Help_About_Click) }), });

            StatusBar = new StatusBar();
            StatusBar.Dock = DockStyle.Bottom;
            StatusBar.Text = Program.Version;

            WindowBar = new ToolStrip();
            WindowBar.Dock = DockStyle.Top;
            WindowBar.LayoutStyle = ToolStripLayoutStyle.Flow;
            WindowBar.RenderMode = ToolStripRenderMode.System;

            base.ClientSize = new Size(700, 500);
            //base.DoubleBuffered = true;      
            base.IsMdiContainer = true;
            //foreach (Control ctrl in Controls)
            //{
            //    if (ctrl is MdiClient)
            //    {
            //        Console.WriteLine("MdiClient found!");
            //        this.MdiClient = ((MdiClient)ctrl);
            //        break;
            //    }
            //}
            base.Text = Program.Name;
            base.Menu = MenuBar;

            base.Controls.Add(WindowBar);
            base.Controls.Add(StatusBar);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        //public MdiClient MdiClient;

        void Chat_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void Irc_NewConnection_Click(object sender, EventArgs e)
        {
            new nIRC.Irc.IrcEnvironment();
        }

        void Irc_ConnectionWizard_Click(object sender, EventArgs e)
        {
            new IrcWizard().ShowDialog();
        }

        void View_WindowBar_Click(object sender, EventArgs e)
        {
            MenuItem menuitem = ((MenuItem)sender);
            menuitem.Checked = !menuitem.Checked;
            this.WindowBar.Visible = menuitem.Checked;
        }

        void View_StatusBar_Click(object sender, EventArgs e)
        {
            MenuItem menuitem = ((MenuItem)sender);
            menuitem.Checked = !menuitem.Checked;
            this.StatusBar.Visible = menuitem.Checked;
        }

        void View_Transfers_Click(object sender, EventArgs e)
        {
            nIRC.UI.Environment.TransferManager.Visible = (!((MenuItem)sender).Checked);
        }

        void Windows_Cascade_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        void Windows_TileVertical_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        void Windows_TileHorizontal_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        void Windows_ArrangeIcons_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

#if DEBUG

        void Debug_ChatWindow_Click(object sender, EventArgs e)
        {
            nIRC.UI.Environment.ShowChatWindow(new SubEnvironment(), "Debug_ChatWindow", 0, true, true, true);
        }

#endif

        void Tools_Options_Click(object sender, EventArgs e)
        {
            MessageBox.Show("There is no option.");
        }

        void Help_About_Click(object sender, EventArgs e)
        {
            new AboutWindow().ShowDialog();
        }
    }
}
