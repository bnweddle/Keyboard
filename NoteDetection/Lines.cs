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
            for (i = 0; i < 3; i++)
                g.DrawLine(Pens.White, 0, i * staffHeight, 900, i * staffHeight); // White space for extra room
            for (; i < 12; i++)
            {
                // System.Diagnostics.Debug.WriteLine($"{i * staffHeight } high treble");
                g.DrawLine(Pens.Wheat, 0, i * staffHeight, 900, i * staffHeight); // High notes
            }
            for (; i < 17; i++)
            {
                // System.Diagnostics.Debug.WriteLine($"{i * staffHeight} middle treble");
                g.DrawLine(Pens.Black, 0, i * staffHeight, 900, i * staffHeight); // Middle treble clef range
            }
            i = 17;
            g.DrawLine(Pens.Wheat, 0, i * staffHeight, 900, i * staffHeight);
            i++;
            for (; i < 22; i++)
                g.DrawLine(Pens.White, 0, i * staffHeight, 900, i * staffHeight); // Middle notes
            for (; i < 27; i++)
            {
                // System.Diagnostics.Debug.WriteLine($"{i * staffHeight} middle bass");
                g.DrawLine(Pens.Black, 0, i * staffHeight, 900, i * staffHeight); // Middle bass clef range
            }
            for (; i < 33; i++)
            {
                // System.Diagnostics.Debug.WriteLine($"{i * staffHeight} low bass");
                g.DrawLine(Pens.Wheat, 0, i * staffHeight, 900, i * staffHeight); // Low notes
            }
        }
    }
}
