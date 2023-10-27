using AcmePayLtdLibrary.Commands;
using AcmePayLtdLibrary.Models;
using AcmePayLtdLibrary.Models.Request;
using AcmePayLtdLibrary.Models.Response;
using AcmePayLtdLibrary.Queries;
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

        public AuthorizeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<TransactionController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetTransactionModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<GetTransactionModel>>> Get()
        {
            var transactionList = await _mediator.Send(new GetTransactionListQuery());
            if (transactionList.IsNullOrEmpty())
            {
                return NotFound("No Transactions Found");
            }
            return transactionList.ToList();
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
        public async Task<ActionResult<StatusResponseModel>> Post(string id, [FromBody] string orderReference)
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
    }
}
