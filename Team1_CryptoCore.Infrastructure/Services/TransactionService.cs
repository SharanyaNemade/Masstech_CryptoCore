using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1_CryptoCore.Application.DTO;
using Team1_CryptoCore.Application.Interface;
using Team1_CryptoCore.Infrastructure.Data;
using Team1_CryptoCore.Domain.Models;

namespace Team1_CryptoCore.Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        ApplicationDbContext db;
        IMapper mapper;

        public TransactionService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        // Create
        public void Add(TransactionDTO dto)
        {
            var data = mapper.Map<Transaction>(dto);
            db.Transaction.Add(data);
            db.SaveChanges();
        }

        // Get all
        public List<TransactionDTO> GetAll()
        {
            var data = db.Transaction.ToList();   
            return mapper.Map<List<TransactionDTO>>(data);
        }

        // Get by Id
        public TransactionDTO GetById(int id)
        {
            var data = db.Transaction
                         .FirstOrDefault(x => x.TransactionId == id);

            if (data == null)
                return null;

            return mapper.Map<TransactionDTO>(data);
        }
        

        // Update
        public void Update(TransactionDTO dto)
        {
            var existing = db.Transaction
                             .FirstOrDefault(x => x.TransactionId == dto.TransactionId);

            if (existing != null)
            {
                mapper.Map(dto, existing);
                existing.ModifiedAt = DateTime.Now;

                db.SaveChanges();
            }
        }

        // Delete
        public void Delete(int id)
        {
            var data = db.Transaction
                         .FirstOrDefault(x => x.TransactionId == id);

            if (data != null)
            {
                data.DeletedAt = DateTime.Now;
                db.SaveChanges();
            }
        }
    }
}
