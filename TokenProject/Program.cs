using APIWEB.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using TokenProject;
using TokenProject.Entities;
using TokenProject.Extensions;
using TokenProject.Handler;
using TokenProject.IRepositories;
using TokenProject.Repositories;
using TokenProject.Services;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddCustomSwagger();
builder.Services.AddDbContext<AppDbContext>(opt =>
opt.UseSqlServer(configuration.GetConnectionString("MyDefault")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddAutoMapper(typeof(MapperProfiles));
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});
builder.Services.AddSingleton<string>("key");
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddEventSourceLogger();
builder.Logging.AddFilter("Microsoft", LogLevel.Error);
builder.Logging.AddFilter("Microsoft", LogLevel.Information);
//builder.Logging.AddFile("logs/{Date}.log");
builder.Logging.AddFile("bin/Debug/net8.0/logs/{Date}.log");


var app = builder.Build();
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var services = serviceScope.ServiceProvider;

    await LoginUserSeeder.SeedAsync(services);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
