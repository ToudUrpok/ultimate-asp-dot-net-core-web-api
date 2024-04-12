namespace Entities.Exceptions;

public sealed class CompanyNotFoundException : NotFoundException
{
    public CompanyNotFoundException(Guid companyId)
        :base ($"The Company with Id: {companyId} doesn't exist.")
    {
    }

    public CompanyNotFoundException(string companyName)
        : base($"The Company with Name: {companyName} doesn't exist.")
    {
    }
}
