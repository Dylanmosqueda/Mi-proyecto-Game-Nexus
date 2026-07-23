using Microsoft.AspNetCore.Mvc;
using Game_Nexus.API.Models;
using Game_Nexus.API.Services;
using Game_Nexus.API.Strategies;

namespace Game_Nexus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] string? genero, [FromQuery] string? orderBy)
        {
            ISortingStrategy? strategy = orderBy?.ToLower() switch
            {
                "title" => new SortByTitleStrategy(),
                "rating" => new SortByCalificacionStrategy(),
                _ => null
            };

            var items = _itemService.GetAll(genero, strategy);
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _itemService.GetById(id);
            if (item == null)
            {
                return NotFound(new { error = "Videojuego no encontrado" });
            }
            return Ok(item);
        }
    }
}
