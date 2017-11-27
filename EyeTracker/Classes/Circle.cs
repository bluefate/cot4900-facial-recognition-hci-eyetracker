using System.Drawing;
using Emgu.CV.Structure;

namespace EyeTrackerOnWindows
{
	public class Circle
	{
		public Point Center;
		public int Radius;

		public int Y { get { return Center.Y; } }
		public int X { get { return Center.X; } }


		public int minY { get { return Center.Y- Radius; } }
		public int minX { get { return Center.X - Radius; } }
		public int maxY { get { return Center.Y + Radius; } }
		public int maxX { get { return Center.X + Radius; } }


		public Circle(float x, float y, float radius)
		{
			Center.X = (int)x;
			Center.Y = (int)y;
			Radius = (int)radius;
		}
		public Circle(Point center, float radius)
		{
			Center = center;
			Radius = (int)radius;
		}

		public Circle(CircleF circlef)
		{
			Center.X = (int)circlef.Center.X;
			Center.Y = (int)circlef.Center.Y;
			Radius = (int)circlef.Radius;
		}

		internal void Offset(int x, int y)
		{
			Center.X += x;
			Center.Y += y;
		}
	}
}
