using System.Net.Mail;
using System.Text.RegularExpressions;


namespace Prova1.Application.Utils.Authentication
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
        public static bool IsValidPassword(string password)
        {

            Regex? hasNumber = new Regex(@"[0-9]+");
            Regex? hasLowerChar = new Regex(@"[a-z]+");
            Regex? hasUpperChar = new Regex(@"[A-Z]+");
            Regex? hasNonAlphaNum = new Regex(@"\W|_");
            Regex? hasMinimum8Chars = new Regex(@".{8,}");

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