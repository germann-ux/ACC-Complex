-- Plantilla 14 - REEMPLAZAR ORDEN DE SECCIONES COMPLETO
USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

/* ============================================================================
   PLANTILLA 14 - REEMPLAZAR ORDEN DE SECCIONES COMPLETO
   Ejemplo: N'["charpDialog","teoria","ejemplo","practica","charpTip","video"]'
   ============================================================================ */
DECLARE @IdLeccion INT = 1; -- TODO
DECLARE @NuevoOrdenSecciones NVARCHAR(MAX) = N'["charpDialog","teoria","ejemplo","practica","charpTip"]'; -- TODO

BEGIN TRY
    BEGIN TRAN;

    IF ISJSON(@NuevoOrdenSecciones) <> 1
        THROW 52031, 'NuevoOrdenSecciones no es JSON valido.', 1;

    IF EXISTS
    (
        SELECT j.[value]
        FROM OPENJSON(@NuevoOrdenSecciones) j
        GROUP BY j.[value]
        HAVING COUNT(*) > 1
    )
        THROW 52032, 'NuevoOrdenSecciones contiene tokens duplicados.', 1;

    IF EXISTS
    (
        SELECT 1
        FROM OPENJSON(@NuevoOrdenSecciones) j
        LEFT JOIN
        (
            VALUES
                (N'video'),
                (N'teoria'),
                (N'practica'),
                (N'ejemplo'),
                (N'actividad'),
                (N'compilador'),
                (N'charpTip'),
                (N'charpDialog')
        ) AS permitidas(Token)
            ON permitidas.Token = CAST(j.[value] AS NVARCHAR(50))
        WHERE permitidas.Token IS NULL
    )
        THROW 52033, 'NuevoOrdenSecciones contiene tokens fuera del conjunto permitido.', 1;

    UPDATE acc_academic.Lecciones
    SET OrdenSecciones = @NuevoOrdenSecciones
    WHERE IdLeccion = @IdLeccion;

    IF @@ROWCOUNT <> 1
        THROW 52034, 'No se encontro la leccion objetivo.', 1;

    COMMIT TRAN;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;
GO
