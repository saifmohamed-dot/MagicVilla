using AutoMapper;
using MagicVilla_Web.Dto;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services;
using MagicVilla_Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MagicVilla_Web.Controllers
{
    public class VillaNumberController : Controller
    {
        readonly IVillaNumberServices _service;
        readonly IVillaService _villaService;
        readonly IMapper _mapper;
        public VillaNumberController(IVillaNumberServices service, IMapper mapper, IVillaService villaService)
        {
            _service = service;
            _mapper = mapper;
            _villaService = villaService;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            APIResponse response = await _service.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticUtil.TokenName));
            List<VillaNumberDto> villaNumbers = new List<VillaNumberDto>();
            if (response != null && response.IsSuccess)
            {
                villaNumbers = JsonConvert.DeserializeObject<List<VillaNumberDto>>(Convert.ToString(response.Result)!)!;
            }
            return View(villaNumbers);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateVillaNumber()
        {
            VillaNumberCreateVM vm = new VillaNumberCreateVM();
            vm.VillaList = await GetVillaIdListAsync();
            return View(vm);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateVM createVM)
        {
            if (ModelState.IsValid)
            {
                APIResponse response = await _service.CreateAsync<APIResponse>(createVM.villaNumberCreateDto, HttpContext.Session.GetString(StaticUtil.TokenName));
                if (response != null && response.IsSuccess)
                {
                    TempData["Success"] = "Villa Number Created Successfully!";
                    return RedirectToAction("Index");
                }
            }
            return View(createVM);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteVillaNumber(int id)
        {
            APIResponse response = await _service.DeleteAsync<APIResponse>(id, HttpContext.Session.GetString(StaticUtil.TokenName));
            if (response == null || response.IsSuccess == false)
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateVillaNumber(int id)
        {
            APIResponse getCheckresponse = await _service.GetByIdAsync<APIResponse>(id);
            if (getCheckresponse == null || getCheckresponse.IsSuccess == false)
            {
                
                return BadRequest();
            }
            VillaNumberDto dto = JsonConvert.DeserializeObject<VillaNumberDto>(Convert.ToString(getCheckresponse.Result)!)!;
            VillaNumberUpdateVM updateVM = new VillaNumberUpdateVM();
            updateVM.VillaList = await GetVillaIdListAsync();
            return View(updateVM);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateVM updateVM)
        {
            APIResponse updateCheckResponse = await _service.UpdateAsync<APIResponse>(updateVM.villaNumberUpdateDto.Id
                , updateVM.villaNumberUpdateDto, HttpContext.Session.GetString(StaticUtil.TokenName));
            if (updateCheckResponse ==  null || updateCheckResponse.IsSuccess == false) { return BadRequest(); }
            TempData["Success"] = "Villa Number Updated Successfully!";
            return RedirectToAction("Index");
        }
        private async Task<IEnumerable<SelectListItem>> GetVillaIdListAsync()
        {
            APIResponse response = await _villaService.GetAllAsync<APIResponse>();
            if (response == null || response.IsSuccess == false) { return new List<SelectListItem>(); }
            List<VillaDto> villaDtos = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result)!)!;
            return villaDtos.Select(v => new SelectListItem { Text = v.Name, Value = v.Id.ToString() });
        }
    }
}
