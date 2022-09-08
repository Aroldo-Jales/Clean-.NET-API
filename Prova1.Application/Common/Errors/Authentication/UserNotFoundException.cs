using System.Net;

namespace Prova1.Application.Common.Errors.Authentication;

public class UserNotFoundException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage => "User not found.";
}