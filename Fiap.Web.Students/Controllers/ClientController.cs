using Fiap.Web.Students.Data;
using Fiap.Web.Students.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fiap.Web.Students.Controllers;

public class ClientController : Controller
{

    private readonly IList<ClientModel> _clients;
    private readonly IList<RepresentativeModel> _representatives;
    
    private readonly DatabaseContext _databaseContext;

    public ClientController(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _representatives = _databaseContext.Representative.ToList();
        _clients = _databaseContext.Client.ToList();
        
    }
    // GET
    public IActionResult Index()
    {
        Console.WriteLine(_clients.Count);
        return View(_clients);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
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
            _representatives,
            nameof(RepresentativeModel.RepresentativeId),
            nameof(RepresentativeModel.RepresentativeName)
        );
        ViewBag.Representatives = selectListRepresentative;

        var client =_clients.Where(c => c.ClientId == id).FirstOrDefault();

        return View(client);
    }
    
    [HttpPost]
    public IActionResult Edit(ClientModel clientModel)
    {
        TempData["successMessage"] = $"Client {clientModel.FirstName} Successfully Updated";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Visualize(int id)
    {
        var client = _clients.Where(c => c.ClientId == id).FirstOrDefault();
        
        if (client == null)
        {
            return NotFound();
        }
        return View(client);
    }
    
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var client = _clients.FirstOrDefault(c => c.ClientId == id);

        if (client == null)
        {
            return NotFound();
        }

        _clients.Remove(client);

        TempData["successMessage"] = "Client successfully deleted";

        return RedirectToAction(nameof(Index));
    }

    public static List<ClientModel> GenerateMockClients()
    {
        var clients = new List<ClientModel>();

        for (int i = 1; i <= 5; i++)
        {
            var client = new ClientModel
            {
                ClientId = i,
                FirstName = $"Client {i}",
                LastName = $"Surname {i}",
                Email = $"email{i}@email.com",
                BirthDate = DateTime.Now.AddYears(-30),
                Observation = $"Observation {i}",
                RepresentativeId = i,
                Representative = new RepresentativeModel
                {
                    RepresentativeId = i,
                    RepresentativeName = "Representative" + i,
                    Cpf = $"0000000191"
                }
            };
            clients.Add(client);
        }
        return clients;
    }
    
    
}
