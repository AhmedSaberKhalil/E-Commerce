using E_CommerceWebApi.DTO.DtoModels;
using E_CommerceWebApi.Models;
using E_CommerceWebApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace E_CommerceWebApi.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ReviewController : ControllerBase
	{
		private readonly IRepository<Review> repository;

		//public ProductDto Product { get; }
		
		public ReviewController(IRepository<Review> repository)
		{
			this.repository = repository;
		}
		[HttpGet]
		public IActionResult GetAll()
		{
			return Ok(repository.GetAll());
		}

		[HttpGet]
		[Route("{id:int}", Name = "reviewDetailsRoute")]
		public IActionResult GetById(int id)
		{
			return Ok(repository.GetById(id));
		}

		[HttpPost("Add/")]
		public IActionResult AddProduct(ReviewDto revDto)
		{
			if (ModelState.IsValid)
			{
				Review review = MapToReview(revDto);
				repository.Add(review);
				string actionLink = Url.Link("reviewDetailsRoute", new { id = revDto.Id });
				return Created(actionLink, revDto);
			}
			else
			{
				return BadRequest(ModelState);
			}
		}
		[HttpPut("update/{id}")]
		public IActionResult Edit(int id, ReviewDto revDto)
		{
			if (ModelState.IsValid)
			{
				if (id == revDto.Id)
				{
					Review review = MapToReview(revDto);

					repository.Update(id, review);
					return StatusCode(StatusCodes.Status204NoContent);
				}
				return BadRequest("Invalied data");
			}
			return BadRequest(ModelState);
		}
		[HttpDelete("Delete/{id}")]
		public IActionResult Delete(int id)
		{
			Review deptId = repository.GetById(id);
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
		internal Review MapToReview(ReviewDto RevDto)
		{
			return new Review
			{
				ReviewName = RevDto.ReviewName,
				Content = RevDto.Content,
				ProductId = RevDto.ProductId

			};
		}
	}
}
