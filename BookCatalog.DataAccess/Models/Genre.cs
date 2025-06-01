
using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DataAccess.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public required string Name { get; set; }
    }
}
