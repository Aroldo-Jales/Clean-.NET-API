using System.Net;

namespace Prova1.Application.Common.Errors.Authentication;

public class InvalidPasswordException : Exception, IExceptionBase
{
    public HttpStatusCode StatusCode => HttpStatusCode.UnprocessableEntity;

    public string ErrorMessage => "Password is invalid.";
}