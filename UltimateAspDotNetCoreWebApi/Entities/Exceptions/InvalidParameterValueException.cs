namespace Entities.Exceptions;

public sealed class InvalidParameterValueException<T>(string paramName, T paramValue) :
    BadRequestException($"'{paramValue}' value is not valid for parameter '{paramName}' of type {typeof(T)}")
{
}
