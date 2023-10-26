using AcmePayLtdLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AcmePayLtdLibrary.DataAccess
{
    public class DbTransactionDataAccess : ITransactionDataAccess
    {
        private readonly TransactionDbContext _context;

        public DbTransactionDataAccess(TransactionDbContext context)
        {
            _context = context;
        }
        public async Task<TransactionModel> AuthorizeTransactionAsync(TransactionModel transaction)
        {
            if (!_context.Transactions.IsNullOrEmpty() && _context.Transactions.Any(t => t.Uuid == transaction.Uuid))
            {
                return null;
            }

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<TransactionModel?> GetTransactionByIdAync(Guid Id)
        {
            if(_context.Transactions.IsNullOrEmpty())
            {
                return null;
            }
            return await _context.Transactions.FirstOrDefaultAsync(t => t.Uuid == Id);
        }

        public async Task<List<TransactionModel>?> GetTransactionsAync()
        {
            if (_context.Transactions.IsNullOrEmpty())
            {
                return null;
            }
            return await _context.Transactions.ToListAsync();
        }

        public async Task<TransactionModel?> VoidTransaction(string orderReference, Guid Uuid)
        {
            if (_context.Transactions.IsNullOrEmpty())
            {
                return null;
            }
            var transaction = _context.Transactions.FirstOrDefault(t => t.Uuid == Uuid);
            if (transaction == null)
            {
                return null;
            }
            transaction.VoidOrderReference = orderReference;
            transaction.Status = (int)Status.Voided;

            await _context.SaveChangesAsync();
            return transaction;
        }
    }
}
