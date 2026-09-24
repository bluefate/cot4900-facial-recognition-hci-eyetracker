# COT 4900 — Facial Recognition for HCI

**Course:** COT 4900 — Facial Recognition for HCI  
**Term:** Fall 2017  
**School:** Florida Atlantic University (FAU)  
**Author:** John Hernandez

Windows / mobile prototypes that use a webcam (or device camera) plus OpenCV / Google Vision for face and eye tracking in HCI experiments.

## Historical screenshots (Fall 2017)

Stills from original **Eye Tracker Form** session recordings. Overlays: face (red), eyes (blue), iris (green).

**3 October 2017**

<img src="Captures/stills/2017-10-03-eye-tracker-form.jpg" alt="Eye Tracker Form — 3 Oct 2017" width="560" />

**7 November 2017**

<img src="Captures/stills/2017-11-07-eye-tracker-form.jpg" alt="Eye Tracker Form — 7 Nov 2017" width="560" />

## Layout

| Path | Description |
|------|-------------|
| `EyeTracker.sln` | Full solution (opens projects under `src/`) |
| `src/EyeTracker/` | WinForms eye tracker (`EyeTrackerOnWindows`) — `net481` + `net8.0-windows` + `net9.0-windows` |
| `src/MobileEyeTracker/` | Xamarin / OpenCV Android eye tracker |
| `src/EyeTrackWithGoogleVision/` | Android Google Vision face tracker |
| `src/OpenCV.Binding/` | **Third-party support** — OpenCV Android binding (see `THIRD_PARTY.md`) |
| `Captures/` | Session videos, session logs, and related media |
| `submitted/orginal export submited/` | Original multipart `EyeTrackerTests.7z` submission archive |

## Third-party support

- `src/OpenCV.Binding/` — vendor OpenCV binding used by `MobileEyeTracker` only; not authored coursework. Details: [`src/OpenCV.Binding/THIRD_PARTY.md`](src/OpenCV.Binding/THIRD_PARTY.md).
- Emgu.CV on Windows may be under LGPL; see `src/EyeTracker/License-LGPL.txt` where present.

## Stack

- **Windows eye tracker** (`src/EyeTracker/`): multi-targets **.NET Framework 4.8.1** (`net481`), **.NET 8** (`net8.0-windows`), and **.NET 9** (`net9.0-windows`), WinForms + Emgu.CV 4.9
- Xamarin.Android + OpenCV / Google Vision (mobile projects; unchanged archive stack)
- Haar cascades for face and eye detection

## Build (Windows eye tracker)

From a Windows machine with the [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) (covers 8/9 builds; optionally also the [.NET Framework 4.8.1 Developer Pack](https://dotnet.microsoft.com/download/dotnet-framework/net481)):

```bash
dotnet restore src/EyeTracker/EyeTrackerOnWindows.csproj
dotnet build src/EyeTracker/EyeTrackerOnWindows.csproj -c Release
# Framework 4.8.1 only:
dotnet build src/EyeTracker/EyeTrackerOnWindows.csproj -f net481 -c Release
# .NET 8 only:
dotnet build src/EyeTracker/EyeTrackerOnWindows.csproj -f net8.0-windows -c Release
# .NET 9 only:
dotnet build src/EyeTracker/EyeTrackerOnWindows.csproj -f net9.0-windows -c Release
dotnet run --project src/EyeTracker/EyeTrackerOnWindows.csproj -f net9.0-windows
```

Or open `EyeTracker.sln` in Visual Studio and run `EyeTrackerOnWindows` with a webcam. Mobile projects still need the Android workload / Xamarin tooling.
