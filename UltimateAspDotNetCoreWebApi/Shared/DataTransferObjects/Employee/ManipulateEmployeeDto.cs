using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Employee;

public abstract record ManipulateEmployeeDto
{
    [Display(Name = "Employee Name")]
    [Required(ErrorMessage = "{0} is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the {0} is {1} characters.")]
    public string? Name { get; init; }

    [Display(Name = "Employee Age")]
    [Range(18, int.MaxValue, ErrorMessage = "{0} is required and it can't be lower than {1}")]
    public int Age { get; init; }

    [Display(Name = "Employee Position")]
    [Required(ErrorMessage = "{0} is a required field.")]
    [MaxLength(20, ErrorMessage = "Maximum length for the {0} is {1} characters.")]
    public string? Position { get; init; }
}
