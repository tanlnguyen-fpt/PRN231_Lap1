using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int UnitInStock { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public virtual Category? Category { get; set; }
    }
}
