using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ValidVacations.Data;
using ValidVacations.Services.Customers;
using ValidVacations.Services.Vacations;

var builder = WebApplication.CreateBuilder(args);

// Connect DB
builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IVacationService, VacationService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();


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
