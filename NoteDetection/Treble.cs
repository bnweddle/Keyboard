﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDetection
{
    /// <summary>
    /// Need to move this to be used with Symbol
    /// </summary>
    public class Treble
    {
        /// <summary>
        /// The unicode symbol for the g treble clef
        /// </summary>
       public  string TrebleClef { get; } = "\uD834\uDD1E";

        /// <summary>
        /// the X position for the graphics
        /// </summary>
        public float X { get; set; } = 55;

        /// <summary>
        /// the Y position for the graphics
        /// </summary>
        public float Y { get; set; } = 25;

        /// <summary>
        /// The size of the font
        /// </summary>
        public float Size { get; set; } = 75;

        /// <summary>
        /// The brush the paint with
        /// </summary>
        private Brush noteBrush = Brushes.Black;

        /// <summary>
        /// The style of the font
        /// </summary>
        private FontStyle fontStyle = FontStyle.Regular;

        /// <summary>
        /// Draws the treble symbol on the form
        /// </summary>
        /// <param name="g">the graphics to call the draw method</param>
        /// <param name="font">the font needed to draw the symbol</param>
        /// <param name="ff">the font family to which the font belongs</param>
        public void DrawTreble(Graphics g, Font font, FontFamily ff)
        {
            font = new Font(ff, this.Size, fontStyle);
            g.DrawString(this.TrebleClef, font, noteBrush, this.X, this.Y);
            g.DrawString("\uD834\uDD34", font, noteBrush, this.X + 5, this.Y - 25);
        }
    }
}
