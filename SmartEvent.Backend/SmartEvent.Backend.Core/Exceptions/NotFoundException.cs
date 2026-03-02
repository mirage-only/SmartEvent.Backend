using System.Net;

namespace SmartEvent.Backend.Core.Exceptions;

//404
public class NotFoundException(string message, int statusCode) : BaseException(message, (int)HttpStatusCode.NotFound);