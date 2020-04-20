using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDetection
{
    // Might move to Symbol or something
    public enum Hand
    {
        Right,
        Left
    }

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
        public string GetNoteSymbol(Timing symbol)
        {
            string unicode = "";
            switch(symbol)
            {
                case Timing.Sixteenth:
                    unicode = "\uD834\uDD61";
                    break;
                case Timing.Eighth:
                    unicode = "\uD834\uDD60";
                    break;
                case Timing.Quarter:
                    unicode = "\uD834\uDD5F";
                    break;
                case Timing.ThirdQuart:
                    unicode = "\uD834\uDD5F"; // Need a dot beside it
                    break;
                case Timing.Half:
                    unicode = "\uD834\uDD5E";
                    break;
                case Timing.ThirdHalf:
                    unicode = "\uD834\uDD5E"; // Need a dot beside it
                    break;
                case Timing.Whole:
                    unicode = "\uD834\uDD5D";
                    break;
            }

            return unicode;
        }

        /// THIS IS NOT USED AS OF YET
        public string GetChromaticSymbol(Chromatic symbol)
        {
            string unicode = "";
            switch (symbol)
            {
                case Chromatic.Natural:
                    unicode = "\u266E";
                    break;
                case Chromatic.Flat:
                    unicode = "\u266D";
                    break;
                case Chromatic.Sharp:
                    unicode = "\u266F";
                    break;
            }
            return unicode;
        }

        public Image GetImage(Timing symbol, out Timing time)
        {
            Image image = null;
            time = Timing.Quarter;
            switch (symbol)
            {
                case Timing.Sixteenth:
                    time = Timing.Sixteenth;
                    image = Properties.Resources.leftSixteen;
                    break;
                case Timing.Eighth:
                    time = Timing.Eighth;
                    image = Properties.Resources.leftEight;
                    break;
                case Timing.Quarter:
                    time = Timing.Quarter;
                    image = Properties.Resources.leftFour;
                    break;
                case Timing.ThirdQuart:
                    time = Timing.ThirdQuart;
                    image = Properties.Resources.leftFour; // Need a dot beside it
                    break;
                case Timing.Half:
                    time = Timing.Half;
                    image = Properties.Resources.leftHalf;
                    break;
                case Timing.ThirdHalf:
                    time = Timing.ThirdHalf;
                    image = Properties.Resources.leftHalf; // Need a dot beside it
                    break;
                case Timing.Whole:
                    // Maybe something about Centering the image where I want it? Ask Professor.
                    time = Timing.Whole;
                    image = Properties.Resources.whole; // size differently than others will mess things up!!
                    break;
            }
            return image;
        }
    }
}
