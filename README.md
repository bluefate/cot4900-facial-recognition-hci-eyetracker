# COT 4900 — Facial Recognition for HCI

**Course:** COT 4900 — Facial Recognition for HCI  
**Term:** Fall 2017  
**School:** Florida Atlantic University (FAU)  
**Author:** John Hernandez

Windows / mobile prototypes that use a webcam (or device camera) plus OpenCV / Google Vision for face and eye tracking in HCI experiments.

## Layout

| Path | Description |
|------|-------------|
| `EyeTracker.sln` | Full solution (Windows, mobile, Google Vision, OpenCV binding) |
| `EyeTracker/` | WinForms eye tracker (`EyeTrackerOnWindows`) + `Captures/` session videos |
| `MobileEyeTracker/` | Xamarin / OpenCV Android eye tracker |
| `EyeTrackWithGoogleVision/` | Android Google Vision face tracker |
| `OpenCV.Binding/` | **Third-party support** — OpenCV Android binding (see `OpenCV.Binding/THIRD_PARTY.md`) |
| `submitted/orginal export submited/` | Original multipart `EyeTrackerTests.7z` submission archive |
| `Looking directly into the camera.docx` | Course notes / write-up |

The Windows UI captures frames, runs face/eye detection, and draws overlays. Session recordings from Oct 3 and Nov 7 2017 live under `EyeTracker/Captures/` with `SESSION.md` logs.

## Stack (as archived)

- C# / .NET Framework (Windows Forms Emgu.CV app)
- Xamarin.Android + OpenCV / Google Vision (mobile projects)
- Haar cascades for face and eye detection

## Build

1. Open `EyeTracker.sln` in Visual Studio (Windows; Android workload for mobile projects).
2. Restore NuGet packages.
3. Build/run `EyeTrackerOnWindows` with a webcam for the desktop prototype.

## Third-party support

- `OpenCV.Binding/` — vendor OpenCV binding used by `MobileEyeTracker` only; not authored coursework. Details: [`OpenCV.Binding/THIRD_PARTY.md`](OpenCV.Binding/THIRD_PARTY.md).
- Emgu.CV on Windows may be under LGPL; see `EyeTracker/License-LGPL.txt` where present.
