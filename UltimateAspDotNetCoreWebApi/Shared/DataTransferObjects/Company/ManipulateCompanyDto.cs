using Shared.DataTransferObjects.Employee;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Company;

public abstract record ManipulateCompanyDto
{
    [Display(Name = "Company Name")]
    [Required(ErrorMessage = "{0} is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the {0} is 60 characters.")]
    public string? Name { get; init; }

    [Display(Name = "Company Address")]
    [Required(ErrorMessage = "{0} is a required field.")]
    [MaxLength(200, ErrorMessage = "Maximum length for the {0} is {1} characters")]
    public string? Address { get; init; }

    public string? Country { get; init; }

    public IEnumerable<CreateEmployeeDto>? Employees { get; init; }
}
