using Xamarin.Forms;
using Android.Media;
using Abv123.Interfaces;

[assembly: Dependency(typeof(AudioService))]
public class AudioService : IAudio
{


    static MediaPlayer player;
    public void PlayAudioFile(string fileName)
    {
        var fd = Android.App.Application.Context.Assets.OpenFd(fileName);

        //player?.Stop();
        player = new MediaPlayer();
        if (player != null) player.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length); else return;
        player.Prepared += (s, e) =>
        {
            if (player != null)
            {
                player.Start();
            }
            else return;
        };
        player.Completion += (s, e) => { (s as MediaPlayer).Release(); };
        if (player != null) player.Prepare(); else return;
    }

}