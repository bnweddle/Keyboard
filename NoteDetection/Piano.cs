using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sanford.Multimedia.Midi;
using Sanford.Multimedia.Midi.UI;

namespace NoteDetection
{
    public partial class Piano : Form
    {
        private bool playing = false;

        private int outDeviceID = 0;

        private OutputDevice outDevice;

        public Piano()
        {
            InitializeComponent();

            this.PianoControl.Size = this.Size;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (OutputDevice.DeviceCount == 0)
            {
                MessageBox.Show("No MIDI output devices available.", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);

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
                    MessageBox.Show(ex.Message, "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    Close();
                }
            }

            base.OnLoad(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            PianoControl.PressPianoKey(e.KeyCode);

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            PianoControl.ReleasePianoKey(e.KeyCode);

            base.OnKeyUp(e);
        }

        private void PianoControl_PianoKeyDown(object sender, PianoKeyEventArgs e)
        {
            if (playing)
            {
                return;
            }

            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, e.NoteID, 127));
        }

        private void PianoControl_PianoKeyUp(object sender, PianoKeyEventArgs e)
        {
            if (playing)
            {
                return;
            }

            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, e.NoteID, 0));
        }
    }
}
