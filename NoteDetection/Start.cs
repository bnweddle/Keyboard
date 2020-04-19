using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteDetection
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }

        private void uxOpen_Click(object sender, EventArgs e)
        {
            Chromatic type = Chromatic.Natural;

            if(uxSharp.Checked == true)
                type = Chromatic.Sharp;
            if (uxFlats.Checked)
                type = Chromatic.Flat;

            SheetMusic sheet = new SheetMusic();
            Piano piano = new Piano(Convert.ToInt32(BPM.Text), type, sheet);
            System.Diagnostics.Debug.WriteLine($"{type } chromatic");
            piano.Show();
            this.Hide();
            
        }
    }
}
