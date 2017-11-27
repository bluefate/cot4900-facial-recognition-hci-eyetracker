using System.Windows.Forms;

namespace EyeTrackerOnWindows
{
	internal static class Program
	{
		[STAThread]
		private static void Main()
		{
			ApplicationConfiguration.Initialize();
			Application.Run(new EyeTrackerForm());
		}
	}
}
