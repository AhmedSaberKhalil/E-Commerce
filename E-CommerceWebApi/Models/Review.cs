using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceWebApi.Models
{
	public class Review
	{
		public int Id { get; set; }

		[ForeignKey("Product")]
		public int ProductId { get; set; }
        [Required(ErrorMessage = "Review name is required.")]
        [StringLength(15, ErrorMessage = "Review name cannot exceed 15 characters.")]
        public string ReviewName { get; set; }
        [Required(ErrorMessage = "Content is required.")]
        [StringLength(20, ErrorMessage = "Content cannot exceed 20 characters.")]
        public string Content { get; set; }

		public Product Product { get; set; }
	}
}
