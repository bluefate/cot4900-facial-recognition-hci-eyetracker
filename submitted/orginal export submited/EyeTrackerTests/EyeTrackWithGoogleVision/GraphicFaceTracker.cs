using Android.Gms.Vision;
using Android.Gms.Vision.Faces;
using System;
using System.Threading.Tasks;

namespace EyeTrackWithGoogleVision
{

	class GraphicFaceTracker : Tracker //, CameraSource.IPictureCallback
	{
		private GraphicOverlay mOverlay;
		private FaceGraphic mFaceGraphic;
		private CameraSource mCameraSource = null;
		//private bool isProcessing = false;


		//public GraphicFaceTracker(GraphicOverlay overlay)
		//{
		//	mOverlay = overlay;
		//	mFaceGraphic = new FaceGraphic(overlay);
		//}


		public GraphicFaceTracker(GraphicOverlay overlay, CameraSource cameraSource = null)
		{
			mOverlay = overlay;
			mFaceGraphic = new FaceGraphic(overlay);
			mCameraSource = cameraSource;

		}

		public override void OnNewItem(int id, Java.Lang.Object item)
		{
			mFaceGraphic.SetId(id);
			//if (mCameraSource != null && !isProcessing)
			//	mCameraSource.TakePicture(null, this);
		}

		public override void OnUpdate(Detector.Detections detections, Java.Lang.Object item)
		{
			var face = item as Face;
			mOverlay.Add(mFaceGraphic);
			mFaceGraphic.UpdateFace(face);

		}

		public override void OnMissing(Detector.Detections detections)
		{
			mOverlay.Remove(mFaceGraphic);

		}

		public override void OnDone()
		{
			mOverlay.Remove(mFaceGraphic);

		}

		//public void OnPictureTaken(byte[] data)
		//{
		//	Task.Run(async () =>
		//	{
		//		try
		//		{
		//			isProcessing = true;

		//			Console.WriteLine("face detected: ");

		//		}

		//		finally
		//		{
		//			isProcessing = false;


		//		}

		//	});
		//}
	}

}