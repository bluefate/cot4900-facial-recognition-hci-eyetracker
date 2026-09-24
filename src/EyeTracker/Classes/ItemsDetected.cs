using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
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
					if (string.IsNullOrEmpty(HaarCascade) == true)
						throw new System.Exception("Missing HaarCascade / Could not load");
					else
						_Classifier = new CascadeClassifier(HaarCascade);
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

		public Rectangle[] GetRactangles(IInputArray image, int minNeighbors )
		{
			int width = 10;
			int height = 10;
			double scaleFactor = 1.2;
			Size size = new Size(width, height);
			return  Classifier.DetectMultiScale(image, scaleFactor, minNeighbors, size);
		}
		public CircleF[] GetCircles(IInputArray image, double resolution, double minDistance, double cannyThreshold, double accumulatorThreshold, int minRadius , int maxRadius )
		{
			return CvInvoke.HoughCircles(image, HoughType.Gradient, resolution, minDistance, cannyThreshold, accumulatorThreshold, minRadius, maxRadius);
		}

	}
}
