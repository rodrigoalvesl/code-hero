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
            var desc = string.IsNullOrEmpty(hero.Description) ? "Este personagem não tem descrição." : hero.Description;
            hero.Description = desc;
            HeroData = hero;
        }
    }
}
