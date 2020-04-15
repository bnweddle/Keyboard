using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDetection
{
    public class Note
    {
        protected const int kXRadius = 4;
        protected const int kYRadius = 3;
        protected const int kStem = 15;
        protected bool HasLine = false;
        public const int kNoteSpacing = 20;
        public const int kClefOffset = 50;

        public enum Chromatic
        {
            Flat,
            Sharp,
            Natural
        };

        public enum Timing
        {
            sixteenth,
            eighth,
            quarter,
            half,
            third,
            whole
        };

        public enum Pitch
        {
            C,
            D,
            E,
            F,
            G,
            A,
            B
        }

        public enum Octave
        {
            low,
            middle,
            high
        }

        public Point position = new Point(0, 0);
        public Chromatic chromatic = Chromatic.Natural;
        public Timing timing = Timing.quarter;
        public Pitch pitch = Pitch.C;
        public Octave octave = Octave.middle;


    }
}
