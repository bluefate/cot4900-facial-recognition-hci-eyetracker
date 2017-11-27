# COT 4900 — Facial Recognition for HCI

**Course:** COT 4900 — Facial Recognition for HCI  
**Term:** Fall 2017 (modernized to .NET 8)  
**School:** Florida Atlantic University (FAU)  
**Author:** John Hernandez

Webcam / device-camera prototypes for face and eye tracking in HCI experiments.

## Layout

| Path | Description |
|------|-------------|
| `EyeTracker.sln` | Active solution (Windows + Android) |
| `src/EyeTracker/` | **.NET 8** WinForms eye tracker (`net8.0-windows`, Emgu.CV) |
| `src/MobileEyeTracker/` | **.NET 8** Android eye/face tracker (`net8.0-android`, CameraX + ML Kit) |
| `Captures/` | Session videos, session logs, and related media |
| `archive/xamarin/` | Original Xamarin projects (Google Vision + OpenCV.Binding mobile) |
| `submitted/orginal export submited/` | Original multipart `EyeTrackerTests.7z` submission archive |

## What changed in the modernization

- Dropped third-party `OpenCV.Binding` (large native OpenCV Android binding).
- Windows app retargeted from .NET Framework to **`net8.0-windows`** with Emgu.CV 4.9 PackageReferences.
- Mobile OpenCV/Xamarin app replaced by a **.NET 8 Android** app using CameraX + ML Kit Face Detection (OpenCV Binding is no longer required).
- `EyeTrackWithGoogleVision` kept under `archive/xamarin/` (Google Mobile Vision is deprecated).

## Build

### Windows (`EyeTrackerOnWindows`)

1. Install [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) and Visual Studio 2022 (Windows workload).
2. Open `EyeTracker.sln` or:
   `dotnet build src/EyeTracker/EyeTrackerOnWindows.csproj -c Release`
3. Run on Windows with a webcam.

### Android (`EyeTrackerWithOpenCV`)

1. Install .NET 8 SDK + Android workload: `dotnet workload install android`
2. `dotnet build src/MobileEyeTracker/EyeTrackerWithOpenCV.csproj -c Release`
3. Deploy to an emulator or device (camera permission required).

## License

Emgu.CV / OpenCV components are under their upstream licenses (see `src/EyeTracker/License-LGPL.txt` where present). ML Kit / AndroidX packages use Google / AndroidX terms.
