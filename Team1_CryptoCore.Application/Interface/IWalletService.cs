using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1_CryptoCore.Application.DTO;

namespace Team1_CryptoCore.Application.Interface
{
    public interface IWalletService
    {
        void Add(WalletDTO dto);
        //List<WalletDTO> GetAll(int page, int pageSize);
        List<WalletDTO> GetAll(int page, int pageSize);

        WalletDTO GetById(int id);
        void Update(WalletDTO dto);
        void Delete(int id);
    }
}
