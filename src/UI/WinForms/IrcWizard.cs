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
    partial class IrcWizard : Form
    {
        private Label label1;
        private TextBox textBox1;
        private NumericUpDown numericUpDown1;
        private Label label2;
        private Label label3;
        private TextBox textBox2;
        private Label label4;
        private Button button1;
    
        public IrcWizard()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IrcWizard));
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown1.BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(313, 200);
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.Text = "Connect";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top 
                               | System.Windows.Forms.AnchorStyles.Left
                               | System.Windows.Forms.AnchorStyles.Right;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Size = new System.Drawing.Size(376, 75);
            this.label1.Text = @"You are about to connect to an IRC server. This is a machine, or a network of them, which is connected to the internet or your lan, and is running some kind of IRC server software obviously. In order to connect you need to know the domain name of the server. If you do not know where to find it, then I suggest you should go find out what you're actually doing here.";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(88, 90);
            this.textBox1.Size = new System.Drawing.Size(219, 20);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(313, 90);
            this.numericUpDown1.Size = new System.Drawing.Size(75, 20);
            this.numericUpDown1.Minimum = (decimal)short.MinValue + 1;
            this.numericUpDown1.Maximum = (decimal)short.MaxValue - 1;
            this.numericUpDown1.Value = (decimal)new Random().Next(6660, 6670);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 90);
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.Text = "Server:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 120);
            this.label3.Size = new System.Drawing.Size(376, 40);
            this.label3.Text = @"IRC servers have multiple channels, where people can actually talk to each other. If you want to enter one, you should write it\'s name here:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(88, 160);
            this.textBox2.Size = new System.Drawing.Size(300, 20);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 160);
            this.label4.Size = new System.Drawing.Size(70, 20);
            this.label4.Text = "Channel:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // IrcWizard
            // 
            this.ClientSize = new System.Drawing.Size(400, 235);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = ".nIRC - [IRC Connection Wizard]";
            this.numericUpDown1.EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
