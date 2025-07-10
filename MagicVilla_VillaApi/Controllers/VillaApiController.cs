using AutoMapper;
using MagicVilla_VillaApi.DataStore;
using MagicVilla_VillaApi.Dto;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]/")]
    public class VillaApiController : ControllerBase
    {
        readonly ILogger<VillaApiController> _logger;
        readonly VillasDBContext _dbContext;
        readonly IMapper _mapper;
        readonly IVillaRepository _repository;
        readonly APIResponse _response;
        public VillaApiController(ILogger<VillaApiController> logger 
            , VillasDBContext dbContext
            , IMapper mapper
            , IVillaRepository repository)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
            _repository = repository;
            _response = new();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            _logger.LogInformation("Get All Villas Values");
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            _response.Result = await _repository.GetAllAsync();
            return Ok(_response);
        }
        [HttpGet("{id:int}" , Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVilla(int? id)
        {
            if (id == null)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            Villa? value = await _repository.GetByIdAsync(id.Value);
            if (value == null)
            {
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            _response.Result = _mapper.Map<VillaDto>(value);
            return Ok(_response);
        }
        // start from here ->
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody]VillaCreateDto villa)
        {
            if (villa == null)
            {
                _response.PopulateOnFail(System.Net.HttpStatusCode.BadRequest);
                return BadRequest(villa);
            }
            Villa vi = _mapper.Map<Villa>(villa);
            vi.DateCreated = DateTime.Now;
            vi.DateUpdated = DateTime.Now;
            var lastId = await _repository.CreateAsync(vi);
            vi.Id = lastId;
            _response.PopulateOnSuccess(System.Net.HttpStatusCode.Created, vi);
            _logger.LogInformation($"Create Villa {lastId}");
            return CreatedAtRoute("GetVilla", new { id = lastId }, _response);
        }
        [HttpDelete("{id:int}" , Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int? id)
        {
            if (id == 0 || id == null)
            {
                _response.PopulateOnFail(System.Net.HttpStatusCode.BadRequest);
                return BadRequest(_response); // 400 bad
            }
            Villa? villa = await _repository.GetByIdAsync(id.Value);
            if (villa == null)
            {
                _response.PopulateOnFail(System.Net.HttpStatusCode.NotFound);
                return NotFound(); // 404 not 
            }
            await _repository.DeleteAsync(villa);
            return NoContent();
        }
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int? id, [FromBody] VillaUpdateDto villa)
        {
            if (id == 0 || id == null || id != villa.Id)
            {
                _response.PopulateOnFail(System.Net.HttpStatusCode.BadRequest);
                return BadRequest(_response); // 400 <-
            }
            Villa? vill = await _repository.GetByIdAsync(id.Value, false);
            if (vill  == null)
            {
                _response.PopulateOnFail(System.Net.HttpStatusCode.NotFound);
                return NotFound(_response); // 404 <-
            }
            //vill = _mapper.Map<Villa>(villa);
            //vill.DateUpdated = DateTime.Now;
            //_dbContext.Villas.Update(vill);
            //await _dbContext.SaveChangesAsync();
            vill = _mapper.Map<Villa>(villa);
            await _repository.Update(vill);
            return NoContent();
        }
    }
}
