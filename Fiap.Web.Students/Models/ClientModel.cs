using System.ComponentModel.DataAnnotations;

namespace Fiap.Web.Students.Models;

public class ClientModel
{
    public int ClientId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }
    public string? Observation { get; set; }
    public int RepresentativeId { get; set; }
    public RepresentativeModel? Representative { get; set; }
}
