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
    public class CoinGeckoIDService : ICoinGeckoID
    {

        ApplicationDbContext db;
        IMapper mapper;

        public CoinGeckoIDService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }


        public async Task<CoinIdDTO> GetCoinById(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "MyCryptoApp/1.0");

                var response = await client.GetAsync(
                    $"https://api.coingecko.com/api/v3/coins/{id}"
                );

                var json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API Error: {response.StatusCode} - {json}");
                }

                var data = JsonSerializer.Deserialize<CoinIdDTO>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data!;
            }
        }
    }
}
