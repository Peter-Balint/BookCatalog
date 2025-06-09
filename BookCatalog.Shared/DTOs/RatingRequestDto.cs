using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.Shared.DTOs
{
    public record RatingRequestDto
    {
        public int BookId { get; set; }
        public string UserId { get; set; } = null!;

        [Range(1,10)]
        public int Value { get; set; }
    }
}
