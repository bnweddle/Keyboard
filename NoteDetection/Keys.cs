using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDetection
{
    public class Keys
    {
        private int[] blackKeys = new int[36]
        {
            1, 4, 6, 9, 11, 13, 16, 18, 21, 23, 25, 28, 30, 33, 35, 37, 40, 42, 45, 47, 49,
            52, 54, 57, 59, 61, 64, 66, 69, 71, 73, 76, 78, 81, 83, 85
        };

        private int[] whiteKeys = new int[52]
        {
            0, 2, 3, 5, 7, 8, 10, 12, 14, 15, 17, 19, 20, 22, 24, 26, 27, 29, 31, 32, 34, 36,
            38, 39, 41, 43, 44, 46, 48, 50, 51, 53, 55, 56, 58, 60, 62, 63, 65, 67, 68, 70, 72,
            74, 75, 77, 79, 80, 82, 84, 86, 87
        };

        private double[] positions = new double[88];

        public int BlackKeyPress(int noteID, out bool chrom)
        {
            // Need to check if any black of the black keys values equal noteID
            for (int i = 0; i < blackKeys.Length; i++)
            {
                if (blackKeys[i] == noteID - 21)
                {
                    chrom = true;
                    return blackKeys[i];
                }
                else
                {
                    chrom = false;
                }
            }

            chrom = false;
            return -1;
        }

        public int WhiteKeyPress(int noteID, out bool chrom)
        {
            for (int i = 0; i < whiteKeys.Length; i++)
            {
                if (whiteKeys[i] == noteID - 21)
                {
                    chrom = false;
                    return i;
                }
            }

            chrom = true;
            return -1;
        }

        public void SetPositions(int blackPressed, int whitePressed, Chromatic type, bool chrom)
        {
            
            double index = 0;
            for (int i = 0; i < positions.Length; i++)
            {
                // start position will need to change for the left hand 
                positions[i] = 410 - index;

                if (whitePressed != -1)
                {
                    index = 0;
                    index = whitePressed * 7.5;

                    if (positions[i] <= 237.5) 
                    {
                        positions[i] -= 60; // move to the g clef
                        Global.Handy = Hand.Right;
                    }
                    else
                    {
                        Global.Handy = Hand.Left;
                    }
                }

                if (chrom)
                {
                    // LIMITATION!!! currently, must not click sharps/flats notes first.
                    if (type == Chromatic.Sharp)
                    {
                        // only works if you hit note after it
                        // Need to come up with index  calculation for sharps
                        positions[i] = positions[blackPressed - 1]; // sharps, same as before note
                        i++;
                    }
                    else if (type == Chromatic.Flat)
                    {
                        // only works if you hit note before it
                        // Need to come up with index calculation for flats
                        positions[i] = positions[blackPressed + 1]; // flats, same as after note
                        i++;
                    }
                }

            }
        }

        public double GetPosition(int noteID)
        {
            return positions[noteID - 21];
        }
    }
}
