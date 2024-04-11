using System.Text.Json.Serialization;

namespace E_CommerceWebApi.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }
		public virtual List<Review> Reviews { get; set; }
	}
}
