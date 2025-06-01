
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookCatalog.DataAccess.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public required string Name { get; set; }
        public required string Synopsis {  get; set; }
        public DateTime PublishedAt { get; set; }

        [ForeignKey("Genre")]
        public int GenreId {  get; set; }
        public Genre Genre { get; set; } = null!;

        [ForeignKey("Author")]
        public int AuthorId {  get; set; }
        public Author Author { get; set; } = null!;
    }
}
