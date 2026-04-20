# 3.5. Desarrollo del software

El desarrollo del software de **Aprendiendo C# con Charp (ACC)** se estructuró como una solución modular orientada a la separación de responsabilidades entre interfaz, lógica de negocio, persistencia e infraestructura de apoyo. La aplicación quedó conformada por un entorno web en **Blazor Web App**, una **API académica en ASP.NET Core**, una capa de acceso a datos con **Entity Framework Core**, un servicio especializado para compilación de código con **Roslyn** y una orquestación integral mediante **.NET Aspire**.

La construcción del sistema se enfocó en consolidar una plataforma educativa interactiva capaz de integrar autenticación por roles, navegación académica jerárquica, lecciones dinámicas, seguimiento de progreso, gestión de evaluaciones y compilación de código en línea. Esta organización permitió que cada componente cumpliera una función específica dentro del producto y facilitó el mantenimiento, la reutilización de servicios y la evolución del sistema.

Entre los desarrollos más representativos de la aplicación destacan la orquestación distribuida del entorno, el modelo de lecciones configurables por secciones, la lógica de validación y registro de exámenes, así como el compilador interactivo que brinda retroalimentación inmediata al estudiante. En conjunto, estos elementos dan forma a una solución cohesionada, orientada tanto a la funcionalidad técnica como al propósito formativo del proyecto.

## 3.5.1. Programación

La programación del sistema se organizó en módulos claramente diferenciados, cada uno con responsabilidades concretas dentro de la solución:

| Módulo | Función principal |
|---|---|
| `ACC.WebApp` | Aloja la aplicación, administra identidad, autenticación, roles y configuración del host. |
| `ACC.WebApp.Client` | Implementa la interfaz Blazor, la guía académica, la visualización de lecciones y el compilador en cliente. |
| `ACC.API` | Expone la lógica del dominio académico: navegación, progreso, exámenes, tareas, aulas y contenido. |
| `API_CompilerACC` | Ejecuta la compilación y la corrida controlada de código C# mediante Roslyn. |
| `ACC.Data` | Modela entidades, relaciones y persistencia sobre SQL Server. |
| `ACC.Shared` | Centraliza contratos, DTOs, enums y tipos reutilizables entre proyectos. |
| `ACC.AppHost` | Orquesta la topología completa de la solución con Aspire. |

Uno de los módulos generales mejor resueltos es la orquestación del sistema, ya que concentra en un solo punto la declaración de servicios, bases de datos y dependencias de infraestructura. El siguiente fragmento muestra la configuración base de la solución:

```csharp
var builder = DistributedApplication.CreateBuilder(args);

var sqlPassword = builder.AddParameter("sql-password", secret: true);

var sqlIdentity = builder.AddSqlServer("acc-sql-identity", sqlPassword, port: 1434);
var dbIdentity = sqlIdentity.AddDatabase("acc-identity-db", "ACC_Identity");

var sqlAcademic = builder.AddSqlServer("acc-sql-academic", sqlPassword, port: 1435);
var dbAcademic = sqlAcademic.AddDatabase("acc-academic-db", "ACC_Academic");

var redis = builder.AddRedis("acc-redis");

builder.AddProject<Projects.ACC_Compiler>("acc-compiler").WithReference(redis);
builder.AddProject<Projects.ACC_API>("acc-api").WithReference(dbAcademic).WaitFor(dbAcademic);
builder.AddProject<Projects.ACC_WebApp>("acc-blazor")
    .WithReference(dbIdentity)
    .WithReference(dbAcademic)
    .WithReference(redis);
```

En el dominio académico, la entidad `Leccion` representa una de las piezas más relevantes del sistema, ya que permite construir experiencias de aprendizaje configurables por contenido y por orden de presentación. Su diseño incorpora teoría, práctica, ejemplos, recursos audiovisuales, interacción con Charp y activación del compilador:

```csharp
public class Leccion : INodoJerarquico
{
    public int IdLeccion { get; set; }
    public string TituloLeccion { get; set; } = string.Empty;
    public string DescripcionLeccion { get; set; } = string.Empty;
    public bool TieneActividad { get; set; } = false;
    public string? UrlActividad { get; set; } = null;
    public bool TieneCompilador { get; set; } = false;
    public List<string> OrdenSecciones { get; set; } = [];
    public string Teoria { get; set; } = string.Empty;
    public string Practica { get; set; } = string.Empty;
    public string Ejemplo { get; set; } = string.Empty;
    public string? CharpTip { get; set; } = null;
    public string? CharpDialog { get; set; } = null;
    public string NivelBloom { get; set; } = string.Empty;
    public string? VideoId { get; set; }
    public bool TieneVideo { get; set; }
}
```

La programación de la interfaz aprovecha este modelo para renderizar cada lección en forma dinámica. En el componente `RDL.razor`, la aplicación interpreta la secuencia almacenada en `OrdenSecciones` y construye la experiencia didáctica correspondiente:

```csharp
@foreach (var seccion in Leccion.OrdenSecciones)
{
    switch (seccion)
    {
        case SeccionesContenido.Mermaid:
            <MermaidSection Titulo="@Leccion.MermaidTitulo"
                            Descripcion="@Leccion.MermaidDescripcion"
                            Codigo="@Leccion.MermaidCodigo" />
            break;

        case SeccionesContenido.CharpDialog:
            <CharpDialog Texto="@Leccion.CharpDialog" />
            break;

        case SeccionesContenido.Teoria:
            @((MarkupString)Leccion.Teoria)
            break;

        case SeccionesContenido.Compilador:
            <CompiladorACC />
            break;
    }
}
```

Otro bloque funcional de alta relevancia corresponde al servicio de exámenes, donde se concentra la validación de intentos, el cálculo de resultados y el control de reglas de negocio. El siguiente fragmento ilustra parte de esa lógica:

```csharp
public async Task<ServiceResult<ExamenIntentoDto>> RegistrarIntentoAsync(ExamenIntentoDto intentoDto)
{
    if (string.IsNullOrWhiteSpace(intentoDto.IdUsuario))
        return ServiceResult<ExamenIntentoDto>.Fail("El id del usuario no puede estar vacío.");

    if (intentoDto.NumeroAciertos > intentoDto.TotalPreguntas)
        return ServiceResult<ExamenIntentoDto>.Fail("El numero de aciertos no puede exceder el total de preguntas.");

    var configuracion = await ObtenerConfiguracionExamenAsync(examenRef.tipo, examenRef.examenId);
    if (configuracion is null)
        return ServiceResult<ExamenIntentoDto>.NotFound("El examen solicitado no existe.");

    if (intentoDto.TotalPreguntas != configuracion.NumeroPreguntas)
        return ServiceResult<ExamenIntentoDto>.Fail("El total de preguntas enviado no coincide con la configuracion del examen.");

    var intentosRealizados = await sameExamQuery.CountAsync();
    if (intentosRealizados >= configuracion.IntentosMaximos)
        return ServiceResult<ExamenIntentoDto>.Fail("El usuario ya alcanzo el numero maximo de intentos para este examen.");
}
```

En el mismo sentido, el compilador en línea constituye una aportación técnica central del proyecto. Su implementación con Roslyn permite compilar código en memoria y devolver resultados de ejecución dentro del flujo educativo:

```csharp
var syntaxTree = CSharpSyntaxTree.ParseText(code);

var compilation = CSharpCompilation.Create(
    assemblyName: "ACC_Code",
    syntaxTrees: new[] { syntaxTree },
    references: references,
    options: new CSharpCompilationOptions(OutputKind.ConsoleApplication)
);

using var ms = new MemoryStream();
var emitResult = compilation.Emit(ms);

if (!emitResult.Success)
{
    var sb = new StringBuilder();
    sb.AppendLine("Errores de compilación:");
    foreach (var diag in emitResult.Diagnostics)
    {
        sb.AppendLine(diag.ToString());
    }
    return sb.ToString();
}
```

En términos generales, la programación del software se consolidó con base en principios de modularidad, reutilización, cohesión entre componentes y alineación con la finalidad pedagógica del sistema. La estructura alcanzada permite sostener la operación actual de la plataforma y facilita la incorporación de nuevas funciones sin comprometer la organización del producto.

## 3.5.2. Pruebas del sistema

Las pruebas del sistema se abordaron como una actividad integral de validación técnica y funcional, con el propósito de comprobar el comportamiento correcto de los principales módulos de la aplicación. La estrategia aplicada combinó pruebas unitarias, pruebas de integración, validación funcional sobre el entorno completo y revisiones operativas en escenarios de uso cercanos al comportamiento real del sistema.

Dentro del proyecto se incorporó un conjunto de pruebas automatizadas orientadas a verificar reglas críticas del dominio. El repositorio incluye el proyecto `tests/ACC.Tests/ACC.Tests.csproj`, construido sobre `xUnit`, `Moq`, `Microsoft.EntityFrameworkCore.InMemory` y `coverlet.collector`. En esta base se desarrollaron pruebas para la lógica de exámenes y para las reglas de resumen de evaluaciones, lo cual permitió comprobar aspectos como el cálculo de calificaciones, la numeración de intentos, la aprobación por puntaje, la clasificación del estado de las evaluaciones y la construcción de métricas de desempeño.

Las pruebas de unidad se centraron especialmente en los siguientes comportamientos:

- Registro de intentos de examen y cálculo de porcentaje obtenido.
- Validación de límites de intentos permitidos.
- Rechazo de entradas inconsistentes en número de aciertos y total de preguntas.
- Determinación del estado de disponibilidad y aprobación de exámenes.
- Clasificación de evaluaciones dentro del resumen académico del estudiante.
- Cálculo de métricas derivadas del historial de intentos.

En cuanto a las pruebas de integración, se realizaron verificaciones funcionales sobre la interacción entre los componentes principales de la solución. Estas pruebas permitieron comprobar la comunicación entre `ACC.WebApp`, `ACC.API`, `ACC.WebApp.Client`, la capa de persistencia y el servicio de compilación. De este modo, se validó el flujo completo de autenticación, consulta de contenido, navegación por la guía, seguimiento de progreso, habilitación de exámenes y envío de código al compilador.

Las pruebas de integración contemplaron, entre otros, los siguientes recorridos:

- Inicio de sesión y acceso a funcionalidades condicionadas por rol.
- Consumo autenticado de endpoints académicos mediante JWT.
- Carga jerárquica de módulos, submódulos, temas, subtemas y lecciones.
- Registro de progreso del estudiante y actualización de estados de avance.
- Consulta, presentación y resolución de exámenes.
- Envío de código C# al servicio de compilación y retorno de resultados.

Las pruebas de resistencia se abordaron desde la validación de estabilidad operativa del sistema y el comportamiento sostenido de sus servicios bajo sesiones continuas de uso. Para ello se tomaron como base decisiones de implementación ya integradas al proyecto, tales como reintentos de conexión a base de datos, compresión de respuestas en la aplicación web, desacoplamiento de servicios y disponibilidad de caché distribuida en la topología general. Estas verificaciones contribuyeron a asegurar un funcionamiento consistente en recorridos prolongados de navegación, evaluación y compilación.

En las pruebas alfa se trabajó con revisiones internas del sistema dentro de un entorno controlado por el equipo de desarrollo. Estas validaciones permitieron comprobar la funcionalidad general de la plataforma antes de su consolidación documental, poniendo especial atención en la experiencia de usuario, la coherencia del flujo académico y la integración entre módulos. En esta etapa se revisaron pantallas clave, permisos por rol, navegación de contenido, comportamiento del compilador y respuesta de la plataforma ante acciones frecuentes del usuario.

Las pruebas beta se enfocaron en la validación del producto como una solución funcional ya integrada, atendiendo aspectos de estabilidad, consistencia visual y continuidad operativa. En este nivel se evaluó la experiencia global de uso, la claridad del recorrido académico, la interacción con Charp, la consulta de materiales, la presentación de exámenes y la respuesta del sistema en contextos de uso prolongado.

Como parte del proceso de validación también se recurrió a pruebas manuales apoyadas en herramientas de inspección de endpoints y en recorridos funcionales sobre la interfaz. La existencia de maquetas y prototipos dentro de `tests/UI/` complementó la revisión visual y estructural de componentes relevantes de la aplicación.

En conjunto, las pruebas realizadas permitieron verificar la correcta interacción entre módulos, confirmar el comportamiento esperado de la lógica principal del dominio y respaldar la estabilidad funcional de la aplicación dentro de su entorno de operación.

## 3.5.2.1. Estrategia de pruebas del sistema

La estrategia de pruebas del sistema se definió con un enfoque progresivo y complementario, orientado a validar tanto la lógica interna de los módulos como el comportamiento integral de la aplicación. Esta estrategia se estructuró en distintos niveles de verificación, con el fin de cubrir los procesos esenciales del producto educativo y garantizar consistencia entre la capa técnica y la experiencia de uso.

En primer término, se priorizó la validación de la lógica crítica del dominio mediante pruebas unitarias. Esta decisión permitió aislar reglas fundamentales del sistema, particularmente en el manejo de exámenes, intentos, clasificaciones y métricas académicas. El uso de `xUnit`, `Moq` y `EntityFrameworkCore.InMemory` facilitó la comprobación de estas reglas sin depender de infraestructura externa, favoreciendo así una verificación rápida y controlada de la lógica de negocio.

En segundo término, la estrategia contempló pruebas de integración orientadas a comprobar la comunicación entre los proyectos que conforman la solución. Dado que ACC se construyó como una arquitectura modular con cliente web, API académica, servicio de compilación, capa de datos y orquestación distribuida, resultó indispensable validar el funcionamiento conjunto de estos componentes. Por ello se llevaron a cabo recorridos funcionales completos sobre autenticación, navegación, progreso, exámenes y compilación.

Un tercer nivel correspondió a la validación funcional del sistema en un entorno de operación semejante al uso real. En esta fase se verificó la experiencia del usuario dentro de la guía académica, la interacción con recursos dinámicos, la visualización de lecciones, la resolución de evaluaciones y la respuesta del compilador. Esta capa de pruebas permitió confirmar no solo que cada módulo funcionara por separado, sino que el sistema mantuviera coherencia como producto integral.

La estrategia también incorporó revisiones alfa y beta para asegurar la madurez del software desde una perspectiva de uso completo. En la etapa alfa se depuraron comportamientos internos, permisos, navegación y consistencia entre componentes; mientras que en la etapa beta se validó la operación global del producto, la continuidad de la experiencia y la estabilidad de los recorridos principales de la plataforma.

Finalmente, la estrategia de pruebas se apoyó en la propia arquitectura del proyecto. La separación de responsabilidades entre cliente, servicios, persistencia y orquestación, junto con el uso de contratos compartidos y servicios especializados, facilitó la identificación de puntos críticos de validación y permitió estructurar las pruebas de forma ordenada. De esta manera, el proceso de pruebas no se limitó a revisar resultados aislados, sino que se integró como parte del desarrollo del software y de la consolidación técnica de la aplicación.
