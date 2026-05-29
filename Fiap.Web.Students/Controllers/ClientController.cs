using Fiap.Web.Students.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.Students.Controllers;

public class ClientController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
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