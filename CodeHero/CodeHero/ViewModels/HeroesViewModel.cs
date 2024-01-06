using CodeHero.Models;
using CodeHero.Services;
using System.Collections.Generic;
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
                OnPropertyChanged(nameof(CurrentPage));
                UpdateBackButtonVisibility();
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

        private async Task RetrieveHeroesListAsync()
        {
            CurrentState = LayoutState.Loading;
            var list = await Service.GetHeroes(CurrentPage);
            HeroesList = list;
            CurrentState = LayoutState.None;
        }

        private void UpdateBackButtonVisibility()
        {
            OnPropertyChanged("IsBackEnabled");
        }            
    }
}