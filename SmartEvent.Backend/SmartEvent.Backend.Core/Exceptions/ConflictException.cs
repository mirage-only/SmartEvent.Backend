using System.Net;

namespace SmartEvent.Backend.Core.Exceptions;

//409
public class ConflictException(string message): BaseException(message, (int)HttpStatusCode.Conflict);