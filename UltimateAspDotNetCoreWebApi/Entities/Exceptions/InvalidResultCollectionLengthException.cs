namespace Entities.Exceptions;

public sealed class InvalidResultCollectionLengthException(int expectedLength, int actualLength) :
    BadRequestException($"Result collection length ({actualLength}) mismatch comparing to requested ids count ({expectedLength}).")
{
}
