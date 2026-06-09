using AutoMapper;
using Fiap.Web.Students.Data;
using Fiap.Web.Students.Models;
using Fiap.Web.Students.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.Students.Controllers;

public class ClientController : Controller
{
    
    private readonly DatabaseContext _databaseContext;

    private readonly IMapper _mapper;

    public ClientController(DatabaseContext databaseContext, IMapper mapper)
    {
        _databaseContext = databaseContext;
        _mapper = mapper;
    }
    // GET
    public IActionResult Index()
    {
        var clients = _databaseContext.Client
            .Include(client => client.Representative)
            .ToList();
        if (clients.Count == 0)
        {
            clients = new List<ClientModel>();
        }
        return View(clients);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        var viewModel = new ClientCreateViewModel
        {
            Representative = new SelectList(
                _databaseContext.Representative.ToList(),
                nameof(RepresentativeModel.RepresentativeId),
                nameof(RepresentativeModel.RepresentativeName))
        };
        Console.WriteLine("Create() action executed");
        
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Create(ClientCreateViewModel viewModel)
    {

        if (ModelState.IsValid)
        {
            var clientModel = _mapper.Map<ClientModel>(viewModel);
            _databaseContext.Client.Add(clientModel);
            _databaseContext.SaveChanges();

            TempData["successMessage"] = "Client Successfully Created";
            return RedirectToAction(nameof(Index));
        }else
        {
            viewModel.Representative = new SelectList(_databaseContext.Representative.ToList(), "RepresentativeId", "RepresentativeName");
            return View(viewModel);
        }

    
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
            TempData["successMessage"] = $"Client successfully deleted";
        }
        else
        {
            TempData["successMessage"] = "Ops! Client not found";
        }
        
        return RedirectToAction(nameof(Index));
    }
    
}
