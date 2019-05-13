# bottomsuplist

Steps to reproduce the issue:
1. (Obvious) Clone, build, and run the app

*Expected:*
The app loads and you can enter dummy text messages

*Actual:*
The app fails immediately on startup saying...
    System.Runtime.InteropServices.COMException: Error HRESULT E_FAIL has been returned from a call to a COM component.
       at Windows.UI.Xaml.FrameworkElement.MeasureOverride(Size availableSize)
