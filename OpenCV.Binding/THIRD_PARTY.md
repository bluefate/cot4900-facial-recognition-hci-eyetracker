# OpenCV.Binding — third-party support

**Status:** Third-party / vendor support library (not course-authored application code).

This project is a **Xamarin.Android binding** around **OpenCV 3.x** native binaries and Java APIs. It was included with the Fall 2017 COT 4900 submission so `MobileEyeTracker` could link against OpenCV on Android.

## What lives here

- Native static/shared libraries under `Jars/` (`.a`, `.so`) for multiple ABIs  
- OpenCV Java docs under `Jars/docs/`  
- Binding transforms (`Transforms/`) and the binding `.csproj`

## Ownership / license

OpenCV is upstream open-source software (see [opencv.org](https://opencv.org/) and the licenses shipped with OpenCV). This folder is retained only as **build support** for the historical mobile project—not as original HCI coursework.

The Windows desktop app (`EyeTracker` / Emgu.CV) does **not** require this project.
