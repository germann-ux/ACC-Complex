-- Plantilla 03 - ACTUALIZAR NIVEL BLOOM
USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

/* ============================================================================
   PLANTILLA 03 - ACTUALIZAR NIVEL BLOOM
   Valores validos: Recordar, Comprender, Aplicar, Analizar, Evaluar, Crear
   ============================================================================ */
DECLARE @IdLeccion INT = 1; -- TODO
DECLARE @NuevoNivelBloom NVARCHAR(20) = N'Comprender'; -- TODO

BEGIN TRY
    BEGIN TRAN;

    IF @NuevoNivelBloom NOT IN (N'Recordar', N'Comprender', N'Aplicar', N'Analizar', N'Evaluar', N'Crear')
        THROW 52005, 'NivelBloom invalido.', 1;

    UPDATE acc_academic.Lecciones
    SET NivelBloom = @NuevoNivelBloom
    WHERE IdLeccion = @IdLeccion;

    IF @@ROWCOUNT <> 1
        THROW 52006, 'No se encontro la leccion objetivo.', 1;

    COMMIT TRAN;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;
GO
