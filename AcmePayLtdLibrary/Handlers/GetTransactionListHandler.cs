using AcmePayLtdLibrary.DataAccess;
using AcmePayLtdLibrary.Models;
using AcmePayLtdLibrary.Models.Response;
using AcmePayLtdLibrary.Queries;
using AcmePayLtdLibrary.Services;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmePayLtdLibrary.Handlers
{
    public class GetTransactionListHandler : IRequestHandler<GetTransactionListQuery, IEnumerable<GetTransactionModel>>
    {
        private readonly ITransactionDataAccess _data;
        private readonly ITransactionHelpers _transactionHelpers;

        public GetTransactionListHandler(ITransactionDataAccess data, ITransactionHelpers transactionHelpers)
        {
            _data = data;
            _transactionHelpers = transactionHelpers;
        }
        public async Task<IEnumerable<GetTransactionModel>?> Handle(GetTransactionListQuery request, CancellationToken cancellationToken)
        {
            var sqlTransations = await _data.GetTransactionsAync();
            if(sqlTransations.IsNullOrEmpty())
            {
                return null;
            }

            return sqlTransations.Select(sqlTransations => _transactionHelpers.MapSqlTransactionToResponse(sqlTransations));
        }
    }
}
