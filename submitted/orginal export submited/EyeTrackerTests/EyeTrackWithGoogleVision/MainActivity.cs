using Android;
using Android.App;
using Android.Content.PM;
using Android.Gms.Common;
using Android.Gms.Vision;
using Android.Gms.Vision.Faces;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Util;
using static Android.Gms.Vision.MultiProcessor;

namespace EyeTrackWithGoogleVision
{
	[Activity(Label = "EyeTrackWithGoogleVision", MainLauncher = true, Icon = "@drawable/eye", Theme = "@style/Theme.AppCompat.NoActionBar", ScreenOrientation = ScreenOrientation.FullSensor)]
	public class MainActivity : AppCompatActivity, IFactory
	{
		private static readonly string TAG = "FaceTracker";

		private CameraSource mCameraSource = null;

		private CameraSourcePreview mPreview;
		private GraphicOverlay mGraphicOverlay;


		private static readonly int RC_HANDLE_GMS = 9001;
		private static readonly int RC_HANDLE_CAMERA_PERM = 2;







		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			mPreview = FindViewById<CameraSourcePreview>(Resource.Id.preview);
			mGraphicOverlay = FindViewById<GraphicOverlay>(Resource.Id.faceOverlay);

			if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.Camera) == Permission.Granted)
				CreateCameraSource();
			else
				RequestCameraPermission();
		}

		protected override void OnResume()
		{
			base.OnResume();
			StartCameraSource();
		}

		protected override void OnPause()
		{
			base.OnPause();
			mPreview.Stop();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (mCameraSource != null)
			{
				mCameraSource.Release();
			}
		}








		private void RequestCameraPermission()
		{
			Log.Warn(TAG, "Camera permission is not granted. Requesting permission");

			var permissions = new string[] { Manifest.Permission.Camera };

			if (!ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.Camera))
			{
				ActivityCompat.RequestPermissions(this, permissions, RC_HANDLE_CAMERA_PERM);
				return;
			}

			Snackbar.Make(mGraphicOverlay, Resource.String.permission_camera_rationale,
					Snackbar.LengthIndefinite)
					.SetAction(Resource.String.ok, (o) => { ActivityCompat.RequestPermissions(this, permissions, RC_HANDLE_CAMERA_PERM); })
					.Show();
		}

		/**
		 * Creates and starts the camera.  Note that this uses a higher resolution in comparison
		 * to other detection examples to enable the barcode detector to detect small barcodes
		 * at long distances.
		 */
		private void CreateCameraSource()
		{

			var context = Application.Context;

			FaceDetector detector = new FaceDetector.Builder(context)
					.SetTrackingEnabled(true)
					.SetClassificationType(ClassificationType.All)
					.Build();

			detector.SetProcessor(new MultiProcessor.Builder(this).Build());

			if (!detector.IsOperational)
			{
				// Note: The first time that an app using face API is installed on a device, GMS will
				// download a native library to the device in order to do detection.  Usually this
				// completes before the app is run for the first time.  But if that download has not yet
				// completed, then the above call will not detect any faces.
				//
				// isOperational() can be used to check if the required native library is currently
				// available.  The detector will automatically become operational once the library
				// download completes on device.
				Log.Warn(TAG, "Face detector dependencies are not yet available.");
			}

			mCameraSource = new CameraSource.Builder(context, detector)
					.SetRequestedPreviewSize(640, 480)
					.SetFacing(CameraFacing.Front)
					.SetRequestedFps(15.0f)
					.Build();


		}

		/**
         * Starts or restarts the camera source, if it exists.  If the camera source doesn't exist yet
         * (e.g., because onResume was called before the camera source was created), this will be called
         * again when the camera source is created.
         */
		private void StartCameraSource()
		{

			// check that the device has play services available.
			int code = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this.ApplicationContext);

			if (code != ConnectionResult.Success)
			{
				Dialog dlg = GoogleApiAvailability.Instance.GetErrorDialog(this, code, RC_HANDLE_GMS);
				dlg.Show();
			}

			if (mCameraSource != null)
			{
				try
				{
					mPreview.Start(mCameraSource, mGraphicOverlay);
				}
				catch (System.Exception e)
				{
					Log.Error(TAG, "Unable to start camera source.", e);
					mCameraSource.Release();
					mCameraSource = null;
				}
			}
		}
		public Tracker Create(Java.Lang.Object item)
		{
			return new GraphicFaceTracker(mGraphicOverlay, mCameraSource);
		}


		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
		{
			if (requestCode != RC_HANDLE_CAMERA_PERM)
			{
				Log.Debug(TAG, "Got unexpected permission result: " + requestCode);
				base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
				return;
			}

			if (grantResults.Length != 0 && grantResults[0] == Permission.Granted)
			{
				Log.Debug(TAG, "Camera permission granted - initialize the camera source");
				CreateCameraSource();
				return;
			}

			Log.Error(TAG, "Permission not granted: results len = " + grantResults.Length + " Result code = " + (grantResults.Length > 0 ? grantResults[0].ToString() : "(empty)"));

			var builder = new Android.Support.V7.App.AlertDialog.Builder(this);

			builder.SetTitle("EyeTrackWithGoogleVision")
					.SetMessage(Resource.String.no_camera_permission)
					.SetPositiveButton(Resource.String.ok, (o, e) => Finish())
					.Show();

		}
	}



}


