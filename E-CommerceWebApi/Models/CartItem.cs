using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_CommerceWebApi.Models
{
	public class CartItem
	{
		public int Id { get; set; }
		[ForeignKey("Product")]

		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public Product Product { get; set; }

	}
}
