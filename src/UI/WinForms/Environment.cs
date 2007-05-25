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
using System.IO;

namespace nIRC.UI
{
    static partial class Environment
    {
        public static MainWindow Window;
        public static Collection<SubEnvironment> Children = new Collection<SubEnvironment>();
        public static TransferManager TransferManager; // not needed to be public

        public static void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            nIRC.Config.Load();

            Window = new MainWindow();

            TransferManager = new TransferManager();
            TransferManager.Size = new Size(500, 300);
            TransferManager.TopLevel = false;
            TransferManager.MdiParent = nIRC.UI.Environment.Window;

            //TransferManager.Show();
            //TransferManager.Controls.Add(new TransferWindow());
            //TransferManager.Controls.Add(new TransferWindow());

            Application.Run(Window);
        }

        public static ChatWindow ShowChatWindow(SubEnvironment owner, string tag, int group, bool input, bool userlist, bool output)
        {
            if (Window.InvokeRequired) { return (ChatWindow)(Window.Invoke(showChatWindow, new object[] { owner, tag, group, input, userlist, output })); }
            return new ChatWindow(owner, tag, group, input, userlist, output);
        }

        /// <summary>
        /// This creates a new download 'window' in the download manager of this user interface.
        /// </summary>
        /// <returns></returns>
        public static TransferWindow ShowTransferWindow(SubEnvironment owner)
        {
            return null;
        }

        public static void ShowLaunchWindow()
        {
            new LaunchWindow().ShowDialog();
        }

        //public static void SwitchTo(Window window)
        //{
        //    //foreach (Window w in Window.WindowPanel.Controls) w.HideWindow();
        //    //Window.WindowPanel.Controls.Clear();
        //    //window.ShowWindow();
        //    window.Activate();
        //}
    }
}
