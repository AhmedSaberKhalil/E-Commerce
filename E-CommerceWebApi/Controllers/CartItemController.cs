using E_CommerceWebApi.DTO.DtoModels;
using E_CommerceWebApi.Models;
using E_CommerceWebApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceWebApi.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class CartItemController : ControllerBase
	{

		private readonly IRepository<CartItem> repository;

		public CartItemDto CartItem { get; }

		public CartItemController(IRepository<CartItem> repository)
		{
			this.repository = repository;
		}
		[HttpGet]
		public IActionResult GetAll()
		{
			return Ok(repository.GetAll());
		}

		[HttpGet]
		[Route("{id:int}", Name = "CartItemDetailsRoute")]
		public IActionResult GetById(int id)
		{
			return Ok(repository.GetById(id));
		}

		[HttpPost("Add/")]
		public IActionResult AddToCartItem(CartItemDto cartItemDto)
		{
			if (ModelState.IsValid)
			{
				CartItem cartItem = MapToCartItem(cartItemDto);
				repository.Add(cartItem);
				string actionLink = Url.Link("CartItemDetailsRoute", new { id = cartItemDto.Id });
				return Created(actionLink, cartItemDto);
			}
			else
			{
				return BadRequest(ModelState);
			}
		}
		[HttpPut("update/{id}")]
		public IActionResult Edit(int id, CartItemDto cartItemDto)
		{
			if (ModelState.IsValid)
			{
				if (id == cartItemDto.Id)
				{
					CartItem cartItem = MapToCartItem(cartItemDto);

					repository.Update(id, cartItem);
					return StatusCode(StatusCodes.Status204NoContent);
				}
				return BadRequest("Invalied data");
			}
			return BadRequest(ModelState);
		}
		[HttpDelete("Delete/{id}")]
		public IActionResult Delete(int id)
		{
			CartItem cartItemId = repository.GetById(id);
			if (cartItemId == null)
			{
				return NotFound("Data Not Found");
			}
			else
			{
				try
				{
					repository.Delete(cartItemId);
					return StatusCode(StatusCodes.Status204NoContent);
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}

			}
		}
		internal CartItem MapToCartItem(CartItemDto cartItemDto)
		{
			return new CartItem
			{
				ProductId = cartItemDto.ProductId,
				Quantity = cartItemDto.Quantity
			};
		}

	}
}
