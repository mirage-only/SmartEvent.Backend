using System.Net;

namespace SmartEvent.Backend.Core.Exceptions;

//401
public class UnauthorizedException(string message, int statusCode) : BaseException(message, (int)HttpStatusCode.Unauthorized);