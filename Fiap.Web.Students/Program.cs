using Fiap.Web.Students.Data;
using Fiap.Web.Students.Data.Repository;
using Fiap.Web.Students.Logging;
using Fiap.Web.Students.Models;
using Fiap.Web.Students.Services;
using Fiap.Web.Students.ViewModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("OracleConnection");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Connection string 'OracleConnection' was not found. Configure it in appsettings.json, appsettings.Development.json, user secrets, or environment variables.");
}

builder.Services.AddDbContext<DatabaseContext>(
    opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(builder.Environment.IsDevelopment())
);

#region Register IServiceCollection

builder.Services.AddSingleton<ICustomLogger, FileLogger>();

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IRepresentativeRepository, RepresentativeRepository>();

builder.Services.AddScoped<IRepresentativeService, RepresentativeService>();
builder.Services.AddScoped<IClientService, ClientService>();
#endregion

#region AutoMapper

builder.Services.AddAutoMapper(c =>
{
    c.AllowNullCollections = true;
    c.AllowNullDestinationValues = true;

    c.CreateMap<ClientModel, ClientCreateViewModel>()
        .ForMember(dest => dest.Representative, opt => opt.Ignore());
    c.CreateMap<ClientCreateViewModel, ClientModel>()
        .ForMember(dest => dest.Representative, opt => opt.Ignore());
}, typeof(ClientModel).Assembly);
#endregion

builder.Services.AddControllersWithViews();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
