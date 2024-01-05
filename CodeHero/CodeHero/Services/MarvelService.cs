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
        private const string MarvelAPIUrl = "https://gateway.marvel.com/v1/public/";
        private const string PublicKey = "ba5eb31ac2810f2abbfaf38d3da56685";
        private const string PrivateKey = "c36656854a60d5b233225b19e1d208133928234d";

        public async Task<List<Hero>> GetHeroes()
        {
            string timeStamp = DateTime.Now.Ticks.ToString();
            string hash = CreateMD5($"{timeStamp}{PrivateKey}{PublicKey}");
            string url = $"{MarvelAPIUrl}characters?apikey={PublicKey}&ts={timeStamp}&hash={hash}&limit=4&offset=4";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
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
