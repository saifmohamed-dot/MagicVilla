using MagicVilla_VillaApi;
using MagicVilla_VillaApi.DataStore;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Repository;
using MagicVilla_VillaApi.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<VillasDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IVillaRepository, VillaRepository>();
builder.Services.AddScoped<DbContext , VillasDBContext>();
builder.Services.AddScoped<IVillaNumberRepository, VillaNumberRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IRequestedAppointmentRepository, RequestedAppointmentRepository>();
builder.Services.AddScoped<IVillaPreviewImagesRepository, VillaPreviewImagesRepository>();
var jwtOptions = builder.Configuration.GetSection("JWT").Get<JwtOption>();
builder.Services.AddSingleton(jwtOptions);
builder.Services.AddAuthentication()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // will use what we will assign for the authentication services above through DI

app.UseAuthorization();

app.MapControllers();

app.Run();
