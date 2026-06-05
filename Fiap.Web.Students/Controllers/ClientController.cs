using Fiap.Web.Students.Data;
using Fiap.Web.Students.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fiap.Web.Students.Controllers;

public class ClientController : Controller
{

    
    
    private readonly DatabaseContext _databaseContext;

    public ClientController(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    // GET
    public IActionResult Index()
    {
        var _clients = _databaseContext.Client.ToList();
        if (_clients == null || _clients.Count == 0)
        {
            _clients = new List<ClientModel>();
        }
        return View(_clients);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        Console.WriteLine("Create() action executed");

        var selectListRepresentative = new SelectList(
            _databaseContext.Representative.ToList(),
            nameof(RepresentativeModel.RepresentativeId),
            nameof(RepresentativeModel.RepresentativeName)
        );
        ViewBag.Representatives = selectListRepresentative;
        
        return View(new ClientModel());
    }
    
    [HttpPost]
    public IActionResult Create(ClientModel clientModel)
    {
        _databaseContext.Client.Add(clientModel);
        _databaseContext.SaveChanges();
        
        TempData["successMessage"] = "Client Successfully Created";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var selectListRepresentative = new SelectList(
            _databaseContext.Representative.ToList(),
            nameof(RepresentativeModel.RepresentativeId),
            nameof(RepresentativeModel.RepresentativeName)
        );
        ViewBag.Representatives = selectListRepresentative;

        var client = _databaseContext.Client.Find(id);
        return View(client);
    }
    
    [HttpPost]
    public IActionResult Edit(ClientModel clientModel)
    {
        _databaseContext.Client.Update(clientModel);
        _databaseContext.SaveChanges();
        
        TempData["successMessage"] = $"Client {clientModel.FirstName} Successfully Updated";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Visualize(int id)
    {
        var selectListRepresentatives = new SelectList(_databaseContext.Representative.ToList(), 
            nameof(RepresentativeModel.RepresentativeId),
            nameof(RepresentativeModel.RepresentativeName)
        );
        
        ViewBag.Representatives = selectListRepresentatives;

        var consultedClient = _databaseContext.Client.Find(id);
        return View(consultedClient);
    }
    
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var consultedClient = _databaseContext.Client.Find(id);

        if (consultedClient != null)
        {
            _databaseContext.Client.Remove(consultedClient);
            _databaseContext.SaveChanges();
            TempData["successMessage"] = $"Client {consultedClient} successfully deleted";
        }
        else
        {
            TempData["successMessage"] = "Ops! Client not found";
        }
        
        return RedirectToAction(nameof(Index));
    }
    
}
