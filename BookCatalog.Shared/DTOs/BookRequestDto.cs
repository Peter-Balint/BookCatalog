using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.Shared.DTOs
{
    public record BookRequestDto
    {
        [MaxLength(255)]
        public required string Name { get; set; }
        public required string Synopsis { get; set; }
        public DateTime PublishedAt { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
    }
}
