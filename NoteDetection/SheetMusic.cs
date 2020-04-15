using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace NoteDetection
{
    public partial class SheetMusic : Form
    {
        public SheetMusic()
        {
            InitializeComponent();
        }

        private int _staffHght = 15;
        private int _noteHght = 12;
        private int _noteWdth = 20;
        private Pen _notePen = new Pen(Color.Black, 2);
        private Brush _noteBrush = Brushes.Black;

        private void SheetMusic_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            // draw some staff lines
            for (int i = 1; i < 6; i++)
                g.DrawLine(Pens.Black, 0, i * _staffHght, 900, i * _staffHght);

            Font f = new Font("Symbola", 72);
            g.DrawString("\uD834\uDD1A", f, _noteBrush, 50, 4);

            // draw four semi-random full and quarter notes
            g.DrawEllipse(_notePen, 10, 2 * _staffHght, _noteWdth, _noteHght);
            g.DrawEllipse(_notePen, 50, 4 * _staffHght, _noteWdth, _noteHght);

            g.FillEllipse(_noteBrush, 100, 2 * _staffHght, _noteWdth, _noteHght);
            g.FillEllipse(_noteBrush, 150, 4 * _staffHght, _noteWdth, _noteHght);
        }
    }
}
