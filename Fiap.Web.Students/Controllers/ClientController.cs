using Fiap.Web.Students.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.Students.Controllers;

public class ClientController : Controller
{
    
    private List<ClientModel> _clients;

    public ClientController()
    {
        _clients = GenerateMockClients();
    }
    // GET
    public IActionResult Index()
    {
        Console.WriteLine(_clients.Count);
        return View(_clients);
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
}