using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDetection
{
    /// <summary>
    /// Round to the nearest 1000s, 100s, 10s. Using to Approximate the time for the Beats Per Minute
    /// Found resource online at
    /// https://stackoverflow.com/questions/13153616/how-to-round-a-integer-to-the-close-hundred
    /// </summary>
    public static class MathExtensions
    {
        public static int Round(this int i, int nearest)
        {
            if (nearest <= 0 || nearest % 10 != 0)
                throw new ArgumentOutOfRangeException("nearest", "Must round to a positive multiple of 10");

            return (i + 5 * nearest / 10) / nearest * nearest;
        }
    }
}
