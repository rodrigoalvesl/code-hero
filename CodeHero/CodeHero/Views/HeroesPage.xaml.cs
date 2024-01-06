using System;
using System.Diagnostics;
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
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = new HeroesViewModel();
        }
    }
}