-- Plantilla 13 - ACTIVAR / DESACTIVAR COMPILADOR + TOKEN "compilador"
USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

/* ============================================================================
   PLANTILLA 13 - ACTIVAR / DESACTIVAR COMPILADOR + TOKEN "compilador"
   @ActivarCompilador = 1 activa y agrega token.
   @ActivarCompilador = 0 desactiva y remueve token.
   ============================================================================ */
DECLARE @IdLeccion INT = 1; -- TODO
DECLARE @ActivarCompilador BIT = 1; -- TODO: 1 o 0

BEGIN TRY
    BEGIN TRAN;

    DECLARE @OrdenActual NVARCHAR(MAX);
    DECLARE @OrdenNuevo NVARCHAR(MAX);

    SELECT @OrdenActual = OrdenSecciones
    FROM acc_academic.Lecciones
    WHERE IdLeccion = @IdLeccion;

    IF @OrdenActual IS NULL
        THROW 52029, 'No se encontro la leccion objetivo.', 1;

    IF ISJSON(@OrdenActual) <> 1
        THROW 52030, 'OrdenSecciones actual no es JSON valido.', 1;

    DECLARE @Orden TABLE
    (
        Pos INT IDENTITY(1,1) PRIMARY KEY,
        Token NVARCHAR(50) NOT NULL
    );

    INSERT INTO @Orden(Token)
    SELECT CAST([value] AS NVARCHAR(50))
    FROM OPENJSON(@OrdenActual)
    ORDER BY TRY_CONVERT(INT, [key]);

    IF @ActivarCompilador = 1
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM @Orden WHERE Token = N'compilador')
            INSERT INTO @Orden(Token) VALUES (N'compilador');
    END
    ELSE
    BEGIN
        DELETE FROM @Orden WHERE Token = N'compilador';
    END

    SELECT @OrdenNuevo = CASE
        WHEN EXISTS (SELECT 1 FROM @Orden)
            THEN N'[' + STRING_AGG(N'"' + REPLACE(Token, N'"', N'\"') + N'"', N',')
                 WITHIN GROUP (ORDER BY Pos) + N']'
        ELSE N'[]'
    END
    FROM @Orden;

    UPDATE acc_academic.Lecciones
    SET TieneCompilador = @ActivarCompilador,
        OrdenSecciones = ISNULL(@OrdenNuevo, N'[]')
    WHERE IdLeccion = @IdLeccion;

    COMMIT TRAN;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;
GO
