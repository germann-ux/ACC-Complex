-- Script final de insercion de leccion con bloques interactivos
-- Generado desde legacy: leccion_subtema_113_primer_vistazo_programa_hola_mundo.sql
-- Modelo vigente: acc_academic.Lecciones + acc_academic.BloquesLeccion
USE [ACC_Academic];
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO
BEGIN TRY
    BEGIN TRAN;
    DECLARE @SubtemaId INT = 113;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.SubTemas WHERE Id_SubTema = @SubtemaId)
        THROW 57001, 'No existe el SubTemaId objetivo en acc_academic.SubTemas.', 1;
    DECLARE @TituloLeccion NVARCHAR(100) = N'Primer vistazo: programa "Hola Mundo"';
    DECLARE @DescripcionLeccion NVARCHAR(500) = N'Muestra el primer codigo real en C#, explica que hace de forma general y lo conecta con el compilador de ACC para ejecutarlo.';
    DECLARE @NivelBloom NVARCHAR(64) = N'Aplicar';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","mermaid","ejemplo","compilador","practica","actividad","charpTip"]';
    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/subtema-113-hola-mundo';
    DECLARE @TieneCompilador BIT = 1;
    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(200) = N'ST113VIDPEND01';
    DECLARE @MermaidTitulo NVARCHAR(160) = N'Del codigo al resultado';
    DECLARE @MermaidDescripcion NVARCHAR(MAX) = N'Muestra el recorrido basico del primer programa: escribirlo, ejecutarlo en ACC y observar el mensaje en pantalla.';
    DECLARE @MermaidCodigo NVARCHAR(MAX) = N'flowchart LR
    A["Codigo Hola Mundo"] --> B["Compilador ACC"]
    B --> C["Ejecucion"]
    C --> D["Mensaje en pantalla"]

    style B fill:#1e1e2a,stroke:#9926fe,stroke-width:2px,color:#f8fafc';
    DECLARE @Teoria NVARCHAR(MAX) = N'
<div class="leccion-teoria">
    <h3>Tu primer programa real</h3>
    <p>Hasta ahora has visto que es C# y algunas de sus caracteristicas. En esta leccion aparece algo clave: tu primer programa real escrito en el lenguaje.</p>
    <p>El ejemplo clasico para comenzar suele ser <strong>Hola Mundo</strong>. Su objetivo no es resolver un problema grande, sino mostrar que ya puedes escribir codigo, ejecutarlo y obtener un resultado visible.</p>

    <h3>Que hace este programa</h3>
    <p>El programa toma una instruccion muy simple y muestra un mensaje en pantalla. Eso basta para entender una idea importante: el codigo no es solo texto, es algo que puede ejecutarse.</p>
    <p>En esta primera mirada no hace falta comprender cada simbolo a detalle. Lo importante es reconocer el conjunto, ver que produce una salida y empezar a perderle miedo al codigo.</p>

    <h3>Conexion con el compilador ACC</h3>
    <p>El compilador de ACC ya abre con un ejemplo listo para probar. Eso te permite ver el programa en accion desde esta misma leccion y comprobar como el cambio en el codigo produce un cambio en el resultado.</p>
    <img src="https://placehold.co/1200x675?text=Primer+programa+en+Csharp" alt="Primer vistazo al programa Hola Mundo (pendiente)">

    <div class="alert alert-info">
        <p class="alert-title">Idea clave</p>
        <p>Este primer programa importa porque conecta tres cosas por primera vez: escribir codigo, ejecutarlo y observar un resultado real.</p>
    </div>

    <div class="fomentador">
        <p>Si quieres profundizar mÃ¡s en este primer programa y en la forma en que se ejecuta, puedes ver contenido mÃ¡s detallado en el capÃ­tulo sobre Hola Mundo en C# haciendo clic <a href="Capitulo/Contenido/ID_CONTENIDO_PENDIENTE_HOLA_MUNDO_CSHARP">aquÃ­</a>.</p>
    </div>
</div>';
    DECLARE @Practica NVARCHAR(MAX) = N'
<div class="leccion-practicas">
    <h3>Practica guiada</h3>
    <ol>
        <li>Ejecuta el codigo tal como aparece en el compilador de ACC.</li>
        <li>Observa que mensaje muestra en pantalla.</li>
        <li>Cambia el texto <code>"Hola desde ACC!"</code> por otro saludo simple.</li>
        <li>Ejecuta otra vez y compara el nuevo resultado.</li>
    </ol>
    <p>Despues, explica con tus palabras que relacion hay entre cambiar una linea del codigo y ver un resultado distinto.</p>
    <div class="alert alert-success">
        <p class="alert-title">Criterio de logro</p>
        <p>Esta correcto si logras ejecutar el ejemplo, modificar el mensaje y reconocer que el resultado cambia porque el codigo tambien cambio.</p>
    </div>
</div>';
    DECLARE @Ejemplo NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>El codigo que veras en ACC</h3>
    <p>Este es el ejemplo base que el compilador de ACC ya carga para ti:</p>
    <pre><code>using System;
class Program {
    static void Main(string[] args) {
        Console.WriteLine("Hola desde ACC!");
    }
}</code></pre>
    <p>Al ejecutarlo, el programa muestra un mensaje en pantalla. Ese resultado te confirma que el codigo ya esta funcionando.</p>
    <p>Mas adelante revisaras con calma cada parte del programa. Por ahora basta con entender que este bloque completo representa una primera unidad de trabajo real en C#.</p>
    <img src="https://placehold.co/1200x675?text=Codigo+y+salida+Hola+Mundo" alt="Relacion entre codigo y salida del programa (pendiente)">

    <div class="alert alert-success">
        <p class="alert-title">Lo importante</p>
        <p>No necesitas memorizar todo desde la primera lectura. Primero entiende que el programa se ejecuta y produce una salida visible.</p>
    </div>
</div>';
    DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> En tu primer programa no busques memorizar cada simbolo. Primero entiende la idea grande: escribes algo, lo ejecutas y ves un resultado.</p>';
    DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta leccion vas a ver por primera vez codigo real ejecutandose en C#.</p><p>La meta es que conectes el programa con un resultado visible dentro del compilador de ACC.</p>';
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

