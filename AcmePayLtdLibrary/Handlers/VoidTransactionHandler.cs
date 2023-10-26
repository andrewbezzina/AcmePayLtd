using AcmePayLtdLibrary.Commands;
using AcmePayLtdLibrary.DataAccess;
using AcmePayLtdLibrary.Models;
using AcmePayLtdLibrary.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmePayLtdLibrary.Handlers
{
    public class VoidTransactionHandler : IRequestHandler<VoidTransactionCommand, StatusResponseModel>
    {
        private readonly ITransactionDataAccess _data;

        public VoidTransactionHandler(ITransactionDataAccess data)
        {
            _data = data;
        }
        public async Task<StatusResponseModel> Handle(VoidTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _data.VoidTransaction(request.voidRequest.OrderReference, request.voidRequest.Id);
            if (transaction == null)
            {
                return null;
            }
        
            return new StatusResponseModel
            {
                Id = transaction.Uuid,
                Status = (Status)transaction.Status
            };
        }
    }
}
