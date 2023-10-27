using AcmePayLtdLibrary.Models.Request;
using AcmePayLtdLibrary.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmePayLtdLibrary.Commands
{
    public record CaptureTransactionCommand(PostCaptureRequestModel captureRequest) : IRequest<StatusResponseModel>;
}
