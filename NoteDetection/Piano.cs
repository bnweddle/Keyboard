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
        SheetMusic sheetForm;

        private int offset = 75;
        private bool thirds = false;
        private bool chrom = false;
        private Chromatic chromatic = Chromatic.Natural;

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
            double position = GetPosition(e.NoteID, chromatic);

            sheetForm.SetChromatic(chrom, chromatic);
            sheetForm.UpdatePaint(offset, thirds, position);

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

        public double GetPosition(int note, Chromatic type)
        {
            switch (note)
            {
                case 60: // C
                    chrom = false;
                    return 177.5;
                case 61: // C# or Db
                    chrom = true;
                    if (type == Chromatic.Sharp)
                        return 177.5;
                    else
                        return 170;
                case 62: // D
                    chrom = false;
                    return 170;
                case 63: // D# or Eb
                    chrom = true;
                    if (type == Chromatic.Sharp)
                        return 170;
                    else
                        return 163.5;
                case 64: // E
                    chrom = false;
                    return 163.5;
                case 65: // F
                    chrom = false;
                    return 155;
                case 66: // F# or Gb
                    chrom = true;
                    if (type == Chromatic.Sharp)
                        return 155;
                    else
                        return 147.5;
                case 67: // 
                    chrom = false;
                    return 147.5;
                case 68: // G# or Ab
                    chrom = true;
                    if (type == Chromatic.Sharp)
                        return 147.5;
                    else
                        return 140;
                case 69: // A
                    chrom = false;
                    return 140;
                case 70: // A# or Bb
                    chrom = true;
                    if (type == Chromatic.Sharp)
                        return 140.5;
                    else
                        return 133.5;
                case 71: // B
                    chrom = false;
                    return 133.5;
            }

            return 80;
        }
    }
}
