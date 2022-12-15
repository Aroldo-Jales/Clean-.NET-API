using System.Net;

namespace Prova1.Application.Common.Errors.Authentication
{
    public class InvalidCredentialsException : Exception, IExceptionBase
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public string ErrorMessage => "Invalid credentials.";
    }
}
