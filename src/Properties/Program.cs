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
using System.Windows.Forms;

namespace nIRC
{
    static class Program
    {
        // "svn" is added to the version for builds on the svn,
        // other builds range from "a" to "z".
        public const string Name = ".nIRC";
        public const string Version = "v0.6svn";
        public const string Identifier = "n-irc/0.6svn";
        public const string Contact = "n-irc@kaetemi.be";

        public static string OS = (Environment.OSVersion.Platform.ToString() + "/" + Environment.OSVersion.Version.ToString()).Replace(" ", "");

        [STAThread]
        static void Main() { UI.Environment.Run(); }
    }
}