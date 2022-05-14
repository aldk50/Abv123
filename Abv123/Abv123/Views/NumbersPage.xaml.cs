using Abv123.Interfaces;
using Abv123.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Abv123.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NumbersPage : ContentPage
    {
        public NumbersPage()
        {
            InitializeComponent();
            Color color = Color.FromHex("#1E1E1E");
            this.SetAppThemeColor(BackgroundColorProperty, color, color);
        }
        public void Reload()
        {
            var sw = DependencyService.Get<IDisplayInfo>().GetDisplayWith();
            var sh = DependencyService.Get<IDisplayInfo>().GetDisplayHeight();

            if (sw < sh)
            {
                numLbl.Margin = 2;
                numLbl.FontSize = 20;
                for (int i = 0; i < num.Children.Count; i++)
                {
                    num.Children[i].WidthRequest = ((sw / 2) - num.Children[i].Margin.Left - num.Children[i].Margin.Right);
                    num.Children[i].HeightRequest = num.Children[i].WidthRequest * 0.73;
                }
            }
            else
            {
                numLbl.Margin = 2;
                numLbl.FontSize = 16;
                for (int i = 0; i < num.Children.Count; i++)
                {
                    num.Children[i].WidthRequest = (sw / 5) - num.Children[i].Margin.Left - num.Children[i].Margin.Right;
                    num.Children[i].HeightRequest = num.Children[i].WidthRequest * 0.73;
                }
            }
        }

        int delta;
        protected override void OnAppearing()
        {

            base.OnAppearing();
            var sw = DependencyService.Get<IDisplayInfo>().GetDisplayWith();
            var sh = DependencyService.Get<IDisplayInfo>().GetDisplayHeight();
            num.Children.Clear();
            for (int i = 1; i <= 10; i++)
            {
                var ib = new MyImageButton()
                {
                    Name = i.ToString(),
                    Source = "n" + i.ToString() + ".png",
                    CornerRadius = 8
                };
                if (sw < sh)
                {
                    ib.WidthRequest = (sw / 2) - ib.Margin.Left - ib.Margin.Right;
                    ib.HeightRequest = ib.WidthRequest * 0.73;
                }
                else
                {
                    ib.WidthRequest = (sw / 5) - ib.Margin.Left - ib.Margin.Right;
                    ib.HeightRequest = ib.WidthRequest * 0.73;
                }
                ib.Clicked += Ib_Clicked;
                num.Children.Add(ib);
            }
        }

        private void Ib_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IAudio>().PlayAudioFile($"an{(sender as MyImageButton).Name}.mp3");
            (sender as MyImageButton).BorderColor = Color.FromHex("#FF4081");
            (sender as MyImageButton).BorderWidth = 2;
            Device.StartTimer(TimeSpan.FromSeconds(1), OnTimerTick);
            bool OnTimerTick()
            {
                (sender as MyImageButton).BorderColor = Color.Transparent;
                (sender as MyImageButton).BorderWidth = 0;
                return false;
            }

        }

        private void ContentPage_SizeChanged(object sender, EventArgs e)
        {
           Reload();
        }

    }
}