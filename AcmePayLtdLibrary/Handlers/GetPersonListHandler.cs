using AcmePayLtdLibrary.DataAccess;
using AcmePayLtdLibrary.Models;
using AcmePayLtdLibrary.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AcmePayLtdLibrary.Handlers
{
    public class GetPersonListHandler : IRequestHandler<GetPersonListQuery, List<PersonModel>>
    {
        private readonly IPersonDataAccess _data;

        public GetPersonListHandler(IPersonDataAccess data)
        {
            _data = data;
        }

        public Task<List<PersonModel>> Handle(GetPersonListQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_data.GetPeople());
        }
    }
}
