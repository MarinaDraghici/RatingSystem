using System;
using System.Collections.Generic;

#nullable disable

namespace RatingSystem.Models

{
    public class Rating
    {
        public string Category { get; set; }
        public string ExternalId { get; set; }
        public string UserId { get; set; }
        public decimal RatingValue { get; set; }
        
    }
}
