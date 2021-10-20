using MediatR;

namespace RatingSystem.PublishedLanguage.Commands
{
    public class AddRatingCommand : IRequest
    {
        public string Category { get; set; }
        public string ExternalId { get; set; }
        public string UserId { get; set; }
        public decimal RatingValue { get; set; }
    }
}
