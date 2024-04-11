using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceWebApi.Models
{
	public class Review
	{
		public int Id { get; set; }

		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public string ReviewName { get; set; }
		public string Content { get; set; }

		public Product Product { get; set; }
	}
}
