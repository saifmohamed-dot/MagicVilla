using MagicVilla_Web.Dto;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace MagicVilla_Web.Controllers
{
    public class UserController : Controller
    {
        readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            if(ModelState.IsValid)
            {
                APIResponse loginResponse = await _userRepository.LoginAsync<APIResponse>(loginRequest);
                // if not null or login fail 
                if (loginResponse == null) { return BadRequest(loginResponse); }
                else if (!loginResponse.IsSuccess)
                {
                    // this line will make this error message appear in the password validation place.
                    ModelState.AddModelError("Password", "UserName or Password Incorrect!"); 
                }
                else
                {
                    // Store the token returned from the api 
                    LoginResponseDto user = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(loginResponse.Result)!)!;
                    
                    HttpContext.Session.SetString(StaticUtil.TokenName, user.Token); // for using it in all authorize endpoint .
                    Console.WriteLine($"Token {HttpContext.Session.GetString(StaticUtil.TokenName)} Stored in Sesstoin .");
                    // make some claims
                    ClaimsIdentity identity = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name , user.User.UserName),
                        new Claim(ClaimTypes.Role , user.User.Role),
                        new Claim(ClaimTypes.Sid , user.User.Id.ToString())
                    } , CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    // encrypt pricipals and pass it in the set-cookie response header 
                    await HttpContext.SignInAsync(principal);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(loginRequest);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistrationRequestDto userRegistrationRequestDto)
        {
            if(ModelState.IsValid)
            {
                APIResponse response = await _userRepository.RegisterUserAsync<APIResponse>(userRegistrationRequestDto);
                if (response != null && response.IsSuccess)
                {
                    TempData["Success"] = $"Congratulations {userRegistrationRequestDto.UserName} Yout SignUp Successfully!";
                    return RedirectToAction("Index", "Home");
                }
                if (response == null) { return BadRequest(); }
                // the only reason this would fail if the client (mvc) passed 
                // is there a username already exists like the one passed to us.
                ModelState.AddModelError(nameof(userRegistrationRequestDto.UserName), "This UserName Already Exists!");
            }
            return View(userRegistrationRequestDto); // if it's not valid in the client side (this mvc is the client side btw) .
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied()
        {
            TempData["Error"] = "This Action Need An Admin Permission!";
            return RedirectToAction("Index", "Home");
        }
    }
}
