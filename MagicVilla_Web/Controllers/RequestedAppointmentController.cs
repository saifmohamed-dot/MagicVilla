using MagicVilla_Web.DTO;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services;
using MagicVilla_Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_Web.Controllers
{
    public class RequestedAppointmentController : Controller
    {
        readonly IRequestedAppointmentService _service;
        APIResponse _response;
        public RequestedAppointmentController(IRequestedAppointmentService service)
        {
            _service = service;
            _response = new APIResponse();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RequestForAppointments([FromForm] RequestListVM vm)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
            }
            _response = await _service.CreateBatch<APIResponse>(vm, HttpContext.Session.GetString(StaticUtil.TokenName));
            if (_response == null || !_response.IsSuccess || _response.Result == null)
            {
                return Json(new { success = false, errors = _response == null ? ["Null Response"] : _response.Errors });
            }
            return Json(new { success = true });
        }
    }
}
