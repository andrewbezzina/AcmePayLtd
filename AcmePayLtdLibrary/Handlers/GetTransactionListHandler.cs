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
    public class GetTransactionListHandler : IRequestHandler<GetTransactionListQuery, PaginatedItemsViewModel<GetTransactionModel>>
    {
        private readonly int DEFAULT_PAGE_INDEX = 1;
        private readonly int DEFAULT_PAGE_SIZE = 5;
        private readonly ITransactionDataAccess _data;
        private readonly ITransactionHelpers _transactionHelpers;

        public GetTransactionListHandler(ITransactionDataAccess data, ITransactionHelpers transactionHelpers)
        {
            _data = data;
            _transactionHelpers = transactionHelpers;
        }
        public async Task<PaginatedItemsViewModel<GetTransactionModel>?> Handle(GetTransactionListQuery request, CancellationToken cancellationToken)
        {
            int pageIndex = request.pageIndex.HasValue ? request.pageIndex.Value : DEFAULT_PAGE_INDEX;
            int pageSize = request.pageSize.HasValue ? request.pageSize.Value : DEFAULT_PAGE_SIZE;

            var sqlTransations = await _data.GetTransactionsAync(pageIndex, pageSize);
            if(sqlTransations == null || sqlTransations.Data.IsNullOrEmpty())
            {
                return null;
            }

            PaginatedItemsViewModel<GetTransactionModel> paginatedResponse = new(
                sqlTransations.PageIndex,
                sqlTransations.PageSize,
                sqlTransations.Count,
                sqlTransations.Data.Select(sqlTransation => _transactionHelpers.MapSqlTransactionToResponse(sqlTransation))
                );

            return paginatedResponse;
        }
    }
}
