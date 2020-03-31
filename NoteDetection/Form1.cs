using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisioForge.MediaFramework.media;
using System.Speech;
using System.Speech.Synthesis;
using System.Media;
using TTimer = System.Timers.Timer;

namespace NoteDetection
{
    public partial class Form1 : Form {

        SoundPlayer pianoNotes;
        bool[] keys = new bool[128];
        bool[] oldKeys = new bool[128];
        TTimer timer = new TTimer(100);

        /// <summary>
        /// Used http://theremin.music.uiowa.edu/MISpiano.html
        /// for music notes
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            timer.AutoReset = true;
            timer.Elapsed += TimerTicking;
            timer.Start();
        }

        
        private void TimerTicking(object sender, EventArgs e)
        {
            //update key strutures 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //midi tag, start and stop
            keys[0] = true;
            pianoNotes = new SoundPlayer(Properties.Resources.A0);
            pianoNotes.Play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pianoNotes = new SoundPlayer(Properties.Resources.Bb0);
            pianoNotes.Play();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if(e.KeyCode == Keys.R)
            {
                keys[0] = true;
                button2.PerformClick();
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Q)
            {
                button1.PerformClick();
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            keys[0] = false;
        }
    }
}
