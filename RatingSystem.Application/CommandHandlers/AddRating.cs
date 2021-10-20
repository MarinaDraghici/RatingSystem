using MediatR;
using RatingSystem.Data;
using RatingSystem.Models;
using RatingSystem.PublishedLanguage.Commands;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RatingSystem.Application.WriteOperations
{
    public class AddRating : IRequestHandler<AddRatingCommand>
    {
        private readonly IMediator _mediator;
        private readonly RatingDbContext _dbContext;

        public AddRating(IMediator mediator, RatingDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        public Task<Unit> Handle(AddRatingCommand request, CancellationToken cancellationToken)
        {
            var rating = _dbContext.Ratings.FirstOrDefault(x => x.ExternalId == request.ExternalId && x.Category == request.Category && x.UserId == request.UserId);
            if (rating == null)
            {
                rating = new Rating
                {
                    Category = request.Category,
                    ExternalId = request.ExternalId,
                    UserId = request.UserId,
                    RatingValue = request.RatingValue
                };
                _dbContext.Ratings.Add(rating);
            }
            else
            {
                rating.RatingValue = request.RatingValue;
            }

            _dbContext.SaveChanges();

            var avrating = _dbContext.AverageRatings.FirstOrDefault(x => x.ExternalId == rating.ExternalId && x.Category == rating.Category);
            if (avrating == null)
            {
                avrating = new AverageRating
                {
                    Category = rating.Category,
                    ExternalId = rating.ExternalId,
                    AverageRatingValue = rating.RatingValue
                };
                _dbContext.AverageRatings.Add(avrating);
            }
            else
            {
                var averageRating = _dbContext
                .Ratings
                .Where(x => x.ExternalId == rating.ExternalId && x.Category == rating.Category)
                .Average(x => x.RatingValue);
                avrating.AverageRatingValue = averageRating;
            }
            _dbContext.SaveChanges();
            //_dbContext.AverageRatings.Add(avrating);
            return Unit.Task;
        }
    }
}


