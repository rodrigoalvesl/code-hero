using CodeHero.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CodeHero.Views
{
    public partial class HeroesPage : ContentPage
    {
        public HeroesPage()
        {
            InitializeComponent();

            BindingContext = new HeroesViewModel();
        }
    }
}