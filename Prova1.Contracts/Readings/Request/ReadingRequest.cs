namespace Prova1.Contracts.Readings.Request;

    public record ReadingRequest
    (
        string Title,
        string SubTitle,
        int TagId,
        bool Stopped,
        bool Completed,
        int CurrentPage
    );