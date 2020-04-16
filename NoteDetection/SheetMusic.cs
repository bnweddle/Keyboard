﻿using System;
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
            CargoPrivateFontCollection();
        }

        FontFamily ff;
        Font font;
        private int _staffHght = 15;
        private int _noteHght = 12;
        private int _noteWdth = 20;
        private Pen _notePen = new Pen(Color.Black, 2);
        private Brush _noteBrush = Brushes.Black;

        string gClef = "\uD834\uDD1E";
        string fClef = "\uD834\uDD22";

        private void SheetMusic_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            // draw some staff lines
            for (int i = 1; i < 6; i++)
                g.DrawLine(Pens.Black, 0, i * _staffHght, 900, i * _staffHght);


            FontStyle fontStyle = FontStyle.Regular;
            font = new Font(ff, 50, fontStyle);
            g.DrawString(gClef, font, _noteBrush, 35, 10);

            // draw four semi-random full and quarter notes
           /* g.DrawEllipse(_notePen, 20, 2 * _staffHght, _noteWdth, _noteHght);
            g.DrawEllipse(_notePen, 50, 4 * _staffHght, _noteWdth, _noteHght);

            g.FillEllipse(_noteBrush, 100, 2 * _staffHght, _noteWdth, _noteHght);
            g.FillEllipse(_noteBrush, 150, 4 * _staffHght, _noteWdth, _noteHght);*/
        }

        private void CargoPrivateFontCollection()
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
