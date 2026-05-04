-- Plantilla 05 - ACTUALIZAR CONTENIDO EJEMPLO
USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

/* ============================================================================
   PLANTILLA 05 - ACTUALIZAR CONTENIDO EJEMPLO
   ============================================================================ */
DECLARE @IdLeccion INT = 1; -- TODO
DECLARE @NuevoEjemplo NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>Ejemplo pendiente</h3>
    <p>Contenido pendiente.</p>
    <img src="https://placehold.co/1200x675?text=Imagen+pendiente" alt="Imagen pendiente">
</div>'; -- TODO

BEGIN TRY
    BEGIN TRAN;

    IF NULLIF(LTRIM(RTRIM(@NuevoEjemplo)), N'') IS NULL
        THROW 52009, 'Ejemplo no puede quedar vacio.', 1;

    UPDATE acc_academic.Lecciones
    SET Ejemplo = @NuevoEjemplo
    WHERE IdLeccion = @IdLeccion;

    IF @@ROWCOUNT <> 1
        THROW 52010, 'No se encontro la leccion objetivo.', 1;

    COMMIT TRAN;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;
GO
