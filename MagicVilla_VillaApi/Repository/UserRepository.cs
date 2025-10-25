using AutoMapper;
using MagicVilla_VillaApi.Dto;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Util;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace MagicVilla_VillaApi.Repository
{
    public class UserRepository : IUserRepository
    {
        readonly DbContext _dbContext;
        readonly JwtOption _jwtOptions;
        readonly IMapper _mapper;
        public UserRepository(DbContext db , JwtOption jwtOptions , IMapper mapper)
        {
            _dbContext = db;
            _jwtOptions = jwtOptions;
            _mapper = mapper;
        }

        public async Task<LocalUser?> GetUserByUserName(string userName)
        {
            return await _dbContext.Set<LocalUser>().FirstOrDefaultAsync(user => user.UserName == userName);
        }

        public async Task<LoginResponseDto> LoginUserAsync(LoginRequestDto loginRequestDto)
        {
            LoginResponseDto responseDto = new LoginResponseDto();
            // check username found ->
            LocalUser? user = await GetUserByUserName(loginRequestDto.UserName);
            if (user == null)
            {
                responseDto.IsSuccess = false;
                responseDto.Errors.Add("UserName Not Found");
                return responseDto;
            }
            // check passwords ->
            else if (!VerifyUserPassword(user.Password, loginRequestDto.Password))
            {
                responseDto.IsSuccess = false;
                responseDto.Errors.Add("Password isn't Correct");
                return responseDto;
            }

            // generate jwt token ->
            string token = GenerateJwtToken(user);
            // populate the repsonseDto as Success operation ->
            responseDto.IsSuccess = true;
            responseDto.User = _mapper.Map<UserDto>(user); 
            responseDto.Token = token;
            // return ->
            return responseDto;
        }

        private string GenerateJwtToken(LocalUser user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SigningKey))
                    , SecurityAlgorithms.HmacSha256),
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier , user.UserName),
                    new Claim(ClaimTypes.PrimarySid , user.Id.ToString()),
                    new Claim(ClaimTypes.Role , user.Role)
                })
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            Util.CommonValues.LoggedUserId = user.Id;
            return tokenHandler.WriteToken(token);
        }

        public async Task<LocalUser?> RegisterUserAsync(UserRegistrationRequestDto userRegistrationRequestDto)
        {
            if (await GetUserByUserName(userRegistrationRequestDto.UserName) != null) { return null ;} // there is a user already take this username.
            LocalUser user = new LocalUser()
            {
                UserName = userRegistrationRequestDto.UserName,
                Password = PasswordHelper.ComputeHashForPassword(userRegistrationRequestDto.Password),
                Role = userRegistrationRequestDto.Role
            };
            _dbContext.Set<LocalUser>().Add(user);
            await _dbContext.SaveChangesAsync();
            user.Password = "";
            return user;
        }
        bool VerifyUserPassword(string storedHashPassword , string entredPassword)
        {
            return Util.PasswordHelper.VerifyPassword(storedHashPassword, entredPassword);
        }

        public Task<List<LocalUser>> GetAllUsersAsync()
        {
            return _dbContext.Set<LocalUser>().ToListAsync();
        }
    }
}
