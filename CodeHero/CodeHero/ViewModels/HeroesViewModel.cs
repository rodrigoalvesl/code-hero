using CodeHero.Models;
using CodeHero.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CodeHero.ViewModels
{
    public class HeroesViewModel : BaseViewModel
    {
        #region Proprerties
        public MarvelService Service { get; set; }
        public ICommand LoadMoreCommand => new Command(async () => await LoadMoreAsync());

        public ICommand BackCommand => new Command(async () => await BackAsync());

        int _offset;
        public int Offset
        {
            get => _offset;
            set
            {
                _offset = value;
                OnPropertyChanged("Offset");
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
        #endregion

        public HeroesViewModel()
        {
            Service = new MarvelService();
            HeroesList = new List<Hero>();
            Offset = 0;

            Task.Run(async () => await RetrieveHeroesList());
        }

        private async Task LoadMoreAsync()
        {
            Offset++;
            await RetrieveHeroesList();
        }

        private async Task BackAsync()
        {
            Offset--;
            await RetrieveHeroesList();
        }

        private async Task RetrieveHeroesList()
        {
            var list = await Service.GetHeroes(Offset);
            HeroesList = list;
        }
    }
}