using CodeHero.Models;
using CodeHero.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeHero.ViewModels
{
    public class HeroesViewModel : BaseViewModel
    {
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
        public HeroesViewModel()
        {
            var service = new MarvelService();
            HeroesList = new List<Hero>();

            var list = Task.Run(async () => await service.GetHeroes()).Result;

            HeroesList.AddRange(list);

        }
    }
}
