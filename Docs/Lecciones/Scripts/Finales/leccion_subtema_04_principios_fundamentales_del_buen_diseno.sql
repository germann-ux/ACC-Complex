-- Script final de insercion de leccion con bloques interactivos
-- Generado desde legacy: leccion_subtema_04_principios_fundamentales_del_buen_diseno.sql
-- Modelo vigente: acc_academic.Lecciones + acc_academic.BloquesLeccion
USE [ACC_Academic];
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO
BEGIN TRY
    BEGIN TRAN;
    DECLARE @SubtemaId INT = 4;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.SubTemas WHERE Id_SubTema = @SubtemaId)
        THROW 57001, 'No existe el SubTemaId objetivo en acc_academic.SubTemas.', 1;
    DECLARE @TituloLeccion NVARCHAR(100) = N'Principios fundamentales del buen diseÃ±o';
    DECLARE @DescripcionLeccion NVARCHAR(500) = N'Presenta ideas base para mantener un sistema mÃ¡s ordenado, claro y fÃ¡cil de modificar.';
    DECLARE @NivelBloom NVARCHAR(64) = N'Comprender';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","ejemplo","practica","actividad","charpTip"]';
    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/subtema-04-principios-diseno';
    DECLARE @TieneCompilador BIT = 0;
    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(200) = N'ST04VIDPEND01';
    DECLARE @MermaidTitulo NVARCHAR(160) = NULL;
    DECLARE @MermaidDescripcion NVARCHAR(MAX) = NULL;
    DECLARE @MermaidCodigo NVARCHAR(MAX) = NULL;
    DECLARE @Teoria NVARCHAR(MAX) = N'
<div class="leccion-teoria">
    <h3>Ideas que ayudan a mantener el orden</h3>
    <p>Un buen diseÃ±o no depende solo de que el programa funcione. TambiÃ©n importa que sea claro, que no mezcle demasiadas cosas y que pueda modificarse sin romper todo.</p>
    <p>Para lograrlo, existen algunas ideas base que ayudan a revisar si el sistema estÃ¡ bien organizado.</p>
    <ul>
        <li><strong>Una tarea principal por parte:</strong> cada clase o parte del sistema deberÃ­a encargarse de algo concreto. A esto se le relaciona con la cohesiÃ³n y la responsabilidad Ãºnica.</li>
        <li><strong>Pocas dependencias innecesarias:</strong> una parte no deberÃ­a estar demasiado amarrada a muchas otras. Eso se relaciona con el acoplamiento bajo.</li>
        <li><strong>Evitar mezclar demasiadas funciones:</strong> si una clase hace muchas cosas, cuesta mÃ¡s entenderla, probarla y cambiarla.</li>
        <li><strong>Reunir lo que se repite:</strong> si una misma lÃ³gica aparece varias veces, conviene llevarla a un solo lugar para no mantenerla por separado.</li>
    </ul>
    <img src="https://placehold.co/1200x675?text=Principios+de+codigo+ordenado" alt="Ideas base de un diseÃ±o ordenado (pendiente)">

    <div class="alert alert-info">
        <p class="alert-title">Punto clave</p>
        <p>Estos principios no existen para hacer el diseÃ±o mÃ¡s complicado. Sirven para que el sistema sea mÃ¡s claro de entender y mÃ¡s fÃ¡cil de cambiar con el tiempo.</p>
    </div>

    <div class="fomentador">
        <p>Si quieres profundizar mÃ¡s en estas ideas base del buen diseÃ±o, puedes ver contenido mÃ¡s detallado en el capÃ­tulo sobre principios de diseÃ±o haciendo clic <a href="Capitulo/Contenido/ID_CONTENIDO_PENDIENTE_PRINCIPIOS_DISENO">aquÃ­</a>.</p>
    </div>
</div>';
    DECLARE @Practica NVARCHAR(MAX) = N'
<div class="leccion-practicas">
    <h3>PrÃ¡ctica guiada</h3>
    <p>Lee este caso: una sola clase se encarga de validar usuarios, guardar datos, enviar correos y generar reportes.</p>
    <ol>
        <li>Escribe quÃ© tareas distintas estÃ¡n mezcladas en esa clase.</li>
        <li>PropÃ³n cÃ³mo las separarÃ­as en partes mÃ¡s claras.</li>
        <li>Explica por quÃ© esa separaciÃ³n harÃ­a mÃ¡s fÃ¡cil entender o cambiar el sistema.</li>
    </ol>
    <div class="alert alert-success">
        <p class="alert-title">Criterio de logro</p>
        <p>EstÃ¡ correcto si detectas mezcla de responsabilidades, propones una separaciÃ³n razonable y explicas una mejora concreta en claridad o mantenimiento.</p>
    </div>
</div>';
    DECLARE @Ejemplo NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>Ejemplo comparativo</h3>
    <p>Imagina una clase que registra usuarios, envÃ­a correos y genera reportes.</p>

    <div class="alert alert-error">
        <p class="alert-title">DiseÃ±o confuso</p>
        <p>Todo estÃ¡ en una sola clase. Cuando cambias una parte, aumentan las posibilidades de afectar otra que no tenÃ­a relaciÃ³n directa.</p>
    </div>

    <div class="alert alert-success">
        <p class="alert-title">DiseÃ±o mÃ¡s claro</p>
        <p>Una clase registra usuarios, otra envÃ­a correos y otra genera reportes. Cada una tiene una funciÃ³n mÃ¡s concreta y el sistema resulta mÃ¡s fÃ¡cil de mantener.</p>
    </div>

    <img src="https://placehold.co/1200x675?text=Clase+grande+vs+clases+claras" alt="ComparaciÃ³n entre una clase sobrecargada y varias clases claras (pendiente)">

    <p>La mejora no estÃ¡ en dividir por dividir, sino en separar tareas cuando ya estÃ¡n demasiado mezcladas.</p>
</div>';
    DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> Si te cuesta explicar en una sola frase para quÃ© sirve una clase, probablemente estÃ¡ haciendo mÃ¡s de lo que deberÃ­a.</p>';
    DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta lecciÃ³n vas a revisar ideas simples que ayudan a detectar cuÃ¡ndo un diseÃ±o estÃ¡ ordenado y cuÃ¡ndo empieza a complicarse.</p><p>La meta es que reconozcas seÃ±ales bÃ¡sicas de claridad, mezcla de tareas y repeticiÃ³n innecesaria.</p>';
    IF @NivelBloom NOT IN (N'Recordar', N'Comprender', N'Aplicar', N'Analizar', N'Evaluar', N'Crear')
        THROW 57002, 'NivelBloom invalido.', 1;
    IF ISJSON(@OrdenSecciones) <> 1
        THROW 57003, 'OrdenSecciones legacy no es JSON valido.', 1;
    IF EXISTS (SELECT [value] FROM OPENJSON(@OrdenSecciones) GROUP BY [value] HAVING COUNT(*) > 1)
        THROW 57004, 'OrdenSecciones legacy contiene tokens duplicados.', 1;
    IF EXISTS
    (
        SELECT 1
        FROM OPENJSON(@OrdenSecciones) j
        LEFT JOIN
        (
            VALUES
                (N'video'),
                (N'teoria'),
                (N'practica'),
                (N'ejemplo'),
                (N'actividad'),
                (N'compilador'),
                (N'mermaid'),
                (N'charpTip'),
                (N'charpDialog')
        ) permitidas(Token) ON permitidas.Token = CAST(j.[value] AS NVARCHAR(50))
        WHERE permitidas.Token IS NULL
    )
        THROW 57005, 'OrdenSecciones legacy contiene tokens no soportados por BloquesLeccion.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'teoria') AND NULLIF(LTRIM(RTRIM(ISNULL(@Teoria, N''))), N'') IS NULL
        THROW 57006, 'El bloque teoria requiere contenido.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'practica') AND NULLIF(LTRIM(RTRIM(ISNULL(@Practica, N''))), N'') IS NULL
        THROW 57007, 'El bloque practica requiere contenido.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'ejemplo') AND NULLIF(LTRIM(RTRIM(ISNULL(@Ejemplo, N''))), N'') IS NULL
        THROW 57008, 'El bloque ejemplo requiere contenido.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'mermaid') AND NULLIF(LTRIM(RTRIM(ISNULL(@MermaidCodigo, N''))), N'') IS NULL
        THROW 57009, 'El bloque mermaid requiere codigo.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpTip') AND NULLIF(LTRIM(RTRIM(ISNULL(@CharpTip, N''))), N'') IS NULL
        THROW 57010, 'El bloque charpTip requiere texto.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpDialog') AND NULLIF(LTRIM(RTRIM(ISNULL(@CharpDialog, N''))), N'') IS NULL
        THROW 57011, 'El bloque charpDialog requiere texto.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'actividad') AND (@TieneActividad = 0 OR NULLIF(LTRIM(RTRIM(ISNULL(@UrlActividad, N''))), N'') IS NULL)
        THROW 57012, 'El bloque actividad requiere flag y URL.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'compilador') AND @TieneCompilador = 0
        THROW 57013, 'El bloque compilador requiere flag.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'video') AND (@TieneVideo = 0 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NULL)
        THROW 57014, 'El bloque video requiere flag y VideoId.', 1;
    DECLARE @LeccionesARemover TABLE (IdLeccion INT PRIMARY KEY);

    INSERT INTO @LeccionesARemover (IdLeccion)
    SELECT IdLeccion
    FROM acc_academic.Lecciones
    WHERE SubtemaId = @SubtemaId
      AND (TituloLeccion = @TituloLeccion OR IdLeccion < 4000);

    DELETE b
    FROM acc_academic.BloquesLeccion b
    INNER JOIN @LeccionesARemover r ON r.IdLeccion = b.LeccionId;

    DELETE l
    FROM acc_academic.Lecciones l
    INNER JOIN @LeccionesARemover r ON r.IdLeccion = l.IdLeccion;
    INSERT INTO acc_academic.Lecciones
    (
        TituloLeccion,
        DescripcionLeccion,
        SubtemaId
    )
    VALUES
    (
        @TituloLeccion,
        @DescripcionLeccion,
        @SubtemaId
    );
    DECLARE @LeccionId INT = CONVERT(INT, SCOPE_IDENTITY());
    ;WITH SourceBlocks AS
    (
        SELECT
            TRY_CONVERT(INT, j.[key]) AS SourceOrden,
            CAST(j.[value] AS NVARCHAR(50)) AS Token
        FROM OPENJSON(@OrdenSecciones) j
    ),
    MappedBlocks AS
    (
        SELECT
            ROW_NUMBER() OVER (ORDER BY SourceOrden) AS Orden,
            CASE Token
                WHEN N'teoria' THEN N'TextoHtml'
                WHEN N'practica' THEN N'TextoHtml'
                WHEN N'ejemplo' THEN N'TextoHtml'
                WHEN N'mermaid' THEN N'Mermaid'
                WHEN N'charpTip' THEN N'CharpTip'
                WHEN N'charpDialog' THEN N'CharpDialog'
                WHEN N'actividad' THEN N'ActividadExterna'
                WHEN N'compilador' THEN N'Compilador'
                WHEN N'video' THEN N'Video'
            END AS TipoBloque,
            CASE Token
                WHEN N'teoria' THEN N'Teoria'
                WHEN N'practica' THEN N'Practica'
                WHEN N'ejemplo' THEN N'Ejemplo'
                WHEN N'mermaid' THEN NULLIF(@MermaidTitulo, N'')
                ELSE NULL
            END AS Titulo,
            CASE Token
                WHEN N'teoria' THEN CONCAT(N'{"html":"', STRING_ESCAPE(ISNULL(@Teoria, N''), 'json'), N'"}')
                WHEN N'practica' THEN CONCAT(N'{"html":"', STRING_ESCAPE(ISNULL(@Practica, N''), 'json'), N'"}')
                WHEN N'ejemplo' THEN CONCAT(N'{"html":"', STRING_ESCAPE(ISNULL(@Ejemplo, N''), 'json'), N'"}')
                WHEN N'mermaid' THEN CONCAT(N'{"codigo":"', STRING_ESCAPE(ISNULL(@MermaidCodigo, N''), 'json'), N'","descripcion":"', STRING_ESCAPE(ISNULL(@MermaidDescripcion, N''), 'json'), N'"}')
                WHEN N'charpTip' THEN CONCAT(N'{"texto":"', STRING_ESCAPE(ISNULL(@CharpTip, N''), 'json'), N'"}')
                WHEN N'charpDialog' THEN CONCAT(N'{"texto":"', STRING_ESCAPE(ISNULL(@CharpDialog, N''), 'json'), N'"}')
                WHEN N'actividad' THEN CONCAT(N'{"url":"', STRING_ESCAPE(ISNULL(@UrlActividad, N''), 'json'), N'","textoBoton":"Ver actividad interactiva"}')
                WHEN N'compilador' THEN N'{"lenguaje":"csharp","codigoInicial":"","instrucciones":""}'
                WHEN N'video' THEN CONCAT(N'{"videoId":"', STRING_ESCAPE(ISNULL(@VideoId, N''), 'json'), N'","proveedor":"youtube"}')
            END AS ConfiguracionJson
        FROM SourceBlocks
    )
    INSERT INTO acc_academic.BloquesLeccion
    (
        LeccionId,
        TipoBloque,
        Orden,
        ConfiguracionJson,
        Titulo,
        NivelBloom,
        EsObligatorio,
        Puntaje
    )
    SELECT
        @LeccionId,
        TipoBloque,
        Orden,
        ConfiguracionJson,
        Titulo,
        NULLIF(@NivelBloom, N''),
        CAST(0 AS BIT),
        NULL
    FROM MappedBlocks;
    COMMIT TRAN;
    SELECT
        l.IdLeccion,
        l.TituloLeccion,
        l.SubtemaId,
        COUNT(b.IdBloqueLeccion) AS TotalBloques
    FROM acc_academic.Lecciones l
    LEFT JOIN acc_academic.BloquesLeccion b ON b.LeccionId = l.IdLeccion
    WHERE l.IdLeccion = @LeccionId
    GROUP BY l.IdLeccion, l.TituloLeccion, l.SubtemaId;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;
GO

