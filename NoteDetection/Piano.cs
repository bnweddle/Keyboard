/* Author: Bethany Weddle
 * Class: Piano.cs
 * Used PianoControl from MidiKit on Github with Free Software License
 * 
 * TO DO: 
 * 1. Change how Keyboard Keys work
 * 2. Think about Rests (time between pressed notes)  - Get Idea from Professor
 * 3. Implement Measures checking with Time Signature - Get Idea from Professor
 * 4. Fix Offset when more than 1 note is pressed     - Ask Professor
 * 5. Fix Sharp/Flat position to be constant 
 * 6. Fix LIMIATION in Keys.cs                        - Show Professor
 * 
 * */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Sanford.Multimedia.Midi;
using Sanford.Multimedia.Midi.UI;

namespace NoteDetection
{
    /// <summary>
    /// Piano class for displaying the Keyboard
    /// </summary>
    public partial class Piano : Form
    {
        private int outDeviceID = 0;

        private OutputDevice outDevice;

        // Old and New timers for Note duration
        Stopwatch[] oldTimers = new Stopwatch[127]; 
        Stopwatch[] currentTimers = new Stopwatch[127];
        
        int BeatsPerMinute;

        //Used to keep track of how much notes are pressed at once
        int number = 0;
        int shiftX = 0;
        int oldNote = 0;
        int newNote = 0;

        // Note objects
        NoteEstimator noteEstimator;
        Note note = new Note();
        Keys keys = new Keys();
        SheetMusic sheetForm;

        // private variables passed between Forms
        private int offset = 75;
        private bool thirds = false;
        private bool chrom = false;
        private Chromatic chromatic = Chromatic.Natural;

        // index/number of Key pressed
        int whitePressed;
        int blackPressed;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bpm">Beats Per Minute</param>
        /// <param name="type">Chromatic Type</param>
        /// <param name="form">Sheet Music</param>
        public Piano(int bpm, Chromatic type, SheetMusic form)
        {
            InitializeComponent();
            BeatsPerMinute = bpm;
            sheetForm = form;
            chromatic = type; 
            noteEstimator = new NoteEstimator(bpm);
            sheetForm.Show();

            // Initializes the Stopwatch Timers
            for (int i = 0; i < oldTimers.Length; i++)
            {
                oldTimers[i] = new Stopwatch();
                currentTimers[i] = new Stopwatch();
            }
            this.pianoControl.Size = this.Size;
        }

        // Loads the Device for the Piano Control
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

        // For when the Keyboard Note or Mouse  Note is pressed
        private void PianoControl_PianoKeyDown(object sender, PianoKeyEventArgs e)
        {
            oldTimers[e.NoteID].Start();
            oldNote = e.NoteID;

            whitePressed = keys.WhiteKeyPress(e.NoteID, out chrom);
            blackPressed = keys.BlackKeyPress(e.NoteID, out chrom);

            System.Diagnostics.Debug.WriteLine($"{e.NoteID - 21} noteID");
            System.Diagnostics.Debug.WriteLine($"{oldNote - 21} oldNote");
            System.Diagnostics.Debug.WriteLine($"{newNote - 21} newNote");

            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, e.NoteID, 127));
            offset += 45;

            System.Diagnostics.Debug.WriteLine($"{blackPressed } black note index");
            if (number > 0)
            {
                chromatic = keys.ChangePosition(oldNote, newNote, blackPressed, out shiftX, chromatic);
            }

            newNote = oldNote;
        }

        // For when the Keyboard Note or Mouse Note is released
        private void PianoControl_PianoKeyUp(object sender, PianoKeyEventArgs e)
        {
            oldTimers[e.NoteID].Stop();
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, e.NoteID, 0));
            currentTimers[e.NoteID] = oldTimers[e.NoteID];
            long duration = currentTimers[e.NoteID].ElapsedMilliseconds.Round(100);

            // Checking for Thirds
            Timing symbols = noteEstimator.GetNoteFromDuration(duration);
            if (symbols == Timing.ThirdHalf || symbols == Timing.ThirdQuart 
                || symbols == Timing.ThirdEigth || symbols == Timing.ThirdWhole || symbols == Timing.ThirdSixteen)
                thirds = true;
            else
                thirds = false;

            System.Diagnostics.Debug.WriteLine($"{symbols } timing");
            // Setting the Positions
            keys.SetPositions(blackPressed, whitePressed, chromatic, chrom);

            // System.Diagnostics.Debug.WriteLine($"{whitePressed } white note");
            // System.Diagnostics.Debug.WriteLine($"{blackPressed } black note");

            sheetForm.SetChromatic(chrom, chromatic);

            // Globally shared variables
            Global.Symbol = note.GetNoteSymbol(symbols);
            Global.Chromatic = note.GetChromaticSymbol(chromatic);
            Timing time;
            Global.Image = note.GetImage(symbols, out time);
            Global.Time = time;

            sheetForm.UpdatePaint(offset, shiftX, thirds, keys.GetPosition(e.NoteID));

            oldTimers[e.NoteID].Reset();
        }

        // When the user presses a Key
        private void pianoControl_KeyDown(object sender, KeyEventArgs e)
        {
            pianoControl.PressPianoKey(e.KeyCode);
            number++;
            base.OnKeyDown(e);

        }

        // When the user releases a Key 
        private void pianoControl_KeyUp(object sender, KeyEventArgs e)
        {
            // Fixes the issue of double/triple notes increasing the offset too much
            if (number >= 2)
                offset -= (number - 1) * 45;
            pianoControl.ReleasePianoKey(e.KeyCode);
            base.OnKeyUp(e);
            number = 0;
        }

        // For Closing the Forms
        private void Piano_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(1);
        }
       
    }
}
