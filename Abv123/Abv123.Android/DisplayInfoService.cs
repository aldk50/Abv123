using Xamarin.Forms;
using Android.Media;
using Abv123.Interfaces;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Views;

[assembly: Dependency(typeof(DisplayInfoService))]
public class DisplayInfoService : IDisplayInfo
{
    public int GetDisplayHeight()
    {
        var dm = Android.Content.Res.Resources.System?.DisplayMetrics;
        return (int)(dm.HeightPixels / dm.Density);
    }

    public int GetDisplayWith()
    {
        var dm = Android.Content.Res.Resources.System?.DisplayMetrics;
        return (int)(dm.WidthPixels / dm.Density);
    }

}