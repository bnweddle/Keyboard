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
        protected bool HasLine = false;
        public const int NoteSpacing = 15;
        public const int ClefOffset = 50;

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

        public Point position = new Point(0, 0);
        public Chromatic chromatic = Chromatic.Natural;
        public Timing timing = Timing.Quarter;

        public void DrawNote(Timing time)
        { 
            switch(time)
            {
                case Timing.Sixteenth:
                    break;
                case Timing.Eighth:
                    break;
                case Timing.Quarter:
                    break;
                case Timing.Half:
                    break;
                case Timing.Third:
                    //Do this one last
                    break;
                case Timing.Whole:
                    break;         
            }
        }

    }
}
