using Microsoft.AspNetCore.Mvc;
using Game_Nexus.API.Services;

namespace Game_Nexus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET por lista: api/reviews
        [HttpGet]
        public IActionResult GetAll()
        {
            var reviews = _reviewService.GetAll();
            return Ok(reviews);
        }

        // GET por id: api/reviews/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var review = _reviewService.GetById(id);
            if (review == null)
            {
                return NotFound(new { error = "Reseña no encontrada" });
            }
            return Ok(review);
        }
    }
}