using MagicVilla_VillaApi;
using MagicVilla_VillaApi.DataStore;

using MagicVilla_VillaApi.Repository;
using Microsoft.EntityFrameworkCore;

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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
