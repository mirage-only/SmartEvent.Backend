using System.Net;

namespace SmartEvent.Backend.Core.Exceptions;

//403
public class ForbiddenException(string message): BaseException(message, (int)HttpStatusCode.Forbidden);