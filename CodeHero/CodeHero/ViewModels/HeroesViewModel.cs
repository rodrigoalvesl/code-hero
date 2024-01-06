using CodeHero.Models;
using CodeHero.Services;
using CodeHero.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace CodeHero.ViewModels
{
    public class HeroesViewModel : BaseViewModel
    {
        #region Properties
        public MarvelService Service { get; set; }
        public bool IsBackEnabled => CurrentPage != 0;


        private int _currentPage;
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged("CurrentPage");
                UpdateBackButtonVisibility();
            }
        }

        private string _heroName;
        public string HeroName
        {
            get => _heroName;
            set
            {
                _heroName = value;
                OnPropertyChanged("HeroName");

                if (string.IsNullOrEmpty(_heroName))
                    Task.Run(async () => await RetrieveHeroesListAsync());
            }
        }

        Hero _selectedHero;
        public Hero SelectedHero
        {
            get => _selectedHero;
            set
            {
                _selectedHero = value;
                OnPropertyChanged("SelectedHero");
            }
        }

        List<Hero> _heroesList;
        public List<Hero> HeroesList
        {
            get => _heroesList;
            set
            {
                _heroesList = value;
                OnPropertyChanged("HeroesList");
            }
        }

        LayoutState _currentState = LayoutState.Loading;
        public LayoutState CurrentState
        {
            get => _currentState;
            set
            {
                if (_currentState != value)
                {
                    _currentState = value;
                    OnPropertyChanged("CurrentState");
                }
            }
        }
        #endregion

        #region Commands
        public ICommand LoadMoreCommand => new Command(async () => await LoadMoreHeroesAsync());
        public ICommand BackCommand => new Command(async () => await GoBackAsync());
        public ICommand SearchCommand => new Command(async () => await SearchHeroAsync());
        public ICommand SelectedHeroCommand => new Command(async () => await GoToHeroDetailsAsync());
        #endregion

        public HeroesViewModel()
        {
            Service = new MarvelService();
            HeroesList = new List<Hero>();
            CurrentPage = 0;

            Task.Run(async () => await RetrieveHeroesListAsync());
        }
        private async Task LoadMoreHeroesAsync()
        {
            CurrentPage++;
            await RetrieveHeroesListAsync();
        }
        private async Task GoBackAsync()
        {
            CurrentPage--;
            await RetrieveHeroesListAsync();
        }
        private async Task SearchHeroAsync()
        {
            CurrentState = LayoutState.Loading;
            var list = await Service.GetHeroByNameAsync(HeroName);
            HeroesList = list;
            CurrentState = LayoutState.None;
        }
        private async Task GoToHeroDetailsAsync()
        {
            if (SelectedHero != null)
            {                
                await Application.Current.MainPage.Navigation.PushAsync(new HeroDetailsPage(SelectedHero));
            }
        }
        private async Task RetrieveHeroesListAsync()
        {
            CurrentState = LayoutState.Loading;
            var list = await Service.GetHeroesAsync(CurrentPage);
            HeroesList = list;
            CurrentState = LayoutState.None;
        }
        private void UpdateBackButtonVisibility()
        {
            OnPropertyChanged("IsBackEnabled");
        }
    }
}