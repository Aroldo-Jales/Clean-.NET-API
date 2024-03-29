﻿using Prova1.Domain.Entities.Readings;

namespace Prova1.Application.Common.Interfaces.Persistence.Readings
{
    public interface ICommentaryRepository
    {
        Task<List<Commentary>> GetCommentariesByReading(Guid readingId);
        Task<Commentary> AddCommentaryAsync(Commentary commentary);
        Task<Commentary> UpdateCommentaryAsync(Commentary commentary);
        Task RemoveCommentaryAsync(Guid id);
    }
}
