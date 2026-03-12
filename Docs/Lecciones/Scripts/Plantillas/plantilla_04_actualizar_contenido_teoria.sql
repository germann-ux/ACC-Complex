-- Plantilla 04 - ACTUALIZAR CONTENIDO TEORIA
USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

/* ============================================================================
   PLANTILLA 04 - ACTUALIZAR CONTENIDO TEORIA
   ============================================================================ */
DECLARE @IdLeccion INT = 4; -- TODO
DECLARE @NuevoTeoria NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>Ejemplo pendiente</h3>
    <p>Contenido pendiente.</p>
    <img src="https://placehold.co/1200x675?text=Imagen+pendiente" alt="Imagen pendiente">
</div>'; -- TODO

BEGIN TRY
    BEGIN TRAN;

    IF NULLIF(LTRIM(RTRIM(@NuevoTeoria)), N'') IS NULL
        THROW 52007, 'Teoria no puede quedar vacia.', 1;

    UPDATE acc_academic.Lecciones
    SET Teoria = @NuevoTeoria
    WHERE IdLeccion = @IdLeccion;

    IF @@ROWCOUNT <> 1
        THROW 52008, 'No se encontro la leccion objetivo.', 1;

    COMMIT TRAN;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;
GO
