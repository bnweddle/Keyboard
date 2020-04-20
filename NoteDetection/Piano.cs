using System;
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

        int BeatsPerMinute;
        NoteEstimator noteEstimator;
        Note note = new Note();
        Keys keys = new Keys();
        SheetMusic sheetForm;

        private int offset = 75;
        private bool thirds = false;
        private bool chrom = false;
        private Chromatic chromatic = Chromatic.Natural;

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
            offset += 40;
        }

        private void PianoControl_PianoKeyUp(object sender, PianoKeyEventArgs e)
        {   
            oldTimers[e.NoteID].Stop();       
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, e.NoteID, 0));

            currentTimers[e.NoteID] = oldTimers[e.NoteID];
            long duration = currentTimers[e.NoteID].ElapsedMilliseconds.Round(100);

            Timing symbols = noteEstimator.GetNoteFromDuration(duration);
            if (symbols == Timing.ThirdHalf || symbols == Timing.ThirdQuart 
                || symbols == Timing.ThirdEigth || symbols == Timing.ThirdWhole || symbols == Timing.ThirdSixteen)
                thirds = true;
            else
                thirds = false;

            System.Diagnostics.Debug.WriteLine($"{symbols } timing");

            whitePressed = keys.WhiteKeyPress(e.NoteID, out chrom);
            blackPressed = keys.BlackKeyPress(e.NoteID, out chrom);
            keys.SetPositions(blackPressed, whitePressed, chromatic, chrom);

            System.Diagnostics.Debug.WriteLine($"{whitePressed } white note");
            System.Diagnostics.Debug.WriteLine($"{blackPressed } black note");

            sheetForm.SetChromatic(chrom, chromatic);

            // Globally shared variables
            Global.Symbol = note.GetNoteSymbol(symbols);
            Global.Chromatic = note.GetChromaticSymbol(chromatic);
            Timing time;
            Global.Image = note.GetImage(symbols, out time);
            Global.Time = time;

            sheetForm.UpdatePaint(offset, thirds, keys.GetPosition(e.NoteID));

            oldTimers[e.NoteID].Reset();
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
       
    }
}
