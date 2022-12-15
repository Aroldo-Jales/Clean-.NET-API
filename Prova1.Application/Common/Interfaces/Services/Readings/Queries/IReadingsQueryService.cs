using Prova1.Domain.Entities.Readings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova1.Application.Common.Interfaces.Services.Readings.Queries
{
    public interface IReadingsQueryService
    {
        Task<List<Reading>> AllReadingsAsync(int count);

        Task<List<Reading>> AllReadingsByUserAsync(Guid userId);
    }
}
