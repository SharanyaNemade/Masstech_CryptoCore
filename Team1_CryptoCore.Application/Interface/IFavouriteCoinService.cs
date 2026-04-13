using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1_CryptoCore.Application.DTO;

namespace Team1_CryptoCore.Application.Interface
{
    public interface IFavouriteCoinService
    {
        List<FavouriteCoinDTO> GetAll();
        FavouriteCoinDTO GetById(int id);
        List<FavouriteCoinDTO> GetByUser(int userId);
        void Add(FavouriteCoinDTO dto);
        void Delete(int id);
    }
}
