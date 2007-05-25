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
    class ColorSelection : Form
    {
        private TabControl tabControl1;
        private TabPage tabPage1;
        private ColorDialog colorDialog;
        private CheckBox ctcpForegroundCheckbox;
        private GroupBox groupBox1;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private TrackBar ctcpForegroundSelection;
        private Button button1;
        private TextBox textBox1;
        private GroupBox groupBox2;
        private Button button2;
        private TextBox textBox2;
        private TrackBar ctcpBackgroundSelection;
        private RadioButton radioButton4;
        private RadioButton radioButton5;
        private RadioButton radioButton6;
        private CheckBox checkBox1;
        private GroupBox mircForegroundBox;
        private TrackBar mircForegroundSelection;
        private CheckBox mircForegroundCheckbox;
        private GroupBox mircBackgroundBox;
        private TrackBar mircBackgroundSelection;
        private CheckBox mircBackgroundCheckbox;
        private GroupBox mircGroup;
        private RadioButton mircReset;
        private RadioButton mircChange;
        private Button button3;
        private Button button5;
        private Button mircOk;
        private Button button6;
        private TabPage tabPage2;

        public ColorSelection()
        {
            InitializeComponent();
        }
        public ColorSelection(ChatInputBox uiinputbox)
        {
            this.uiInputBox = uiinputbox;
            InitializeComponent();
        }

        ChatInputBox uiInputBox;

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.mircChange = new System.Windows.Forms.RadioButton();
            this.button5 = new System.Windows.Forms.Button();
            this.mircOk = new System.Windows.Forms.Button();
            this.mircReset = new System.Windows.Forms.RadioButton();
            this.mircGroup = new System.Windows.Forms.GroupBox();
            this.mircForegroundCheckbox = new System.Windows.Forms.CheckBox();
            this.mircForegroundBox = new System.Windows.Forms.GroupBox();
            this.mircForegroundSelection = new System.Windows.Forms.TrackBar();
            this.mircBackgroundCheckbox = new System.Windows.Forms.CheckBox();
            this.mircBackgroundBox = new System.Windows.Forms.GroupBox();
            this.mircBackgroundSelection = new System.Windows.Forms.TrackBar();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button6 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.ctcpForegroundCheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.ctcpBackgroundSelection = new System.Windows.Forms.TrackBar();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ctcpForegroundSelection = new System.Windows.Forms.TrackBar();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.mircGroup.SuspendLayout();
            this.mircForegroundBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mircForegroundSelection)).BeginInit();
            this.mircBackgroundBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mircBackgroundSelection)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctcpBackgroundSelection)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctcpForegroundSelection)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(386, 235);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.mircChange);
            this.tabPage1.Controls.Add(this.button5);
            this.tabPage1.Controls.Add(this.mircOk);
            this.tabPage1.Controls.Add(this.mircReset);
            this.tabPage1.Controls.Add(this.mircGroup);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(378, 209);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "mIRC";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // mircChange
            // 
            this.mircChange.AutoSize = true;
            this.mircChange.Checked = true;
            this.mircChange.Location = new System.Drawing.Point(12, 29);
            this.mircChange.Name = "mircChange";
            this.mircChange.Size = new System.Drawing.Size(14, 13);
            this.mircChange.TabIndex = 10;
            this.mircChange.TabStop = true;
            this.mircChange.UseVisualStyleBackColor = true;
            this.mircChange.CheckedChanged += new System.EventHandler(this.mircChange_CheckedChanged);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button5.Location = new System.Drawing.Point(216, 180);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 11;
            this.button5.Text = "Cancel";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // mircOk
            // 
            this.mircOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mircOk.Location = new System.Drawing.Point(297, 180);
            this.mircOk.Name = "mircOk";
            this.mircOk.Size = new System.Drawing.Size(75, 23);
            this.mircOk.TabIndex = 10;
            this.mircOk.Text = "OK";
            this.mircOk.UseVisualStyleBackColor = true;
            this.mircOk.Click += new System.EventHandler(this.mircOk_Click);
            // 
            // mircReset
            // 
            this.mircReset.AutoSize = true;
            this.mircReset.Location = new System.Drawing.Point(12, 6);
            this.mircReset.Name = "mircReset";
            this.mircReset.Size = new System.Drawing.Size(53, 17);
            this.mircReset.TabIndex = 9;
            this.mircReset.TabStop = true;
            this.mircReset.Text = "Reset";
            this.mircReset.UseVisualStyleBackColor = true;
            // 
            // mircGroup
            // 
            this.mircGroup.Controls.Add(this.mircForegroundCheckbox);
            this.mircGroup.Controls.Add(this.mircForegroundBox);
            this.mircGroup.Controls.Add(this.mircBackgroundCheckbox);
            this.mircGroup.Controls.Add(this.mircBackgroundBox);
            this.mircGroup.Location = new System.Drawing.Point(6, 29);
            this.mircGroup.Name = "mircGroup";
            this.mircGroup.Size = new System.Drawing.Size(366, 95);
            this.mircGroup.TabIndex = 8;
            this.mircGroup.TabStop = false;
            this.mircGroup.Text = "    Change";
            // 
            // mircForegroundCheckbox
            // 
            this.mircForegroundCheckbox.AutoSize = true;
            this.mircForegroundCheckbox.Location = new System.Drawing.Point(12, 19);
            this.mircForegroundCheckbox.Name = "mircForegroundCheckbox";
            this.mircForegroundCheckbox.Size = new System.Drawing.Size(15, 14);
            this.mircForegroundCheckbox.TabIndex = 1;
            this.mircForegroundCheckbox.UseVisualStyleBackColor = true;
            this.mircForegroundCheckbox.CheckedChanged += new System.EventHandler(this.mircForegroundCheckbox_CheckedChanged);
            // 
            // mircForegroundBox
            // 
            this.mircForegroundBox.Controls.Add(this.mircForegroundSelection);
            this.mircForegroundBox.Enabled = false;
            this.mircForegroundBox.Location = new System.Drawing.Point(6, 19);
            this.mircForegroundBox.Name = "mircForegroundBox";
            this.mircForegroundBox.Size = new System.Drawing.Size(174, 70);
            this.mircForegroundBox.TabIndex = 6;
            this.mircForegroundBox.TabStop = false;
            this.mircForegroundBox.Text = "    Foreground";
            // 
            // mircForegroundSelection
            // 
            this.mircForegroundSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mircForegroundSelection.BackColor = System.Drawing.Color.Black;
            this.mircForegroundSelection.LargeChange = 1;
            this.mircForegroundSelection.Location = new System.Drawing.Point(6, 19);
            this.mircForegroundSelection.Maximum = 15;
            this.mircForegroundSelection.Name = "mircForegroundSelection";
            this.mircForegroundSelection.Size = new System.Drawing.Size(162, 45);
            this.mircForegroundSelection.TabIndex = 5;
            this.mircForegroundSelection.Value = 1;
            this.mircForegroundSelection.Scroll += new System.EventHandler(this.mircColorSelection_Scroll);
            // 
            // mircBackgroundCheckbox
            // 
            this.mircBackgroundCheckbox.AutoSize = true;
            this.mircBackgroundCheckbox.Location = new System.Drawing.Point(192, 19);
            this.mircBackgroundCheckbox.Name = "mircBackgroundCheckbox";
            this.mircBackgroundCheckbox.Size = new System.Drawing.Size(15, 14);
            this.mircBackgroundCheckbox.TabIndex = 1;
            this.mircBackgroundCheckbox.UseVisualStyleBackColor = true;
            this.mircBackgroundCheckbox.CheckedChanged += new System.EventHandler(this.mircBackgroundCheckbox_CheckedChanged);
            // 
            // mircBackgroundBox
            // 
            this.mircBackgroundBox.Controls.Add(this.mircBackgroundSelection);
            this.mircBackgroundBox.Enabled = false;
            this.mircBackgroundBox.Location = new System.Drawing.Point(186, 19);
            this.mircBackgroundBox.Name = "mircBackgroundBox";
            this.mircBackgroundBox.Size = new System.Drawing.Size(174, 70);
            this.mircBackgroundBox.TabIndex = 7;
            this.mircBackgroundBox.TabStop = false;
            this.mircBackgroundBox.Text = "    Background";
            // 
            // mircBackgroundSelection
            // 
            this.mircBackgroundSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mircBackgroundSelection.BackColor = System.Drawing.Color.White;
            this.mircBackgroundSelection.LargeChange = 1;
            this.mircBackgroundSelection.Location = new System.Drawing.Point(6, 19);
            this.mircBackgroundSelection.Maximum = 15;
            this.mircBackgroundSelection.Name = "mircBackgroundSelection";
            this.mircBackgroundSelection.Size = new System.Drawing.Size(162, 45);
            this.mircBackgroundSelection.TabIndex = 5;
            this.mircBackgroundSelection.Scroll += new System.EventHandler(this.mircColorSelection_Scroll);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button6);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.ctcpForegroundCheckbox);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(378, 209);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "CTCP/2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button6.Location = new System.Drawing.Point(216, 180);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 5;
            this.button6.Text = "Cancel";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(297, 180);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "OK";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // ctcpForegroundCheckbox
            // 
            this.ctcpForegroundCheckbox.AutoSize = true;
            this.ctcpForegroundCheckbox.Location = new System.Drawing.Point(12, 6);
            this.ctcpForegroundCheckbox.Name = "ctcpForegroundCheckbox";
            this.ctcpForegroundCheckbox.Size = new System.Drawing.Size(15, 14);
            this.ctcpForegroundCheckbox.TabIndex = 1;
            this.ctcpForegroundCheckbox.UseVisualStyleBackColor = true;
            this.ctcpForegroundCheckbox.CheckedChanged += new System.EventHandler(this.ctcpForegroundCheckbox_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.ctcpBackgroundSelection);
            this.groupBox2.Controls.Add(this.radioButton4);
            this.groupBox2.Controls.Add(this.radioButton5);
            this.groupBox2.Controls.Add(this.radioButton6);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(192, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(180, 168);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "    Background";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(99, 139);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Select";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(6, 141);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(87, 20);
            this.textBox2.TabIndex = 6;
            // 
            // ctcpBackgroundSelection
            // 
            this.ctcpBackgroundSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ctcpBackgroundSelection.BackColor = System.Drawing.Color.White;
            this.ctcpBackgroundSelection.LargeChange = 1;
            this.ctcpBackgroundSelection.Location = new System.Drawing.Point(6, 65);
            this.ctcpBackgroundSelection.Maximum = 15;
            this.ctcpBackgroundSelection.Name = "ctcpBackgroundSelection";
            this.ctcpBackgroundSelection.Size = new System.Drawing.Size(168, 45);
            this.ctcpBackgroundSelection.TabIndex = 5;
            this.ctcpBackgroundSelection.Value = 15;
            this.ctcpBackgroundSelection.Scroll += new System.EventHandler(this.ctcpColorSelection_Scroll);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(6, 116);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(74, 17);
            this.radioButton4.TabIndex = 4;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Advanced";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Checked = true;
            this.radioButton5.Location = new System.Drawing.Point(6, 42);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(56, 17);
            this.radioButton5.TabIndex = 3;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "Simple";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(6, 19);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(53, 17);
            this.radioButton6.TabIndex = 2;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "Reset";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.ctcpForegroundSelection);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 168);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "    Foreground";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(99, 139);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Select";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(6, 141);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(87, 20);
            this.textBox1.TabIndex = 6;
            // 
            // ctcpForegroundSelection
            // 
            this.ctcpForegroundSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ctcpForegroundSelection.BackColor = System.Drawing.Color.Black;
            this.ctcpForegroundSelection.LargeChange = 1;
            this.ctcpForegroundSelection.Location = new System.Drawing.Point(6, 65);
            this.ctcpForegroundSelection.Maximum = 15;
            this.ctcpForegroundSelection.Name = "ctcpForegroundSelection";
            this.ctcpForegroundSelection.Size = new System.Drawing.Size(168, 45);
            this.ctcpForegroundSelection.TabIndex = 5;
            this.ctcpForegroundSelection.Scroll += new System.EventHandler(this.ctcpColorSelection_Scroll);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 116);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(74, 17);
            this.radioButton3.TabIndex = 4;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Advanced";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(56, 17);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Simple";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(53, 17);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Reset";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // ColorSelection
            // 
            this.ClientSize = new System.Drawing.Size(386, 235);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ColorSelection";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.mircGroup.ResumeLayout(false);
            this.mircGroup.PerformLayout();
            this.mircForegroundBox.ResumeLayout(false);
            this.mircForegroundBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mircForegroundSelection)).EndInit();
            this.mircBackgroundBox.ResumeLayout(false);
            this.mircBackgroundBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mircBackgroundSelection)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctcpBackgroundSelection)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctcpForegroundSelection)).EndInit();
            this.ResumeLayout(false);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog.ShowDialog();
        }

        private void ctcpForegroundCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = ctcpForegroundCheckbox.Checked;
        }

        private void ctcpColorSelection_Scroll(object sender, EventArgs e)
        {
            TrackBar trackbar = ((TrackBar)sender);
            trackbar.BackColor = FormatColors.Ctcp[trackbar.Value];
        }

        private void mircColorSelection_Scroll(object sender, EventArgs e)
        {
            TrackBar trackbar = ((TrackBar)sender);
            trackbar.BackColor = FormatColors.Mirc[trackbar.Value];
        }

        private void mircChange_CheckedChanged(object sender, EventArgs e)
        {
            mircGroup.Enabled = mircChange.Checked;
        }

        private void mircForegroundCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            mircForegroundBox.Enabled = mircForegroundCheckbox.Checked;
        }

        private void mircBackgroundCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            mircBackgroundBox.Enabled = mircBackgroundCheckbox.Checked;
        }

        private void mircOk_Click(object sender, EventArgs e)
        {
            if (mircReset.Checked)
            {
                uiInputBox.SelectedText = "\x0003";
            }
            else if (mircForegroundCheckbox.Checked && mircBackgroundCheckbox.Checked)
            {
                uiInputBox.SelectedText = "\x0003" + mircForegroundSelection.Value + "," + mircBackgroundSelection.Value;
            }
            else if (mircForegroundCheckbox.Checked)
            {
                uiInputBox.SelectedText = "\x0003" + mircForegroundSelection.Value;
            }
            else if (mircBackgroundCheckbox.Checked)
            {
                uiInputBox.SelectedText = "\x000399," + mircBackgroundSelection.Value;
            }
            Close();
        }
    }
}
