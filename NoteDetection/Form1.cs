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

namespace NoteDetection
{
    public partial class Form1 : Form {

        /// <summary>
        /// Used http://theremin.music.uiowa.edu/MISpiano.html
        /// for music notes
        /// </summary>

        public Form1()
        {
            InitializeComponent();
        }

        private void button21_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer a0 = new System.Media.SoundPlayer(NoteDetection.Properties.Resources.A0);
            a0.Play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer Bb0 = new System.Media.SoundPlayer(NoteDetection.Properties.Resources.Bb0);
            Bb0.Play();
        }
    }
}
