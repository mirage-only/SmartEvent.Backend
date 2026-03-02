using System.Net;

namespace SmartEvent.Backend.Core.Exceptions;

//400
public class ValidationException: BaseException
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException(IDictionary<string, string[]> errors): base("There are validation errors ", (int)HttpStatusCode.BadRequest)
    {
        Errors = errors;
    }

    public ValidationException(string propertyName, string message) : base("Validation Error",
        (int)HttpStatusCode.BadRequest)
    {
        Errors = new Dictionary<string, string[]>
        {
            {propertyName, [message]}
        };
    }
}