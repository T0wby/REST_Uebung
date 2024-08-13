using TowbyJobs.Services.Companys;
using TowbyJobs.Services.Jobs;
using TowbyJobs.Data;
using Microsoft.EntityFrameworkCore;
using TowbyJobs.Services.Cities;
using TowbyJobs.Services.States;
using TowbyJobs.Services.Countries;

var builder = WebApplication.CreateBuilder(args);

// Connect DB
builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddScoped<ICountryService, CountryService>();
//builder.Services.AddScoped<AppDbContext>();


var app = builder.Build();

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
