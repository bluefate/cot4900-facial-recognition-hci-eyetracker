using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Bitmap;
using Emgu.CV.Structure;

namespace EyeTrackerOnWindows
{
	public partial class EyeTrackerForm : Form
	{
		private const int LineThickness = 1;

		private VideoCapture _capture;
		private ItemsDetected _faces;
		private ItemsDetected _eyes;
		private ItemsDetected _irises;
		private bool _cameraErrorShown;

		public EyeTrackerForm()
		{
			InitializeComponent();
			FormClosed += OnFormClosed;

			try
			{
				_capture = new VideoCapture();
				_faces = new ItemsDetected("haarcascade_frontalface_default.xml");
				_eyes = new ItemsDetected("haarcascade_eye.xml");
				_irises = new ItemsDetected();

				Application.Idle += ProcessFrame;
			}
			catch (Exception ex)
			{
				ShowCameraError(ex.Message);
			}
		}

		private void ProcessFrame(object sender, EventArgs e)
		{
			if (_capture == null)
				return;

			try
			{
				using (Mat frame = _capture.QueryFrame())
				{
					if (frame == null || frame.IsEmpty)
					{
						ShowCameraError("No image found. Camera may not be connected.");
						return;
					}

					_cameraErrorShown = false;
					ErrorMessage.Visible = false;

					_faces.Rectangles.Clear();
					_eyes.Rectangles.Clear();
					_irises.Circles.Clear();

					DetectFace.Detect(frame, _faces, _eyes, _irises);

					foreach (Rectangle face in _faces.Rectangles)
						CvInvoke.Rectangle(frame, face, new Bgr(Color.Red).MCvScalar, LineThickness);

					foreach (Rectangle eye in _eyes.Rectangles)
						CvInvoke.Rectangle(frame, eye, new Bgr(Color.Blue).MCvScalar, LineThickness);

					foreach (Circle iris in _irises.Circles)
						CvInvoke.Circle(frame, iris.Center, iris.Radius, new Bgr(Color.Green).MCvScalar, LineThickness);

					Image previous = pictureBox1.Image;
					pictureBox1.Image = frame.ToBitmap();
					previous?.Dispose();
				}
			}
			catch
			{
				// Keep the idle loop alive if a single frame fails.
			}
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Idle -= ProcessFrame;
			_capture?.Dispose();
			_capture = null;
			pictureBox1.Image?.Dispose();
			pictureBox1.Image = null;
		}

		private void ShowCameraError(string message)
		{
			ErrorMessage.Visible = true;
			ErrorMessage.Text = message;
			if (!_cameraErrorShown)
			{
				_cameraErrorShown = true;
				MessageBox.Show(message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
	}
}
