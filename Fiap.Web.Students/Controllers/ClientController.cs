using AutoMapper;
using Fiap.Web.Students.Data;
using Fiap.Web.Students.Models;
using Fiap.Web.Students.Services;
using Fiap.Web.Students.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.Students.Controllers;

public class ClientController : Controller
{
  

    private readonly IMapper _mapper;
    private readonly IRepresentativeService _representativeService;
    private readonly IClientService _clientService;

    public ClientController(IMapper mapper, IRepresentativeService representativeService, IClientService clientService)
    {
        _mapper = mapper;
        _representativeService = representativeService;
        _clientService = clientService;
    }
    // GET
    public IActionResult Index()
    {
        var clients = _clientService.GetAll();
        return View(clients);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        var viewModel = new ClientCreateViewModel
        {
            Representative = new SelectList(
                _representativeService.GetAll(),
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
            _clientService.Add(clientModel);
            

            TempData["successMessage"] = "Client Successfully Created";
            return RedirectToAction(nameof(Index));
        }else
        {
            viewModel.Representative = new SelectList(_representativeService.GetAll(), "RepresentativeId", "RepresentativeName");
            return View(viewModel);
        }
}

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var selectListRepresentative = new SelectList(
            _representativeService.GetAll(),
            nameof(RepresentativeModel.RepresentativeId),
            nameof(RepresentativeModel.RepresentativeName)
        );

        var client = _clientService.GetById(id);
        return View(client);
    }
    
    [HttpPost]
    public IActionResult Edit(ClientModel clientModel)
    {
        _clientService.Update(clientModel);
        
        TempData["successMessage"] = $"Client {clientModel.FirstName} Successfully Updated";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Visualize(int id)
    {
        var selectListRepresentatives = new SelectList(_representativeService.GetAll(), 
            nameof(RepresentativeModel.RepresentativeId),
            nameof(RepresentativeModel.RepresentativeName)
        );
        
        

        var consultedClient = _clientService.GetById(id);
        return View(consultedClient);
    }
    
    [HttpGet]
    public IActionResult Delete(int id)
    {
        _clientService.Delete(id);
        TempData["successMessage"] = "Client Successfully Deleted";
        return RedirectToAction(nameof(Index));
    }
}
