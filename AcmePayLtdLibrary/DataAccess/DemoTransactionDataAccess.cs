using AcmePayLtdLibrary.Models;
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
            _transactions.Add(new TransactionModel { Amount = 50, CardholderNumber = "1234123412341234", Currency = "EUR", CVV = 123, ExpirationMonth = 12, ExpirationYear = 2023, HolderName = "Andrew Bezzina", Id = 1, OrderReference = "Test 1", UUID = Guid.NewGuid().ToString(), Status = Status.Authorized });
            _transactions.Add(new TransactionModel { Amount = 75, CardholderNumber = "1234123412341234", Currency = "EUR", CVV = 123, ExpirationMonth = 12, ExpirationYear = 2023, HolderName = "Andrew Bezzina", Id = 2, OrderReference = "Test 2", UUID = Guid.NewGuid().ToString(), Status = Status.Authorized });
        }

        public Task<List<TransactionModel>> GetTransactionsAync()
        {
            return Task.FromResult(_transactions);
        }

        //TODO replace with input contract
        public Task<TransactionModel> AuthorizeTransactionAsync(TransactionModel transaction)
        {
            transaction.Id = _transactions.Max(x => x.Id) + 1;
            _transactions.Add(transaction);
            return Task.FromResult(transaction);
        }

        public Task<TransactionModel> GetTransactionByIdAync(string Id)
        {
            var transaction = _transactions.FirstOrDefault(t => t.UUID ==  Id);

            return Task.FromResult(transaction);
        }
    }
}
