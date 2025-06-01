
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

        [ForeignKey("Author")]
        public int AuthorId {  get; set; }
    }
}
