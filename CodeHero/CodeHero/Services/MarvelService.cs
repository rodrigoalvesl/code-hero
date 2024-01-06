using CodeHero.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CodeHero.Services
{
    public class MarvelService
    {
        #region Properties
        private string Hash;
        private readonly string TimeStamp = DateTime.Now.Ticks.ToString();
        private const string MarvelAPIUrl = "https://gateway.marvel.com/v1/public/";
        private const string PublicKey = "ba5eb31ac2810f2abbfaf38d3da56685";
        private const string PrivateKey = "c36656854a60d5b233225b19e1d208133928234d";
        #endregion

        public MarvelService()
        {
            Hash = CreateMD5($"{TimeStamp}{PrivateKey}{PublicKey}");
        }

        public async Task<List<Hero>> GetHeroesAsync(int currentPage)
        {
            var offset = currentPage * 4;

            var url = $"{MarvelAPIUrl}characters?apikey={PublicKey}&ts={TimeStamp}&hash={Hash}&limit=4&offset={offset}";
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<MarvelResponseModel>(content);

                    return result.Data.Results;
                }
                else
                    return null;
            }
        }

        public async Task<List<Hero>> GetHeroByNameAsync(string name)
        {

            var url = $"{MarvelAPIUrl}characters?apikey={PublicKey}&ts={TimeStamp}&hash={Hash}&name={name}";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<MarvelResponseModel>(content);

                    return result.Data.Results;
                }
                else
                    return null;
            }
        }

        private string CreateMD5(string input)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
