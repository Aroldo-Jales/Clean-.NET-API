using System.Net;

namespace Prova1.Application.Common.Errors.Authentication;

public class InvalidPasswordException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.UnprocessableEntity;

    public string ErrorMessage => "Password is invalid.";
}