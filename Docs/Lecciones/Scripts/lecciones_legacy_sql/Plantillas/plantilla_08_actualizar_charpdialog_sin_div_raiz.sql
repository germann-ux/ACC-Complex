-- Plantilla 08 - ACTUALIZAR CHARPDIALOG (SIN DIV RAIZ)
USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

/* ============================================================================
   PLANTILLA 08 - ACTUALIZAR CHARPDIALOG (SIN DIV RAIZ)
   ============================================================================ */
DECLARE @IdLeccion INT = 1; -- TODO
DECLARE @NuevoCharpDialog NVARCHAR(MAX) =
    N'<p>Mensaje pendiente para el estudiante.</p>'; -- TODO

BEGIN TRY
    BEGIN TRAN;

    IF NULLIF(LTRIM(RTRIM(@NuevoCharpDialog)), N'') IS NULL
        THROW 52016, 'CharpDialog no puede quedar vacio.', 1;

    IF @NuevoCharpDialog LIKE N'%<div%'
        THROW 52017, 'CharpDialog no debe incluir wrapper <div>.', 1;

    UPDATE acc_academic.Lecciones
    SET CharpDialog = @NuevoCharpDialog
    WHERE IdLeccion = @IdLeccion;

    IF @@ROWCOUNT <> 1
        THROW 52018, 'No se encontro la leccion objetivo.', 1;

    COMMIT TRAN;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;
GO
