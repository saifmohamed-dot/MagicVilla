using AutoMapper;
using MagicVilla_VillaApi.DataStore;
using MagicVilla_VillaApi.Dto;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Repository;
using MagicVilla_VillaApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace MagicVilla_VillaApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]/")]
    public class VillaApiController : ControllerBase
    {
        readonly ILogger<VillaApiController> _logger;
        readonly IMapper _mapper;
        readonly IVillaRepository _repository;
        readonly IRequestedAppointmentRepository _requestedAppointmentRepository;
        readonly APIResponse _response;
        public VillaApiController(ILogger<VillaApiController> logger 
            , IMapper mapper
            , IVillaRepository repository
            , IRequestedAppointmentRepository requestedAppointmentRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
            _response = new();
            _requestedAppointmentRepository = requestedAppointmentRepository;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetVillas([FromQuery] int? pageNo , [FromQuery] int? pageSize)
        {
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            if (pageNo == null || pageSize == null)
            {
                _response.Result = _mapper.Map<List<VillaDto>>(await _repository.GetAllAsync(0 , Util.CommonValues.IndexPreviewSize,false , includeProperties:"Owner"));
            }
            else
            {
                _response.Result = _mapper.Map<List<VillaDto>>(await _repository.GetAllAsync(pageNo.Value, pageSize.Value, false , includeProperties:"Owner"));
            }
            return Ok(_response);
        }
        
        
        [HttpGet("{id:int}" , Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVilla(int? id)
        {
            Console.WriteLine("Get Villa Id Called");
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
        [Authorize]
        [HttpGet("Vpreview/{id:int}")]
        public async Task<ActionResult<APIResponse>> GetVillaPreview(int? id)
        {
            if (id == null)
            {
                _response.PopulateOnFail(System.Net.HttpStatusCode.BadRequest, ["ID Can not Be Null"]);
                return BadRequest(_response);
            }
            string? primarySuid = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            Villa? v = await _repository.GetVillaPreviewQuery(id.Value, Convert.ToInt32(primarySuid));
            if(v == null)
            {
                _response.PopulateOnFail(System.Net.HttpStatusCode.NotFound, ["Villa Not Found"]);
                return NotFound(_response);
            }
            // TODO : Include The Requested Appointment.
            VillaPreviewDto preview = _mapper.Map<VillaPreviewDto>(v);
            _response.PopulateOnSuccess(System.Net.HttpStatusCode.OK, preview);
            return Ok(_response);
        }
        [Authorize]
        [HttpGet("OwnerV")]
        public async Task<ActionResult<APIResponse>> GetVillasByOwner()
        {
            int id = Convert.ToInt32(User.FindFirst(ClaimTypes.PrimarySid)?.Value);
            try
            {
                IEnumerable<OwnerVillaDto> vList = await _repository.GetOwnerVillas(id);
                _response.PopulateOnSuccess(System.Net.HttpStatusCode.OK, vList);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.PopulateOnFail(System.Net.HttpStatusCode.BadRequest , [ex.Message]);
                return BadRequest(_response);
            }
        }
        // start from here ->
        [Authorize]
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
            _response.PopulateOnSuccess(System.Net.HttpStatusCode.Created, vi.Id);
            _logger.LogInformation($"Create Villa {lastId}");
            //return CreatedAtRoute("GetVilla", new { id = lastId }, _response); we need the villa Id 
            return Ok(_response);
        }
        [Authorize]
        [HttpDelete("{id:int}" , Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
            // return NoContent(); cancelled cuz of the web application needed the response delete service.
            return Ok(_response);
        }
        [Authorize]
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
            vill = _mapper.Map<Villa>(villa);
            await _repository.Update(vill);
            // return NoContent(); cancelled cuz of the web application needed the response update service.
            return Ok(_response);
        }
        [Authorize]
        [HttpPost("ContinueSubmitting")]
        public async Task<ActionResult<APIResponse>> AddRelativeDataToVilla([FromBody] VillaAppointmentAndImagesCreateVM vm)
        {
            if(vm == null)
            {
                _response.PopulateOnFail(System.Net.HttpStatusCode.BadRequest, ["Create dto is null"]);
                return BadRequest(_response);
            }
            if(vm.ImageCreateDtos == null || vm.ImageCreateDtos.Count() != 3)
            {
                _response.PopulateOnFail(System.Net.HttpStatusCode.BadRequest, ["Only Three Images"]);
                return BadRequest(_response);
            }
            if (vm.Appointments == null || vm.Appointments.Count() == 0)
            {
                _response.PopulateOnFail(System.Net.HttpStatusCode.BadRequest, ["At Least One Appointment"]);
                return BadRequest(_response);
            }
            // TODO : don't forget to check if the userId is the owner of the villa that you need to do this operation on.
            try
            {
                await _repository.AddRelativeDataToVillaCreateAsync(vm);
                _response.PopulateOnSuccess(System.Net.HttpStatusCode.Created, $"Relative Created For VillaId : {vm.Appointments[0].VillaId}");
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.PopulateOnFail(System.Net.HttpStatusCode.BadRequest, [ex.Message]);
                return BadRequest(_response); 
            }
        }

    }
}
