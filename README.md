# COT 4900 — Facial Recognition for HCI

**Course:** COT 4900 — Facial Recognition for HCI  
**Term:** Fall 2017  
**School:** Florida Atlantic University (FAU)  
**Author:** John Hernandez

Windows Forms prototype that uses a webcam plus Haar cascades to detect faces and eyes in real time for human–computer interaction experiments.

## What’s in this repo

| Path | Description |
|------|-------------|
| `EyeTracker.sln` | Visual Studio solution |
| `EyeTracker/` | WinForms app (Emgu.CV / OpenCV) |
| `Looking directly into the camera.docx` | Course project write-up / notes |
| Haar cascade XMLs | Face and eye detectors used at runtime |

The UI captures frames from the default camera, runs face and eye detection, and draws rectangles on the live preview (faces in red, eyes in blue). A track bar adjusts a detection sensitivity parameter.

## Stack (as archived)

- C# / .NET Framework 4.6.1
- Windows Forms
- [Emgu.CV](http://www.emgu.com/) 3.3.0.2824 (OpenCV wrapper)
- OpenCV Haar cascades (`haarcascade_frontalface_default.xml`, `haarcascade_eye.xml`, plus alternate cascade files)

## Build notes

This tree is a restored archive. The `.csproj` also references helper types under `Classes/` (`DetectFace`, `ItemsDetected`, `Circle`, `Program`) and `Properties/` that are not present in this snapshot. Restoring those sources (or rewriting the detection helpers) is required before a clean build.

To work with what is here:

1. Open `EyeTracker.sln` in Visual Studio (Windows).
2. Restore NuGet packages (`EMGU.CV`, `ZedGraph`).
3. Ensure a webcam is available when running.

## License

Emgu.CV components in this project are under the LGPL; see `EyeTracker/License-LGPL.txt`.
