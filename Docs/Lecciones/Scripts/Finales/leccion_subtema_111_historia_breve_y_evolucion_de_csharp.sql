-- Insercion de leccion (propuesta final, no ejecutada automaticamente)
-- SubtemaId objetivo: 111
-- Subtema: Historia breve y evolucion de C#

USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRAN;

    DECLARE @SubtemaId INT = 111;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.SubTemas WHERE Id_SubTema = @SubtemaId)
        THROW 54201, 'No existe el SubTemaId=111 en acc_academic.SubTemas.', 1;

    DECLARE @TituloLeccion NVARCHAR(100) = N'Historia breve y evolucion de C#';
    DECLARE @DescripcionLeccion NVARCHAR(500) = N'Explica el origen de C#, su relacion con .NET y la diferencia entre .NET Framework, .NET Core y ASP.NET.';
    DECLARE @NivelBloom NVARCHAR(20) = N'Comprender';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","mermaid","ejemplo","practica","actividad","charpTip"]';

    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/subtema-111-historia-csharp';
    DECLARE @TieneCompilador BIT = 0;
    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(20) = N'ST111VIDPEND01';

    DECLARE @MermaidTitulo NVARCHAR(200) = N'Evolucion del ecosistema C#';
    DECLARE @MermaidDescripcion NVARCHAR(500) = N'Muestra el paso de C# junto a .NET, desde .NET Framework hasta .NET Core y el modelo actual de .NET.';
    DECLARE @MermaidCodigo NVARCHAR(MAX) = N'flowchart LR
    A["C# nace con Microsoft"] --> B["C# sobre .NET Framework"]
    B --> C["C# evoluciona con .NET"]
    C --> D[".NET Core moderniza la plataforma"]
    D --> E[".NET actual unifica el camino"]

    style D fill:#1e1e2a,stroke:#9926fe,stroke-width:2px,color:#f8fafc';

    DECLARE @Teoria NVARCHAR(MAX) = N'
<div class="leccion-teoria">
    <h3>De donde viene C#</h3>
    <p>C# nacio en el entorno de Microsoft como un lenguaje pensado para crear software de forma moderna, clara y estructurada. Desde el inicio estuvo ligado a .NET, que es la plataforma con la que suele trabajar.</p>
    <p>Eso significa que C# y .NET estan muy relacionados, pero no son lo mismo: C# es el lenguaje; .NET es la plataforma donde ese lenguaje se usa para construir y ejecutar aplicaciones.</p>

    <h3>Que fue cambiando con el tiempo</h3>
    <p>Al principio, muchos proyectos con C# trabajaban sobre <strong>.NET Framework</strong>, que fue durante años la base clasica del ecosistema y estuvo muy ligada a Windows.</p>
    <p>Despues aparecio <strong>.NET Core</strong>, una etapa mas moderna que abrio el camino hacia desarrollo multiplataforma y una estructura mas flexible.</p>
    <p>Con el tiempo, el ecosistema siguio avanzando hasta el modelo actual de <strong>.NET</strong>, donde el camino se simplifica y se busca una plataforma mas unificada.</p>

    <h3>Lo que suele confundirse</h3>
    <ul>
        <li><strong>C#:</strong> es el lenguaje de programacion.</li>
        <li><strong>.NET:</strong> es la plataforma general donde C# se apoya para crear aplicaciones.</li>
        <li><strong>.NET Framework:</strong> fue la base clasica de .NET, muy asociada a Windows.</li>
        <li><strong>.NET Core:</strong> fue una evolucion mas moderna y multiplataforma de la plataforma.</li>
        <li><strong>ASP.NET:</strong> es una tecnologia del ecosistema .NET para crear aplicaciones web.</li>
        <li><strong>ASP.NET Core:</strong> es la version web ligada al modelo moderno de .NET Core y .NET.</li>
    </ul>
    <img src="https://placehold.co/1200x675?text=Historia+y+evolucion+de+Csharp" alt="Relacion entre C#, .NET y sus etapas (pendiente)">

    <div class="alert alert-info">
        <p class="alert-title">Idea clave</p>
        <p>No todo lo que suena parecido significa lo mismo. C# es el lenguaje; .NET es la plataforma; ASP.NET es una tecnologia web dentro de ese ecosistema.</p>
    </div>

    <div class="fomentador">
        <p>Si quieres profundizar más en la historia de C# y en la diferencia entre .NET Framework, .NET Core y ASP.NET, puedes ver contenido más detallado en el capítulo sobre evolución de C# haciendo clic <a href="Capitulo/Contenido/ID_CONTENIDO_PENDIENTE_HISTORIA_CSHARP">aquí</a>.</p>
    </div>
</div>';

    DECLARE @Ejemplo NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>Ejemplo de confusion comun</h3>
    <p>Una persona puede decir: "Voy a aprender ASP.NET" o "Voy a trabajar con .NET". Eso no significa exactamente lo mismo que decir "Voy a aprender C#".</p>
    <p>Si aprende <strong>C#</strong>, aprende el lenguaje. Si trabaja con <strong>.NET</strong>, trabaja en la plataforma. Si usa <strong>ASP.NET</strong>, entra al area web de ese ecosistema.</p>
    <p>Por eso conviene separar bien cada nombre desde el inicio.</p>
    <img src="https://placehold.co/1200x675?text=Csharp+NET+y+ASP.NET" alt="Diferencia entre lenguaje, plataforma y tecnologia web (pendiente)">

    <div class="alert alert-success">
        <p class="alert-title">Lo importante</p>
        <p>Entender esta diferencia evita muchas confusiones cuando empieces a leer documentacion, ver tutoriales o trabajar con proyectos reales.</p>
    </div>
</div>';

    DECLARE @Practica NVARCHAR(MAX) = N'
<div class="leccion-practicas">
    <h3>Practica guiada</h3>
    <ol>
        <li>Escribe con tus palabras cual es la diferencia entre C# y .NET.</li>
        <li>Explica que cambia entre .NET Framework y .NET Core de forma general.</li>
        <li>Indica donde entra ASP.NET dentro de este ecosistema.</li>
    </ol>
    <p>Despues, responde por que puede ser confuso leer estos nombres si no se separan bien desde el principio.</p>
    <div class="alert alert-success">
        <p class="alert-title">Criterio de logro</p>
        <p>Esta correcto si distingues lenguaje, plataforma y tecnologia web, y si reconoces que la evolucion del ecosistema cambio el alcance de C# con el tiempo.</p>
    </div>
</div>';

    DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> Cuando leas sobre C#, .NET o ASP.NET, primero pregunta: estoy viendo un lenguaje, una plataforma o una tecnologia especifica?</p>';
    DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta leccion vas a entender de donde viene C# y como fue creciendo junto a .NET.</p><p>La meta es que dejes de ver estos nombres como si fueran lo mismo y empieces a ubicarlos con claridad.</p>';

    IF EXISTS
    (
        SELECT 1
        FROM acc_academic.Lecciones
        WHERE SubtemaId = @SubtemaId
          AND TituloLeccion = @TituloLeccion
    )
        THROW 54202, 'Ya existe una leccion con este titulo para SubtemaId=111.', 1;

    IF @NivelBloom NOT IN (N'Recordar', N'Comprender', N'Aplicar', N'Analizar', N'Evaluar', N'Crear')
        THROW 54203, 'NivelBloom invalido.', 1;

    IF ISJSON(@OrdenSecciones) <> 1
        THROW 54204, 'OrdenSecciones no es JSON valido.', 1;

    IF EXISTS
    (
        SELECT j.[value]
        FROM OPENJSON(@OrdenSecciones) j
        GROUP BY j.[value]
        HAVING COUNT(*) > 1
    )
        THROW 54205, 'OrdenSecciones contiene tokens duplicados.', 1;

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
        THROW 54206, 'OrdenSecciones contiene tokens fuera del conjunto permitido.', 1;

    DECLARE @SecTeoria BIT      = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'teoria') THEN 1 ELSE 0 END;
    DECLARE @SecMermaid BIT     = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'mermaid') THEN 1 ELSE 0 END;
    DECLARE @SecEjemplo BIT     = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'ejemplo') THEN 1 ELSE 0 END;
    DECLARE @SecPractica BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'practica') THEN 1 ELSE 0 END;
    DECLARE @SecCharpTip BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpTip') THEN 1 ELSE 0 END;
    DECLARE @SecCharpDialog BIT = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpDialog') THEN 1 ELSE 0 END;
    DECLARE @SecActividad BIT   = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'actividad') THEN 1 ELSE 0 END;
    DECLARE @SecCompilador BIT  = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'compilador') THEN 1 ELSE 0 END;
    DECLARE @SecVideo BIT       = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'video') THEN 1 ELSE 0 END;

    IF @SecTeoria = 1 AND NULLIF(LTRIM(RTRIM(@Teoria)), N'') IS NULL THROW 54207, 'Falta Teoria.', 1;
    IF @SecMermaid = 1 AND NULLIF(LTRIM(RTRIM(@MermaidCodigo)), N'') IS NULL THROW 542071, 'Falta MermaidCodigo.', 1;
    IF @SecEjemplo = 1 AND NULLIF(LTRIM(RTRIM(@Ejemplo)), N'') IS NULL THROW 54208, 'Falta Ejemplo.', 1;
    IF @SecPractica = 1 AND NULLIF(LTRIM(RTRIM(@Practica)), N'') IS NULL THROW 54209, 'Falta Practica.', 1;
    IF @SecCharpTip = 1 AND NULLIF(LTRIM(RTRIM(@CharpTip)), N'') IS NULL THROW 54210, 'Falta CharpTip.', 1;
    IF @SecCharpDialog = 1 AND NULLIF(LTRIM(RTRIM(@CharpDialog)), N'') IS NULL THROW 54211, 'Falta CharpDialog.', 1;
    IF @SecCharpTip = 1 AND @CharpTip LIKE N'%<div%' THROW 54212, 'CharpTip no debe incluir <div>.', 1;
    IF @SecCharpDialog = 1 AND @CharpDialog LIKE N'%<div%' THROW 54213, 'CharpDialog no debe incluir <div>.', 1;
    IF @SecActividad = 1 AND (@TieneActividad = 0 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NULL) THROW 54214, 'Actividad requiere flag y URL.', 1;
    IF @SecActividad = 0 AND (@TieneActividad = 1 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NOT NULL) THROW 54215, 'Sin actividad, limpiar flag/URL.', 1;
    IF @SecCompilador = 1 AND @TieneCompilador = 0 THROW 54216, 'Compilador requiere flag.', 1;
    IF @SecCompilador = 0 AND @TieneCompilador = 1 THROW 54217, 'Sin compilador, flag debe ser 0.', 1;
    IF @SecVideo = 1 AND (@TieneVideo = 0 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NULL OR @VideoId LIKE N'%youtube%' OR @VideoId LIKE N'%http%') THROW 54218, 'Video requiere flag y VideoId limpio.', 1;
    IF @SecVideo = 0 AND (@TieneVideo = 1 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NOT NULL) THROW 54219, 'Sin video, limpiar flag/VideoId.', 1;

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
