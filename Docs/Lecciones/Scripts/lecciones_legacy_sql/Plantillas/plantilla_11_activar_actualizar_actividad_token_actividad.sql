-- Plantilla 11 - ACTIVAR / ACTUALIZAR ACTIVIDAD + TOKEN "actividad"
USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

/* ============================================================================
   PLANTILLA 11 - ACTIVAR / ACTUALIZAR ACTIVIDAD + TOKEN "actividad"
   ============================================================================ */
DECLARE @IdLeccion INT = 1; -- TODO
DECLARE @NuevaUrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad'; -- TODO

BEGIN TRY
    BEGIN TRAN;

    IF NULLIF(LTRIM(RTRIM(@NuevaUrlActividad)), N'') IS NULL
        THROW 52024, 'UrlActividad no puede quedar vacia al activar actividad.', 1;

    DECLARE @OrdenActual NVARCHAR(MAX);
    DECLARE @OrdenNuevo NVARCHAR(MAX);

    SELECT @OrdenActual = OrdenSecciones
    FROM acc_academic.Lecciones
    WHERE IdLeccion = @IdLeccion;

    IF @OrdenActual IS NULL
        THROW 52025, 'No se encontro la leccion objetivo.', 1;

    IF ISJSON(@OrdenActual) <> 1
        THROW 52026, 'OrdenSecciones actual no es JSON valido.', 1;

    DECLARE @Orden TABLE
    (
        Pos INT IDENTITY(1,1) PRIMARY KEY,
        Token NVARCHAR(50) NOT NULL
    );

    INSERT INTO @Orden(Token)
    SELECT CAST([value] AS NVARCHAR(50))
    FROM OPENJSON(@OrdenActual)
    ORDER BY TRY_CONVERT(INT, [key]);

    IF NOT EXISTS (SELECT 1 FROM @Orden WHERE Token = N'actividad')
        INSERT INTO @Orden(Token) VALUES (N'actividad');

    SELECT @OrdenNuevo =
        N'[' + STRING_AGG(N'"' + REPLACE(Token, N'"', N'\"') + N'"', N',')
            WITHIN GROUP (ORDER BY Pos) + N']'
    FROM @Orden;

    UPDATE acc_academic.Lecciones
    SET TieneActividad = 1,
        UrlActividad = LTRIM(RTRIM(@NuevaUrlActividad)),
        OrdenSecciones = @OrdenNuevo
    WHERE IdLeccion = @IdLeccion;

    COMMIT TRAN;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;
GO
