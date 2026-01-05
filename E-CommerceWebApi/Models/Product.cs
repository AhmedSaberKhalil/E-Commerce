using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_CommerceWebApi.Models
{
	public class Product
	{
		public int Id { get; set; }
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(15, ErrorMessage = "Product name cannot exceed 15 characters.")]
        public string Name { get; set; }
        [Range(0.01, 999999.99, ErrorMessage = "Price must be between 0.01 and 999999.99.")]
        [Column(TypeName = "decimal(18,2)")]

        public decimal Price { get; set; }
        [StringLength(100, ErrorMessage = "Description cannot exceed 100 characters.")]
        public string Description { get; set; }
		public virtual List<Review> Reviews { get; set; }
	}
}
