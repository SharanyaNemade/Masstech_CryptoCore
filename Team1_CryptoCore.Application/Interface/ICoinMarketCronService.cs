using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1_CryptoCore.Application.DTO;

namespace Team1_CryptoCore.Application.Interface
{
    public interface ICoinMarketCronService
    {
        //Task SyncMarketCoins(int page);

        Task<List<CoinGeckoDTO>> SyncMarketCoins(int page);
    }
}
