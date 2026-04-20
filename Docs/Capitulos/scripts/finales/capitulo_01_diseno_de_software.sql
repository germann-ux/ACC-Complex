-- Insercion de capitulo de biblioteca (propuesta final, no ejecutada automaticamente)
-- Capitulo: Diseno de software
-- Jerarquia objetivo: Modulo 1 / Submodulo 1 / Tema 1

USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRAN;

    DECLARE @ModuloId INT = 1;
    DECLARE @SubmoduloId INT = 1;
    DECLARE @TemaId INT = 1;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.Modulos WHERE Id_Modulo = @ModuloId)
        THROW 56001, 'No existe el ModuloId=1 en acc_academic.Modulos.', 1;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.SubModulos WHERE Id_SubModulo = @SubmoduloId AND Id_Modulo = @ModuloId)
        THROW 56002, 'No existe el SubmoduloId=1 asociado al ModuloId=1.', 1;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.Temas WHERE Id_Tema = @TemaId AND Id_SubModulo = @SubmoduloId)
        THROW 56003, 'No existe el TemaId=1 asociado al SubmoduloId=1.', 1;

    DECLARE @TituloCapitulo NVARCHAR(100) = N'Diseño de software';
    DECLARE @SubtituloCapitulo NVARCHAR(100) = N'Fundamentos, estructura y criterio para organizar sistemas';
    DECLARE @DescripcionCapitulo NVARCHAR(500) = N'Capitulo que aborda el diseño de software como dominio conceptual: su definicion, su lugar en el ciclo de vida, la diferencia entre diseño logico y fisico, los principios de un buen diseño y el impacto tecnico de diseñar bien o mal.';

    IF EXISTS
    (
        SELECT 1
        FROM acc_academic.Capitulos
        WHERE TituloCapitulo = @TituloCapitulo
    )
        THROW 56004, 'Ya existe un capitulo con el titulo "Diseno de software".', 1;

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
