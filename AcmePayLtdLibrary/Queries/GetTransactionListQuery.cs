using AcmePayLtdLibrary.Models;
using AcmePayLtdLibrary.Models.Response;
using MediatR;

namespace AcmePayLtdLibrary.Queries
{
    public record GetTransactionListQuery(int? pageIndex, int? pageSize) : IRequest<PaginatedItemsViewModel<GetTransactionModel>>;
}
