namespace Prova1.Contracts.Readings.Request;

    public record AnnotationRequest
    (
        int id,
        Guid readingId,
        string content,
        int page
    );
