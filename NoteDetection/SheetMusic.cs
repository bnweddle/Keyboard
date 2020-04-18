using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.Collections.Generic;

namespace NoteDetection
{
    public partial class SheetMusic : Form
    {     
        private int staffHeight = 15;

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        public SheetMusic()
        {
            InitializeComponent();
            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(3000, this.Size.Height - 100);
            this.ResizeRedraw = true;
            ImportFont();
        }

        FontFamily ff;
        Font font;

        Treble treble = new Treble();
        Bass bass = new Bass();
        List<Symbol> DrawingNote = new List<Symbol>();
        List<Point> Points = new List<Point>();
        Graphics g;
        int offset;
        bool thirds;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        public void UpdatePaint(int off, bool third)
        {
            this.thirds = third;
            Symbol symbol = new Symbol(Global.Symbol);
            Point point = new Point(off, (int)treble.Y - 5);
            DrawingNote.Add(symbol);
            Points.Add(point);

            if (third)
            {   // For checking if it is a third note to add the dot, position should not change
                Symbol s = new Symbol("\uD834\uDD58", 25);
                Point p = new Point(point.X + 30, point.Y + 48);
                DrawingNote.Add(s);
                Points.Add(p);
            }

            offset = off;
            Invalidate();
        }

        private void SheetMusic_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.TranslateTransform(this.AutoScrollPosition.X, this.AutoScrollPosition.Y);
            g.SmoothingMode = SmoothingMode.HighQuality;
            DrawLines(g);

            for(int i = 0; i < DrawingNote.Count; i++)
            {
                DrawingNote[i].DrawSymbol(g, font, ff, DrawingNote[i].Unicode, Points[i].X, Points[i].Y);
            }
                

            treble.DrawTreble(g, font, ff);
            bass.DrawTreble(g, font, ff);
        }

        /// <summary>
        /// Imports the Symbola font to allow Musical Unicode Symbols to be drawn
        /// </summary>
        private void ImportFont()
        {
            // Create the byte array and get its length
            byte[] fontArray = Properties.Resources.Symbola;
            int dataLength = Properties.Resources.Symbola.Length;


            // ASSIGN MEMORY AND COPY  BYTE[] ON THAT MEMORY ADDRESS
            IntPtr ptrData = Marshal.AllocCoTaskMem(dataLength);
            Marshal.Copy(fontArray, 0, ptrData, dataLength);

            uint cFonts = 0;
            AddFontMemResourceEx(ptrData, (uint)fontArray.Length, IntPtr.Zero, ref cFonts);

            PrivateFontCollection pfc = new PrivateFontCollection();
            //PASS THE FONT TO THE  PRIVATEFONTCOLLECTION OBJECT
            pfc.AddMemoryFont(ptrData, dataLength);

            //FREE THE  "UNSAFE" MEMORY
            Marshal.FreeCoTaskMem(ptrData);

            ff = pfc.Families[0];
            font = new Font(ff, 15f, FontStyle.Bold);
        }

        public void DrawLines(Graphics g)
        {
            int i;
            // draw some staff lines, 900 will need to change as user is playing, want to scroll with sheet music as user plays as well
            for (i = 3; i < 8; i++)
                g.DrawLine(Pens.Black, 0, i * staffHeight, this.Size.Width, i * staffHeight);
            for (; i < 13; i++)
                g.DrawLine(Pens.Wheat, 0, i * staffHeight, this.Size.Width, i * staffHeight);
            for (; i < 18; i++)
                g.DrawLine(Pens.Black, 0, i * staffHeight, this.Size.Width, i * staffHeight);
        }
    }
}
