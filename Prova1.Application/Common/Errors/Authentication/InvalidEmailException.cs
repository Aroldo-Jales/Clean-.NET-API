using System.Net;

namespace Prova1.Application.Common.Errors.Authentication;

public class InvalidEmailException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.UnprocessableEntity;

    public string ErrorMessage => "Email is invalid.";
}