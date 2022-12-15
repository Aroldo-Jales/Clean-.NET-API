using System.Net;

namespace Prova1.Application.Common.Errors.Authentication
{
    public class PhoneNumberAlreadyExistsException : Exception, IExceptionBase
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
        public string ErrorMessage => "This phone number already exists.";
    }
}