using E_CommerceWebApi.DTO;
using E_CommerceWebApi.Models;

namespace E_CommerceWebApi.Repository
{
	public interface IProductRepository : IRepository<Product>
	{
		 Product GetProducttWithReviews(int ProductId);
	}
}
