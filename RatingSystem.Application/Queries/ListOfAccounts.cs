using Abstractions;
using FluentValidation;
using MediatR;
using RatingSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RatingSystem.Application.Queries
{
    public class GetAverageRating
    {
        public class Query : IRequest<List<Model>>
        {
        }

        public class QueryHandler : IRequestHandler<Query, List<Model>>
        {
            private readonly RatingDbContext _dbContext;

            public QueryHandler(RatingDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public Task<List<Model>> Handle(Query request, CancellationToken cancellationToken)
            {
                var db = _dbContext.AverageRatings;
                var result = db.Select(x => new Model
                {
                    ExternalId = x.ExternalId,
                    Category=x.Category,
                    AverageRatingValue = x.AverageRatingValue
                }).Take(2)
                    .ToList();
                return Task.FromResult(result);
            }
        }

        public class Model
        {
            public string Category { get; set; }
            public string ExternalId { get; set; }
            public decimal AverageRatingValue { get; set; }
        }
    }
}
