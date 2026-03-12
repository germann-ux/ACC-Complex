-- Plantilla 01 - ACTUALIZAR TITULO
USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

/* ============================================================================
   PLANTILLA 01 - ACTUALIZAR TITULO
   ============================================================================ */
DECLARE @IdLeccion INT = 1; -- TODO
DECLARE @NuevoTitulo NVARCHAR(100) = N'Titulo pendiente'; -- TODO

BEGIN TRY
    BEGIN TRAN;

    IF NULLIF(LTRIM(RTRIM(@NuevoTitulo)), N'') IS NULL
        THROW 52001, 'NuevoTitulo no puede quedar vacio.', 1;

    UPDATE acc_academic.Lecciones
    SET TituloLeccion = LTRIM(RTRIM(@NuevoTitulo))
    WHERE IdLeccion = @IdLeccion;

    IF @@ROWCOUNT <> 1
        THROW 52002, 'No se encontro la leccion objetivo.', 1;

    COMMIT TRAN;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;
GO
