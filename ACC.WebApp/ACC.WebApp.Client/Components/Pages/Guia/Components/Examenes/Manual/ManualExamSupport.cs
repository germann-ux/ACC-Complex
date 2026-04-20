using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using ACC.WebApp.Client.Services;

namespace ACC.WebApp.Client.Components.Pages.Guia.Components.Examenes.Manual;

public static class ManualExamSupport
{
    public static ManualExamQuestion CreateQuestion(
        string id,
        string prompt,
        string correctOptionKey,
        (string key, string text)[] options,
        string? sectionTitle = null,
        string? explanation = null,
        IReadOnlyList<ManualExamContentBlock>? stemBlocks = null,
        IReadOnlyList<ManualExamContentBlock>? explanationBlocks = null)
    {
        return new ManualExamQuestion
        {
            Id = id,
            SectionTitle = sectionTitle,
            Prompt = prompt,
            StemBlocks = stemBlocks ?? [],
            CorrectOptionKey = correctOptionKey,
            Options = options
                .Select(option => new ManualExamOption
                {
                    Key = option.key,
                    Text = option.text
                })
                .ToList(),
            Explanation = explanation,
            ExplanationBlocks = explanationBlocks ?? []
        };
    }

    public static ManualExamContentBlock Paragraph(string text)
        => new()
        {
            Type = ManualExamContentBlockType.Paragraph,
            Text = text
        };

    public static ManualExamContentBlock Code(string text, string? language = null)
        => new()
        {
            Type = ManualExamContentBlockType.Code,
            Text = text,
            Language = language
        };

    public static string GetBlockReason(ExamenEstadoDto estado)
    {
        if (!estado.EstaHabilitado)
            return "Este examen a\u00FAn no est\u00E1 habilitado para tu cuenta.";

        if (estado.IntentosRestantes <= 0)
            return "Ya alcanzaste el n\u00FAmero m\u00E1ximo de intentos para este examen.";

        return "Este examen no est\u00E1 disponible por ahora.";
    }

    public static string GetTypeSlug(ExamenTipo tipo)
        => tipo switch
        {
            ExamenTipo.Modulo => "modulo",
            ExamenTipo.SubModulo => "submodulo",
            _ => "libre"
        };

    public static ExamenTipo ParseType(string? slug)
    {
        var normalized = (slug ?? string.Empty).Trim().ToLowerInvariant();
        return normalized switch
        {
            "examensubmodulo" => ExamenTipo.SubModulo,
            "submodulo" => ExamenTipo.SubModulo,
            "examenmodulo" => ExamenTipo.Modulo,
            "modulo" => ExamenTipo.Modulo,
            _ => ExamenTipo.Libre
        };
    }

    public static string BuildExamRoute(ExamenTipo tipo, int refId)
        => $"/examen/{GetTypeSlug(tipo)}/{refId}";

    public static string BuildResultRoute(ExamenTipo tipo, int refId)
        => $"/examen/resultado/{GetTypeSlug(tipo)}/{refId}";

    public static string GetPassingText(int passingScore, int totalQuestions)
    {
        if (passingScore <= totalQuestions)
        {
            var percentage = Math.Round(passingScore * 100.0 / totalQuestions, 0);
            return $"M\u00EDnimo {passingScore} aciertos ({percentage:0}%)";
        }

        return $"M\u00EDnimo {passingScore}%";
    }

    public static Task<ManualExamSubmissionOutcome> RegisterSubModuloAttemptAsync(
        ExamenesServiceClient examenesService,
        ExamenSubModuloDto examen,
        ManualExamSubmission submission,
        Func<ExamenEstadoDto, Task>? onEstadoActualizado = null,
        string successMessage = "Tu intento del examen de subm\u00F3dulo fue registrado.")
    {
        return RegisterAttemptAsync(
            examenesService,
            ExamenTipo.SubModulo,
            examen.Id,
            submission,
            currentSubmission => new ExamenIntentoDto
            {
                IdUsuario = string.Empty,
                ExamenSubModuloId = examen.Id,
                NumeroAciertos = currentSubmission.NumeroAciertos,
                TotalPreguntas = currentSubmission.TotalPreguntas,
                TiempoSegundos = currentSubmission.TiempoSegundos
            },
            onEstadoActualizado,
            successMessage);
    }

    public static Task<ManualExamSubmissionOutcome> RegisterModuloAttemptAsync(
        ExamenesServiceClient examenesService,
        ExamenModuloDto examen,
        ManualExamSubmission submission,
        Func<ExamenEstadoDto, Task>? onEstadoActualizado = null,
        string successMessage = "Tu intento del examen de m\u00F3dulo fue registrado.")
    {
        return RegisterAttemptAsync(
            examenesService,
            ExamenTipo.Modulo,
            examen.Id,
            submission,
            currentSubmission => new ExamenIntentoDto
            {
                IdUsuario = string.Empty,
                ExamenModuloId = examen.Id,
                NumeroAciertos = currentSubmission.NumeroAciertos,
                TotalPreguntas = currentSubmission.TotalPreguntas,
                TiempoSegundos = currentSubmission.TiempoSegundos
            },
            onEstadoActualizado,
            successMessage);
    }

    private static async Task<ManualExamSubmissionOutcome> RegisterAttemptAsync(
        ExamenesServiceClient examenesService,
        ExamenTipo tipo,
        int examenId,
        ManualExamSubmission submission,
        Func<ManualExamSubmission, ExamenIntentoDto> dtoFactory,
        Func<ExamenEstadoDto, Task>? onEstadoActualizado,
        string successMessage)
    {
        var registrado = await examenesService.RegistrarIntentoExamenAsync(dtoFactory(submission));
        if (registrado is null)
        {
            return new ManualExamSubmissionOutcome
            {
                Success = false,
                ErrorMessage = "No se pudo registrar el intento del examen."
            };
        }

        ExamenEstadoDto? estadoActualizado = null;
        try
        {
            estadoActualizado = await examenesService.ObtenerEstadoExamenAsync(tipo, examenId);
            if (estadoActualizado is not null && onEstadoActualizado is not null)
            {
                await onEstadoActualizado(estadoActualizado);
            }
        }
        catch
        {
            // La consulta de estado es secundaria; el intento ya fue persistido.
        }

        return new ManualExamSubmissionOutcome
        {
            Success = true,
            SuccessMessage = successMessage,
            Aprobado = registrado.Aprobado,
            PorcentajeObtenido = registrado.PorcentajeObtenido,
            EstadoActualizado = estadoActualizado
        };
    }
}
