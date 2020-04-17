using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDetection
{
    public enum Timing
    {
        Sixteenth = 0,   // "\uD834\uDD61"
        Eighth,          // "\uD834\uDD60"
        Quarter,         // "\uD834\uDD5F"
        ThirdQuart,      // Need a dot beside it
        Half,            // "\uD834\uDD5E"
        ThirdHalf,       // Need a dot beside it
        Whole            // "\uD834\uDD5D"
    };

    public enum Chromatic
    {
        Flat,      // "\u266D"
        Sharp,     // "\u266F"
        Natural    // "\u266E"
    };

    public class Note
    {
        private Pen notePen = new Pen(Color.Black, 2);
        private Brush noteBrush = Brushes.Black;

    }
}
