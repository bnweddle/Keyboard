using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDetection
{
    public static class MathExtensions
    {
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

        /// <summary>
        /// Approximates the time that the note is pressed based on the beats per minute
        /// </summary>
        /// <param name="bpm">beats per minute indicated by the user in the Start form</param>
        /// <returns>an int[] holding the approximations for each type of Timing Note</returns>
        public static double[] ApproxTime(this int bpm)
        {
            double[] noteApprox = new double[6];
            int minute = 60000;
            noteApprox[0] = minute / bpm;
            double quartNote = noteApprox[0];    // Quart Note
            noteApprox[1] = quartNote * 2;       // Half Note
            noteApprox[2] = quartNote / 2;       // Eighteth Note
            noteApprox[3] = quartNote / 4;       // Sixteenth Note
            noteApprox[4] = quartNote * 1.5;     // Third Note
            noteApprox[5] = quartNote * 4;       // Whole Note
            return noteApprox;
        }

    }
}
