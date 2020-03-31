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
            this.pianoControl1 = new Sanford.Multimedia.Midi.UI.PianoControl();
            this.SuspendLayout();
            // 
            // pianoControl1
            // 
            this.pianoControl1.HighNoteID = 109;
            this.pianoControl1.Location = new System.Drawing.Point(13, 22);
            this.pianoControl1.LowNoteID = 21;
            this.pianoControl1.Margin = new System.Windows.Forms.Padding(4);
            this.pianoControl1.Name = "pianoControl1";
            this.pianoControl1.NoteOnColor = System.Drawing.Color.SkyBlue;
            this.pianoControl1.Size = new System.Drawing.Size(1662, 206);
            this.pianoControl1.TabIndex = 6;
            this.pianoControl1.Text = "pianoControl1";
            // 
            // Piano
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1661, 250);
            this.Controls.Add(this.pianoControl1);
            this.Name = "Piano";
            this.Text = "Piano";
            this.ResumeLayout(false);

        }

        #endregion

        private Sanford.Multimedia.Midi.UI.PianoControl pianoControl1;
    }
}