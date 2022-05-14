using Abv123.Interfaces;
using Abv123.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Abv123.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AbcPage : ContentPage
    {
        public AbcPage()
        {
            InitializeComponent();
        }
        public void Reload()
        {
            var sw = DependencyService.Get<IDisplayInfo>().GetDisplayWith();
            var sh = DependencyService.Get<IDisplayInfo>().GetDisplayHeight();

            for (int i = 0; i < abc.Children.Count; i++)
            {
                if (sw < sh)
                {
                    abcLbl.Margin = 2;
                    abcLbl.FontSize = 20;



                    abc.Children[i].WidthRequest = (sw / 4) - abc.Children[i].Margin.Left - abc.Children[i].Margin.Right;
                    abc.Children[i].HeightRequest = abc.Children[i].WidthRequest * 0.75;
                }
                else
                {
                    abcLbl.Margin = 2;
                    abcLbl.FontSize = 16;


                    abc.Children[i].WidthRequest = (sw / 8) - abc.Children[i].Margin.Left - abc.Children[i].Margin.Right;
                    abc.Children[i].HeightRequest = abc.Children[i].WidthRequest * 0.75;
                }

                if (i == 27)
                {
                    for (int ii = 0; ii < (abc.Children[i] as StackLayout).Children.Count; ii++)
                    {
                        (abc.Children[i] as StackLayout).Children[ii].WidthRequest = (abc.Children[27].WidthRequest - 8) / 3 - 2;
                        (abc.Children[i] as StackLayout).Children[ii].Margin = new Thickness(0, 0, 0, 0);
                        (abc.Children[i] as StackLayout).Children[ii].HorizontalOptions = LayoutOptions.Center;
                    }
                }
            }
        }
        protected override void OnAppearing()
        {
            var sw = DependencyService.Get<IDisplayInfo>().GetDisplayWith();
            var sh = DependencyService.Get<IDisplayInfo>().GetDisplayHeight();
            Color color = Color.FromHex("#1E1E1E");
            Switch myswitch = new Switch();
            myswitch.IsToggled = isWords;
            myswitch.Toggled += Myswitch_Toggled;
            myswitch.HorizontalOptions = LayoutOptions.Center;
            this.SetAppThemeColor(BackgroundColorProperty, color, color);
            base.OnAppearing();
            abc.Children.Clear();
            StackLayout collection = new StackLayout();
            collection.Orientation = StackOrientation.Horizontal;
            collection.Margin = 4;
            collection.HorizontalOptions = LayoutOptions.StartAndExpand;
            collection.VerticalOptions = LayoutOptions.Center;
            for (int i = 1; i <= 33; i++)
            {
                var ib = new MyImageButton()
                {
                    Name = i.ToString(),
                    Source = "k" + i.ToString() + ".png"
                };
                if (sw < sh && ib.Name != "28" && ib.Name != "29" && ib.Name != "30")
                {
                    ib.WidthRequest = (sw / 4) - ib.Margin.Left - ib.Margin.Right;
                    ib.HeightRequest = ib.WidthRequest * 0.75;

                }
                else
                {
                    ib.WidthRequest = (sw / 8) - ib.Margin.Left - ib.Margin.Right;
                    ib.HeightRequest = ib.WidthRequest * 0.75;

                }
                if (ib.Name == "27")
                {
                    collection.WidthRequest = ib.WidthRequest;
                    collection.HeightRequest = ib.HeightRequest;
                }
                if (ib.Name == "28")
                {
                    ib.Margin = new Thickness(0, 0, 0, 0);
                    ib.WidthRequest = (collection.WidthRequest - 8) / 3 - 2;
                    ib.HorizontalOptions = LayoutOptions.Center;
                    collection.Children.Add(ib);
                }
                if (ib.Name == "29")
                {
                    ib.Margin = new Thickness(0, 0, 0, 0);
                    ib.WidthRequest = (collection.WidthRequest - 8) / 3 - 2;
                    ib.HorizontalOptions = LayoutOptions.Center;
                    collection.Children.Add(ib);
                }
                if (ib.Name == "30")
                {
                    ib.Margin = new Thickness(0, 0, 0, 0);
                    ib.WidthRequest = (collection.WidthRequest - 8) / 3 - 2;
                    ib.HorizontalOptions = LayoutOptions.Center;
                    collection.Children.Add(ib);
                }
                ib.Clicked += Ib_Clicked;
                if (!(ib.Name == "28" || ib.Name == "29" || ib.Name == "30")) abc.Children.Add(ib);
                if (ib.Name == "30") abc.Children.Add(collection);
            }

            abc.Children.Add(new StackLayout
            {
                Margin = 4,
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                WidthRequest = abc.Children[0].Width,
                HeightRequest = abc.Children[0].Height,
                Children =
                {
                    new Label
                    {
                        Text="Слова",
                        TextColor= Color.WhiteSmoke,
                        VerticalOptions= LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Start,
                        FontSize = 12,
                        Margin = new Thickness(2,0,2,0),
                        FontAttributes= FontAttributes.Bold,
                    },
                    myswitch

                }
            });
        }

        private void Myswitch_Toggled(object sender, ToggledEventArgs e)
        {
            isWords = (sender as Switch).IsToggled;
        }

        static bool isWords;

        private void Ib_Clicked(object sender, EventArgs e)
        {

            string filepath = "";
            if (!isWords)
            {

                filepath = $"a{(sender as MyImageButton).Name}.mp3";
            }
            else
            {
                if ((sender as MyImageButton).Name == "28" || (sender as MyImageButton).Name == "29" || (sender as MyImageButton).Name == "30")
                {
                    goto step1;
                }
                else
                {
                    filepath = $"w{(sender as MyImageButton).Name}.mp3";
                }
            }
            DependencyService.Get<IAudio>().PlayAudioFile(filepath);
        step1:
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
