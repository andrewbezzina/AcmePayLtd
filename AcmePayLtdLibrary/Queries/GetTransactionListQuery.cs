using AcmePayLtdLibrary.Models.Response;
using MediatR;

namespace AcmePayLtdLibrary.Queries
{
    public record GetTransactionListQuery() : IRequest<IEnumerable<GetTransactionModel>>;
}
