using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EyeTrackerOnWindows
{
	public partial class EyeTrackerForm : Form
	{

		private VideoCapture capture;

		public EyeTrackerForm()
		{
			InitializeComponent();

			try
			{
				capture = new VideoCapture();
			Application.Idle += new EventHandler(ProcessFrame);

			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return;
			}
		}

		private void ProcessFrame(object sender, EventArgs e)
		{
			try
			{
				if (capture?.QueryFrame() is null)
				{
					NoCameraError();
					return;
				}

				Mat frame = capture.QueryFrame();

				ItemsDetected faces = new ItemsDetected("haarcascade_frontalface_default.xml");
				//ItemsDetected faces = new ItemsDetected("haarcascade_frontalface_alt.xml");
				ItemsDetected eyes = new ItemsDetected("haarcascade_eye.xml");
				//ItemsDetected eyes = new ItemsDetected("haarcascade_eye_tree_eyeglasses.xml");
				ItemsDetected irises = new ItemsDetected();

				DetectFace.Detect(frame, faces, eyes, irises);

				int lineThikness = 1;

				foreach (Rectangle face in faces.Rectangles)
					CvInvoke.Rectangle(frame, face, new Bgr(Color.Red).MCvScalar, lineThikness);
				foreach (Rectangle eye in eyes.Rectangles)
					CvInvoke.Rectangle(frame, eye, new Bgr(Color.Blue).MCvScalar, lineThikness);
				//foreach (Circle iris in irises.Circles)
				//	CvInvoke.Circle(frame, iris.Center, iris.Radius, new Bgr(Color.Green).MCvScalar, lineThikness);

				Image<Bgr, Byte> ImageFrame = frame.ToImage<Bgr, Byte>();
				pictureBox1.Image = ImageFrame.ToBitmap();
			}
			catch { return; }

		}

		private void ViewerClosed(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void NoCameraError()
		{
			ErrorMessage.Visible = true;
			ErrorMessage.Text = "No image found. Camera may not be conencted";

		}

	}

}
