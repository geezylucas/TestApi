using Microsoft.AspNetCore.Mvc;
using ConnectData.Services;
using ConnectData.DTOs;
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

        //GET: api/area
        [HttpGet]
        public async Task<ActionResult<List<AreaDTO>>> GetAreas()
        {
            return Ok(await _areaService.GetAreas());
        }
    }
}
