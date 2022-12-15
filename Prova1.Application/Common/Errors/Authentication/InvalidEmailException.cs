using System.Net;

namespace Prova1.Application.Common.Errors.Authentication;

public class InvalidEmailException : Exception, IExceptionBase
{
    public HttpStatusCode StatusCode => HttpStatusCode.UnprocessableEntity;

    public string ErrorMessage => "This email is invalid.";
}