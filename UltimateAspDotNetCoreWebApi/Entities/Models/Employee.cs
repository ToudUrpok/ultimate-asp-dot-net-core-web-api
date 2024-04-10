using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Employee
{
    [Column("EmployeeId")]
    public Guid Id { get; set; }

    [Display(Name = "Employee Name")]
    [Required(ErrorMessage = "{0} is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the {0} is {1} characters.")]
    public string? Name { get; set; }

    [Display(Name = "Employee Age")]
    [Required(ErrorMessage = "{0} is a required field.")]
    public int Age { get; set; }

    [Display(Name = "Employee Position")]
    [Required(ErrorMessage = "{0} is a required field.")]
    [MaxLength(20, ErrorMessage = "Maximum length for the {0} is {1} characters.")]
    public string? Position { get; set; }

    [ForeignKey(nameof(Company))]
    public Guid CompanyId { get; set; }

    public Company? Company { get; set; }
}
