using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingSystem.Models
{
    public class AverageRating
    {
       public string Category { get; set; }
        public string ExternalId { get; set; }
        public decimal AverageRatingValue { get; set; }
    }
}
