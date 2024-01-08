using CodeHero.Models;
using CodeHero.Services;
using CodeHero.Views;
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
        public bool IsBackEnabled => CurrentPage != 1;

        private bool _isNavigable = true;
        public bool IsNavigable
        {
            get => _isNavigable;
            set
            {
                _isNavigable = value;
                OnPropertyChanged("IsNavigable");
            }
        }

        private int _nextPage;
        public int NextPage
        {
            get => _nextPage;
            set
            {
                _nextPage = value;
                OnPropertyChanged("NextPage");
            }
        }

        private int _previousPage;
        public int PreviousPage
        {
            get => _previousPage;
            set
            {
                _previousPage = value;
                OnPropertyChanged("PreviousPage");
            }
        }


        private int _currentPage;
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged("CurrentPage");
                UpdateBackButtonVisibility();
                NextPage = _currentPage + 1;
                PreviousPage = _currentPage - 1;
            }
        }

        private string _heroSearchName;
        public string HeroSearchName
        {
            get => _heroSearchName;
            set
            {
                _heroSearchName = value;
                OnPropertyChanged("HeroSearchName");

                if (string.IsNullOrEmpty(_heroSearchName))
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
            CurrentPage = 1;

            Task.Run(async () => await RetrieveHeroesListAsync());
        }

        private async Task LoadMoreHeroesAsync()
        {
            HeroSearchName = string.Empty;
            IsNavigable = false;
            CurrentPage++;
            await RetrieveHeroesListAsync();
        }

        private async Task GoBackAsync()
        {
            HeroSearchName = string.Empty;
            IsNavigable = false;
            CurrentPage--;
            await RetrieveHeroesListAsync();
        }

        private async Task SearchHeroAsync()
        {
            IsNavigable = false;
            var list = await Service.GetHeroByNameAsync(HeroSearchName);
            HeroesList = new List<Hero>(list);
            CurrentState = LayoutState.None;
            IsNavigable = true;
        }

        private async Task GoToHeroDetailsAsync()
        {
            if (SelectedHero != null && IsNavigable)
            {                
                await Application.Current.MainPage.Navigation.PushAsync(new HeroDetailsPage(SelectedHero));
            }
        }

        private async Task RetrieveHeroesListAsync()
        {
            IsNavigable = false;
            CurrentState = LayoutState.Loading;
            var list = await Service.GetHeroesAsync(CurrentPage);        
            HeroesList = new List<Hero>(list);
            CurrentState = LayoutState.None;
            IsNavigable = true;
        }

        private void UpdateBackButtonVisibility()
        {
            OnPropertyChanged("IsBackEnabled");
        }
    }
}