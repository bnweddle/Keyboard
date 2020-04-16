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
            for (i = 3; i < 8; i++)
                g.DrawLine(Pens.Black, 0, i * staffHeight, 900, i * staffHeight);
            for (; i < 12; i++)
                g.DrawLine(Pens.White, 0, i * staffHeight, 900, i * staffHeight);
            for (; i < 17; i++)
                g.DrawLine(Pens.Black, 0, i * staffHeight, 900, i * staffHeight);
        }
    }
}
