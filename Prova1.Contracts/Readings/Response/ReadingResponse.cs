namespace Prova1.Contracts.Readings.Response;

public record ReadingResponse
(
    string Title,
    string SubTitle,
    bool Stopped,
    bool Completed,
    int CurrentPage
);