using AcmePayLtdLibrary.Commands;
using AcmePayLtdLibrary.DataAccess;
using AcmePayLtdLibrary.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AcmePayLtdLibrary.Handlers
{
    public class AuthorizeTransactionHandler : IRequestHandler<AuthorizeTransactionCommand, TransactionModel>
    {
        private readonly ITransactionDataAccess _data;

        public AuthorizeTransactionHandler(ITransactionDataAccess data)
        {
            _data = data;
        }
        public Task<TransactionModel> Handle(AuthorizeTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = new TransactionModel
            {
                Amount = request.Transaction.Amount,
                Currency = request.Transaction.Currency,
                CardholderNumber = request.Transaction.CardholderNumber,
                HolderName = request.Transaction.HolderName,
                ExpirationMonth = request.Transaction.ExpirationMonth,
                ExpirationYear = request.Transaction.ExpirationYear,
                CVV = request.Transaction.CVV,
                OrderReference = request.Transaction.OrderReference,
                UUID = Guid.NewGuid().ToString(),
                Status = Status.Authorized
            };
            return _data.AuthorizeTransactionAsync(transaction);
        }
    }
}
