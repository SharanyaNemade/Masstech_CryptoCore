using Quartz;
using Microsoft.Extensions.DependencyInjection;
using Team1_CryptoCore.Application.Interface;

public class CronMarketJob : IJob
{
    IServiceScopeFactory scopeFactory;

    public CronMarketJob(IServiceScopeFactory scopeFactory)
    {
        this.scopeFactory = scopeFactory;
    }


    private static bool isRunning = false;


    // Added this 
    public async Task Execute(IJobExecutionContext context)
    {
        if (isRunning)
        {
            Console.WriteLine("Job already running. Skipping...");
            return;
        }

        try
        {
            isRunning = true;

            using var scope = scopeFactory.CreateScope();

            var service = scope.ServiceProvider
                .GetRequiredService<ICronGeckoService>();

            await service.RunCronMarketSync(1,50);
        }
        finally
        {
            isRunning = false;
        }
    }
}




    //public async Task Execute(IJobExecutionContext context)
    //{
    //    using var scope = scopeFactory.CreateScope();

    //    var service = scope.ServiceProvider.GetRequiredService<ICronGeckoService>();

    //for (int i = 1; i <= 3; i++)
    //{
    //    await service.RunCronMarketSync(i);
    //}


    //await service.RunCronMarketSync(1);

    //Console.WriteLine("Cron Completed");
    //    }












//using Microsoft.Extensions.DependencyInjection;
//using Quartz;
//using Team1_CryptoCore.Application.Interface;

//namespace Team1_CryptoCore.Infrastructure.Jobs
//{
//    public class CoinMarketQuartzJob : IJob
//    {
//        IServiceScopeFactory scopeFactory;

//        public CoinMarketQuartzJob(IServiceScopeFactory scopeFactory)
//        {
//            this.scopeFactory = scopeFactory;
//        }

//        public async Task Execute(IJobExecutionContext context)
//        {
//            Console.WriteLine($"[QUARTZ START] {DateTime.Now}");

//            using (var scope = scopeFactory.CreateScope())
//            {
//                try
//                {
//                    var cronService = scope.ServiceProvider
//                        .GetRequiredService<ICoinMarketCronService>();

//                    await cronService.SyncMarketCoins(1);

//                    Console.WriteLine($"[QUARTZ END] {DateTime.Now}");
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"[QUARTZ ERROR] {ex.Message}");
//                }
//            }
//        }
//    }
//}










// REMOVED TEMPORARILY

//using Microsoft.Extensions.DependencyInjection;
//using Quartz;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Team1_CryptoCore.Application.Interface;
//using Team1_CryptoCore.Infrastructure.Data;
//using Team1_CryptoCore.Domain.Models;

//namespace Team1_CryptoCore.Infrastructure.Jobs
//{
//    public class CoinMarketQuartzJob : IJob
//    {
//        IServiceScopeFactory scopeFactory;

//        public CoinMarketQuartzJob(IServiceScopeFactory scopeFactory)
//        {
//            this.scopeFactory = scopeFactory;
//        }

//        public async Task Execute(IJobExecutionContext context)
//        {
//            Console.WriteLine($"[QUARTZ START] {DateTime.Now}");

//            using (var scope = scopeFactory.CreateScope())
//            {
//                var cronService = scope.ServiceProvider.GetRequiredService<ICoinMarketCronService>();

//                await cronService.SyncMarketCoins(1);

//                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

//                try
//                {
//                    var coins = await service.GetMarketCoins(1);

//                    Console.WriteLine($"Coins fetched: {coins.Count}");

//                    foreach (var coin in coins)
//                    {
//                        Console.WriteLine($"Processing: {coin.Id}");

//                        var existing = db.Coins
//                            .FirstOrDefault(x => x.CoinId == coin.Id);

//                        if (existing != null)
//                        {
//                            existing.CurrentPrice = coin.CurrentPrice;
//                            existing.PriceChange24h = coin.PriceChange1h;
//                            existing.ModifiedAt = DateTime.Now;
//                        }
//                        else
//                        {
//                            db.Coins.Add(new CoinMarket
//                            {
//                                CoinId = coin.Id,
//                                Symbol = coin.Symbol,
//                                Name = coin.Name,
//                                Image = "",
//                                CurrentPrice = coin.CurrentPrice,
//                                MarketCap = 0,
//                                MarketCapRank = 0,
//                                High24h = 0,
//                                Low24h = 0,
//                                PriceChange24h = coin.PriceChange1h,
//                                CreatedAt = DateTime.Now
//                            });
//                        }
//                    }

//                    await db.SaveChangesAsync();

//                    Console.WriteLine($"[QUARTZ END] {DateTime.Now}");
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"[QUARTZ ERROR] {ex.Message}");
//                }
//            }
//        }
//    }
//}




//public async Task Execute(IJobExecutionContext context)
//{
//    using (var scope = scopeFactory.CreateScope())
//    {
//        var service = scope.ServiceProvider
//            .GetRequiredService<ICoinGeckoMarketService>();

//        var db = scope.ServiceProvider
//            .GetRequiredService<ApplicationDbContext>();

//        try
//        {
//            var coins = await service.GetMarketCoins(1);

//            foreach (var coin in coins)
//            {
//                var existing = db.Coins
//                    .FirstOrDefault(x => x.CoinId == coin.Id);

//                if (existing != null)
//                {
//                    // 🔥 UPDATE existing
//                    existing.CurrentPrice = coin.CurrentPrice;
//                    existing.PriceChange24h = coin.PriceChange1h;
//                    existing.ModifiedAt = DateTime.Now;
//                }
//                else
//                {
//                    // 🔥 INSERT new
//                    var entity = new CoinMarket
//                    {
//                        CoinId = coin.Id,
//                        Symbol = coin.Symbol,
//                        Name = coin.Name,
//                        Image = "",
//                        CurrentPrice = coin.CurrentPrice,
//                        MarketCap = 0,
//                        MarketCapRank = 0,
//                        High24h = 0,
//                        Low24h = 0,
//                        PriceChange24h = coin.PriceChange1h,
//                        CreatedAt = DateTime.Now
//                    };

//                    db.Coins.Add(entity);
//                }
//            }

//            await db.SaveChangesAsync();

//            Console.WriteLine($"[QUARTZ] Job ran at {DateTime.Now}");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"[QUARTZ ERROR] {ex.Message}");
//        }
//    }
//}
