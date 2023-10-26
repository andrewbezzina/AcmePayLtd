using AcmePayLtdLibrary.Models;
using AcmePayLtdLibrary.Models.Response;
using MediatR;

namespace AcmePayLtdLibrary.Queries
{
    public record GetTransactionByIdQuery(string Id) : IRequest<GetTransactionModel>;

}
