using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Team1_CryptoCore.Application.DTO;
using Team1_CryptoCore.Application.Interface;
using Team1_CryptoCore.Infrastructure.Data;


namespace Team1_CryptoCore.Infrastructure.Services
{
    public class CoinGeckoService : ICoinGeckoService
    {

        ApplicationDbContext db;
        IMapper mapper;

        public CoinGeckoService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }




        public async Task<List<CoinGeckoDTO>> GetCoinsFromAPI(int page)
        {
            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("User-Agent", "MyCryptoApp/1.0");

                var response = await client.GetAsync("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd");

                var json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API Error: {response.StatusCode} - {json}");
                }

                var data = JsonSerializer.Deserialize<List<CoinGeckoDTO>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new List<CoinGeckoDTO>();
            }
        }


        //public async Task<List<CoinGeckoDTO>> GetCoinsFromAPI()
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        var response = await client.GetAsync("https://api.coingecko.com/api/v3/coins/list");

        //        if (!response.IsSuccessStatusCode)
        //        return new List<CoinGeckoDTO>();

        //        var json = await response.Content.ReadAsStringAsync();

        //        var data = JsonSerializer.Deserialize<List<CoinGeckoDTO>>(json, new JsonSerializerOptions

        //        {
        //            PropertyNameCaseInsensitive = true
        //        });

        //        return data!;
        //    }
        //}
    }
}
