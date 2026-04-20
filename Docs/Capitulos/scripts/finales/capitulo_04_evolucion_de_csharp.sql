-- Insercion de capitulo de biblioteca (propuesta final, no ejecutada automaticamente)
-- Capitulo: Evolucion de C#
-- Jerarquia objetivo: Modulo 1 / Submodulo 2 / Tema 17

USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRAN;

    DECLARE @ModuloId INT = 1;
    DECLARE @SubmoduloId INT = 2;
    DECLARE @TemaId INT = 17;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.Modulos WHERE Id_Modulo = @ModuloId)
        THROW 56301, 'No existe el ModuloId=1 en acc_academic.Modulos.', 1;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.SubModulos WHERE Id_SubModulo = @SubmoduloId AND Id_Modulo = @ModuloId)
        THROW 56302, 'No existe el SubmoduloId=2 asociado al ModuloId=1.', 1;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.Temas WHERE Id_Tema = @TemaId AND Id_SubModulo = @SubmoduloId)
        THROW 56303, 'No existe el TemaId=17 asociado al SubmoduloId=2.', 1;

    DECLARE @TituloCapitulo NVARCHAR(100) = N'Evolucion de C#';
    DECLARE @SubtituloCapitulo NVARCHAR(100) = N'Origen, crecimiento y vigencia del lenguaje';
    DECLARE @DescripcionCapitulo NVARCHAR(500) = N'Capitulo centrado en el origen y la evolucion de C# como lenguaje. Explica su contexto de aparicion, su crecimiento tecnico y la relacion entre esos cambios y su permanencia en escenarios modernos de desarrollo.';

    IF EXISTS
    (
        SELECT 1
        FROM acc_academic.Capitulos
        WHERE TituloCapitulo = @TituloCapitulo
    )
        THROW 56304, 'Ya existe un capitulo con el titulo "Evolucion de C#".', 1;

    INSERT INTO acc_academic.Capitulos
    (
        TituloCapitulo,
        SubtituloCapitulo,
        Descripcion,
        ModuloId,
        SubmoduloId,
        TemaId,
        LeccionId
    )
    VALUES
    (
        @TituloCapitulo,
        @SubtituloCapitulo,
        @DescripcionCapitulo,
        @ModuloId,
        @SubmoduloId,
        @TemaId,
        NULL
    );

    DECLARE @IdCapitulo INT = SCOPE_IDENTITY();

    PRINT CONCAT('Capitulo insertado correctamente. IdCapitulo=', @IdCapitulo, '.');

    COMMIT;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;

    THROW;
END CATCH;
GO
