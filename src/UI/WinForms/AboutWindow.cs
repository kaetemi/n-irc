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

namespace nIRC.UI
{
    public partial class AboutWindow : Form
    {
        private TextBox textBox1;
        private Button button1;
    
        public AboutWindow()
        {
            this.SuspendLayout();

            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();

            this.textBox1.Font = new Font(FontFamily.GenericMonospace, 6.0f);
            this.textBox1.ForeColor = Color.Black;
            this.textBox1.BackColor = Color.White;
            this.textBox1.ReadOnly = true;
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(360, 211);
            this.textBox1.Text = ".nIRC - an IRC client written in C# and IronPython.\r\nCopyright (C) 2007  Jan Boon\r\nFonteinstraat 65, 1502 Lembeek, Belgium\r\n\r\nThis program is free software; you can redistribute it and/or\r\nmodify it under the terms of the GNU General Public License\r\nas published by the Free Software Foundation; either version 2\r\nof the License, or (at your option) any later version.\r\n\r\nThis program is distributed in the hope that it will be useful,\r\nbut WITHOUT ANY WARRANTY; without even the implied warranty of\r\nMERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the\r\nGNU General Public License for more details.\r\n\r\nYou should have received a copy of the GNU General Public License\r\nalong with this program; if not, write to the Free Software\r\nFoundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.";

            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(297, 229);
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.Focus();

            this.ClientSize = new System.Drawing.Size(384, 264);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Text = "About .nIRC";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
