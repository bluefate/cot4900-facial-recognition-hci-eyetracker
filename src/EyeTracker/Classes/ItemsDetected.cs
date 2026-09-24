using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.Collections.Generic;
using System.Drawing;

namespace EyeTrackerOnWindows
{
	public class ItemsDetected
	{
		public List<Rectangle> Rectangles;
		public List<Circle> Circles;
		public string HaarCascade;
		private CascadeClassifier _Classifier;

		public CascadeClassifier Classifier
		{
			get
			{
				if (_Classifier is null)
				{
					if (string.IsNullOrEmpty(HaarCascade))
						throw new System.Exception("Missing HaarCascade / Could not load");
					_Classifier = new CascadeClassifier(HaarCascade);
				}
				return _Classifier;
			}
		}

		public ItemsDetected()
		{
			Rectangles = new List<Rectangle>();
			Circles = new List<Circle>();
		}

		public ItemsDetected(string haarcascadeFileName)
		{
			Rectangles = new List<Rectangle>();
			Circles = new List<Circle>();
			HaarCascade = haarcascadeFileName;
		}

		public Rectangle[] GetRactangles(IInputArray image, int minNeighbors)
		{
			const double scaleFactor = 1.2;
			Size size = new Size(10, 10);
			return Classifier.DetectMultiScale(image, scaleFactor, minNeighbors, size);
		}

		public CircleF[] GetCircles(
			IInputArray image,
			double resolution,
			double minDistance,
			double cannyThreshold,
			double accumulatorThreshold,
			int minRadius,
			int maxRadius)
		{
			using (var circles = new VectorOfVec3f())
			{
				CvInvoke.HoughCircles(
					image,
					circles,
					HoughTypes.Gradient,
					resolution,
					minDistance,
					cannyThreshold,
					accumulatorThreshold,
					minRadius,
					maxRadius);

				var result = new CircleF[circles.Size];
				for (int i = 0; i < circles.Size; i++)
				{
					var v = circles[i];
					result[i] = new CircleF(new PointF(v.X, v.Y), v.Z);
				}
				return result;
			}
		}
	}
}
