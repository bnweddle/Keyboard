using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDetection
{
    public class Treble
    {
        /// <summary>
        /// The unicode symbol for the g treble clef
        /// </summary>
       public  string TrebleClef { get; } = "\uD834\uDD1E";

        /// <summary>
        /// the X position for the graphics
        /// </summary>
        public float X { get; set; } = 35;

        /// <summary>
        /// the Y position for the graphics
        /// </summary>
        public float Y { get; set; } = 40;

        /// <summary>
        /// The size of the font
        /// </summary>
        public float Size { get; set; } = 75;

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
            g.DrawString(this.TrebleClef, font, noteBrush, this.X, this.Y);
            g.DrawString("\uD834\uDD34", font, noteBrush, this.X + 10, this.Y - 10);
        }
    }
}
