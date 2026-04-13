using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Team1_CryptoCore.Application.DTO;
using Team1_CryptoCore.Application.Interface;
using Team1_CryptoCore.Domain.Models;
using Team1_CryptoCore.Infrastructure.Data;

namespace Team1_CryptoCore.Infrastructure.Services
{
    public class FavouriteCoinService : IFavouriteCoinService
    {
        ApplicationDbContext db;
        IMapper mapper;

        public FavouriteCoinService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

      
        // Get all
        public List<FavouriteCoinDTO> GetAll()
        {
            var data = db.Set<FavouriteCoin>()
                         .Include(x => x.User)
                         .ToList();

            return mapper.Map<List<FavouriteCoinDTO>>(data);
        }

        // Get by Id
        public FavouriteCoinDTO GetById(int id)
        {
            var data = db.Set<FavouriteCoin>()
                         .FirstOrDefault(x => x.FavouriteId == id);

            return mapper.Map<FavouriteCoinDTO>(data);
        }

        // Get By User
        public List<FavouriteCoinDTO> GetByUser(int userId)
        {
            var data = db.Set<FavouriteCoin>()
                         .Where(x => x.UserId == userId)
                         .ToList();

            return mapper.Map<List<FavouriteCoinDTO>>(data);
        }


        //  Add
        public void Add(FavouriteCoinDTO dto)
        {
            // Prevent duplicate favourite (important real-world logic)
            var exists = db.Set<FavouriteCoin>()
                           .Any(x => x.UserId == dto.UserId && x.CoinId == dto.CoinId);

            if (exists)
                throw new Exception("Coin already added to favourites");

            var data = mapper.Map<FavouriteCoin>(dto);

            data.AddedAt = DateTime.Now;

            db.Set<FavouriteCoin>().Add(data);
            db.SaveChanges();
        }


        // Delete
        public void Delete(int id)
        {
            var data = db.Set<FavouriteCoin>()
                         .FirstOrDefault(x => x.FavouriteId == id);

            if (data != null)
            {
                db.Remove(data);
                db.SaveChanges();
            }
        }
    }
}