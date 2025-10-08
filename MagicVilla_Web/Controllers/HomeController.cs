using MagicVilla_Web.Dto;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVillaService _service;
        public HomeController(ILogger<HomeController> logger , IVillaService service)
        {
            _logger = logger;
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<VillaDto>? list = new();
            APIResponse response = await _service.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result)!);
            }
            return View(list);
        }
        public IActionResult Privacy()
        {
            return View();
        }

    }
}
