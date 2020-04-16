using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDetection
{
    public class Bass
    {
        /// <summary>
        /// The unicode symbol for the f bass clef
        /// </summary>
        public string BassClef { get; } = "\uD834\uDD22";

        /// <summary>
        /// the X position for the graphics
        /// </summary>
        public float X { get; set; } = 35;

        /// <summary>
        /// the Y position for the graphics
        /// </summary>
        public float Y { get; set; } = 100;

        /// <summary>
        /// The size of the font
        /// </summary>
        public float Size { get; set; } = 50;

        /// <summary>
        /// The brush the paint with
        /// </summary>
        private Brush noteBrush = Brushes.Black;

        /// <summary>
        /// Draws the treble symbol on the form
        /// </summary>
        /// <param name="g">the graphics to call the draw method</param>
        /// <param name="font">the font needed to draw the symbol</param>
        public void DrawTreble(Graphics g, Font font)
        {
            g.DrawString(this.BassClef, font, noteBrush, this.X, this.Y);
        }
    }
}
