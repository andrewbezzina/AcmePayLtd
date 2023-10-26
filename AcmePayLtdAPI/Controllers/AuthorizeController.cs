using AcmePayLtdLibrary.Commands;
using AcmePayLtdLibrary.Models;
using AcmePayLtdLibrary.Models.Request;
using AcmePayLtdLibrary.Models.Response;
using AcmePayLtdLibrary.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        // TODO change output model
        public async Task<IEnumerable<GetTransactionModel>> Get()
        {
            return await _mediator.Send(new GetTransactionListQuery());
        }

        // GET api/<TransactionController>/5
        [HttpGet("{id}")]
        public async Task<GetTransactionModel> Get(string id)
        {
            return await _mediator.Send(new GetTransactionByIdQuery(id));
        }

        // POST api/<TransactionController>
        [HttpPost]
        //TODO change to input model
        public async Task<TransactionModel> Post([FromBody] PostAuthorizeTransactionModel transaction)
        {
            return await _mediator.Send ( new AuthorizeTransactionCommand(transaction));
            
        }

    }
}
