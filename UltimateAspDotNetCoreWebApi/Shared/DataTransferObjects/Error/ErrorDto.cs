namespace Shared.DataTransferObjects.Error;

public record ErrorDto
{
    public required int StatusCode { get; init; }
    public required string Message { get; init; }
}