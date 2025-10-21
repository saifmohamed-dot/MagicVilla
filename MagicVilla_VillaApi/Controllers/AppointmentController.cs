using AutoMapper;
using MagicVilla_VillaApi.Dto;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;
using System.Security.Claims;

namespace MagicVilla_VillaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        readonly IAppointmentRepository _appointmentRepo;
        readonly IVillaRepository _villaRepository;
        readonly IMapper _mapper;
        readonly APIResponse _aPIResponse;
        public AppointmentController(IAppointmentRepository appointmentRepository , IMapper mapper , IVillaRepository villaRepository)
        {
            _appointmentRepo = appointmentRepository;
            _mapper = mapper;
            _aPIResponse = new APIResponse();
            _villaRepository = villaRepository;
        }
        [Authorize]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)] // if Appointment found 
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if id null
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetAppointment(int? id)
        {
            if (id == null)
            {
                _aPIResponse.PopulateOnFail(HttpStatusCode.BadRequest , ["Id Is Null"]);
                return BadRequest(_aPIResponse);
            }
            Appointment appont = await _appointmentRepo.GetByIdAsync(id.Value, tracking: false
                                , incincludeProperties: "Villa");
            if (appont == null)
            {
                _aPIResponse.PopulateOnFail(HttpStatusCode.NotFound, ["Appointment Not Found"]);
                return NotFound(_aPIResponse);
            }
            _aPIResponse.PopulateOnSuccess(HttpStatusCode.OK, _mapper.Map<AppointmentDto>(appont));
            return Ok(_aPIResponse);
        }
        [Authorize]
        [HttpGet("VillaAppointments/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if id null
        public async Task<ActionResult<APIResponse>> GetAppointmentByVilla(int? id)
        {
            if (id == null)
            {
                _aPIResponse.PopulateOnFail(HttpStatusCode.BadRequest, ["Id Is Null"]);
                return BadRequest(_aPIResponse);
            }
            IEnumerable<Appointment> appointments = await _appointmentRepo.FindAllAsync(ap => ap.VillaId == id);
            _aPIResponse.PopulateOnSuccess(HttpStatusCode.OK, _mapper.Map<IEnumerable<AppointmentDto>>(appointments));
            return Ok(_aPIResponse);
        }
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> GetAppointments()
        {
            List<AppointmentDto> appsDto = _mapper.Map<List<AppointmentDto>>(await _appointmentRepo.GetAllAsync(includeProperties: "Villa"));
            _aPIResponse.PopulateOnSuccess(System.Net.HttpStatusCode.OK, appsDto);
            return Ok(_aPIResponse);
        }
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)] // if created success 
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // villa not found
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] // if the user isn't authorized itself , or for appointment creation.
        public async Task<ActionResult<APIResponse>> Create(AppointmentCreateDto dto)
        {

            // think about who should create and not create an appointment 
            // and when / who it should be validated (here and/or mvc)
            // you could validate this by checking if the userid (found on the claims constructed by [Authorized] ) is the owner if it .
            // check if the villa already found 
            Villa villa = await _villaRepository.GetByIdAsync(dto.VillaId, tracking: false);
            if (villa == null)
            {
                _aPIResponse.PopulateOnFail(System.Net.HttpStatusCode.BadRequest , ["Villa Requested To Create Appointment ON Not Found"]);
                return BadRequest(_aPIResponse);
            }
            // check if the User Initiate the creation of an appointment is the owner 
            string? primarySuid = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            if (string.IsNullOrEmpty(primarySuid) || primarySuid != villa.OwnerId.ToString())
            {
                _aPIResponse.PopulateOnFail(System.Net.HttpStatusCode.Unauthorized, ["You are not the Villa Owner To Create Appointment ON it"]);
                return Unauthorized(_aPIResponse);
            }
            int appId = await _appointmentRepo.CreateAsync(_mapper.Map<Appointment>(dto));
            _aPIResponse.PopulateOnSuccess(HttpStatusCode.Created , appId);
            return Ok(_aPIResponse);
        }
    }
}
