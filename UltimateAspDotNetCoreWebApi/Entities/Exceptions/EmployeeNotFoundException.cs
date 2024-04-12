namespace Entities.Exceptions;

public sealed class EmployeeNotFoundException : NotFoundException
{
    public EmployeeNotFoundException(Guid employeeId)
        : base($"Employee with Id: {employeeId} doesn't exist.")
    {
    }
}
