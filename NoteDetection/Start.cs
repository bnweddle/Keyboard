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
            SheetMusic sheet = new SheetMusic();
            Piano piano = new Piano(Convert.ToInt32(BPM.Text), sheet);
            piano.Show();
            this.Hide();
            
        }
    }
}
