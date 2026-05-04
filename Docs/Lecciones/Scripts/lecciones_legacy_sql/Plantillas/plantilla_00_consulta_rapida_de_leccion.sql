-- Plantilla 00 - CONSULTA RAPIDA DE LECCION
USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

/* ============================================================================
   PLANTILLA 00 - CONSULTA RAPIDA DE LECCION
   ============================================================================ */
DECLARE @IdLeccion INT = 1; -- TODO

SELECT
    l.IdLeccion,
    l.SubtemaId,
    l.TituloLeccion,
    l.DescripcionLeccion,
    l.NivelBloom,
    l.TieneActividad,
    l.UrlActividad,
    l.TieneCompilador,
    l.TieneVideo,
    l.VideoId,
    l.OrdenSecciones
FROM acc_academic.Lecciones l
WHERE l.IdLeccion = @IdLeccion;
GO
