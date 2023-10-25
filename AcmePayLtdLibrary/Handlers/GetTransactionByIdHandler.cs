using AcmePayLtdLibrary.DataAccess;
using AcmePayLtdLibrary.Models;
using AcmePayLtdLibrary.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmePayLtdLibrary.Handlers
{
    internal class GetTransactionByIdHandler : IRequestHandler<GetTransactionByIdQuery, TransactionModel>
    {
        private readonly ITransactionDataAccess _transactionDataAccess;

        public GetTransactionByIdHandler(ITransactionDataAccess transactionDataAccess)
        {
            _transactionDataAccess = transactionDataAccess;
        }
        public Task<TransactionModel> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            //TODO validation
            return _transactionDataAccess.GetTransactionByIdAync(request.Id);
        }
    }
}
