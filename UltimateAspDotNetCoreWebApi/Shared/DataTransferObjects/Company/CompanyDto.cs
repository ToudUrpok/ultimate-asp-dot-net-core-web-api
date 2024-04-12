namespace Shared.DataTransferObjects.Company;

public record CompanyDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string FullAddress { get; init; }
}
