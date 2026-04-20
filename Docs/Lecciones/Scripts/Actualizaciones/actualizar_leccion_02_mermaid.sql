-- Actualizacion puntual de Mermaid para la leccion existente:
-- IdLeccion = 2
-- TituloLeccion = Fases del diseno en el ciclo de vida

USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRAN;

    DECLARE @IdLeccion INT = 2;

    IF NOT EXISTS
    (
        SELECT 1
        FROM acc_academic.Lecciones
        WHERE IdLeccion = @IdLeccion
    )
        THROW 53601, 'No existe la leccion esperada con IdLeccion=2.', 1;

    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","mermaid","ejemplo","practica","actividad","charpTip"]';
    DECLARE @MermaidTitulo NVARCHAR(200) = N'Flujo basico del desarrollo';
    DECLARE @MermaidDescripcion NVARCHAR(500) = N'Muestra el orden general del trabajo y resalta que el diseño ocurre antes de programar.';
    DECLARE @MermaidCodigo NVARCHAR(MAX) = N'flowchart LR
    A["Entender el problema"] --> B["Diseñar el sistema"]
    B --> C["Programar"]
    C --> D["Probar"]
    D --> E["Ajustar si hace falta"]

    style B fill:#1e1e2a,stroke:#9926fe,stroke-width:2px,color:#f8fafc';

    UPDATE acc_academic.Lecciones
    SET OrdenSecciones = @OrdenSecciones,
        MermaidTitulo = @MermaidTitulo,
        MermaidDescripcion = @MermaidDescripcion,
        MermaidCodigo = @MermaidCodigo
    WHERE IdLeccion = @IdLeccion;

    COMMIT TRAN;

    SELECT
        IdLeccion,
        TituloLeccion,
        OrdenSecciones,
        MermaidTitulo,
        MermaidDescripcion,
        MermaidCodigo
    FROM acc_academic.Lecciones
    WHERE IdLeccion = @IdLeccion;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;
GO
