using AcmePayLtdLibrary.Commands;
using AcmePayLtdLibrary.DataAccess;
using AcmePayLtdLibrary.Models;
using AcmePayLtdLibrary.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AcmePayLtdLibrary.Handlers
{
    public class AuthorizeTransactionHandler : IRequestHandler<AuthorizeTransactionCommand, StatusResponseModel>
    {
        private readonly ITransactionDataAccess _data;

        public AuthorizeTransactionHandler(ITransactionDataAccess data)
        {
            _data = data;
        }
        public async Task<StatusResponseModel> Handle(AuthorizeTransactionCommand request, CancellationToken cancellationToken)
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
                AuthorizeOrderReference = request.Transaction.OrderReference,
                Uuid = Guid.NewGuid(),
                Status = Status.Authorized,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };
            var authorizedTransaction = await _data.AuthorizeTransactionAsync(transaction);
            if (authorizedTransaction == null || authorizedTransaction.Status != Status.Authorized)
            {
                return null;
            }

            return new StatusResponseModel
            {
                Id = authorizedTransaction.Uuid,
                Status = authorizedTransaction.Status
            };
        }
    }
}
