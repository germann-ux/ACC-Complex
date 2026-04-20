-- Insercion de leccion (propuesta final, no ejecutada automaticamente)
-- SubtemaId objetivo: 113
-- Subtema: Primer vistazo: programa "Hola Mundo"

USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRAN;

    DECLARE @SubtemaId INT = 113;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.SubTemas WHERE Id_SubTema = @SubtemaId)
        THROW 54601, 'No existe el SubTemaId=113 en acc_academic.SubTemas.', 1;

    DECLARE @TituloLeccion NVARCHAR(100) = N'Primer vistazo: programa "Hola Mundo"';
    DECLARE @DescripcionLeccion NVARCHAR(500) = N'Muestra el primer codigo real en C#, explica que hace de forma general y lo conecta con el compilador de ACC para ejecutarlo.';
    DECLARE @NivelBloom NVARCHAR(20) = N'Aplicar';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","mermaid","ejemplo","compilador","practica","actividad","charpTip"]';

    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/subtema-113-hola-mundo';
    DECLARE @TieneCompilador BIT = 1;
    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(20) = N'ST113VIDPEND01';

    DECLARE @MermaidTitulo NVARCHAR(200) = N'Del codigo al resultado';
    DECLARE @MermaidDescripcion NVARCHAR(500) = N'Muestra el recorrido basico del primer programa: escribirlo, ejecutarlo en ACC y observar el mensaje en pantalla.';
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
        <p>Si quieres profundizar más en este primer programa y en la forma en que se ejecuta, puedes ver contenido más detallado en el capítulo sobre Hola Mundo en C# haciendo clic <a href="Capitulo/Contenido/ID_CONTENIDO_PENDIENTE_HOLA_MUNDO_CSHARP">aquí</a>.</p>
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

    DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> En tu primer programa no busques memorizar cada simbolo. Primero entiende la idea grande: escribes algo, lo ejecutas y ves un resultado.</p>';
    DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta leccion vas a ver por primera vez codigo real ejecutandose en C#.</p><p>La meta es que conectes el programa con un resultado visible dentro del compilador de ACC.</p>';

    IF EXISTS
    (
        SELECT 1
        FROM acc_academic.Lecciones
        WHERE SubtemaId = @SubtemaId
          AND TituloLeccion = @TituloLeccion
    )
        THROW 54602, 'Ya existe una leccion con este titulo para SubtemaId=113.', 1;

    IF @NivelBloom NOT IN (N'Recordar', N'Comprender', N'Aplicar', N'Analizar', N'Evaluar', N'Crear')
        THROW 54603, 'NivelBloom invalido.', 1;

    IF ISJSON(@OrdenSecciones) <> 1
        THROW 54604, 'OrdenSecciones no es JSON valido.', 1;

    IF EXISTS
    (
        SELECT j.[value]
        FROM OPENJSON(@OrdenSecciones) j
        GROUP BY j.[value]
        HAVING COUNT(*) > 1
    )
        THROW 54605, 'OrdenSecciones contiene tokens duplicados.', 1;

    IF EXISTS
    (
        SELECT 1
        FROM OPENJSON(@OrdenSecciones) j
        LEFT JOIN
        (
            VALUES (N'video'), (N'teoria'), (N'mermaid'), (N'practica'), (N'ejemplo'),
                   (N'actividad'), (N'compilador'), (N'charpTip'), (N'charpDialog')
        ) permitidas(Token) ON permitidas.Token = CAST(j.[value] AS NVARCHAR(50))
        WHERE permitidas.Token IS NULL
    )
        THROW 54606, 'OrdenSecciones contiene tokens fuera del conjunto permitido.', 1;

    DECLARE @SecTeoria BIT      = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'teoria') THEN 1 ELSE 0 END;
    DECLARE @SecMermaid BIT     = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'mermaid') THEN 1 ELSE 0 END;
    DECLARE @SecEjemplo BIT     = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'ejemplo') THEN 1 ELSE 0 END;
    DECLARE @SecPractica BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'practica') THEN 1 ELSE 0 END;
    DECLARE @SecCharpTip BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpTip') THEN 1 ELSE 0 END;
    DECLARE @SecCharpDialog BIT = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpDialog') THEN 1 ELSE 0 END;
    DECLARE @SecActividad BIT   = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'actividad') THEN 1 ELSE 0 END;
    DECLARE @SecCompilador BIT  = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'compilador') THEN 1 ELSE 0 END;
    DECLARE @SecVideo BIT       = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'video') THEN 1 ELSE 0 END;

    IF @SecTeoria = 1 AND NULLIF(LTRIM(RTRIM(@Teoria)), N'') IS NULL THROW 54607, 'Falta Teoria.', 1;
    IF @SecMermaid = 1 AND NULLIF(LTRIM(RTRIM(@MermaidCodigo)), N'') IS NULL THROW 546071, 'Falta MermaidCodigo.', 1;
    IF @SecEjemplo = 1 AND NULLIF(LTRIM(RTRIM(@Ejemplo)), N'') IS NULL THROW 54608, 'Falta Ejemplo.', 1;
    IF @SecPractica = 1 AND NULLIF(LTRIM(RTRIM(@Practica)), N'') IS NULL THROW 54609, 'Falta Practica.', 1;
    IF @SecCharpTip = 1 AND NULLIF(LTRIM(RTRIM(@CharpTip)), N'') IS NULL THROW 54610, 'Falta CharpTip.', 1;
    IF @SecCharpDialog = 1 AND NULLIF(LTRIM(RTRIM(@CharpDialog)), N'') IS NULL THROW 54611, 'Falta CharpDialog.', 1;
    IF @SecCharpTip = 1 AND @CharpTip LIKE N'%<div%' THROW 54612, 'CharpTip no debe incluir <div>.', 1;
    IF @SecCharpDialog = 1 AND @CharpDialog LIKE N'%<div%' THROW 54613, 'CharpDialog no debe incluir <div>.', 1;
    IF @SecActividad = 1 AND (@TieneActividad = 0 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NULL) THROW 54614, 'Actividad requiere flag y URL.', 1;
    IF @SecActividad = 0 AND (@TieneActividad = 1 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NOT NULL) THROW 54615, 'Sin actividad, limpiar flag/URL.', 1;
    IF @SecCompilador = 1 AND @TieneCompilador = 0 THROW 54616, 'Compilador requiere flag.', 1;
    IF @SecCompilador = 0 AND @TieneCompilador = 1 THROW 54617, 'Sin compilador, flag debe ser 0.', 1;
    IF @SecVideo = 1 AND (@TieneVideo = 0 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NULL OR @VideoId LIKE N'%youtube%' OR @VideoId LIKE N'%http%') THROW 54618, 'Video requiere flag y VideoId limpio.', 1;
    IF @SecVideo = 0 AND (@TieneVideo = 1 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NOT NULL) THROW 54619, 'Sin video, limpiar flag/VideoId.', 1;

    INSERT INTO acc_academic.Lecciones
    (
        TituloLeccion, DescripcionLeccion, TieneActividad, UrlActividad, TieneCompilador,
        OrdenSecciones, SubtemaId, MermaidTitulo, MermaidDescripcion, MermaidCodigo,
        Teoria, Practica, Ejemplo, CharpTip, CharpDialog,
        NivelBloom, VideoId, TieneVideo
    )
    VALUES
    (
        @TituloLeccion, @DescripcionLeccion, @TieneActividad, @UrlActividad, @TieneCompilador,
        @OrdenSecciones, @SubtemaId, @MermaidTitulo, @MermaidDescripcion, @MermaidCodigo,
        @Teoria, @Practica, @Ejemplo, @CharpTip, @CharpDialog,
        @NivelBloom, @VideoId, @TieneVideo
    );

    COMMIT TRAN;

    SELECT TOP (1)
        IdLeccion, TituloLeccion, SubtemaId, NivelBloom, OrdenSecciones, MermaidTitulo, TieneActividad, TieneCompilador, TieneVideo
    FROM acc_academic.Lecciones
    WHERE SubtemaId = @SubtemaId
      AND TituloLeccion = @TituloLeccion
    ORDER BY IdLeccion DESC;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;
GO
