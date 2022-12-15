using Prova1.Domain.Entities.Readings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova1.Application.Common.Interfaces.Persistence.Readings
{
    public interface ICommentaryRepository
    {
        Task<Commentary> AddCommentaryAsync();
        Task<Commentary> UpdateCommentaryAsync();
        Task RemoveCommentaryAsync();
    }
}
