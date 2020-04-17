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

        // private int noteHght = 12;
        // private int noteWdth = 20;
        private Pen notePen = new Pen(Color.Black, 2);
        private Brush noteBrush = Brushes.Black;


        public enum Chromatic
        {
            Flat,      // "\u266D"
            Sharp,     // "\u266F"
            Natural    // "\u266E"
        };

        public enum Timing
        {
            Quarter,
            Half,
            Eighth,
            Sixteenth,
            Third,
            Whole
        };

        private long duration;

        public Note(long d)
        {
            duration = d;
        }
        

    }
}
