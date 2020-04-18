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
        string timeSymbol;
        private int outDeviceID = 0;

        private OutputDevice outDevice;

        Stopwatch[] oldTimers = new Stopwatch[127]; 
        public Stopwatch[] currentTimers = new Stopwatch[127];
        public Queue<int> orderedNotes = new Queue<int>();
        public Dictionary<int, Stopwatch> currentNote;

        int BeatsPerMinute;
        NoteEstimator noteEstimator;
        Note note = new Note();
        SheetMusic sheetForm;

        public Piano(int bpm, SheetMusic form)
        {
            InitializeComponent();
            BeatsPerMinute = bpm;
            sheetForm = form;
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

        public void DisplayBPM()
        {
            double[] beats = BeatsPerMinute.ApproxTime();
            System.Diagnostics.Debug.WriteLine($"{ beats[0]} quart note");
            System.Diagnostics.Debug.WriteLine($"{ beats[1]} half note");
            System.Diagnostics.Debug.WriteLine($"{ beats[2]} eighteth note");
            System.Diagnostics.Debug.WriteLine($"{ beats[3]} sixteenth note");
            System.Diagnostics.Debug.WriteLine($"{ beats[4]} third quart note");
            System.Diagnostics.Debug.WriteLine($"{ beats[5]} whole note");
        }

        private void PianoControl_PianoKeyDown(object sender, PianoKeyEventArgs e)
        {
            oldTimers[e.NoteID].Start();
            orderedNotes.Enqueue(e.NoteID);
            System.Diagnostics.Debug.WriteLine($"{e.NoteID } pressed note");
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, e.NoteID, 127));
            Global.Played = true;

        }

        private void PianoControl_PianoKeyUp(object sender, PianoKeyEventArgs e)
        {   
            oldTimers[e.NoteID].Stop();       
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, e.NoteID, 0));
            currentTimers[e.NoteID] = oldTimers[e.NoteID];
            CurrentPlayedNote(currentTimers[e.NoteID], orderedNotes);
            System.Diagnostics.Debug.WriteLine($"{currentTimers[e.NoteID].ElapsedMilliseconds } current pressed milli time");
            DisplayBPM();
            long duration = currentTimers[e.NoteID].ElapsedMilliseconds.Round(100);
            Timing symbols = noteEstimator.GetNoteFromDuration(duration);
            Global.Symbol = note.GetNoteSymbol(symbols);
            sheetForm.UpdatePaint();
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

        public Dictionary<int, Stopwatch> CurrentPlayedNote(Stopwatch timers, Queue<int> ordered)
        {
            currentNote = new Dictionary<int, Stopwatch>();
            int noteID = ordered.Dequeue();
            currentNote.Add(noteID, timers);        
            return currentNote;
        }

        private void Piano_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
