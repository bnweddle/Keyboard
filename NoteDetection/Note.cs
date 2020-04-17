using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDetection
{
    public class Note
    {
        protected bool HasLine = false;
        public const int NoteSpacing = 15;
        public const int ClefOffset = 50;
        public Point position = new Point(0, 0);
        public Chromatic chromatic = Chromatic.Natural;
        public Timing timing = Timing.Quarter;

        private int noteHght = 12;
        private int noteWdth = 20;
        private Pen notePen = new Pen(Color.Black, 2);
        private Brush noteBrush = Brushes.Black;


        public enum Chromatic
        {
            Flat,
            Sharp,
            Natural
        };

        public enum Timing
        {
            Sixteenth,
            Eighth,
            Quarter,
            Half,
            Third,
            Whole
        };


        public void ApproxTime(Stopwatch time, int bpm)
        {
            int minuteMil = 60000;

            

            // figure out how to do beats per minute
            // Need bpm in here to do this.
           

        }

        

    }
}
