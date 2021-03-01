using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Services;
using BusinessLogic.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;

        public AreaController(IAreaService areaService)
        {
            _areaService = areaService;
        }

        // GET: api/area
        [HttpGet]
        public async Task<ActionResult<List<AreaDTO>>> GetAreas()
        {
            return Ok(await _areaService.GetAreas());
        }

        // GET: api/area/1
        [HttpGet("{id}")]
        public async Task<ActionResult<AreaDTO>> GetArea(int id)
        {
            var area = await _areaService.GetArea(id);

            if (area == null)
            {
                return NotFound();
            }

            return Ok(area);
        }


        // POST: api/area
        [HttpPost]
        public async Task<ActionResult<AreaDTO>> PostArea(AreaDTO area)
        {
            var classBaseAreaDTO = await _areaService.InsertArea(area);

            if (classBaseAreaDTO.AnyClass == null)
            {
                return BadRequest(classBaseAreaDTO.Error);
            }

            return CreatedAtAction("GetArea", new { id = classBaseAreaDTO.AnyClass.Id }, classBaseAreaDTO.AnyClass);
        }

        // DELETE: api/area/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArea(int id)
        {
            var area = await _areaService.GetArea(id);

            if (area == null)
            {
                return NotFound();
            }

            string result = _areaService.RemoveArea(id, area.Name);

            if (!result.Equals("OK"))
            {
                return BadRequest(result);
            }

            return NoContent();
        }
    }
}
