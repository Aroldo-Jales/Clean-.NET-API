using Prova1.Application.Common.Interfaces.Services;

namespace Prova1.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.Now;
    }
}