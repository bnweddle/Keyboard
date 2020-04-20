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
        public float Size { get; set; } = 65;

        /// <summary>
        /// the X position for the graphics
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// the Y position for the graphics
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// the width for the image
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// the height for the image
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// The image for the left hand notes
        /// </summary>
        public Image Image { get; set; }

        /// <summary>
        /// The brush the paint with
        /// </summary>
        private Brush noteBrush = Brushes.Black;

        /// <summary>
        /// The style of the font
        /// </summary>
        private FontStyle fontStyle = FontStyle.Regular;

        /// <summary>
        /// The unicode symbol
        /// </summary>
        public string Unicode { get; set; }

        public Symbol(string code, float size, float x, float y)
        {
            Unicode = code;
            Size = size;
            X = x;
            Y = y;
        }

        public Symbol(Image image, float x, float y, float width, float height)
        {
            Image = image;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public void DrawSymbol(Graphics g, Font font, FontFamily ff, string unicode, float x, float y)
        {
            font = new Font(ff, this.Size, fontStyle);
            g.DrawString(unicode, font, noteBrush, x, y);
        }

        public void DrawSymbol(Graphics g, float x, float y, float width, float height)
        {
            // 20, 60 for regular notes, not sure about whole note
            g.DrawImage(this.Image, x, y, width, height);
        }

        public void DrawSymbol(Graphics g, Font font, FontFamily ff, int xOffset, int yOffset)
        {
            font = new Font(ff, this.Size, fontStyle);
            // Bass xOffset = 10 yOffset = 30
            // Treble xOffset = 5 yOffset = 25
            g.DrawString(this.Unicode, font, noteBrush, this.X, this.Y);
            g.DrawString("\uD834\uDD34", font, noteBrush, this.X + xOffset, this.Y - yOffset);
        }
    }
}
