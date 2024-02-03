using AutoMapper;
using DomainLayer.AutoMapper;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using RepositoryLayer.AppDbContexts;
using RepositoryLayer.Repository;
using ServiceLayer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

builder.Services.AddScoped<ComplaintTicketingRepo>();
builder.Services.AddScoped<ComplaintTicketingService>();
builder.Services.AddScoped<UserRepo>();
builder.Services.AddScoped<UserManagerService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<UserContext>();

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();