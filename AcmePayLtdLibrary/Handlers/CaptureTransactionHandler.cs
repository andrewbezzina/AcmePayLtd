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
    public class CaptureTransactionHandler : IRequestHandler<CaptureTransactionCommand, StatusResponseModel>
    {
        private readonly ITransactionDataAccess _data;

        public CaptureTransactionHandler(ITransactionDataAccess data)
        {
            _data = data;
        }
        public async Task<StatusResponseModel> Handle(CaptureTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _data.GetTransactionByIdAync(request.captureRequest.Id);
            if (transaction == null || transaction.Status != Status.Authorized)
            {
                return null;
            }
            var capturedTransaction = await _data.CaptureTransaction(request.captureRequest.OrderReference, request.captureRequest.Id);

            return new StatusResponseModel
            {
                Id = capturedTransaction.Uuid,
                Status = capturedTransaction.Status
            };
        }
    }
}
