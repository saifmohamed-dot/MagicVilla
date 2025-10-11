using AutoMapper;
using MagicVilla_Web.Dto;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Specialized;

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
        [Authorize(Roles ="Admin")]
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
        public async Task<IActionResult> VillaPreview()
        {
            return View();
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
        
    }
}
