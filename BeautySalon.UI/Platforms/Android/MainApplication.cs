using Android.App;
using Android.Runtime;
using AndroidX.AppCompat.App;

namespace BeautySalon.UI;

[Application]
public sealed class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership) : base(handle, ownership)
    {
        AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
    }

    protected override MauiApp CreateMauiApp()
    {
        return MauiProgram.CreateMauiApp();
    }
}