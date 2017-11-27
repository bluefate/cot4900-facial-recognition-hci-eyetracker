using Android.App;
using Android.OS;
using Android.Views;
using Java.IO;
using OpenCV.Android;
using OpenCV.Core;
using OpenCV.ImgProc;
using OpenCV.ObjDetect;
using Size = OpenCV.Core.Size;

namespace EyeTrackerWithOpenCV
{
	[Activity(Label = "Eye Tracker", MainLauncher = true, Icon = "@drawable/eye")]

	public class MainActivity : Activity, CameraBridgeViewBase.ICvCameraViewListener2
	{
		private static readonly Scalar FACE_RECT_COLOR = new Scalar(0, 255, 0, 255);


		private Mat colorMat;
		private Mat grayMat;
		public CascadeClassifier cascadeClassifier;
		public File cascadeFile;
		public DetectionBasedTracker detectionBasedTracker;
		private float relativeFaceSize = 0.2f;
		private int absoluteFaceSize = 0;
		private CameraBridgeViewBase mPreview;
		private LoaderCallback loaderCallback;
		






		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			Window.AddFlags(WindowManagerFlags.KeepScreenOn);

			SetContentView(Resource.Layout.Main);
			mPreview = FindViewById<CameraBridgeViewBase>(Resource.Id.surfaceView);
			mPreview.Visibility = ViewStates.Visible;
			mPreview.SetCvCameraViewListener2(this);
			mPreview.SetCameraIndex(CameraBridgeViewBase.CameraIdFront);
			//cameraView.EnableView();
			loaderCallback = new LoaderCallback(this, this, mPreview);
		}

		protected override void OnPause()
		{
			base.OnPause();
			if (mPreview != null)
				mPreview.DisableView();
		}

		protected override void OnResume()
		{
			base.OnResume();
			if (!OpenCVLoader.InitDebug())
				OpenCVLoader.InitAsync(OpenCVLoader.OpencvVersion300, this, loaderCallback);
			else
				loaderCallback.OnManagerConnected(LoaderCallbackInterface.Success);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			mPreview.DisableView();
		}









		public void OnCameraViewStarted(int width, int height)
		{
			grayMat = new Mat();
			colorMat = new Mat();
		}

		public void OnCameraViewStopped()
		{
			grayMat.Release();
			colorMat.Release();
		}

		public Mat OnCameraFrame(CameraBridgeViewBase.ICvCameraViewFrame inputFrame)
		{

			colorMat = inputFrame.Rgba();
			grayMat = inputFrame.Gray();

			if (absoluteFaceSize == 0)
			{
				int height = grayMat.Rows();
				if (Java.Lang.Math.Round(height * relativeFaceSize) > 0)
					absoluteFaceSize = Java.Lang.Math.Round(height * relativeFaceSize);
				detectionBasedTracker.setMinFaceSize(absoluteFaceSize);
			}

			MatOfRect faces = new MatOfRect();

			if (detectionBasedTracker != null)
				detectionBasedTracker.detect(grayMat, faces);

			Rect[] facesArray = faces.ToArray();
			for (int i = 0; i < facesArray.Length; i++)
				Imgproc.Rectangle(colorMat, facesArray[i].Tl(), facesArray[i].Br(), FACE_RECT_COLOR, 1); //CameraBridgeViewBase.HAIR_LINE

			return colorMat;
		}
	}

}