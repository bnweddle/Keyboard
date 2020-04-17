using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDetection
{
    public class NoteEstimator
    {
        private long[] thresholds;

        public NoteEstimator(int bpm)
        {
            thresholds = new long[7];
            int minute = 60000;

            thresholds[(int)Timing.Sixteenth] = (minute / bpm) / 3;
            thresholds[(int)Timing.Eighth] = (long)((minute / bpm) / 1.5);
            thresholds[(int)Timing.Quarter] = (long)((minute / bpm) * 1.5);
            thresholds[(int)Timing.ThirdQuart] = (long)((minute / bpm) * 2);
            thresholds[(int)Timing.Half] = (minute / bpm) * 3;
            thresholds[(int)Timing.ThirdHalf] = (long)((minute / bpm) * 3.5);
            thresholds[(int)Timing.Whole] = (long)((minute / bpm) * 4.5);

        }

        public Timing GetNoteFromDuration(long duration)
        {
            if(duration < thresholds[0])
            {
                return Timing.Sixteenth;
            }
            if(duration < thresholds[1])
            {
                return Timing.Eighth;
            }
            if(duration < thresholds[2])
            {
                return Timing.Quarter;
            }
            if (duration < thresholds[3])
            {
                return Timing.ThirdQuart;
            }
            if (duration < thresholds[4])
            {
                return Timing.Half;
            }
            if(duration < thresholds[5])
            {
                return Timing.ThirdHalf;
            }
            return Timing.Whole;
        }

    }
}
