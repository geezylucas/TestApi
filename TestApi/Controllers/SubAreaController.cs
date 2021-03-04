using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Services;
using BusinessLogic.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubAreaController : ControllerBase
    {
        private readonly ISubAreaService _subAreaService;

        public SubAreaController(ISubAreaService subAreaService)
        {
            _subAreaService = subAreaService;
        }

        // GET: api/subarea
        [HttpGet]
        public async Task<ActionResult<List<SubAreaDTO>>> GetSubAreas()
        {
            return Ok(await _subAreaService.GetSubAreas());
        }

        // GET: api/subarea/1
        [HttpGet("{id}")]
        public async Task<ActionResult<SubAreaDTO>> GetSubArea(int id)
        {
            var subArea = await _subAreaService.GetSubArea(id);

            if (subArea == null)
            {
                return NotFound();
            }

            return Ok(subArea);
        }


        // POST: api/subarea
        [HttpPost]
        public async Task<ActionResult<SubAreaDTO>> PostSubArea(SubAreaDTO subArea)
        {
            var classBaseSubAreaDTO = await _subAreaService.InsertSubArea(subArea);

            if (classBaseSubAreaDTO.AnyClass == null)
            {
                return BadRequest(classBaseSubAreaDTO.Error);
            }

            return CreatedAtAction("GetSubArea", new { id = classBaseSubAreaDTO.AnyClass.Id }, classBaseSubAreaDTO.AnyClass);
        }

        // PUT: api/subarea/1
        [HttpPut("{id}")]
        public async Task<ActionResult> PutSubArea(int id, SubAreaDTO subArea)
        {
            if (id != subArea.Id)
            {
                return BadRequest();
            }

            string result = await _subAreaService.EditSubArea(subArea);

            if (!result.Equals("OK"))
            {
                return BadRequest(result);
            }

            return NoContent();
        }

        // DELETE: api/subarea/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubArea(int id)
        {
            var subArea = await _subAreaService.GetSubArea(id);

            if (subArea == null)
            {
                return NotFound();
            }

            string result = await _subAreaService.RemoveSubArea(id, subArea.Name);

            if (!result.Equals("OK"))
            {
                return BadRequest(result);
            }

            return NoContent();
        }
    }
}
