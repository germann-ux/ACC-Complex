using ACC.Shared.DTOs;
using ACC.Shared.Enums;

namespace ACC.WebApp.Client.Components.Pages.Guia.Components.Examenes.Manual;

public enum ManualExamContentBlockType
{
    Paragraph,
    Code
}

public sealed class ManualExamContentBlock
{
    public ManualExamContentBlockType Type { get; init; }
    public required string Text { get; init; }
    public string? Language { get; init; }
}

public sealed class ManualExamOption
{
    public required string Key { get; init; }
    public required string Text { get; init; }
}

public sealed class ManualExamQuestion
{
    public required string Id { get; init; }
    public string? SectionTitle { get; init; }
    public required string Prompt { get; init; }
    public IReadOnlyList<ManualExamContentBlock> StemBlocks { get; init; } = [];
    public required IReadOnlyList<ManualExamOption> Options { get; init; }
    public required string CorrectOptionKey { get; init; }
    public string? Explanation { get; init; }
    public IReadOnlyList<ManualExamContentBlock> ExplanationBlocks { get; init; } = [];
}

public sealed class ManualExamSubmission
{
    public int NumeroAciertos { get; init; }
    public int TotalPreguntas { get; init; }
    public int? TiempoSegundos { get; init; }
}

public sealed class ManualExamSubmissionOutcome
{
    public bool Success { get; init; }
    public string? ErrorMessage { get; init; }
    public string? SuccessMessage { get; init; }
    public bool Aprobado { get; init; }
    public double PorcentajeObtenido { get; init; }
    public ExamenEstadoDto? EstadoActualizado { get; init; }
}

public sealed class ManualExamResultSnapshot
{
    public required ExamenTipo Tipo { get; init; }
    public int RefId { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required IReadOnlyList<ManualExamQuestion> Questions { get; init; }
    public required IReadOnlyDictionary<string, string> SelectedOptions { get; init; }
    public required ManualExamSubmission Submission { get; init; }
    public required ManualExamSubmissionOutcome Outcome { get; init; }
    public int PassingScore { get; init; }
    public int? TimeLimitSeconds { get; init; }
    public DateTimeOffset SubmittedAt { get; init; }
}
