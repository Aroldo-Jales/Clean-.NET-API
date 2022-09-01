using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Prova1.Api.Application.Helpers
{
    public static class Validation
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public static bool IsValidPassword(string password) {
    
        var hasNumber = new Regex(@"[0-9]+");
        var hasLowerChar = new Regex(@"[a-z]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasNonAlphaNum = new Regex(@"\W|_");
        var hasMinimum8Chars = new Regex(@".{8,}");

        bool isValidated = 
        hasNumber.IsMatch(password) && 
        hasLowerChar.IsMatch(password) &&
        hasUpperChar.IsMatch(password) && 
        hasNonAlphaNum.IsMatch(password) &&
        hasMinimum8Chars.IsMatch(password);

        return isValidated;
        }
    }
}