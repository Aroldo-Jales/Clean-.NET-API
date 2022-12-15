

using System.Net;

namespace Prova1.Application.Common.Errors.Authentication
{
    public class InvalidTokenException : Exception, IExceptionBase
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public string ErrorMessage => "Token is invalid.";
    }
}