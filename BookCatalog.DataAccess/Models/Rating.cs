
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookCatalog.DataAccess.Models
{
    [PrimaryKey("BookId","UserId")]
    public class Rating
    {
        [ForeignKey("Book")]
        [Column(Order = 1)]
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        [ForeignKey("User")]
        [Column(Order = 2)]
        public required string UserId { get; set; }
        public User User { get; set; } = null!;

        public int Value {  get; set; }
    }
}
