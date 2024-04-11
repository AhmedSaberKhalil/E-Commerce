using E_CommerceWebApi.DTO.DtoModels;
using E_CommerceWebApi.Models;
using E_CommerceWebApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace E_CommerceWebApi.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ProductController : ControllerBase
	{
		private readonly IProductRepository repository;

		public ProductDto Product { get; }

		public ProductController(IProductRepository repository)
		{
			this.repository = repository;
		}
		[HttpGet]
		public IActionResult GetAll()
		{
			return Ok(repository.GetAll());
		}

		[HttpGet]
		[Route("{id:int}", Name = "ProducttDetailsRoute")]
		public IActionResult GetById(int id)
		{
			return Ok(repository.GetById(id));
		}

		[HttpPost("Add/")]
		public IActionResult AddProduct(ProductDto product)
		{
			if (ModelState.IsValid)
			{
				Product prod = MapToProduct(product);
				repository.Add(prod);
				string actionLink = Url.Link("ProducttDetailsRoute", new { id = product.Id });
				return Created(actionLink, product); 
			}
			else
			{
				return BadRequest(ModelState);
			}
		}
		[HttpPut("update/{id}")]
		public IActionResult Edit(int id, ProductDto product)
		{
			if (ModelState.IsValid)
			{
				if (id == product.Id)
				{
					Product prod = MapToProduct(product);

					repository.Update(id, prod);
					return StatusCode(StatusCodes.Status204NoContent);
				}
				return BadRequest("Invalied data");
			}
			return BadRequest(ModelState);
		}
		[HttpDelete("Delete/{id}")]
		public IActionResult Delete(int id)
		{
			Product deptId = repository.GetById(id);
			if (deptId == null)
			{
				return NotFound("Data Not Found");
			}
			else
			{
				try
				{
					repository.Delete(deptId);
					return StatusCode(StatusCodes.Status204NoContent);
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}

			}
		}

		[HttpGet("Prodect&Review/")]
		public IActionResult GetProducttWithReviews(int protId)
		{
			Product product = repository.GetProducttWithReviews(protId);
			ProductNameWithAllReviews dto = new ProductNameWithAllReviews();
			if (product == null)
			{
				return BadRequest("Invalied data");
			}
			else
			{
				dto.Id = product.Id;
				dto.ProductName = product.Name;
				foreach (var item in product.Reviews)
				{
					dto.ReviewsList.Add(item.Content);
				}
			}
			return Ok(dto);

		}
		internal Product MapToProduct(ProductDto productDto)
		{
			return new Product
			{
				Name = productDto.Name,
				Price = productDto.Price,
				Description = productDto.Description
			};
		}
	}		
}
