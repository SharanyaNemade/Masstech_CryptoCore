using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1_CryptoCore.Application.DTO;
using Team1_CryptoCore.Domain.Models;

namespace Team1_CryptoCore.Application.Mapping
{
    public class DTOMapping:Profile
    {
        public DTOMapping()
        {
            CreateMap<User, UserDTO>();
            CreateMap<User, RegisterUserDTO>().ReverseMap();
            CreateMap<Transaction, TransactionDTO>().ReverseMap();
            CreateMap<Wallet, WalletDTO>().ReverseMap();
            CreateMap<FavouriteCoin, FavouriteCoinDTO>();
            CreateMap<Transaction, TransactionDTO>();
            CreateMap<CoinMarket, CoinGeckoDTO>();
            CreateMap<CoinMarket, CoinMarketDTO>();
            CreateMap<CryptoCoin, CoinIdDTO>();
            CreateMap<BankDetails, BankDetailsDTO>();
            CreateMap<Wallet, WalletDTO>();
            CreateMap<WalletTransaction, WalletTransactionDTO>();
            CreateMap<Wishlist, WishlistDTO>();
        }
    }
}
