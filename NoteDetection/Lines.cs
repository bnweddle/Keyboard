using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDetection
{
    public class Lines
    {
        private int staffHeight = 15;

        public void DrawLines(Graphics g)
        {
            int i;
            // draw some staff lines, 900 will need to change as user is playing, want to scroll with sheet music as user plays as well
            for (i = 0; i < 9; i++)
                g.DrawLine(Pens.Wheat, 0, i * staffHeight, 900, i * staffHeight);
            for (; i < 14; i++)
                g.DrawLine(Pens.Black, 0, i * staffHeight, 900, i * staffHeight);
            for (; i < 19; i++)
                g.DrawLine(Pens.Wheat, 0, i * staffHeight, 900, i * staffHeight);
            for (; i < 24; i++)
                g.DrawLine(Pens.Black, 0, i * staffHeight, 900, i * staffHeight);
            for (; i < 30; i++)
                g.DrawLine(Pens.Wheat, 0, i * staffHeight, 900, i * staffHeight);
        }
    }
}
