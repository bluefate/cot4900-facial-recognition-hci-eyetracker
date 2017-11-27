using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace EyeTrackerOnWindows
{

	public static class DetectFace
	{
		public static void Detect(Mat image, ItemsDetected faces, ItemsDetected eyes, ItemsDetected irises)
		{



			Mat grayMat = new Mat();
			CvInvoke.CvtColor(image, grayMat, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
			CvInvoke.EqualizeHist(grayMat, grayMat);

			faces.Rectangles.AddRange(faces.GetRactangles(grayMat, 15));

			foreach (Rectangle face in faces.Rectangles)
			{
				using (Mat faceRegion = new Mat(grayMat, face))
				{
					foreach (Rectangle eye in eyes.GetRactangles(faceRegion, 30))
					{
						Rectangle eyeRect = eye;
						eyeRect.Offset(face.X, face.Y);

						int eyePosition = face.Y + eye.Y;
						if (eyePosition < face.Y + face.Height / 2)
						{
							eyes.Rectangles.Add(eyeRect);
							using (Mat eyeRegion = new Mat(grayMat, eye))
							{
								//valueTracked = (int)(face.Width * 0.25);
								int faceWidth = face.Width;

								double cannyThreshold = 130.0;
								double circleAccumulatorThreshold = 10;
								int minRadius = faceWidth;// 5;
								int maxRadius = faceWidth + 10;// 15;
								double minDistince = 10.0;
								double accccumulatorResolution = 4.0;

								foreach (CircleF circle in eyes.GetCircles(eyeRegion,
																			accccumulatorResolution,
																			minDistince,
																			cannyThreshold,
																			circleAccumulatorThreshold,
																			minRadius,
																			maxRadius))
								{
									Circle eyeF = new Circle(eye.X, eye.Y, (eye.Size.Width + eye.Size.Height) / 6);
									eyeF.Offset(face.X + eye.Size.Width / 2, face.Y + eye.Size.Height / 2);

									Circle iris = new Circle(circle);
									iris.Offset(face.X + eye.X, face.Y + eye.Y);


									if (iris.minX > eyeF.minX &&
										iris.maxX < eyeF.maxX &&
										iris.minY > eyeF.minY &&
										iris.maxY < eyeF.maxY)
									{
										irises.Circles.Add(iris);
									}



								}
							}
						}
					}
				}

			}
		}


	}
}
