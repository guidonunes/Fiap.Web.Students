using Fiap.Web.Students.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fiap.Web.Students.Controllers;

public class ClientController : Controller
{

    private readonly IList<ClientModel> _clients;
    private readonly IList<RepresentativeModel> _representatives;

    public ClientController()
    {
        _clients = GenerateMockClients();
        _representatives = GenerateMockRepresentatives();
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
        Console.WriteLine("Client Successfully Created");
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
                BirthDate = DateTime.Now.AddYears(-30).ToString("dd/MM/yyyy"),
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
    
    public static List<RepresentativeModel> GenerateMockRepresentatives()
    {
        return new List<RepresentativeModel>
        {
            new RepresentativeModel
            {
                RepresentativeId = 1,
                RepresentativeName = "Representative 1",
                Cpf = "00000000191"
            },
            new RepresentativeModel
            {
                RepresentativeId = 2,
                RepresentativeName = "Representative 2",
                Cpf = "00000000272"
            },
            new RepresentativeModel
            {
                RepresentativeId = 3,
                RepresentativeName = "Representative 3",
                Cpf = "00000000353"
            }
        };
    }
}