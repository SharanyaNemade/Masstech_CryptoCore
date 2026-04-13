using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1_CryptoCore.Application.DTO;

namespace Team1_CryptoCore.Application.Interface
{
    public interface ITransactionService
    {
        void Add(TransactionDTO dto);

        List<TransactionDTO> GetAll();
        
        TransactionDTO GetById(int id);

        void Update(TransactionDTO dto);

        void Delete(int id);    
    }
}
