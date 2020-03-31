namespace NoteDetection
{
    partial class Piano
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PianoControl = new Sanford.Multimedia.Midi.UI.PianoControl();
            this.SuspendLayout();
            // 
            // PianoControl
            // 
            this.PianoControl.HighNoteID = 109;
            this.PianoControl.Location = new System.Drawing.Point(29, 3);
            this.PianoControl.LowNoteID = 21;
            this.PianoControl.Margin = new System.Windows.Forms.Padding(4);
            this.PianoControl.Name = "PianoControl";
            this.PianoControl.NoteOnColor = System.Drawing.Color.AliceBlue;
            this.PianoControl.Size = new System.Drawing.Size(1689, 201);
            this.PianoControl.TabIndex = 6;
            this.PianoControl.PianoKeyDown += new System.EventHandler<Sanford.Multimedia.Midi.UI.PianoKeyEventArgs>(this.PianoControl_PianoKeyDown);
            this.PianoControl.PianoKeyUp += new System.EventHandler<Sanford.Multimedia.Midi.UI.PianoKeyEventArgs>(this.PianoControl_PianoKeyUp);
            // 
            // Piano
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1924, 205);
            this.Controls.Add(this.PianoControl);
            this.Name = "Piano";
            this.Text = "Piano";
            this.ResumeLayout(false);

        }

        #endregion

        private Sanford.Multimedia.Midi.UI.PianoControl PianoControl;
    }
}