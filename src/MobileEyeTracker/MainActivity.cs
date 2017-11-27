using Android;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Camera.Core;
using AndroidX.Camera.Lifecycle;
using AndroidX.Camera.View;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Java.Util.Concurrent;
using Xamarin.Google.MLKit.Vision.Common;
using Xamarin.Google.MLKit.Vision.Face;

namespace EyeTrackerWithOpenCV
{
	[Activity(
		Label = "@string/app_name",
		MainLauncher = true,
		Theme = "@style/AppTheme",
		ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : AppCompatActivity
	{
		private const int CameraPermissionRequest = 1001;

		private PreviewView? _previewView;
		private TextView? _statusText;
		private IFaceDetector? _faceDetector;
		private IExecutorService? _cameraExecutor;

		protected override void OnCreate(Bundle? savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.activity_main);

			_previewView = FindViewById<PreviewView>(Resource.Id.previewView);
			_statusText = FindViewById<TextView>(Resource.Id.statusText);
			_cameraExecutor = Executors.NewSingleThreadExecutor();

			_faceDetector = FaceDetection.GetClient(
				new FaceDetectorOptions.Builder()
					.SetPerformanceMode(FaceDetectorOptions.PerformanceModeFast)
					.SetLandmarkMode(FaceDetectorOptions.LandmarkModeAll)
					.SetClassificationMode(FaceDetectorOptions.ClassificationModeAll)
					.Build());

			if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) == Permission.Granted)
				StartCamera();
			else
				ActivityCompat.RequestPermissions(this, new[] { Manifest.Permission.Camera }, CameraPermissionRequest);
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			if (requestCode == CameraPermissionRequest &&
			    grantResults.Length > 0 &&
			    grantResults[0] == Permission.Granted)
			{
				StartCamera();
			}
			else if (_statusText != null)
			{
				_statusText.Text = "Camera permission is required.";
			}
		}

		private void StartCamera()
		{
			var cameraProviderFuture = ProcessCameraProvider.GetInstance(this);
			cameraProviderFuture.AddListener(new Java.Lang.Runnable(() =>
			{
				var cameraProvider = (ProcessCameraProvider)cameraProviderFuture.Get()!;
				var preview = new Preview.Builder().Build();
				preview.SetSurfaceProvider(_previewView!.SurfaceProvider);

				var analysis = new ImageAnalysis.Builder()
					.SetBackpressureStrategy(ImageAnalysis.StrategyKeepOnlyLatest)
					.Build();

				analysis.SetAnalyzer(_cameraExecutor!, new FaceAnalyzer(_faceDetector!, count =>
				{
					RunOnUiThread(() =>
					{
						if (_statusText != null)
							_statusText.Text = count == 0 ? "No face" : $"Faces: {count}";
					});
				}));

				cameraProvider.UnbindAll();
				cameraProvider.BindToLifecycle(
					this,
					CameraSelector.DefaultFrontCamera,
					preview,
					analysis);
			}), ContextCompat.GetMainExecutor(this));
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			_faceDetector?.Close();
			_cameraExecutor?.Shutdown();
		}

		private sealed class FaceAnalyzer : Java.Lang.Object, ImageAnalysis.IAnalyzer
		{
			private readonly IFaceDetector _detector;
			private readonly Action<int> _onResult;

			public FaceAnalyzer(IFaceDetector detector, Action<int> onResult)
			{
				_detector = detector;
				_onResult = onResult;
			}

			public void Analyze(IImageProxy imageProxy)
			{
				try
				{
					var mediaImage = imageProxy.Image;
					if (mediaImage == null)
						return;

					var input = InputImage.FromMediaImage(mediaImage, imageProxy.ImageInfo.RotationDegrees);
					var task = _detector.Process(input);
					task.AddOnSuccessListener(new SuccessListener(faces =>
					{
						_onResult(faces?.Size() ?? 0);
					}));
					task.AddOnCompleteListener(new CompleteListener(() => imageProxy.Close()));
				}
				catch
				{
					imageProxy.Close();
				}
			}
		}

		private sealed class SuccessListener : Java.Lang.Object, Android.Gms.Tasks.IOnSuccessListener
		{
			private readonly Action<Java.Util.IList?> _callback;
			public SuccessListener(Action<Java.Util.IList?> callback) => _callback = callback;
			public void OnSuccess(Java.Lang.Object? result) => _callback(result as Java.Util.IList);
		}

		private sealed class CompleteListener : Java.Lang.Object, Android.Gms.Tasks.IOnCompleteListener
		{
			private readonly Action _callback;
			public CompleteListener(Action callback) => _callback = callback;
			public void OnComplete(Android.Gms.Tasks.Task task) => _callback();
		}
	}
}
