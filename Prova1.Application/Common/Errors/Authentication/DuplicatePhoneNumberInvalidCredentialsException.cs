using System.Net;

namespace Prova1.Application.Common.Errors.Authentication;

public class DuplicatePhoneNumberInvalidCredentialsException : Exception, IExceptionBase
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public string ErrorMessage => "Duplicate phone number.";
}