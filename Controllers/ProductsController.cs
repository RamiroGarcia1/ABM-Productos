using Microsoft.AspNetCore.Mvc;
using Práctica_Controller_Service_Repository.Models.DTO.Reponses;
using Práctica_Controller_Service_Repository.Models.DTO.Requests;
using Práctica_Controller_Service_Repository.Services.Interfaces;

namespace Práctica_Controller_Service_Repository.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _service.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _service.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductForCreateDto dto)
        {
            try
            {
                var created = _service.CreateProduct(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductForUpdateDto dto)
        {
            var existing = _service.GetProductById(id);
            if (existing == null)
            {
                return NotFound();
            }
            _service.UpdateProduct(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _service.GetProductById(id);
            if (existing == null)
            {
                return NotFound();
            }
            _service.DeleteProduct(id);
            return NoContent();
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string name)
        {
            var products = _service.SearchProductsByName(name);
            return Ok(products);
        }

        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            var stats = _service.GetStats();
            return Ok(stats);
        }
    }
}