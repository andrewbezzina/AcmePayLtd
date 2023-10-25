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
    public class GetTransactionListHandler : IRequestHandler<GetTransactionListQuery, List<TransactionModel>>
    {
        private readonly ITransactionDataAccess _data;

        public GetTransactionListHandler(ITransactionDataAccess data)
        {
            _data = data;
        }
        public Task<List<TransactionModel>> Handle(GetTransactionListQuery request, CancellationToken cancellationToken)
        {
            return _data.GetTransactionsAync();
        }
    }
}
