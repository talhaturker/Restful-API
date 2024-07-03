using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyRestApi.Models;
using MyRestApi.Services;
using System.Collections.Generic;
using System.Linq;

namespace MyRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SparePartsController : ControllerBase
    {
        private readonly SparePartService _sparePartService;

        public SparePartsController(SparePartService sparePartService)
        {
            _sparePartService = sparePartService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_sparePartService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var sparePart = _sparePartService.GetById(id);
            if (sparePart == null)
                return NotFound(new { error = "Spare part not found" });

            return Ok(sparePart);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] SparePart sparePartFromBody, [FromQuery] SparePart sparePartFromQuery)
        {
            var sparePart = sparePartFromBody ?? sparePartFromQuery;

            if (sparePart == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            _sparePartService.Add(sparePart);
            return CreatedAtAction(nameof(GetById), new { id = sparePart.Id }, sparePart);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SparePart sparePart)
        {
            if (sparePart == null || sparePart.Id != id || !ModelState.IsValid)
                return BadRequest(ModelState);

            var existingSparePart = _sparePartService.GetById(id);
            if (existingSparePart == null)
                return NotFound(new { error = "Spare part not found" });

            _sparePartService.Update(sparePart);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<SparePart> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest(new { error = "Patch document is null" });

            var existingSparePart = _sparePartService.GetById(id);
            if (existingSparePart == null)
                return NotFound(new { error = "Spare part not found" });

            patchDoc.ApplyTo(existingSparePart, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _sparePartService.Update(existingSparePart);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var sparePart = _sparePartService.GetById(id);
            if (sparePart == null)
                return NotFound(new { error = "Spare part not found" });

            _sparePartService.Delete(id);
            return NoContent();
        }

        [HttpGet("list")]
        public IActionResult List([FromQuery] string name, [FromQuery] string sortBy)
        {
            var spareParts = _sparePartService.GetAll();

            if (!string.IsNullOrEmpty(name))
            {
                spareParts = spareParts.Where(sp => sp.Name.Contains(name)).ToList();
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                spareParts = sortBy.ToLower() switch
                {
                    "name" => spareParts.OrderBy(sp => sp.Name).ToList(),
                    "price" => spareParts.OrderBy(sp => sp.Price).ToList(),
                    _ => spareParts
                };
            }

            return Ok(spareParts);
        }
    }
}
