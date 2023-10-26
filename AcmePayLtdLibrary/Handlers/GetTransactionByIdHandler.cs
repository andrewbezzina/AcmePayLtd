using AcmePayLtdLibrary.DataAccess;
using AcmePayLtdLibrary.Models;
using AcmePayLtdLibrary.Models.Response;
using AcmePayLtdLibrary.Queries;
using AcmePayLtdLibrary.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmePayLtdLibrary.Handlers
{
    internal class GetTransactionByIdHandler : IRequestHandler<GetTransactionByIdQuery, GetTransactionModel>
    {
        private readonly ITransactionDataAccess _transactionDataAccess;
        private readonly ITransactionHelpers _transactionHelpers;

        public GetTransactionByIdHandler(ITransactionDataAccess transactionDataAccess, ITransactionHelpers transactionHelpers)
        {
            _transactionDataAccess = transactionDataAccess;
            _transactionHelpers = transactionHelpers;
        }
        public async Task<GetTransactionModel> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            //TODO validation
            var sqlTransaction = await _transactionDataAccess.GetTransactionByIdAync(request.Id);
            return _transactionHelpers.MapSqlTransactionToResponse(sqlTransaction);
        }
    }
}
