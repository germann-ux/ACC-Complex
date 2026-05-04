-- Plantilla 02 - ACTUALIZAR DESCRIPCION
USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

/* ============================================================================
   PLANTILLA 02 - ACTUALIZAR DESCRIPCION
   ============================================================================ */
DECLARE @IdLeccion INT = 1; -- TODO
DECLARE @NuevaDescripcion NVARCHAR(500) = N'Descripcion pendiente'; -- TODO

BEGIN TRY
    BEGIN TRAN;

    IF NULLIF(LTRIM(RTRIM(@NuevaDescripcion)), N'') IS NULL
        THROW 52003, 'NuevaDescripcion no puede quedar vacia.', 1;

    UPDATE acc_academic.Lecciones
    SET DescripcionLeccion = LTRIM(RTRIM(@NuevaDescripcion))
    WHERE IdLeccion = @IdLeccion;

    IF @@ROWCOUNT <> 1
        THROW 52004, 'No se encontro la leccion objetivo.', 1;

    COMMIT TRAN;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;
GO
