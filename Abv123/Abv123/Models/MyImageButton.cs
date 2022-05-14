using Xamarin.Forms;

namespace Abv123.Models
{
    public class MyImageButton : ImageButton
    {
        public MyImageButton()
        {
            CornerRadius = 4;
            Aspect = Aspect.Fill;
            Margin = 4;
            

        }
        public string Name { get; set; }

    }
}
