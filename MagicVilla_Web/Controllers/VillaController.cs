using AutoMapper;
using MagicVilla_Web.Dto;
using MagicVilla_Web.DTO;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services;
using MagicVilla_Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Threading.Tasks.Dataflow;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MagicVilla_Web.Controllers
{
    public class VillaController : Controller
    {
        readonly IVillaService _service;
        readonly IMapper _mapper;
        public VillaController(IVillaService service , IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [Authorize("Admin")]
        public async Task<IActionResult> Index()
        {
            List<VillaDto>? list = new();
            // TODO : do not forget to specify the number of villa that will apear in the index home paage 
            APIResponse response = await _service.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result)!);
            }
            return View(list);
        }
        public async Task<IActionResult> BrowsVilla(int currPage)
        {
            List<VillaDto>? list = new();
            // TODO : do not forget to specify the number of villa that will apear in the index home paage 
            APIResponse response = await _service.GetAllAsync<APIResponse>(currPage, Util.CommonValues.PageSize);
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result)!);
            }
            TempData["PageNo"] = currPage;
            return View(list);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> VillaPreview(int? vId)
        {
            if(vId == null)
            {
                TempData["Error"] = "Must Be Valid Id";
                return RedirectToAction("Index", "Home");
            }
            APIResponse response = await ((VillaService)_service).GetVillaPreviewById<APIResponse>(vId.Value, HttpContext.Session.GetString(StaticUtil.TokenName));
            if(response == null || !response.IsSuccess || response.Result == null)
            {
                TempData["Error"] = "Cannot Display This Villa ";
                return RedirectToAction("Index", "Home");
            }
            VillaPreviewDto dto = JsonConvert.DeserializeObject<VillaPreviewDto>(Convert.ToString(response.Result)!)!;
            return View(dto);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult CreateVilla()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(VillaCreateDto villa)
        {
            if (ModelState.IsValid)
            {
                APIResponse response = await _service.CreateAsync<APIResponse>(villa , HttpContext.Session.GetString(StaticUtil.TokenName));
                if (response != null && response.IsSuccess)
                {
                    TempData["Success"] = "Villa Created Successfully!";
                    return RedirectToAction("Index");
                }
            }
            return View(villa);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateVilla(int id)
        {
            APIResponse response = await _service.GetByIdAsync<APIResponse>(id , HttpContext.Session.GetString(StaticUtil.TokenName));
            if (response == null || !response.IsSuccess)
            {
                return BadRequest();
            }
            
            VillaDto villaDto = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Result)!)!;
            return View(_mapper.Map<VillaUpdateDto>(villaDto));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaUpdateDto villa)
        {
            if (ModelState.IsValid)
            {
                APIResponse response = await _service.UpdateAsync<APIResponse>(villa.Id, villa , HttpContext.Session.GetString(StaticUtil.TokenName));
                if (response != null && response.IsSuccess)
                {
                    TempData["Success"] = "Villa Updated Successfully!";
                    return RedirectToAction("Index");
                }
            }
            return View(villa);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            APIResponse response = await _service.DeleteAsync<APIResponse>(id, HttpContext.Session.GetString(StaticUtil.TokenName));
            if (response == null || !response.IsSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult SellVilla()
        {
            return View();
        }
        [Authorize] 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SellVilla(VillaCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                // create the image url 
                var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                var filename = Guid.NewGuid().ToString() + Path.GetExtension(dto.UploadedImage.FileName);
                var fullName =  Path.Combine(rootPath, filename);
                dto.ImageUrl = filename;
                string? primarySuid = User.FindFirst(ClaimTypes.Sid)?.Value;
                dto.OwnerId = Convert.ToInt32(primarySuid);
                APIResponse response = await _service.CreateAsync<APIResponse>(dto, HttpContext.Session.GetString(StaticUtil.TokenName));
                if (response != null && response.IsSuccess && response.Result != null)
                {
                    using (var stream = new FileStream(fullName, FileMode.Create))
                    {
                        await dto.UploadedImage.CopyToAsync(stream);
                    }
                    TempData["VillaId"] = Convert.ToString(response.Result);
                    return View("ContinueSubmittingVilla");
                }
            }
            return View(dto);
        }
        public IActionResult TestView()
        {
            TempData["VillaId"] = "1";
            return View("ContinueSubmittingVilla");
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinishSubmittingVilla([FromForm] VillaAppointmentAndImagesCreateVM vm)
        {
            if (vm == null)
            {
                return Json(new { Success = false, Error = new string[] { "All Field Are Required" } });
            }
            if (vm.Images == null || vm.Images.Count == 0)
            {
                return Json(new { Success = false, Error = new string[] { "You Have To Upload Three Images ONLY!" } });
            }
            if (vm.Appointments == null || vm.Appointments.Count == 0)
            {
                return Json(new { Success = false, Error = new string[] { "You Have To Upload At Least One Appointment" } });
            }
            Util.ViewModelsPropertiesAdjustment.AppointmentImagesVmAdjustment(vm);
            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
            var images = vm.Images; // need this to serialize the images after inserting it in the database .
            vm.Images = null; // useless to transmit it (a lot of bytes) .
            try
            {
                APIResponse response = await ((VillaService)_service).CreateAsync<APIResponse>(vm, HttpContext.Session.GetString(StaticUtil.TokenName));
                if (response != null && response.IsSuccess && response.Result != null)
                {
                    for (int i = 0; i < images.Count(); i++)
                    {
                        var fullName = Path.Combine(rootPath, vm.ImageCreateDtos[i].ImageUrl);
                        using (var stream = new FileStream(fullName, FileMode.Create))
                        {
                            await images[i].CopyToAsync(stream);
                        }
                    }
                    TempData["Success"] = "Villa Created Successfully!";
                    return Json(new { Success = true, Error = new string[] { } });
                }
                return Json(new { Success = false, Error = new string[] { "Fail" } });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Error = new string[] { ex.Message } });
            }
        }
        //[HttpPost]
        //public IActionResult FinishSubmittingVillaTest([FromForm] VillaAppointmentAndImagesCreateVM vm)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Json(new { Success = false, Error = ModelState.Values.Select(ve => ve.Errors.Select(er => er.ErrorMessage)) });
        //    }
        //    Util.ViewModelsPropertiesAdjustment.AppointmentImagesVmAdjustment(vm);
        //    return Json(new { Success = true, data = vm.ImageCreateDtos.Count() });
        //}
    }
}
