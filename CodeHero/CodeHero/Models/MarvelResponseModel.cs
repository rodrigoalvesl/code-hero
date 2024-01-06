using System.Collections.Generic;

namespace CodeHero.Models
{
    public class MarvelResponseModel
    {
        public CharacterDataContainer Data { get; set; }
    }

    public class CharacterDataContainer
    {
        public List<Hero> Results { get; set; }
    }

    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Thumbnail thumbnail { get; set; }
        public string Picture
        {
            get
            {
                if (thumbnail != null)
                {
                    return $"{thumbnail.Path}.{thumbnail.Extension}";
                }
                return string.Empty;
            }
        }
    }
    public class Thumbnail
    {
        public string Path { get; set; }
        public string Extension { get; set; }
    }
}
