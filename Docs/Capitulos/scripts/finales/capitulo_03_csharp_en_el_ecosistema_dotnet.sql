-- Insercion de capitulo de biblioteca (propuesta final, no ejecutada automaticamente)
-- Capitulo: C# en el ecosistema .NET
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
        THROW 56201, 'No existe el ModuloId=1 en acc_academic.Modulos.', 1;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.SubModulos WHERE Id_SubModulo = @SubmoduloId AND Id_Modulo = @ModuloId)
        THROW 56202, 'No existe el SubmoduloId=2 asociado al ModuloId=1.', 1;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.Temas WHERE Id_Tema = @TemaId AND Id_SubModulo = @SubmoduloId)
        THROW 56203, 'No existe el TemaId=17 asociado al SubmoduloId=2.', 1;

    DECLARE @TituloCapitulo NVARCHAR(100) = N'C# en el ecosistema .NET';
    DECLARE @SubtituloCapitulo NVARCHAR(100) = N'Lenguaje, plataforma y tecnologias relacionadas';
    DECLARE @DescripcionCapitulo NVARCHAR(500) = N'Capitulo orientado a diferenciar con precision C#, .NET y tecnologias cercanas del ecosistema. Organiza lenguaje, plataforma, herramientas, componentes y escenarios de uso para evitar confusiones terminologicas frecuentes.';

    IF EXISTS
    (
        SELECT 1
        FROM acc_academic.Capitulos
        WHERE TituloCapitulo = @TituloCapitulo
    )
        THROW 56204, 'Ya existe un capitulo con el titulo "C# en el ecosistema .NET".', 1;

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
