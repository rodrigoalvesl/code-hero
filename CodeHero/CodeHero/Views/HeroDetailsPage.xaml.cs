using CodeHero.Models;
using CodeHero.ViewModels;
using Xamarin.Forms;

namespace CodeHero.Views
{
    public partial class HeroDetailsPage : ContentPage
	{
		public HeroDetailsPage(Hero hero)
		{
			InitializeComponent();
			BindingContext = new DetailsViewModel(hero);
		}
	}
}