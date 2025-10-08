using AutoMapper;
using MagicVilla_VillaApi.Dto;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaApi.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class VillaNumberController : ControllerBase
    {
        readonly IVillaNumberRepository _villaNumberRepository;
        readonly IVillaRepository _villaRepository;
        readonly ILogger<VillaNumberController> _logger;
        readonly IMapper _mapper;
        public VillaNumberController(IVillaNumberRepository villaNumberRepo
            , IVillaRepository villaRepository , ILogger<VillaNumberController> logger, IMapper mapper)
        {
            _villaNumberRepository = villaNumberRepo;
            _villaRepository = villaRepository;
            _logger = logger;
            _mapper = mapper;
         }
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetNumbers()
        {
            APIResponse apiResponse = new APIResponse();
            try
            {
                var res = await _villaNumberRepository.GetAllAsync("Villa");
                apiResponse.PopulateOnSuccess(System.Net.HttpStatusCode.OK
                            , _mapper.Map<IEnumerable<VillaNumberDto>>(res));
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.PopulateOnFail(System.Net.HttpStatusCode.InternalServerError);
                apiResponse.Errors.Add(ex.Message);
                return BadRequest(apiResponse);
            }
        }
        //[Authorize]
        [HttpGet("{id:int}", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int? id)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (id == null)
                {
                    response.PopulateOnFail(System.Net.HttpStatusCode.BadRequest);
                    response.Errors.Add("ID Is Mandatory For This EndPoint");
                    return BadRequest(response);
                }

                VillaNumber villano = await _villaNumberRepository.GetByIdAsync(id.Value , tracking:false , incincludeProperties:"Villa");
                if (villano == null)
                {
                    response.PopulateOnFail(System.Net.HttpStatusCode.NotFound);
                    response.Errors.Add("ID Not Found");
                    return NotFound(response);
                }
                else
                {
                    response.PopulateOnSuccess(System.Net.HttpStatusCode.OK
                        , _mapper.Map<VillaNumberDto>(villano));
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.PopulateOnFail(System.Net.HttpStatusCode.BadRequest);
                response.Errors.Add(ex.Message);
                return BadRequest(response);
            }
        }
        [Authorize]
        [HttpPost(Name = "CreateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDto dto)
        {
            APIResponse response = new APIResponse();
            try
            {
                var villaNo = await _villaNumberRepository.GetByIdAsync(dto.Id);
                // check if the id already found
                if (villaNo != null)
                {
                    response.PopulateOnFail(System.Net.HttpStatusCode.Conflict);
                    response.Errors.Add("ID Already Exists!");
                    return Conflict(response);
                }
                // check for referencial integrity ->
                else if (await _villaRepository.GetByIdAsync(dto.VillaId) == null)
                {
                    response.PopulateOnFail(System.Net.HttpStatusCode.BadRequest);
                    response.Errors.Add("VillaId Not Found!");
                    return BadRequest(response);
                }
                else
                {
                    VillaNumber vn = _mapper.Map<VillaNumber>(dto);
                    vn.DateCreated = DateTime.Now;
                    vn.DateUpdated = DateTime.Now;
                    await _villaNumberRepository.CreateAsync(vn);
                    response.PopulateOnSuccess(System.Net.HttpStatusCode.Created, _mapper.Map<VillaNumberDto>(vn));
                    return CreatedAtRoute("GetVillaNumber", new { id = dto.Id}, response);
                }
            }
            catch (Exception ex)
            {
                response.PopulateOnFail(System.Net.HttpStatusCode.BadRequest);
                response.Errors.Add(ex.Message);
                return BadRequest(response);
            }
        }
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int? id)
        {
            APIResponse response = new APIResponse();
            if (id == null)
            {
                response.PopulateOnFail(System.Net.HttpStatusCode.BadRequest);
                response.Errors.Add("Id Is Mandatory");
                return BadRequest(response);
            }
            try
            {
                var villano = await _villaNumberRepository.GetByIdAsync(id.Value , tracking:false , incincludeProperties:"Villa");
                if (villano == null)
                {
                    response.PopulateOnFail(System.Net.HttpStatusCode.NotFound);
                    response.Errors.Add("VillaNumber(id) To remove not found!");
                    Console.WriteLine($"VillaNumber Not Found {id.Value}");
                    return NotFound(response);
                }
                else
                {
                    await _villaNumberRepository.DeleteAsync(villano);
                    response.PopulateOnSuccess(System.Net.HttpStatusCode.NoContent , null);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.PopulateOnFail(System.Net.HttpStatusCode.BadRequest);
                response.Errors.Add(ex.Message);
                return BadRequest(response);
            }
        }
        [Authorize]
        [HttpPut("{id:int}" , Name ="UpdateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int? id , [FromBody]VillaNumberUpdateDto dto)
        {
            APIResponse response = new APIResponse();
            if (id == null)
            {
                response.PopulateOnFail(System.Net.HttpStatusCode.BadRequest);
                response.Errors.Add("Id Is Mandatory");
                return BadRequest(response);
            }
            try
            {
                var villano = await _villaNumberRepository.GetByIdAsync(id.Value , false);
                if (villano == null)
                {
                    response.PopulateOnFail(System.Net.HttpStatusCode.NotFound);
                    response.Errors.Add("VillaNumber(id) To update not found!");
                    return NotFound(response);
                }
                else
                {
                    // check if the client want to change the referencing VillaId ->
                    if (dto.VillaId != villano.VillaId)
                    {
                        // check if the new id is found in villa table 
                        if (await _villaRepository.GetByIdAsync(dto.VillaId) == null)
                        {
                            response.PopulateOnFail(System.Net.HttpStatusCode.NotFound);
                            response.Errors.Add("The VillaId You Want To Update Not Found");
                            return NotFound(response);
                        }
                    }
                    var updatedVillaNo = _mapper.Map<VillaNumber>(dto);
                    await _villaNumberRepository.Update(updatedVillaNo);
                    response.PopulateOnSuccess(System.Net.HttpStatusCode.OK, _mapper.Map<VillaNumberDto>(dto));
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.PopulateOnFail(System.Net.HttpStatusCode.BadRequest);
                response.Errors.Add(ex.Message);
                return BadRequest(response);
            }
        }
    }
}
