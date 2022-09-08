using System.Net;

namespace Prova1.Application.Common.Errors.Authentication
{
    public class InvalidCredentialsException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public string ErrorMessage => "Invalid credentials.";
    }
}
