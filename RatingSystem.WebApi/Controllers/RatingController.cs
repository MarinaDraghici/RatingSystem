using Microsoft.AspNetCore.Mvc;
using RatingSystem.Application.Queries;
using RatingSystem.PublishedLanguage.Commands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RatingSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly MediatR.IMediator _mediator;

        public RatingController(MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<string> AddRating(AddRatingCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return "OK";
        }

        [HttpGet]
        [Route("GetRatings")]
        // query: http://localhost:5000/api/account/listofaccounts?personid=1&cnp=1961231..
        // route: http://localhost:5000/api/account/listofaccounts/1/1961231..
        public async Task<List<GetAverageRating.Model>> GetListOfConferences(CancellationToken cancellationToken)
        {
            var query = new GetAverageRating.Query();
            var result = await _mediator.Send(query, cancellationToken);
            return result;
        }
    }
}
