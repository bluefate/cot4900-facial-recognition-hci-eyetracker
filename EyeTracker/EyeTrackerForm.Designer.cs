namespace EyeTracker
{
	partial class EyeTrackerForm
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
			this.ErrorMessage = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.trackBar = new System.Windows.Forms.TrackBar();
			this.trackerLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
			this.SuspendLayout();
			// 
			// ErrorMessage
			// 
			this.ErrorMessage.AutoSize = true;
			this.ErrorMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ErrorMessage.Location = new System.Drawing.Point(0, 0);
			this.ErrorMessage.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
			this.ErrorMessage.Name = "ErrorMessage";
			this.ErrorMessage.Size = new System.Drawing.Size(0, 25);
			this.ErrorMessage.TabIndex = 0;
			this.ErrorMessage.Visible = false;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Margin = new System.Windows.Forms.Padding(6);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(1588, 1193);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// trackBar
			// 
			this.trackBar.Location = new System.Drawing.Point(1496, 12);
			this.trackBar.Maximum = 100;
			this.trackBar.Minimum = -100;
			this.trackBar.Name = "trackBar";
			this.trackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackBar.Size = new System.Drawing.Size(80, 1181);
			this.trackBar.TabIndex = 2;
			this.trackBar.Value = 5;
			this.trackBar.Scroll += new System.EventHandler(this.trackBar1_Scroll);
			// 
			// trackerLabel
			// 
			this.trackerLabel.AutoSize = true;
			this.trackerLabel.Location = new System.Drawing.Point(1368, 12);
			this.trackerLabel.Name = "trackerLabel";
			this.trackerLabel.Size = new System.Drawing.Size(23, 25);
			this.trackerLabel.TabIndex = 3;
			this.trackerLabel.Text = "0";
			// 
			// EyeTrackerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1588, 1193);
			this.Controls.Add(this.trackerLabel);
			this.Controls.Add(this.trackBar);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.ErrorMessage);
			this.Margin = new System.Windows.Forms.Padding(6);
			this.Name = "EyeTrackerForm";
			this.Text = "Eye Tracker Form";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label ErrorMessage;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.TrackBar trackBar;
		private System.Windows.Forms.Label trackerLabel;
	}
}

