using CodeHero.Models;

namespace CodeHero.ViewModels
{
    public class DetailsViewModel : BaseViewModel
    {
        Hero _heroData;
        public Hero HeroData
        {
            get => _heroData;
            set
            {
                _heroData = value;
                OnPropertyChanged("HeroData");
            }
        }
        public DetailsViewModel(Hero hero)
        {
            HeroData = new Hero
            {
                Name = hero.Name,
                Description = hero.Description
            };               
        }
    }
}
