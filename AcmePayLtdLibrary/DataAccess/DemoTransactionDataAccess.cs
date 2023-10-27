using AcmePayLtdLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmePayLtdLibrary.DataAccess
{
    public class DemoTransactionDataAccess : ITransactionDataAccess
    {
        private List<TransactionModel> _transactions = new();
        public DemoTransactionDataAccess()
        {
            _transactions.Add(new TransactionModel { Amount = 50, CardholderNumber = "1234123412341234", Currency = "EUR", CVV = 123, ExpirationMonth = 12, ExpirationYear = 2023, HolderName = "Andrew Bezzina", Id = 1, AuthorizeOrderReference = "Test 1", Uuid = Guid.NewGuid(), Status = Status.Authorized });
            _transactions.Add(new TransactionModel { Amount = 75, CardholderNumber = "1234123412341234", Currency = "EUR", CVV = 123, ExpirationMonth = 12, ExpirationYear = 2023, HolderName = "Andrew Bezzina", Id = 2, AuthorizeOrderReference = "Test 2", Uuid = Guid.NewGuid(), Status = Status.Authorized });
        }

        public Task<PaginatedItemsViewModel<TransactionModel>?> GetTransactionsAync(int pageIndex, int pageSize)
        {
            // ignoring pagination in demo data access.
            PaginatedItemsViewModel<TransactionModel> paginatedList = new(pageIndex, pageSize, 0, _transactions);
            return Task.FromResult(paginatedList);
        }

        public Task<TransactionModel> AuthorizeTransactionAsync(TransactionModel transaction)
        {
            transaction.Id = _transactions.Max(x => x.Id) + 1;
            _transactions.Add(transaction);
            return Task.FromResult(transaction);
        }

        public Task<TransactionModel> GetTransactionByIdAync(Guid Id)
        {
            var transaction = _transactions.FirstOrDefault(t => t.Uuid ==  Id);

            return Task.FromResult(transaction);
        }

        public Task<TransactionModel?> VoidTransaction(string orderReference, Guid Uuid)
        {
            var transaction = _transactions.FirstOrDefault(t => t.Uuid == Uuid);
            if (transaction == null)
            {
                return null;
            }
            transaction.VoidOrderReference = orderReference;
            transaction.Status = Status.Voided;

            return Task.FromResult(transaction);
        }

        public Task<TransactionModel?> CaptureTransaction(string orderReference, Guid Uuid)
        {
            var transaction = _transactions.FirstOrDefault(t => t.Uuid == Uuid);
            if (transaction == null)
            {
                return null;
            }
            transaction.CaptureOrderReference = orderReference;
            transaction.Status = Status.Captured;

            return Task.FromResult(transaction);
        }

    }
}
