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
        private int staffWidth = 900;
        private int scrollWidth = 1200;

        private int scroll = 0;

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        public SheetMusic()
        {
            InitializeComponent();
            this.AutoScroll = true;
            this.ResizeRedraw = true;
            ImportFont();
        }

        FontFamily ff;
        Font font;

        Symbol treble = new Symbol("\uD834\uDD1E", 75, 55, 175);
        Symbol bass = new Symbol("\uD834\uDD22", 75, 50, 330);

        Symbol upperTreble = new Symbol("\uD834\uDD1E", 75, 55, 70);
        Symbol lowerBass = new Symbol("\uD834\uDD22", 75, 50, 435);

        List<Symbol> DrawingRightNotes = new List<Symbol>();
        List<Symbol> DrawingLeftNotes = new List<Symbol>();

        Graphics g;
        int offset;
        bool thirds;

        int handOffsetX;
        int handOffsetY;

        Chromatic chromValue = Chromatic.Natural;

        public Point DeskopLocation { get; internal set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        public void UpdatePaint(int off, bool third, double position)
        {
            scrollWidth += 65;
            staffWidth += 35;
            scroll += 35;

            this.thirds = third;
            this.AutoScrollMinSize = new Size(scrollWidth, this.Size.Height - 100);
            this.AutoScrollPosition = new Point(scroll, 0);
            Symbol symbol;

            if (Global.Handy == Hand.Right)
            {
                symbol = new Symbol(Global.Symbol, 65, off, (float)position);
                DrawingRightNotes.Add(symbol);
                handOffsetX = 0;
                handOffsetY = 0;

            }
            else // Left Hand
            {
                if (third) handOffsetX = 15; else handOffsetX = 18;
                handOffsetY = 70;

                // Whole notes must be sized differently
                if (Global.Time == Timing.Whole || Global.Time == Timing.ThirdWhole)
                {
                    symbol = new Symbol(Global.Image, off, (float)position, 24, 15);
                    DrawingLeftNotes.Add(symbol);
                }
                else
                {
                    // All other left hand notes
                    symbol = new Symbol(Global.Image, off, (float)position, 20, 60);
                    DrawingLeftNotes.Add(symbol);
                }
            }


            if (third)
            {   // For checking if it is a third note to add the dot, if whole note it will need to be swifted slightly
                if (Global.Time == Timing.ThirdWhole) handOffsetX -= 5;
                Symbol s = new Symbol("\uD834\uDD58", 25, symbol.X + 30 - handOffsetX, symbol.Y + 48 - handOffsetY);
                DrawingRightNotes.Add(s);
            }
            if (chromValue == Chromatic.Sharp)
            {
                Symbol s = new Symbol(Global.Chromatic, 20, symbol.X - handOffsetX, symbol.Y + 70 - handOffsetY);
                DrawingRightNotes.Add(s);
            }
            if (chromValue == Chromatic.Flat)
            {
                Symbol s = new Symbol(Global.Chromatic, 20, symbol.X - handOffsetX, symbol.Y + 70 - handOffsetY);
                DrawingRightNotes.Add(s);
            }



            offset = off;
            Invalidate();
        }

        public bool SetChromatic(bool isChromatic, Chromatic type)
        {
            this.chromValue = type;
            if(isChromatic)
            {
                if (type == Chromatic.Sharp)
                   this.chromValue = Chromatic.Sharp;
                else
                   this.chromValue = Chromatic.Flat;
            }
            else
            {
                this.chromValue = Chromatic.Natural;
            }

            return isChromatic;
        }

        private void SheetMusic_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.TranslateTransform(this.AutoScrollPosition.X, this.AutoScrollPosition.Y);
            g.SmoothingMode = SmoothingMode.HighQuality;
            DrawLines(g);

            for (int i = 0; i < DrawingRightNotes.Count; i++)
            {
                DrawingRightNotes[i].DrawSymbol(g, font, ff, DrawingRightNotes[i].Unicode, DrawingRightNotes[i].X, DrawingRightNotes[i].Y);
            }

            for(int i = 0; i < DrawingLeftNotes.Count; i++)
            {
                DrawingLeftNotes[i].DrawSymbol(g, DrawingLeftNotes[i].X, DrawingLeftNotes[i].Y, DrawingLeftNotes[i].Width, DrawingLeftNotes[i].Height);
            }

            treble.DrawSymbol(g, font, ff, 5, 25);
            bass.DrawSymbol(g, font, ff, 10, 30);
            upperTreble.DrawSymbol(g, font, ff, 5, 25);
            lowerBass.DrawSymbol(g, font, ff, 10, 30);

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
            for (i = 0; i < 4; i++)
                g.DrawLine(Pens.White, 0, i * staffHeight, staffWidth, i * staffHeight); // White space for extra room
            for (; i < 13; i++)
            {
                // System.Diagnostics.Debug.WriteLine($"{i * staffHeight } high treble");
                g.DrawLine(Pens.Wheat, 0, i * staffHeight, staffWidth, i * staffHeight); // High notes
            }
            for (; i < 18; i++)
            {
                // System.Diagnostics.Debug.WriteLine($"{i * staffHeight} middle treble");
                g.DrawLine(Pens.Black, 0, i * staffHeight, staffWidth, i * staffHeight); // Middle treble clef range
            }
            i = 18;
            g.DrawLine(Pens.Wheat, 0, i * staffHeight, staffWidth, i * staffHeight); 
            i++;
            for (; i < 23; i++)
                g.DrawLine(Pens.White, 0, i * staffHeight, staffWidth, i * staffHeight); // Middle notes
            for (; i < 28; i++)
            {
                // System.Diagnostics.Debug.WriteLine($"{i * staffHeight} middle bass");
                g.DrawLine(Pens.Black, 0, i * staffHeight, staffWidth, i * staffHeight); // Middle bass clef range
            }
            for (; i < 34; i++)
            {
                // System.Diagnostics.Debug.WriteLine($"{i * staffHeight} low bass");
                g.DrawLine(Pens.Wheat, 0, i * staffHeight, staffWidth, i * staffHeight); // Low notes
            }
        }
    }
}
