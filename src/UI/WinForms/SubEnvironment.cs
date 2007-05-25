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

namespace nIRC.UI
{
    /// <summary>
    /// Handles the interaction between the IRC connection code and the UI itself,
    /// which allows us to compile for an entirely diffent user interface engine, 
    /// with (as good as) no code changes.
    /// 
    /// It creates the windows, by using functions provided by the Environment.
    /// 
    /// The following is required within a user interface:
    /// Cl Environment()
    ///    Fu Run()
    ///    Fu ShowChatWindow(SubEnvironment owner, string tag, int group, bool input, bool userlist, bool output) - THREADCHECK
    /// Cl Window(string tag)
    ///    Fu GetTag()
    ///    Fu SetTag(string tag) - THREADCHECK
    ///    Fu GetTitle()
    ///    Fu SetTitle(string tag) - THREADCHECK
    /// Cl ChatWindow(string tag, bool input, bool userlist, bool output) : base(tag) // Window
    ///    Fu WriteOutput(Color basecolor, string text) - THREADCHECK
    ///    Ev ReadInput(string text)
    /// Cl DownloadWindow
    /// 
    /// The subenvironmentss are subclassed from this class. This class contains
    /// stuff specific to this user interface, allowing the environment to communicate
    /// with the subenvironment.
    /// 
    /// </summary>
    class SubEnvironment
    {
        public SubEnvironment()
        {
            ToolBarSeperator = new ToolStripSeparator();
            Environment.Children.Add(this);
            int index = Environment.Window.WindowBar.Items.Count;
            while (index > 0)
            {
                if (Environment.Window.WindowBar.Items[index - 1] is ToolStripSeparator)
                {
                    break;
                }
                index--;
            }
            Environment.Window.WindowBar.Items.Insert(index, ToolBarSeperator);
        }
        public ToolStripSeparator ToolBarSeperator;

        public Collection<Window> Windows = new Collection<Window>();

        public virtual void Close()
        {
            nIRC.UI.Environment.Children.Remove(this);
            Window[] windows = new Window[Windows.Count];
            Windows.CopyTo(windows, 0);
            foreach (Window window in windows)
            {
                try
                {
                    if (window != null) if (!window.Disposing)
                            window.Close();
                }
                catch { }
            }
            nIRC.UI.Environment.Window.WindowBar.Items.Remove(this.ToolBarSeperator);
        }
    }
}
