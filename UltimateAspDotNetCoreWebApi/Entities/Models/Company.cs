using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Company
{
    [Column("CompanyId")]
    public Guid Id { get; set; }

    [Display(Name = "Company Name")]
    [Required(ErrorMessage = "{0} is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the {0} is 60 characters.")]
    public string? Name { get; set; }

    [Display(Name = "Company Address")]
    [Required(ErrorMessage = "{0} is a required field.")]
    [MaxLength(200, ErrorMessage = "Maximum length for the {0} is {1} characters")]
    public string? Address { get; set; }

    public string? Country { get; set; }

    public ICollection<Employee>? Employees { get; set; }
}
