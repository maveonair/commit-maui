using Foundation;
using ObjCRuntime;
using UIKit;

namespace Commit.Desktop;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}

