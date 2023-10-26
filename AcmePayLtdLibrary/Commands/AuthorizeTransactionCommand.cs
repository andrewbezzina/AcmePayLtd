using AcmePayLtdLibrary.Models;
using AcmePayLtdLibrary.Models.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmePayLtdLibrary.Commands
{
    public record AuthorizeTransactionCommand(PostAuthorizeTransactionModel Transaction) : IRequest<TransactionModel>;
}
