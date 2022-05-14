using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Abv123.ViewModels
{
    public class NumberPageViewModel : BindableObject
    {
        private string goabc;
        public string GoAbcText
        {
            get => goabc;
            set
            {
                goabc = value;
                OnPropertyChanged(nameof(GoAbcText));
            }
        }
        public NumberPageViewModel()
        {
            GoAbcText = "<< Буквы";
        }


    }
}
