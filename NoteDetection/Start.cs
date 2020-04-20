using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            sheet.Location = new Point(500, 200);
            Piano piano = new Piano(Convert.ToInt32(BPM.Text), type, sheet);
            piano.Location = new Point(250, 0);
            System.Diagnostics.Debug.WriteLine($"{type } chromatic");
            piano.Show();
            this.Hide();
            
        }
    }
}
