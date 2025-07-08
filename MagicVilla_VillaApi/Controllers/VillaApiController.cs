using AutoMapper;
using MagicVilla_VillaApi.DataStore;
using MagicVilla_VillaApi.Dto;
using MagicVilla_VillaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MagicVilla_VillaApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]/")]
    public class VillaApiController : ControllerBase
    {
        readonly ILogger<VillaApiController> _logger;
        readonly VillasDBContext _dbContext;
        readonly IMapper _mapper;
        public VillaApiController(ILogger<VillaApiController> logger 
            , VillasDBContext dbContext
            , IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Get All Villas Values");
            return Ok(await _dbContext.Villas.ToListAsync());
        }
        [HttpGet("{id:int}" , Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto>> GetVilla(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Villa? value = await _dbContext.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            VillaDto villaDto = _mapper.Map<VillaDto>(value);
            return Ok(villaDto);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody] VillaCreateDto villa)
        {
            if (villa == null)
            {
                return BadRequest();
            }
            Villa vi = _mapper.Map<Villa>(villa);
            _dbContext.Villas.Add(vi);
            vi.DateCreated = DateTime.Now;
            vi.DateUpdated = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            int lastId = _dbContext.Villas.OrderByDescending(v => v.Id)
                .Select(v => v.Id)
                .First();
            _logger.LogInformation($"Create Villa {lastId}");
            return CreatedAtRoute("GetVilla", new { id = lastId }, _mapper.Map<VillaDto>(villa));
        }
        [HttpDelete("{id:int}" , Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int? id)
        {
            if (id == 0 || id == null)
            {
                return BadRequest(); // 400 bad
            }
            Villa? villa = await _dbContext.Villas.Where(v => v.Id == id).FirstOrDefaultAsync();
            if (villa == null)
            {
                return NotFound(); // 404 not 
            }
            _dbContext.Villas.Remove(villa);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateVilla(int? id, [FromBody] VillaUpdateDto villa)
        {
            if (id == 0 || id == null || id != villa.Id)
            {
                return BadRequest(); // 400 <-
            }
            Villa? vill = await _dbContext.Villas.AsNoTracking().Where(v => v.Id == id).FirstOrDefaultAsync();
            if (vill  == null)
            {
                return NotFound(); // 404 <-
            }
            vill = _mapper.Map<Villa>(villa);
            vill.DateUpdated = DateTime.Now;
            _dbContext.Villas.Update(vill);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
