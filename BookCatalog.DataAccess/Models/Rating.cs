
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookCatalog.DataAccess.Models
{
    public class Rating
    {
        [ForeignKey("Book")]
        [Key]
        [Column(Order = 1)]
        public int BookId { get; set; }

        [ForeignKey("User")]
        [Key]
        [Column(Order = 2)]
        public int UserId { get; set; }

        public int Value {  get; set; }
    }
}
