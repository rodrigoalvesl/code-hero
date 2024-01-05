using CodeHero.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CodeHero.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeroesPage : ContentPage
    {
        public HeroesPage()
        {
            InitializeComponent();

            BindingContext = new HeroesViewModel();
        }
    }
}