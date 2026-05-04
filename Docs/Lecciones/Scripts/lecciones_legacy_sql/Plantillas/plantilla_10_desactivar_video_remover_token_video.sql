-- Plantilla 10 - DESACTIVAR VIDEO + REMOVER TOKEN "video"
USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

/* ============================================================================
   PLANTILLA 10 - DESACTIVAR VIDEO + REMOVER TOKEN "video"
   ============================================================================ */
DECLARE @IdLeccion INT = 1; -- TODO

BEGIN TRY
    BEGIN TRAN;

    DECLARE @OrdenActual NVARCHAR(MAX);
    DECLARE @OrdenNuevo NVARCHAR(MAX);

    SELECT @OrdenActual = OrdenSecciones
    FROM acc_academic.Lecciones
    WHERE IdLeccion = @IdLeccion;

    IF @OrdenActual IS NULL
        THROW 52022, 'No se encontro la leccion objetivo.', 1;

    IF ISJSON(@OrdenActual) <> 1
        THROW 52023, 'OrdenSecciones actual no es JSON valido.', 1;

    DECLARE @Orden TABLE
    (
        Pos INT IDENTITY(1,1) PRIMARY KEY,
        Token NVARCHAR(50) NOT NULL
    );

    INSERT INTO @Orden(Token)
    SELECT CAST([value] AS NVARCHAR(50))
    FROM OPENJSON(@OrdenActual)
    WHERE CAST([value] AS NVARCHAR(50)) <> N'video'
    ORDER BY TRY_CONVERT(INT, [key]);

    SELECT @OrdenNuevo = CASE
        WHEN EXISTS (SELECT 1 FROM @Orden)
            THEN N'[' + STRING_AGG(N'"' + REPLACE(Token, N'"', N'\"') + N'"', N',')
                 WITHIN GROUP (ORDER BY Pos) + N']'
        ELSE N'[]'
    END
    FROM @Orden;

    UPDATE acc_academic.Lecciones
    SET TieneVideo = 0,
        VideoId = NULL,
        OrdenSecciones = ISNULL(@OrdenNuevo, N'[]')
    WHERE IdLeccion = @IdLeccion;

    COMMIT TRAN;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;
GO
