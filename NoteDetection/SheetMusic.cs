using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Drawing.Text;

namespace NoteDetection
{
    public partial class SheetMusic : Form
    {

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        public SheetMusic()
        {
            InitializeComponent();
            this.AutoScroll = true;
            ImportFont();
        }

        FontFamily ff;
        Font font;

        Treble treble = new Treble();
        Bass bass = new Bass();
        Lines lines = new Lines();

        private void SheetMusic_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            lines.DrawLines(g);


            FontStyle fontStyle = FontStyle.Regular;
            font = new Font(ff, 50, fontStyle);

            treble.DrawTreble(g, font);
            bass.DrawTreble(g, font);


            // draw four semi-random full and quarter notes
           /* g.DrawEllipse(_notePen, 20, 2 * _staffHght, _noteWdth, _noteHght);
            g.DrawEllipse(_notePen, 50, 4 * _staffHght, _noteWdth, _noteHght);

            g.FillEllipse(_noteBrush, 100, 2 * _staffHght, _noteWdth, _noteHght);
            g.FillEllipse(_noteBrush, 150, 4 * _staffHght, _noteWdth, _noteHght);*/
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
    }
}
