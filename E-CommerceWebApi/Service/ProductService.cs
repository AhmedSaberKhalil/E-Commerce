using E_CommerceWebApi.Data;
using E_CommerceWebApi.Models;
using E_CommerceWebApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceWebApi.Service
{
    public class ProductService : Repository<Product>, IProductRepository
	{
		private readonly ECEntity context;

		public ProductService(ECEntity context) : base(context)
		{
			this.context = context;
		}

		Product IProductRepository.GetProducttWithReviews(int ProductId)
		{
			return context.Product.Include(p => p.Reviews).FirstOrDefault(p => p.Id == ProductId);
		}
	}
}
