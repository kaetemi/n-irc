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

#if NET

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace nIRC.UI
{
    partial class ChatOutputBox : RichTextBox
    {
        public ChatOutputBox()
        {
            appendTextDelegate = new ColorStringCallback(AppendText);
            base.SelectionHangingIndent = 16;
        }

        private int fI;
        private int fBegin;
        private int fEnd;
        private string fText;
        private void printRemaining() // used after fI is positioned ON the last formatting char
        {
            base.Select(base.TextLength, 0);
            base.SelectedText = fText.Substring(fBegin, fEnd - fBegin);
            fBegin = fI + 1;
            base.Select(base.TextLength, 0);
        }
        private void mightBeginFormat()
        {
            fEnd = fI;
        }
        private void isntFormat()
        {
            fI = fEnd;
        }

        private ushort fBold = 0;
        private ushort fItalic = 0;
        private ushort fUnderline = 0;
        private ushort fStrikeout = 0;
        private Font fFont;
        private FontStyle fFontStyle;
        private Color fDefaultColor;
        private bool fReverse = false;

        public void FormatReset()
        {
            base.SelectionFont = fFont = this.Font;
            base.SelectionColor = fDefaultColor;
            this.SelectionBackColor = this.BackColor;
            base.DetectUrls = false;
            fBold = 0;
            fItalic = 0;
            fUnderline = 0;
            fStrikeout = 0;
            fFontStyle = 0;
            fReverse = false;
        }

        public void FormatBold() // switches
        {
            if (fBold > 0) fBold = 0;
            else fBold = 1;
            fFontStyle ^= FontStyle.Bold;
            base.SelectionFont = fFont = new Font(fFont, fFontStyle);
        }
        public void FormatBoldAdd() // adds
        {
            fBold++;
            if (fBold == 1) // no change needed if already over 1 ;)
            {
                fFontStyle |= FontStyle.Bold;
                base.SelectionFont = fFont = new Font(fFont, fFontStyle);
            }
        }
        public void FormatBoldRemove() // removes
        {
            if (fBold > 0)
            {
                fBold--; // does not go lower than level 0, according to ctcp specs.
                if (fBold == 0)
                {
                    fFontStyle |= FontStyle.Bold;
                    fFontStyle ^= FontStyle.Bold;
                    base.SelectionFont = fFont = new Font(fFont, fFontStyle);
                }
            }
        }

        public void FormatUnderline() // switches
        {
            if (fUnderline > 0) fUnderline = 0;
            else fUnderline = 1;
            fFontStyle ^= FontStyle.Underline;
            base.SelectionFont = fFont = new Font(fFont, fFontStyle);
        }
        public void FormatUnderlineAdd() // adds
        {
            fUnderline++;
            if (fUnderline == 1) // no change needed if already over 1 ;)
            {
                fFontStyle |= FontStyle.Underline;
                base.SelectionFont = fFont = new Font(fFont, fFontStyle);
            }
        }
        public void FormatUnderlineRemove() // removes
        {
            if (fUnderline > 0)
            {
                fUnderline--; // does not go lower than level 0, according to ctcp specs.
                if (fBold == 0)
                {
                    fFontStyle |= FontStyle.Underline;
                    fFontStyle ^= FontStyle.Underline;
                    base.SelectionFont = fFont = new Font(fFont, fFontStyle);
                }
            }
        }

        public void FormatColorFront(Color color)
        {
            setColorFront(color);
        }

        public void FormatColorFront()
        {
            setColorFront(fDefaultColor);
        }

        public void FormatColorBack(Color color)
        {
            setColorBack(color);
        }

        public void FormatColorBack()
        {
            setColorBack(base.BackColor);
        }

        public void FormatColor(Color front, Color back)
        {
            setColorFront(front);
            setColorBack(back);
        }

        public void FormatColor()
        {
            setColorFront(fDefaultColor);
            setColorBack(base.BackColor);
        }

        private void setColorFront(Color color)
        {
            if (fReverse) this.SelectionBackColor = color;
            else base.SelectionColor = color;
        }

        private void setColorBack(Color color)
        {
            if (fReverse) base.SelectionColor = color;
            else this.SelectionBackColor = color;
        }

        public void FormatColorReverse()
        {
            fReverse = !fReverse;
            Color c = base.SelectionColor;
            base.SelectionColor = this.SelectionBackColor;
            this.SelectionBackColor = c;
        }

        private void tagMircColor()
        {
            mightBeginFormat();

            string numcache;
            int foreground = 99;
            int background = 99;
            fI++;
            if (fI < fText.Length)
            {
                if (char.IsDigit(fText, fI))
                {
                    numcache = fText[fI].ToString();
                    fI++;
                    if (fI < fText.Length)
                    {
                        if (char.IsDigit(fText, fI))
                        {
                            numcache += fText[fI];
                            fI++;
                        }
                    }
                    foreground = Int32.Parse(numcache);
                    if (fI < fText.Length)
                    {
                        if (fText[fI] == ',')
                        {
                            fI++;
                            if (fI < fText.Length)
                            {
                                if (char.IsDigit(fText, fI))
                                {
                                    numcache = fText[fI].ToString();
                                    fI++;
                                    if (fI < fText.Length)
                                    {
                                        if (char.IsDigit(fText, fI))
                                        {
                                            numcache += fText[fI];
                                            fI++;
                                        }
                                    }
                                    background = Int32.Parse(numcache);
                                }
                            }
                        }
                    }
                }
            }
            fI--;

            printRemaining();
            if (foreground == 99 && background == 99)
            {
                FormatColor();
            }
            else
            {
                if (foreground != 99)
                {
                    foreground &= 15;
                    FormatColorFront(FormatColors.Mirc[foreground]);
                }
                if (background != 99)
                {
                    background &= 15;
                    FormatColorBack(FormatColors.Mirc[background]);
                }
            }
        }

        private void tagCtcpFormatPrintRemaining()
        {
            // for compatibility with ctcp draft and ctcp2 draft :)
            // (format tag needs to close in ctcp2)
            if (fText[fI + 1] == '\x0006') fI++;
            printRemaining();
        }

        private void tagCtcpFormat()
        {
            mightBeginFormat();
            fI++;
            switch (fText[fI])
            {
                case 'B': // /test notB+boldB+boldB-boldB-not
                    fI++;
                    switch (fText[fI])
                    {
                        case '+':
                            tagCtcpFormatPrintRemaining();
                            FormatBoldAdd();
                            break;
                        case '-':
                            tagCtcpFormatPrintRemaining();
                            FormatBoldRemove();
                            break;
                        default:
                            fI--;
                            tagCtcpFormatPrintRemaining();
                            FormatBold();
                            break;
                    }
                    break;
                default:
                    isntFormat();
                    break;
            }
        }

        /// <summary>
        /// The text to be appended with time tag and irc formatting.
        /// Handles calls from external threads just fine.
        /// Should not throw any exceptions in any case!
        /// </summary>
        /// <param name="color"></param>
        /// <param name="text"></param>
        public void AppendText(Color color, string text)
        {
            if (InvokeRequired) { this.Invoke(appendTextDelegate, new object[] { color, text }); return; }

            int originalstart = this.SelectionStart;
            int originallength = this.SelectionLength;

            this.fDefaultColor = color;
            this.fText = text;

            // Special code to write time!
            base.Select(base.TextLength, 0);
            FormatReset();
            FormatColorFront(Color.Gray);
            this.SelectedText = string.Format("\r\n[{0}] ", DateTime.Now.ToString("HH:mm:ss"));
            base.Select(base.TextLength, 0);
            FormatColorFront();

            fBegin = 0;
            for (fI = 0; fI < text.Length; fI++)
            {
                switch (text[fI])
                {
                    ///test testingreversebacknormal
                    ////test testingreversebacknormal
                    case '\x02': // bold
                        mightBeginFormat();
                        printRemaining();
                        FormatBold();
                        break;
                    case '\x16': // reverse
                        mightBeginFormat();
                        printRemaining();
                        FormatColorReverse();
                        break;
                    case '\x1F': // underline
                        mightBeginFormat();
                        printRemaining();
                        FormatUnderline();
                        break;
                    case '\x0F': // remove formatting
                        mightBeginFormat();
                        printRemaining();
                        FormatReset();
                        break;
                    case '\x03': // color
                        tagMircColor();
                        break;
                    case '\x0006':
                        tagCtcpFormat();
                        break;
                    //            case 'V':
                    //                this.Select(this.TextLength, 0);
                    //                this.SelectedText = text.Substring(begin, i - begin);
                    //                i = i + 1;
                    //                begin = i + 1;

                    //                tempcolor1 = this.SelectionBackColor;
                    //                reversecolors = !reversecolors;

                    //                this.Select(this.TextLength, 0);
                    //                this.SelectionBackColor = this.SelectionColor;
                    //                this.SelectionColor = tempcolor1;
                    //                break;
                    //            case 'U':
                    //                this.Select(this.TextLength, 0);
                    //                this.SelectedText = text.Substring(begin, i - begin);
                    //                i = i + 1;
                    //                begin = i + 1;

                    //                fontstyle ^= FontStyle.Underline;

                    //                this.Select(this.TextLength, 0);
                    //                this.SelectionFont = font = new Font(font, fontstyle);
                    //                break;
                    //            case 'S':
                    //                this.Select(this.TextLength, 0);
                    //                this.SelectedText = text.Substring(begin, i - begin);
                    //                i = i + 1;
                    //                begin = i + 1;

                    //                fontstyle ^= FontStyle.Strikeout;

                    //                this.Select(this.TextLength, 0);
                    //                this.SelectionFont = font = new Font(font, fontstyle);
                    //                break;
                    //            case 'I':
                    //                this.Select(this.TextLength, 0);
                    //                this.SelectedText = text.Substring(begin, i - begin);
                    //                i = i + 1;
                    //                begin = i + 1;

                    //                fontstyle ^= FontStyle.Italic;

                    //                this.Select(this.TextLength, 0);
                    //                this.SelectionFont = font = new Font(font, fontstyle);
                    //                break;
                    //            case 'C':
                    //                if (i + 2 < text.Length)
                    //                {
                    //                    switch (text[i + 2])
                    //                    {
                    //                        case 'A':
                    //                            if (i + 3 < text.Length)
                    //                            {
                    //                                switch (text[i + 3])
                    //                                {
                    //                                    case '#':
                    //                                        if (i + 9 < text.Length)
                    //                                        {
                    //                                            try
                    //                                            {
                    //                                                tempint1 = Convert.ToInt32(text.Substring(i + 4, 2), 16);
                    //                                                tempint2 = Convert.ToInt32(text.Substring(i + 6, 2), 16);
                    //                                                tempint3 = Convert.ToInt32(text.Substring(i + 8, 2), 16);
                    //                                            }
                    //                                            catch { break; }
                    //                                            try
                    //                                            {
                    //                                                tempcolor1 = Color.FromArgb(tempint1, tempint2, tempint3);
                    //                                            }
                    //                                            catch { break; };
                    //                                            if (i + 10 < text.Length)
                    //                                            {
                    //                                                switch (text[i + 10])
                    //                                                {
                    //                                                    case '#':
                    //                                                        if (i + 16 < text.Length)
                    //                                                        {
                    //                                                            try
                    //                                                            {
                    //                                                                tempint1 = Convert.ToInt32(text.Substring(i + 11, 2), 16);
                    //                                                                tempint2 = Convert.ToInt32(text.Substring(i + 13, 2), 16);
                    //                                                                tempint3 = Convert.ToInt32(text.Substring(i + 15, 2), 16);
                    //                                                            }
                    //                                                            catch { break; }
                    //                                                            try
                    //                                                            {
                    //                                                                tempcolor2 = Color.FromArgb(tempint1, tempint2, tempint3);
                    //                                                            }
                    //                                                            catch { break; };
                    //                                                            this.Select(this.TextLength, 0);
                    //                                                            this.SelectedText = text.Substring(begin, i - begin);
                    //                                                            i = i + 16;
                    //                                                            begin = i + 1;
                    //                                                            this.Select(this.TextLength, 0);
                    //                                                            this.SelectionColor = tempcolor1;
                    //                                                            this.SelectionBackColor = tempcolor2;
                    //                                                            break;
                    //                                                        }
                    //                                                        break;
                    //                                                    case 'I':
                    //                                                        if (i + 11 < text.Length)
                    //                                                        {
                    //                                                            try { tempint1 = Convert.ToInt32(text[i + 11].ToString(), 16); }
                    //                                                            catch { break; }
                    //                                                            tempcolor2 = ctcpcolors[tempint1];
                    //                                                            this.Select(this.TextLength, 0);
                    //                                                            this.SelectedText = text.Substring(begin, i - begin);
                    //                                                            i = i + 11;
                    //                                                            begin = i + 1;
                    //                                                            this.Select(this.TextLength, 0);
                    //                                                            this.SelectionColor = tempcolor1;
                    //                                                            this.SelectionBackColor = tempcolor2;
                    //                                                            break;
                    //                                                        }
                    //                                                        break;
                    //                                                }
                    //                                            }
                    //                                            break;
                    //                                        }
                    //                                        break;
                    //                                    case 'I':
                    //                                        if (i + 4 < text.Length)
                    //                                        {
                    //                                            try { tempint1 = Convert.ToInt32(text[i + 4].ToString(), 16); }
                    //                                            catch { break; }
                    //                                            tempcolor1 = ctcpcolors[tempint1];
                    //                                            if (i + 5 < text.Length)
                    //                                            {
                    //                                                switch (text[i + 5])
                    //                                                {
                    //                                                    case '#':
                    //                                                        if (i + 11 < text.Length)
                    //                                                        {
                    //                                                            try
                    //                                                            {
                    //                                                                tempint1 = Convert.ToInt32(text.Substring(i + 6, 2), 16);
                    //                                                                tempint2 = Convert.ToInt32(text.Substring(i + 8, 2), 16);
                    //                                                                tempint3 = Convert.ToInt32(text.Substring(i + 10, 2), 16);
                    //                                                            }
                    //                                                            catch { break; }
                    //                                                            try
                    //                                                            {
                    //                                                                tempcolor2 = Color.FromArgb(tempint1, tempint2, tempint3);
                    //                                                            }
                    //                                                            catch { break; };
                    //                                                            this.Select(this.TextLength, 0);
                    //                                                            this.SelectedText = text.Substring(begin, i - begin);
                    //                                                            i = i + 11;
                    //                                                            begin = i + 1;
                    //                                                            this.Select(this.TextLength, 0);
                    //                                                            this.SelectionColor = tempcolor1;
                    //                                                            this.SelectionBackColor = tempcolor2;
                    //                                                            break;
                    //                                                        }
                    //                                                        break;
                    //                                                    case 'I':
                    //                                                        if (i + 6 < text.Length)
                    //                                                        {
                    //                                                            try { tempint1 = Convert.ToInt32(text[i + 6].ToString(), 16); }
                    //                                                            catch { break; }
                    //                                                            tempcolor2 = ctcpcolors[tempint1];
                    //                                                            this.Select(this.TextLength, 0);
                    //                                                            this.SelectedText = text.Substring(begin, i - begin);
                    //                                                            i = i + 6;
                    //                                                            begin = i + 1;
                    //                                                            this.Select(this.TextLength, 0);
                    //                                                            this.SelectionColor = tempcolor1;
                    //                                                            this.SelectionBackColor = tempcolor2;
                    //                                                            break;
                    //                                                        }
                    //                                                        break;
                    //                                                }
                    //                                            }
                    //                                            break;
                    //                                        }
                    //                                        break;
                    //                                }
                    //                            }
                    //                            break;
                    //                        case 'F':
                    //                            if (i + 3 < text.Length)
                    //                            {
                    //                                switch (text[i + 3])
                    //                                {
                    //                                    case '#':
                    //                                        if (i + 9 < text.Length)
                    //                                        {
                    //                                            try
                    //                                            {
                    //                                                tempint1 = Convert.ToInt32(text.Substring(i + 4, 2), 16);
                    //                                                tempint2 = Convert.ToInt32(text.Substring(i + 6, 2), 16);
                    //                                                tempint3 = Convert.ToInt32(text.Substring(i + 8, 2), 16);
                    //                                            }
                    //                                            catch { break; }
                    //                                            try
                    //                                            {
                    //                                                tempcolor1 = Color.FromArgb(tempint1, tempint2, tempint3);
                    //                                            }
                    //                                            catch { break; }
                    //                                            this.Select(this.TextLength, 0);
                    //                                            this.SelectedText = text.Substring(begin, i - begin);
                    //                                            i = i + 9;
                    //                                            begin = i + 1;
                    //                                            this.Select(this.TextLength, 0);
                    //                                            this.SelectionColor = tempcolor1;
                    //                                            break;
                    //                                        }
                    //                                        break;
                    //                                    case 'I':
                    //                                        if (i + 4 < text.Length)
                    //                                        {
                    //                                            try { tempint1 = Convert.ToInt32(text[i + 4].ToString(), 16); }
                    //                                            catch { break; }

                    //                                            this.Select(this.TextLength, 0);
                    //                                            this.SelectedText = text.Substring(begin, i - begin);
                    //                                            i = i + 4;
                    //                                            begin = i + 1;

                    //                                            this.Select(this.TextLength, 0);
                    //                                            this.SelectionColor = ctcpcolors[tempint1];
                    //                                            break;
                    //                                        }
                    //                                        break;
                    //                                }
                    //                            }
                    //                            break;
                    //                        case 'B':
                    //                            if (i + 3 < text.Length)
                    //                            {
                    //                                switch (text[i + 3])
                    //                                {
                    //                                    case '#':
                    //                                        if (i + 9 < text.Length)
                    //                                        {
                    //                                            try
                    //                                            {
                    //                                                tempint1 = Convert.ToInt32(text.Substring(i + 4, 2), 16);
                    //                                                tempint2 = Convert.ToInt32(text.Substring(i + 6, 2), 16);
                    //                                                tempint3 = Convert.ToInt32(text.Substring(i + 8, 2), 16);
                    //                                            }
                    //                                            catch { break; }
                    //                                            try
                    //                                            {
                    //                                                tempcolor1 = Color.FromArgb(tempint1, tempint2, tempint3);
                    //                                            }
                    //                                            catch { break; }
                    //                                            this.Select(this.TextLength, 0);
                    //                                            this.SelectedText = text.Substring(begin, i - begin);
                    //                                            i = i + 9;
                    //                                            begin = i + 1;
                    //                                            this.Select(this.TextLength, 0);
                    //                                            this.SelectionBackColor = tempcolor1;
                    //                                            break;
                    //                                        }
                    //                                        break;
                    //                                    case 'I':
                    //                                        if (i + 4 < text.Length)
                    //                                        {
                    //                                            try { tempint1 = Convert.ToInt32(text[i + 4].ToString(), 16); }
                    //                                            catch { break; }

                    //                                            this.Select(this.TextLength, 0);
                    //                                            this.SelectedText = text.Substring(begin, i - begin);
                    //                                            i = i + 4;
                    //                                            begin = i + 1;

                    //                                            this.Select(this.TextLength, 0);
                    //                                            this.SelectionBackColor = ctcpcolors[tempint1];
                    //                                            break;
                    //                                        }
                    //                                        break;
                    //                                }
                    //                            }
                    //                            break;
                    //                        case 'X':
                    //                            if (i + 3 < text.Length)
                    //                            {
                    //                                switch (text[i + 3])
                    //                                {
                    //                                    case 'A':
                    //                                        this.Select(this.TextLength, 0);
                    //                                        this.SelectedText = text.Substring(begin, i - begin);
                    //                                        i = i + 3;
                    //                                        begin = i + 1;
                    //                                        this.Select(this.TextLength, 0);
                    //                                        this.SelectionBackColor = this.BackColor;
                    //                                        this.SelectionColor = this.ForeColor;
                    //                                        break;
                    //                                    case 'F':
                    //                                        this.Select(this.TextLength, 0);
                    //                                        this.SelectedText = text.Substring(begin, i - begin);
                    //                                        i = i + 3;
                    //                                        begin = i + 1;
                    //                                        this.Select(this.TextLength, 0);
                    //                                        this.SelectionColor = this.ForeColor;
                    //                                        break;
                    //                                    case 'B':
                    //                                        this.Select(this.TextLength, 0);
                    //                                        this.SelectedText = text.Substring(begin, i - begin);
                    //                                        i = i + 3;
                    //                                        begin = i + 1;
                    //                                        this.Select(this.TextLength, 0);
                    //                                        this.SelectionBackColor = this.BackColor;
                    //                                        break;
                    //                                    //default:
                    //                                }
                    //                            }
                    //                            break;
                    //                        //default:
                    //                        //    output.AppendText("�");
                    //                        //    break;
                    //                    }
                    //                }
                    //                break;
                    //            case 'F': // INTENTIONALLY NOT IMPLEMENTED
                    //                break;
                    //            case 'N':
                    //                this.Select(this.TextLength, 0);
                    //                this.SelectedText = text.Substring(begin, i - begin);
                    //                i = i + 1;
                    //                begin = i + 1;

                    //                fontstyle = this.Font.Style;
                    //                reversecolors = false;

                    //                this.Select(this.TextLength, 0);
                    //                this.SelectionColor = this.ForeColor;
                    //                this.SelectionBackColor = this.BackColor;
                    //                this.SelectionFont = font = this.Font;
                    //                break;
                    //            case 'X': // NOT IMPLEMENTED YET
                    //                break;
                    //            //default:
                    //            //    break;
                    //        }
                    //        break;
                    //    }
                    //    break;
                    ////default:
                    ////break;
                }
            }
            mightBeginFormat();
            printRemaining();

            if (!this.Focused)
            {
                ScrollToBottom();
                // base.Select(this.TextLength, 0);
                // base.ScrollToCaret();
            }
            base.Select(originalstart, originallength);
        }
        private const int WM_VSCROLL = 0x115;
        private const int SB_BOTTOM = 7;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(
            IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        void ScrollToBottom() { SendMessage(this.Handle, WM_VSCROLL, (IntPtr)SB_BOTTOM, IntPtr.Zero); }

        public ColorStringCallback appendTextDelegate;

        protected override void OnSizeChanged(EventArgs e)
        {
            ScrollToBottom();
            base.OnSizeChanged(e);
        }
    }
}

#endif
