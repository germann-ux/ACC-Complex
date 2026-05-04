-- Plantilla 06 - ACTUALIZAR CONTENIDO PRACTICA
USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

/* ============================================================================
   PLANTILLA 06 - ACTUALIZAR CONTENIDO PRACTICA
   ============================================================================ */
DECLARE @IdLeccion INT = 1; -- TODO
DECLARE @NuevoPractica NVARCHAR(MAX) = N'
<div class="leccion-practicas">
    <h3>Practica pendiente</h3>
    <ol>
        <li>Paso 1 pendiente.</li>
        <li>Paso 2 pendiente.</li>
    </ol>
</div>'; -- TODO

BEGIN TRY
    BEGIN TRAN;

    IF NULLIF(LTRIM(RTRIM(@NuevoPractica)), N'') IS NULL
        THROW 52011, 'Practica no puede quedar vacia.', 1;

    UPDATE acc_academic.Lecciones
    SET Practica = @NuevoPractica
    WHERE IdLeccion = @IdLeccion;

    IF @@ROWCOUNT <> 1
        THROW 52012, 'No se encontro la leccion objetivo.', 1;

    COMMIT TRAN;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;
GO
