﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sanford.Multimedia.Midi;
using Sanford.Multimedia.Midi.UI;

namespace NoteDetection
{
    public partial class Piano : Form
    {

        private int outDeviceID = 0;

        private OutputDevice outDevice;

        Stopwatch[] oldTimers = new Stopwatch[127]; 
        Stopwatch[] currentTimers = new Stopwatch[127];

        int[] blackKeys = new int[36]
        {
            1, 4, 6, 9, 11, 13, 16, 18, 21, 23, 25, 28, 30, 33, 35, 37, 40, 42, 45, 47, 49,
            52, 54, 57, 59, 61, 64, 66, 69, 71, 73, 76, 78, 81, 83, 85
        };

        // Probably won't need
        int[] whiteKeys = new int[52]
        {
            0, 2, 3, 5, 7, 8, 10, 12, 14, 15, 17, 19, 20, 22, 24, 26, 27, 29, 31, 32, 34, 36, 
            38, 39, 41, 43, 44, 46, 48, 50, 51, 53, 55, 56, 58, 60, 62, 63, 65, 67, 68, 70, 72,
            74, 75, 77, 79, 80, 82, 84, 86, 87       
        };

        int BeatsPerMinute;
        NoteEstimator noteEstimator;
        Note note = new Note();
        SheetMusic sheetForm;

        private int offset = 75;
        private bool thirds = false;
        private bool chrom = false;
        private Chromatic chromatic = Chromatic.Natural;

        double[] positions = new double[88];
        int whitePressed;
        int blackPressed;

        public Piano(int bpm, Chromatic type, SheetMusic form)
        {
            InitializeComponent();
            BeatsPerMinute = bpm;
            sheetForm = form;
            chromatic = type; 
            noteEstimator = new NoteEstimator(bpm);
            sheetForm.Show();

            for (int i = 0; i < oldTimers.Length; i++)
            {
                oldTimers[i] = new Stopwatch();
                currentTimers[i] = new Stopwatch();
            }
            this.pianoControl.Size = this.Size;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (OutputDevice.DeviceCount == 0)
            {
                Close();
            }
            else
            {
                try
                {
                    outDevice = new OutputDevice(outDeviceID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!");
                    Close();
                }
            }

            base.OnLoad(e);
        }

        private void PianoControl_PianoKeyDown(object sender, PianoKeyEventArgs e)
        {
            oldTimers[e.NoteID].Start();
            System.Diagnostics.Debug.WriteLine($"{e.NoteID} noteID");
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, e.NoteID, 127));
            Global.Played = true;
            offset += 40;

        }

        private void PianoControl_PianoKeyUp(object sender, PianoKeyEventArgs e)
        {   
            oldTimers[e.NoteID].Stop();       
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, e.NoteID, 0));

            currentTimers[e.NoteID] = oldTimers[e.NoteID];
            long duration = currentTimers[e.NoteID].ElapsedMilliseconds.Round(100);

            Timing symbols = noteEstimator.GetNoteFromDuration(duration);
            if (symbols == Timing.ThirdHalf || symbols == Timing.ThirdQuart)
                thirds = true;
            else
                thirds = false;

            System.Diagnostics.Debug.WriteLine($"{symbols } timing");

            Global.Symbol = note.GetNoteSymbol(symbols);

            whitePressed = WhiteKeyPress(e.NoteID);
            blackPressed = BlackKeyPress(e.NoteID);
            SetPositions(blackPressed, whitePressed, chromatic);

            System.Diagnostics.Debug.WriteLine($"{whitePressed } white note");
            System.Diagnostics.Debug.WriteLine($"{blackPressed } black note");

            sheetForm.SetChromatic(chrom, chromatic);

            sheetForm.UpdatePaint(offset, thirds, GetPosition(positions, e.NoteID));

            oldTimers[e.NoteID].Reset();
            Global.Played = false;
        }

        private void pianoControl_KeyDown(object sender, KeyEventArgs e)
        {
            pianoControl.PressPianoKey(e.KeyCode);
            base.OnKeyDown(e);
        }

        private void pianoControl_KeyUp(object sender, KeyEventArgs e)
        {
            pianoControl.ReleasePianoKey(e.KeyCode);
            base.OnKeyUp(e);
        }

        private void Piano_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(1);
        }
        
        public int BlackKeyPress(int noteID)
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

            return -1;
        }

        public int WhiteKeyPress(int noteID)
        {
            for(int i = 0; i < whiteKeys.Length; i++)
            {
                if (whiteKeys[i] == noteID - 21)
                {
                    chrom = false;
                    return i;
                }
            }

            return -1;
        }

        public void SetPositions(int blackPressed, int whitePressed, Chromatic type)
        {
            double index = 0;
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = 410 - index;

                if (whitePressed != -1)
                {
                    index = 0;
                    index = whitePressed * 7.5;

                    if(positions[i] <= 237.5)
                    {
                        positions[i] -= 60; // move to the g clef
                    }
                }

                if (chrom)
                {
                    if (type == Chromatic.Sharp)
                    {
                        positions[i] = positions[blackPressed - 1]; // sharps, same as before note
                        i++;
                    }
                    else if (type == Chromatic.Flat)
                    {
                        positions[i] = positions[blackPressed + 1]; // flats, same as after note
                        i++;
                    }
                }
                    
            }
        }

        public double GetPosition(double[] positions, int noteID)
        {
            return positions[noteID - 21];
        }
    }
}
