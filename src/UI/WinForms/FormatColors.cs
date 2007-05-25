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
using System.Drawing;

namespace nIRC.UI
{
    static class FormatColors
    {
        public static Color[] Mirc = new Color[16]
            {
                Color.FromArgb(255, 255, 255), //0 white
                Color.FromArgb(0, 0, 0), //1 black
                Color.FromArgb(0, 0, 127), //2 blue     (navy)
                Color.FromArgb(0, 127, 0), //3 green
                Color.FromArgb(255, 0, 0), //4 red
                Color.FromArgb(127, 0, 0), //5 brown    (maroon)
                Color.FromArgb(127, 0, 127), //6 purple
                Color.FromArgb(255, 127, 0), //7 orange   (olive)
                Color.FromArgb(255, 255, 0), //8 yellow
                Color.FromArgb(0, 255, 0), //9 lt.green (lime)
                Color.FromArgb(0, 127, 127), //10 teal    (a kinda green/blue cyan)
                Color.FromArgb(0, 255, 255), //11 lt.cyan (cyan ?) (aqua)
                Color.FromArgb(0, 0, 255), //12 lt.blue (royal)
                Color.FromArgb(255, 0, 255), //13 pink    (light purple) (fuchsia)
                Color.FromArgb(127, 127, 127), //14 grey
                Color.FromArgb(191, 191, 191), //15 lt.grey (silver)
            };

        public static Color[] Ctcp = new Color[16]
            {
                Color.FromArgb(0, 0, 0), //0	Black	000,000,000
                Color.FromArgb(0, 0, 127), //1	Blue	000,000,128
                Color.FromArgb(0, 127, 0), //2	Green	000,128,000
                Color.FromArgb(0, 127, 127), //3	Cyan	000,128,128
                Color.FromArgb(127, 0, 0), //4	Red	128,000,000
                Color.FromArgb(127, 0, 127), //5	Purple	128,000,128
                Color.FromArgb(127, 127, 0), //6	Brown	128,128,000
                Color.FromArgb(191, 191, 191), //7	Lt Gray	204,204,204
                Color.FromArgb(127, 127, 127), //8	Gray	128,128,128
                Color.FromArgb(0, 0, 255), //9	Lt Blue	000,000,255
                Color.FromArgb(0, 255, 0), //A	Lt Green	000,255,000
                Color.FromArgb(0, 255, 255), //B	Lt Cyan	000,255,255
                Color.FromArgb(255, 0, 0), //C	Lt Red	255,000,000
                Color.FromArgb(255, 0, 255), //D	Pink	255,000,255
                Color.FromArgb(255, 255, 0), //E	Yellow	255,255,000
                Color.FromArgb(255, 255, 255), //F	White	255,255,255
            };
    }
}
