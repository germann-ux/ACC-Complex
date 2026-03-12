-- Plantilla 07 - ACTUALIZAR CHARPTIP (SIN DIV RAIZ)
USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

/* ============================================================================
   PLANTILLA 07 - ACTUALIZAR CHARPTIP (SIN DIV RAIZ)
   ============================================================================ */
DECLARE @IdLeccion INT = 1; -- TODO
DECLARE @NuevoCharpTip NVARCHAR(MAX) =
    N'<p><strong>Tip Charp:</strong> Contenido pendiente.</p>'; -- TODO

BEGIN TRY
    BEGIN TRAN;

    IF NULLIF(LTRIM(RTRIM(@NuevoCharpTip)), N'') IS NULL
        THROW 52013, 'CharpTip no puede quedar vacio.', 1;

    IF @NuevoCharpTip LIKE N'%<div%'
        THROW 52014, 'CharpTip no debe incluir wrapper <div>.', 1;

    UPDATE acc_academic.Lecciones
    SET CharpTip = @NuevoCharpTip
    WHERE IdLeccion = @IdLeccion;

    IF @@ROWCOUNT <> 1
        THROW 52015, 'No se encontro la leccion objetivo.', 1;

    COMMIT TRAN;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;
GO
