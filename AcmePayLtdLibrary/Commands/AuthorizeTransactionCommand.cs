using AcmePayLtdLibrary.Models;
using AcmePayLtdLibrary.Models.Request;
using AcmePayLtdLibrary.Models.Response;
using MediatR;

namespace AcmePayLtdLibrary.Commands
{
    public record AuthorizeTransactionCommand(PostAuthorizeTransactionModel Transaction) : IRequest<StatusResponseModel>;
}
