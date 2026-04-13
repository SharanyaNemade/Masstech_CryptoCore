using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1_CryptoCore.Application.DTO;
using Team1_CryptoCore.Application.Interface;
using Team1_CryptoCore.Domain.Models;
using Team1_CryptoCore.Infrastructure.Data;

namespace Team1_CryptoCore.Infrastructure.Services
{
    
        public class WalletService : IWalletService
        {
            ApplicationDbContext db;
            IMapper mapper;
            IMemoryCache cache;

        public WalletService(ApplicationDbContext db, IMapper mapper, IMemoryCache cache)
            {
                this.db = db;
                this.mapper = mapper;
                this.cache = cache;
            }

            // Add
            public void Add(WalletDTO dto)
            {
                var data = mapper.Map<Wallet>(dto);
                db.Wallet.Add(data);
                db.SaveChanges();
            }


        // Get all

        public List<WalletDTO> GetAll(int page, int pageSize)
        {
            string key = $"AddWallet_{page}_{pageSize}";

            if(!cache.TryGetValue(key, out List<WalletDTO>? data))
            {
                var fetch = db.Wallet
                    .OrderBy(x => x.WalletId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                data = mapper.Map<List<WalletDTO>>(fetch);


                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

                cache.Set(key, data, options);
            }
            //var data = db.Wallet.ToList();
            //return mapper.Map<List<WalletDTO>>(data);

            return data!;
        }
        

            // Get by Id
            public WalletDTO GetById(int id)
            {
               var data = db.Wallet
                             .FirstOrDefault(x => x.WalletId == id);

            return mapper.Map<WalletDTO>(data);
            }




            // Update
            public void Update(WalletDTO dto)
            {
                var existing = db.Wallet
                                 .FirstOrDefault(x => x.WalletId == dto.WalletId);

                if (existing != null)
                {
                    existing.Balance = dto.Balance;
                    existing.ModifiedBy = dto.ModifiedBy;
                    existing.ModifiedAt = DateTime.Now;

                    db.SaveChanges();
                }
            }

            // Delete
            public void Delete(int id)
            {
                var data = db.Wallet
                             .FirstOrDefault(x => x.WalletId == id);

                if (data != null)
                {
                    data.DeletedAt = DateTime.Now;
                    data.DeletedBy = "1"; 

                    db.SaveChanges();
                }
            }

        
    }
}