using Abv123.Views;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Abv123
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            CarouselPage mainpage = new CarouselPage() { Children =
                {
                    new AbcPage(),
                    new NumbersPage()
                }
            };

            MainPage = mainpage;

        }
        public int PageNumber = 0;
        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            if ((MainPage as CarouselPage).CurrentPage.Title.ToLower() == "числа") PageNumber = 1;
            else PageNumber = 0;
        }

        protected override void OnResume()
        {
            (MainPage as CarouselPage).Children.Clear();
            CarouselPage mainpage = new CarouselPage()
            {
                Children =
                {
                    new AbcPage(),
                    new NumbersPage()
                }
            };

            MainPage = mainpage;
            (MainPage as CarouselPage).CurrentPage = (MainPage as CarouselPage).Children[PageNumber];
            Task.Delay(1000);
            if (PageNumber == 1) ((MainPage as CarouselPage).CurrentPage as NumbersPage).Reload();
            else
            {
                ((MainPage as CarouselPage).CurrentPage as AbcPage).Reload();
            }


        }


    }
}