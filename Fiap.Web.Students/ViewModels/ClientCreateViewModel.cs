using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fiap.Web.Students.ViewModels;

public class ClientCreateViewModel
{
    public int ClientId { get; set; }
    [Display(Name = "First Name")]
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
    public string FirstName { get; set; } 
    
    [Display(Name = "Last Name")]
    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [Display(Name = "Email")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Birth date is required.")]
    [Display(Name = "Birth Date")]
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }
    [Display(Name = "Observation")]
    [StringLength(100, ErrorMessage = "Observation cannot be longer than 100 characters.")]
    public string? Observation { get; set; }
    [Display(Name = "Representative")]
    [Required(ErrorMessage = "Representative is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Representative must be chosen.")]
    public int RepresentativeId { get; set; }
    [Display(Name = "Representative")]
    public SelectList Representative { get; set; }

    public ClientCreateViewModel()
    {
        Representative = new SelectList(Enumerable.Empty<SelectListItem>());
    }
}