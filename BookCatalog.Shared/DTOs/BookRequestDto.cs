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
        public string Name { get; set; } = string.Empty;

        [MinLength(1)]
        public string Synopsis { get; set; } = null!;
        public DateTime PublishedAt { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
    }
}
