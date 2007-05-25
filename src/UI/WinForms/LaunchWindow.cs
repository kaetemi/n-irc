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
    partial class LaunchWindow : Form
    {
        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private Label label3;
        private TextBox textBox2;
        private Label label4;
        private Button button1;
        private Button button2;
        private TextBox textBox3;
    
        public LaunchWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top 
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Size = new System.Drawing.Size(260, 50);
            this.label1.Text = "It seems that you are running .nIRC for the first time. Please waste a few seconds of your time in order to verify the following settings:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(97, 62);
            this.textBox1.Size = new System.Drawing.Size(175, 20);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.Text = "Nickname:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 88);
            this.label3.Size = new System.Drawing.Size(79, 20);
            this.label3.Text = "Real name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(97, 88);
            this.textBox2.Size = new System.Drawing.Size(175, 20);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 114);
            this.label4.Size = new System.Drawing.Size(79, 20);
            this.label4.Text = "E-mail:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(97, 114);
            this.textBox3.Size = new System.Drawing.Size(175, 20);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(197, 150);
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.Text = "OK";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 150);
            this.button2.Size = new System.Drawing.Size(100, 23);
            this.button2.Text = "Read License";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // LaunchWindow
            // 
            this.ClientSize = new System.Drawing.Size(284, 185);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Text = ".nIRC - [First Launch]";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new AboutWindow().ShowDialog();
        }
    }
}
