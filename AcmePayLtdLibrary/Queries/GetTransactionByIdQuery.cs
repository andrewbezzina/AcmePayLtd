using AcmePayLtdLibrary.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmePayLtdLibrary.Queries
{
    public record GetTransactionByIdQuery(string Id) : IRequest<TransactionModel>;

}
