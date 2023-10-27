using AcmePayLtdLibrary.Commands;
using AcmePayLtdLibrary.Models;
using AcmePayLtdLibrary.Models.Request;
using AcmePayLtdLibrary.Models.Response;
using AcmePayLtdLibrary.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AcmePayLtdAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<PostAuthorizeTransactionModel> _postAuthorizeTransactionValidator;

        public AuthorizeController(IMediator mediator, IValidator<PostAuthorizeTransactionModel> postAuthorizeTransactionValidator)
        {
            _mediator = mediator;
            _postAuthorizeTransactionValidator = postAuthorizeTransactionValidator;
        }

        // GET: api/<TransactionController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetTransactionModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaginatedItemsViewModel<GetTransactionModel>>> Get(int? pageIndex, int? pageSize)
        {
            var transactionList = await _mediator.Send(new GetTransactionListQuery(pageIndex, pageSize));
            if (transactionList == null)
            {
                return NotFound("No Transactions Found");
            }
            return transactionList;
        }

        // GET api/<TransactionController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetTransactionModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetTransactionModel>> Get(string id)
        {
            var transaction = await _mediator.Send(new GetTransactionByIdQuery(id));
            if(transaction == null)
            {
                return NotFound($"No Transaction with Id: {id} found.");
            }
            return transaction;
        }

        // POST api/<TransactionController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StatusResponseModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
         public async Task<ActionResult<StatusResponseModel>> Post([FromBody] PostAuthorizeTransactionModel transaction)
        {
            var validationResult = _postAuthorizeTransactionValidator.Validate(transaction);
            if(!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new Error(e.ErrorCode, e.ErrorMessage));
                return BadRequest(errors);
            }
            var authorizeResponse = await _mediator.Send ( new AuthorizeTransactionCommand(transaction));

            if (authorizeResponse == null)
            {
                return NotFound("Something went wrong when trying to validate transaction.");
            }
            return authorizeResponse;
        }

        // POST api/<TransactionController>/{id}/void
        [HttpPost("{id}/void")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StatusResponseModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StatusResponseModel>> Void(string id, [FromBody] string orderReference)
        {
            PostVoidRequestModel voidRequest = new()
            { 
                Id = new Guid(id),
                OrderReference = orderReference
            };

            var voidResponse = await _mediator.Send(new VoidTransactionCommand(voidRequest));
            
            if (voidResponse == null) 
            {
                return NotFound($"No valid Authorized Transaction found for requested Id: {id}");
            }
            return voidResponse;
        }

        // POST api/<TransactionController>/{id}/capture
        [HttpPost("{id}/capture")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StatusResponseModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StatusResponseModel>> Capture(string id, [FromBody] string orderReference)
        {
            PostCaptureRequestModel captureRequest = new()
            {
                Id = new Guid(id),
                OrderReference = orderReference
            };

            var captureResponse = await _mediator.Send(new CaptureTransactionCommand(captureRequest));

            if (captureResponse == null)
            {
                return NotFound($"No valid Authorized Transaction found for requested Id: {id}");
            }
            return captureResponse;
        }
    }
    public record Error(string errorCode, string errorMessage);
}
