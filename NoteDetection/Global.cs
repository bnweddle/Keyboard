using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDetection
{
    public static class Global
    {

        public static string Symbol { get; set; }

        public static Image GetImage { get; set; }

        /// <summary>
        /// Round to the nearest 1000s, 100s, 10s. Using to Approximate the time for the Beats Per Minute
        /// Found resource online at
        /// https://stackoverflow.com/questions/13153616/how-to-round-a-integer-to-the-close-hundred
        /// </summary>
        /// <param name="i">the value to round</param>
        /// <param name="nearest">the multiple of ten to round closest to</param>
        /// <returns>the rounded new value</returns>
        public static long Round(this long value, int nearest)
        {
            if (nearest <= 0 || nearest % 10 != 0)
                throw new ArgumentOutOfRangeException("nearest", "Must round to a positive multiple of 10");

            return (value + 5 * nearest / 10) / nearest * nearest;
        }

    }
}
