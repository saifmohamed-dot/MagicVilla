using AutoMapper;
using MagicVilla_VillaApi.Dto;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Repository;
using MagicVilla_VillaApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestAppointmentController : ControllerBase
    {
        readonly IMapper _mapper;
        readonly IRequestedAppointmentRepository _requestedAppointmentRepository;
        readonly IAppointmentRepository _appointmentRepository;
        readonly APIResponse _aPIResponse;
        public RequestAppointmentController(IMapper mapper , IRequestedAppointmentRepository requested , IAppointmentRepository appointmentRepository)
        {
            _mapper = mapper;
            _requestedAppointmentRepository = requested;
            _aPIResponse = new APIResponse();
            _appointmentRepository = appointmentRepository;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<APIResponse>> GetRequestedAppointments()
        {
            IEnumerable<RequestedAppointmentDto> reqOpps = _mapper
                            .Map<IEnumerable<RequestedAppointmentDto>>(await _requestedAppointmentRepository.GetAllAsync());
            _aPIResponse.PopulateOnSuccess(System.Net.HttpStatusCode.OK, _aPIResponse);
            return Ok(reqOpps);
        }
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<ActionResult<APIResponse>> GetRequestedAppointment(int? id)
        {
            if (id == null)
            {
                _aPIResponse.PopulateOnFail(System.Net.HttpStatusCode.BadRequest, ["Id Is Null"]);
                return BadRequest(_aPIResponse);
            }
            RequestedAppointment requestedAppointment = await _requestedAppointmentRepository.GetByIdAsync(id.Value);
            if (requestedAppointment == null)
            {
                _aPIResponse.PopulateOnFail(System.Net.HttpStatusCode.NotFound, ["Appointment Request Not Found"]);
                return BadRequest(_aPIResponse);
            }
            _aPIResponse.PopulateOnSuccess(System.Net.HttpStatusCode.OK
                    , _mapper.Map<RequestedAppointmentDto>(requestedAppointment));
            return Ok(requestedAppointment);
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<APIResponse>> Create(RequestedAppointmentCreateDto dto)
        {
            // TODO : Update The Relationship To Be (many-to-many)
            // check if the Requested Appointment Is Already Found (ClientId , AppointmentId) for now
            var req = await _requestedAppointmentRepository
                                        .FindAllAsync(ap => ap.AppointmentId == dto.AppointmentId
                                         && ap.ClientId == dto.ClientId);
            if (req != null && req.Any())
            {
                _aPIResponse.PopulateOnFail(System.Net.HttpStatusCode.Conflict, ["This Request Already Registred !"]);
                return Conflict(_aPIResponse);
            }

            Appointment appoint = await _appointmentRepository.GetByIdAsync(dto.AppointmentId, 
                    tracking: false, incincludeProperties: "Villa");
            // check if the appointment found -> 
            if (appoint == null)
            {
                _aPIResponse.PopulateOnFail(System.Net.HttpStatusCode.NotFound, ["Appointment Not Found"]);
                return NotFound(_aPIResponse);
            }
            // check if the client that requested the appointment is owner
            // this is not allowed (Villa Onwner requesting an appointment from itself.)
            if (appoint.Villa.OwnerId == dto.ClientId)
            {
                _aPIResponse.PopulateOnFail(System.Net.HttpStatusCode.BadRequest, ["Owner Can't be Client his/her Villa"]);
                return BadRequest(_aPIResponse);
            }
            int id = await _requestedAppointmentRepository.CreateAsync(_mapper.Map<RequestedAppointment>(dto));
            _aPIResponse.PopulateOnSuccess(System.Net.HttpStatusCode.Created, $"Created With ID : {id}");
            return Ok(_aPIResponse);
        }
        [Authorize]
        [HttpGet("AppointmentRequests/{id:int}")]
        public async Task<ActionResult<APIResponse>> GetRequests(int? id)
        {
            if (id == null)
            {
                _aPIResponse.PopulateOnFail(System.Net.HttpStatusCode.BadRequest, ["Appointment Id Can not Be Null"]);
                return BadRequest(_aPIResponse);
            }
            try
            {
                var requests = _mapper.Map<IEnumerable<RequestedAppointmentDto>>(await _requestedAppointmentRepository.FindAllAsync(req => req.AppointmentId == id, "Client"));
                _aPIResponse.PopulateOnSuccess(System.Net.HttpStatusCode.OK, requests);
                return Ok(_aPIResponse);
            }
            catch (Exception ex)
            {
                _aPIResponse.PopulateOnFail(System.Net.HttpStatusCode.BadRequest, [ex.Message]);
                return BadRequest(_aPIResponse);
            }
        }
        [HttpPost("Batch")]
        [Authorize]
        public async Task<ActionResult<APIResponse>> CreateBatch(RequestListVM vm)
        {
            try
            {
                await _requestedAppointmentRepository.BulkInsertRequstedAppointments(_mapper.Map<IEnumerable<RequestedAppointment>>(vm.Requests));
                _aPIResponse.PopulateOnSuccess(System.Net.HttpStatusCode.Created, "Requests Created Successfully !");
                return Ok(_aPIResponse);
            }
            catch (Exception ex)
            {
                _aPIResponse.PopulateOnFail(System.Net.HttpStatusCode.BadRequest, [ex.Message]);
                return BadRequest(_aPIResponse);
            }
        }

    }
}
