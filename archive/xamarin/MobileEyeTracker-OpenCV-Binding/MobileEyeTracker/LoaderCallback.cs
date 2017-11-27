using Android.Content;
using Java.IO;
using Java.Lang;
using OpenCV.Android;
using OpenCV.ObjDetect;

namespace EyeTrackerWithOpenCV
{
	public class LoaderCallback : BaseLoaderCallback
	{
		private readonly MainActivity activity;
		private readonly CameraBridgeViewBase cameraView;

		public LoaderCallback(MainActivity activity, Context context, CameraBridgeViewBase cameraView) : base(context)
		{
			this.activity = activity;
			this.cameraView = cameraView;
		}

		public override void OnManagerConnected(int status)
		{
			if (status == LoaderCallbackInterface.Success)
			{
				JavaSystem.LoadLibrary("detection_based_tracker");
				try
				{
					File cascadeDir;

					using (var istr = activity.Resources.OpenRawResource(Resource.Raw.haarcascade_frontalface_default))
					{
						cascadeDir = activity.GetDir("cascasde", FileCreationMode.Private);
						activity.cascadeFile = new File(cascadeDir, "haarcascade_frontalface_default.xml");
						using (FileOutputStream os = new FileOutputStream(activity.cascadeFile))
						{
							int byteRead;
							while ((byteRead = istr.ReadByte()) != -1)
							{
								os.Write(byteRead);
							}
						}
					}

					activity.cascadeClassifier = new CascadeClassifier(activity.cascadeFile.AbsolutePath);
					if (activity.cascadeClassifier.Empty())
					{
						activity.cascadeClassifier = null;
					}

					activity.detectionBasedTracker = new DetectionBasedTracker(activity.cascadeFile.AbsolutePath, 0);

					cascadeDir.Delete();

				}
				catch (IOException e)
				{
					e.PrintStackTrace();
				}
				cameraView.EnableView();
			}
			else
			{
				base.OnManagerConnected(status);
			}
		}
	}
}
