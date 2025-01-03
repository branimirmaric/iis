using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zadatak06.Model;
using Zadatak06.Service;

namespace Zadatak06.Controllers
{
    [Route("api/cartoon")]
    [ApiController]
    public class CartoonController : ControllerBase
    {
        private readonly CartoonService _service;

        public CartoonController(CartoonService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var cartoons = _service.GetAll();
                return Ok(cartoons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            try
            {
                var cartoon = _service.GetByName(name);
                if (cartoon == null)
                    return NotFound($"Cartoon with name '{name}' not found.");

                return Ok(cartoon);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(Cartoon cartoon)
        {
            try
            {
                _service.Add(cartoon);
                return Ok(cartoon);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPut("{name}")]
        public IActionResult Update(string name, Cartoon cartoon)
        {
            try
            {
                var existingCartoon = _service.GetByName(name);
                if (existingCartoon == null)
                    return NotFound($"Cartoon with name '{name}' not found.");

                _service.Update(name, cartoon);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            try
            {
                var existingCartoon = _service.GetByName(name);
                if (existingCartoon == null)
                    return NotFound($"Cartoon with name '{name}' not found.");

                _service.Delete(name);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
