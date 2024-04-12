namespace Shared.DataTransferObjects.Employee;

public record EmployeeDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required int Age { get; init; }
    public required string Position { get; init; }
}
