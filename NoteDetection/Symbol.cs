using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDetection
{
    public class Symbol
    {
        /// <summary>
        /// The size of the font
        /// </summary>
        public float Size { get; set; } = 75;

        private Brush noteBrush = Brushes.Black;
        private FontStyle fontStyle = FontStyle.Regular;

        public void DrawSymbol(Graphics g, Font font, FontFamily ff, string unicode, float x, float y)
        {
            font = new Font(ff, this.Size, fontStyle);
            g.DrawString(unicode, font, noteBrush, x, y);
        }
    }
}
