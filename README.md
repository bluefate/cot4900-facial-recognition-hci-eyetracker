# COT 4900 — Facial Recognition for HCI (Eye Tracker)

**Course:** COT 4900 — Facial Recognition for HCI  
**Term:** Fall 2017  
**School:** Florida Atlantic University (FAU)  
**Author:** John Hernandez  
**Repository:** [bluefate/cot4900-facial-recognition-hci-eyetracker](https://github.com/bluefate/cot4900-facial-recognition-hci-eyetracker)

Prototypes from an HCI coursework project focused on **real-time eye tracking**: locate a face in a camera frame, find the eyes within that face, and estimate iris position so gaze-related interaction can be studied.

## Purpose — eye tracking for HCI

The goal is not generic face ID. The Windows and mobile apps explore **facial-feature pipelines that support eye tracking** in human–computer interaction experiments:

1. **Face** — Haar-cascade detection of the frontal face region  
2. **Eyes** — cascades constrained to the upper half of each face  
3. **Iris** — Hough circle estimates inside each eye ROI  

Live video overlays make the pipeline visible for demos and capture sessions:

| Overlay | Color | Meaning |
|---------|-------|---------|
| Face box | Red | Detected face region |
| Eye boxes | Blue | Detected eye regions |
| Iris circles | Green | Estimated iris / pupil area |

Session recordings and stills under `Captures/` document how the tracker behaved during Fall 2017 labs.

## Historical screenshots (Fall 2017)

Stills from original **Eye Tracker Form** session recordings.

**3 October 2017**

<img src="Captures/stills/2017-10-03-eye-tracker-form.jpg" alt="Eye Tracker Form — 3 Oct 2017" width="560" />

**7 November 2017**

<img src="Captures/stills/2017-11-07-eye-tracker-form.jpg" alt="Eye Tracker Form — 7 Nov 2017" width="560" />

## What’s in this repo

| Path | Description |
|------|-------------|
| `EyeTracker.sln` | Full solution (projects under `src/`) |
| `src/EyeTracker/` | Primary WinForms eye tracker (`EyeTrackerOnWindows`) |
| `src/MobileEyeTracker/` | Xamarin / OpenCV Android eye tracker (archived stack) |
| `src/EyeTrackWithGoogleVision/` | Android Google Vision face tracker (archived stack) |
| `src/OpenCV.Binding/` | **Third-party** OpenCV Android binding (see `THIRD_PARTY.md`) |
| `Captures/` | Session videos, logs, and stills |
| `submitted/orginal export submited/` | Original multipart `EyeTrackerTests.7z` submission archive |

## Current Windows stack

The desktop eye tracker is the maintained build target. One SDK-style project multi-targets:

| Target | TFM | Notes |
|--------|-----|--------|
| .NET Framework **4.8.1** (latest Framework line) | `net481` | Classic WinForms / VS Framework tooling |
| .NET **8** | `net8.0-windows` | LTS modern runtime |
| .NET **9** | `net9.0-windows` | Current modern runtime |

Shared pieces:

- **WinForms** UI (`EyeTrackerForm`) + webcam via Emgu.CV `VideoCapture`
- **Emgu.CV 4.9** (OpenCV .NET wrapper) + Haar cascade XML files in `src/EyeTracker/`
- Detection pipeline in `Classes/` (`DetectFace`, `ItemsDetected`, `Circle`)

Mobile / Google Vision / OpenCV.Binding remain as **historical** Android coursework; they are not part of the net8/net9 modernization.

## Build (Windows eye tracker)

On Windows, install the [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) (builds net8 and net9). For the Framework target, also install the [.NET Framework 4.8.1 Developer Pack](https://dotnet.microsoft.com/download/dotnet-framework/net481).

```bash
dotnet restore src/EyeTracker/EyeTrackerOnWindows.csproj
dotnet build src/EyeTracker/EyeTrackerOnWindows.csproj -c Release

# Single targets
dotnet build src/EyeTracker/EyeTrackerOnWindows.csproj -f net481 -c Release
dotnet build src/EyeTracker/EyeTrackerOnWindows.csproj -f net8.0-windows -c Release
dotnet build src/EyeTracker/EyeTrackerOnWindows.csproj -f net9.0-windows -c Release

dotnet run --project src/EyeTracker/EyeTrackerOnWindows.csproj -f net9.0-windows
```

Or open `EyeTracker.sln` in Visual Studio, set `EyeTrackerOnWindows` as startup, and run with a webcam. Mobile projects still need the Android / Xamarin workload if you rebuild those archives.

## Third-party / licenses

- `src/OpenCV.Binding/` — vendor OpenCV binding used by `MobileEyeTracker` only; not authored coursework. Details: [`src/OpenCV.Binding/THIRD_PARTY.md`](src/OpenCV.Binding/THIRD_PARTY.md).
- Emgu.CV / OpenCV on Windows may be under LGPL; see `src/EyeTracker/License-LGPL.txt`.
