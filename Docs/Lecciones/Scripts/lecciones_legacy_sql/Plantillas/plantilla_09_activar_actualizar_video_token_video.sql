-- Plantilla 09 - ACTIVAR / ACTUALIZAR VIDEO + TOKEN "video"
USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

/* ============================================================================
   PLANTILLA 09 - ACTIVAR / ACTUALIZAR VIDEO + TOKEN "video"
   Poner TieneVideo=1, definir VideoId y asegurar token en OrdenSecciones.
   Si tienes URL completa, extrae el ID de YouTube primero.
   ============================================================================ */
DECLARE @IdLeccion INT = 1; -- TODO
DECLARE @NuevoVideoId NVARCHAR(100) = N'VIDEO_ID_PENDIENTE'; -- TODO

BEGIN TRY
    BEGIN TRAN;

    IF NULLIF(LTRIM(RTRIM(@NuevoVideoId)), N'') IS NULL
        THROW 52019, 'VideoId no puede quedar vacio al activar video.', 1;

    DECLARE @OrdenActual NVARCHAR(MAX);
    DECLARE @OrdenNuevo NVARCHAR(MAX);

    SELECT @OrdenActual = OrdenSecciones
    FROM acc_academic.Lecciones
    WHERE IdLeccion = @IdLeccion;

    IF @OrdenActual IS NULL
        THROW 52020, 'No se encontro la leccion objetivo.', 1;

    IF ISJSON(@OrdenActual) <> 1
        THROW 52021, 'OrdenSecciones actual no es JSON valido.', 1;

    DECLARE @Orden TABLE
    (
        Pos INT IDENTITY(1,1) PRIMARY KEY,
        Token NVARCHAR(50) NOT NULL
    );

    INSERT INTO @Orden(Token)
    SELECT CAST([value] AS NVARCHAR(50))
    FROM OPENJSON(@OrdenActual)
    ORDER BY TRY_CONVERT(INT, [key]);

    IF NOT EXISTS (SELECT 1 FROM @Orden WHERE Token = N'video')
        INSERT INTO @Orden(Token) VALUES (N'video');

    SELECT @OrdenNuevo =
        N'[' + STRING_AGG(N'"' + REPLACE(Token, N'"', N'\"') + N'"', N',')
            WITHIN GROUP (ORDER BY Pos) + N']'
    FROM @Orden;

    UPDATE acc_academic.Lecciones
    SET TieneVideo = 1,
        VideoId = LTRIM(RTRIM(@NuevoVideoId)),
        OrdenSecciones = @OrdenNuevo
    WHERE IdLeccion = @IdLeccion;

    COMMIT TRAN;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;
GO
