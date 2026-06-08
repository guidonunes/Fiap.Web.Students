using System.Diagnostics;
using Fiap.Web.Students.Data;
using Fiap.Web.Students.Logging;
using Microsoft.AspNetCore.Mvc;
using Fiap.Web.Students.Models;

namespace Fiap.Web.Students.Controllers;

public class HomeController : Controller
{
    private readonly ICustomLogger _customLogger;
    private readonly DatabaseContext _databaseContext;

    public HomeController(ICustomLogger customLogger, DatabaseContext databaseContext)
    {
        _customLogger = customLogger;
        _databaseContext = databaseContext;
    }

    public IActionResult Index()
    {
        _customLogger.Log("Servus zusammen!");
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}