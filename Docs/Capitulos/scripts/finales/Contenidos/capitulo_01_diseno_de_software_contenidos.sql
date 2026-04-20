-- Inserción de contenidos del capítulo de biblioteca (propuesta final, no ejecutada automáticamente)
-- Capítulo objetivo: Diseño de software

USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRAN;

    DECLARE @CapituloTitulo NVARCHAR(100) = N'Diseño de software';
    DECLARE @CapituloId INT;

    SELECT TOP (1) @CapituloId = c.IdCapitulo
    FROM acc_academic.Capitulos c
    WHERE c.TituloCapitulo = @CapituloTitulo;

    IF @CapituloId IS NULL
        THROW 56401, 'No existe el capítulo "Diseño de software" en acc_academic.Capitulos.', 1;

    DECLARE @TipoDocumentacion INT = 1;
    DECLARE @TipoConcepto INT = 2;
    DECLARE @TipoErroresComunes INT = 8;
    DECLARE @TipoBuenasPracticas INT = 9;

    DECLARE @NivelGeneral INT = 1;
    DECLARE @NivelPrincipiante INT = 2;
    DECLARE @NivelIntermedio INT = 3;

    IF EXISTS
    (
        SELECT 1
        FROM acc_academic.ContenidoCapitulos
        WHERE CapituloId = @CapituloId
          AND Titulo IN
          (
              N'Qué es el diseño de software',
              N'Diseño dentro del ciclo de vida',
              N'Diseño lógico y diseño físico',
              N'Principios fundamentales del buen diseño',
              N'Impacto de un buen diseño y consecuencias de uno deficiente'
          )
    )
        THROW 56402, 'Ya existe al menos uno de los contenidos previstos para el capítulo "Diseño de software".', 1;

    DECLARE @Html_01 NVARCHAR(MAX) = N'<article class="capitulo">
        <header class="capitulo-section">
            <h1>Qué es el diseño de software</h1>
            <p>El diseño de software es la etapa en la que un problema ya comprendido se transforma en una estructura técnica organizada. No consiste todavía en implementar la solución final, sino en definir cómo debe disponerse para que su construcción, evolución y mantenimiento puedan realizarse con orden y control.</p>
        </header>

        <section class="capitulo-section">
            <h2>Delimitación del concepto</h2>
            <p>En el contexto del desarrollo de software, diseñar no significa decorar interfaces ni tomar decisiones visuales. Significa organizar técnicamente una solución antes de construirla. Implica decidir qué partes compondrán el sistema, qué responsabilidad tendrá cada una, cómo se relacionarán entre sí y bajo qué criterios se conservará esa organización cuando el sistema cambie.</p>
            <p>Por ello, el diseño se sitúa entre la comprensión del problema y la implementación de la solución. Su función es reducir ambigüedad estructural antes de escribir código.</p>
        </section>

        <section class="capitulo-section">
            <h2>Marco conceptual</h2>
            <ul>
                <li><strong>Concepto base:</strong> organización técnica interna del sistema.</li>
                <li><strong>Conceptos derivados:</strong> módulos, responsabilidades, relaciones, distribución de funciones y decisiones de estructura.</li>
                <li><strong>Conceptos relacionados:</strong> análisis, implementación, pruebas, mantenimiento y evolución del sistema.</li>
            </ul>
        </section>

        <section class="capitulo-section">
            <h2>Qué decisiones incluye</h2>
            <p>El diseño de software define cómo se distribuye el trabajo técnico dentro de una solución. Entre las decisiones más frecuentes se encuentran las siguientes:</p>
            <ul>
                <li>qué partes o módulos formarán el sistema</li>
                <li>qué función cumplirá cada parte</li>
                <li>qué información manejará cada una</li>
                <li>cómo se comunicarán entre sí</li>
                <li>dónde conviene separar responsabilidades</li>
                <li>qué restricciones deben mantenerse para conservar el orden interno</li>
            </ul>
            <p>Estas decisiones no son accesorias. Determinan qué tan comprensible, modificable, comprobable y mantenible será el sistema a medida que crezca.</p>
        </section>

        <section class="capitulo-section">
            <h2>Modelo conceptual</h2>
            <p>El diseño puede entenderse como un plano técnico de organización. Un plano no reemplaza al edificio, pero permite construirlo con una disposición previa. Del mismo modo, el diseño no sustituye al código, pero orienta su forma, delimita funciones y evita que la implementación surja de manera desordenada.</p>
        </section>

        <section class="capitulo-section">
            <h2>Relación interna con otros conceptos del capítulo</h2>
            <p>Este contenido sirve como base para comprender el resto del capítulo. A partir de esta noción general se vuelve posible distinguir la posición del diseño dentro del proceso de desarrollo, entender la diferencia entre niveles de diseño y analizar por qué la calidad estructural influye directamente en la modificación, prueba y mantenimiento del sistema.</p>
        </section>

        <section class="capitulo-section">
            <h2>Precisión terminológica</h2>
            <ul>
                <li><strong>Analizar</strong> no es lo mismo que <strong>diseñar</strong>. Analizar busca comprender el problema; diseñar busca organizar la solución.</li>
                <li><strong>Diseñar</strong> no es lo mismo que <strong>programar</strong>. Diseñar establece estructura; programar la materializa mediante código.</li>
                <li><strong>Diseño de software</strong> no equivale a <strong>diseño gráfico</strong> ni a <strong>diseño de experiencia de usuario</strong>. Puede relacionarse con ambos en ciertos sistemas, pero su foco es la organización técnica.</li>
            </ul>
        </section>

        <section class="capitulo-section">
            <h2>Consideraciones y límites</h2>
            <p>Este contenido aborda el diseño de software como disciplina general de organización técnica. No desarrolla todavía patrones de diseño específicos, estilos arquitectónicos, decisiones de infraestructura ni estrategias avanzadas de implementación.</p>
        </section>

        <section class="capitulo-section">
            <h2>Errores comunes o confusiones frecuentes</h2>
            <ul>
                <li>creer que diseñar es una actividad secundaria o meramente documental</li>
                <li>pensar que el diseño solo es necesario en sistemas grandes</li>
                <li>confundir una idea general del sistema con una estructura técnica definida</li>
                <li>suponer que programar bien puede sustituir por completo a un diseño previo</li>
            </ul>
        </section>

        <section class="capitulo-section">
            <h2>Síntesis conceptual</h2>
            <p>El diseño de software organiza la solución antes de implementarla. Su función es establecer responsabilidades, relaciones y criterios de estructura que permitan construir el sistema con mayor claridad y conservar su orden cuando deba cambiar.</p>
        </section>

        <section class="capitulo-section">
            <h2>Aplicación conceptual</h2>
            <p>Este conocimiento se aplica cuando es necesario decidir cómo dividir un sistema, cómo distribuir responsabilidades, cómo reducir desorden interno y cómo preparar una solución para crecer o modificarse sin deteriorarse con rapidez.</p>
        </section>
</article>';

    DECLARE @Html_02 NVARCHAR(MAX) = N'<article class="capitulo">
        <header class="capitulo-section">
            <h1>Diseño dentro del ciclo de vida</h1>
            <p>El diseño de software no es una actividad aislada. Forma parte de una secuencia técnica en la que un problema se comprende, se estructura, se implementa y finalmente se valida. Su función es organizar la solución antes de su construcción, actuando como enlace entre la necesidad identificada y su materialización en código.</p>
        </header>

        <section class="capitulo-section">
            <h2>Ubicación del diseño</h2>
            <p>En una progresión general del desarrollo de software, el flujo puede representarse de la siguiente forma:</p>
            <ol>
                <li>comprender el problema o necesidad</li>
                <li>definir la organización técnica de la solución</li>
                <li>implementar la solución en código</li>
                <li>probar, ajustar y mantener el sistema</li>
            </ol>
            <p>El diseño ocupa la transición entre comprensión e implementación. No resuelve el problema directamente, pero establece la forma en la que esa solución será construida.</p>
        </section>

        <section class="capitulo-section">
            <h2>Función del diseño en la secuencia</h2>
            <p>Cada etapa del ciclo depende de las demás. Sin un análisis adecuado, el diseño se basa en información incompleta. Sin diseño, la implementación pierde coherencia estructural. Sin pruebas, el diseño no puede contrastarse con el comportamiento real del sistema.</p>
            <p>Esto muestra que el diseño no debe interpretarse como una fase independiente, sino como un punto de articulación que condiciona la calidad de las etapas posteriores.</p>
        </section>

        <section class="capitulo-section">
            <h2>Relaciones principales</h2>
            <ul>
                <li><strong>Con el análisis:</strong> recibe objetivos, restricciones y necesidades del sistema.</li>
                <li><strong>Con la implementación:</strong> proporciona una estructura que guía la construcción del código.</li>
                <li><strong>Con las pruebas:</strong> permite verificar si la organización definida responde correctamente a escenarios reales.</li>
                <li><strong>Con el mantenimiento:</strong> determina el costo y la complejidad de modificar el sistema a lo largo del tiempo.</li>
            </ul>
        </section>

        <section class="capitulo-section">
            <h2>Progresión de base a aplicación</h2>
            <p>En etapas iniciales, comprender esta posición evita comenzar a programar sin una estructura previa. En sistemas más complejos, permite identificar por qué una mala organización técnica genera defectos recurrentes, dificultad para escalar y aumento en el costo de los cambios.</p>
        </section>

        <section class="capitulo-section">
            <h2>Modelo conceptual</h2>
            <p>El diseño puede entenderse como el punto donde se define cómo se ensamblará la solución antes de construirla. No produce el sistema final, pero establece las reglas bajo las cuales sus partes se integrarán de forma coherente.</p>
        </section>

        <section class="capitulo-section">
            <h2>Precisión terminológica</h2>
            <ul>
                <li>el ciclo de vida no representa una única metodología, sino una forma general de describir la secuencia del desarrollo</li>
                <li>diseñar antes de programar no implica documentar en exceso, sino definir la estructura mínima necesaria para construir con criterio</li>
                <li>el diseño no es una fase rígida y única; puede iterarse a lo largo del desarrollo</li>
            </ul>
        </section>

        <section class="capitulo-section">
            <h2>Consideraciones y límites</h2>
            <p>La ubicación y profundidad del diseño pueden variar según la metodología utilizada o el tipo de proyecto. Sin embargo, su función esencial permanece: estructurar la solución antes de ampliar su implementación.</p>
        </section>

        <section class="capitulo-section">
            <h2>Errores comunes o confusiones frecuentes</h2>
            <ul>
                <li>pensar que el diseño desaparece en enfoques ágiles</li>
                <li>asumir que el diseño ocurre en una única fase cerrada</li>
                <li>creer que las pruebas pueden compensar una mala estructura técnica</li>
                <li>iniciar la implementación sin haber definido criterios básicos de organización</li>
            </ul>
        </section>

        <section class="capitulo-section">
            <h2>Síntesis conceptual</h2>
            <p>El diseño forma parte del flujo de desarrollo como la etapa que organiza la solución antes de implementarla. Su valor radica en conectar la comprensión del problema con una construcción estructurada y sostenible.</p>
        </section>

        <section class="capitulo-section">
            <h2>Aplicación conceptual</h2>
            <p>Este conocimiento permite identificar cuándo es necesario detener la implementación para replantear la estructura del sistema, evitando que el crecimiento del código se vuelva desordenado o difícil de mantener.</p>
        </section>
</article>';

    DECLARE @Html_03 NVARCHAR(MAX) = N'<article class="capitulo">
        <header class="capitulo-section">
            <h1>Diseño lógico y diseño físico</h1>
            <p>Una misma solución puede estructurarse en distintos niveles. El diseño lógico define la organización conceptual del sistema; el diseño físico traduce esa organización en decisiones concretas de implementación, tecnología y ejecución.</p>
        </header>

        <section class="capitulo-section">
            <h2>Definición contextual</h2>
            <p>El diseño lógico responde a la pregunta <em>cómo debe organizarse la solución</em>. El diseño físico responde a <em>cómo se materializa esa organización dentro de un entorno técnico específico</em>, considerando herramientas, estructuras reales y restricciones operativas.</p>
        </section>

        <section class="capitulo-section">
            <h2>Diferencia central</h2>
            <table>
                <thead>
                    <tr>
                        <th>Aspecto</th>
                        <th>Diseño lógico</th>
                        <th>Diseño físico</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Enfoque</td>
                        <td>Organización conceptual del sistema</td>
                        <td>Implementación concreta de esa organización</td>
                    </tr>
                    <tr>
                        <td>Pregunta principal</td>
                        <td>Cómo se estructura la solución</td>
                        <td>Cómo se construye y ejecuta</td>
                    </tr>
                    <tr>
                        <td>Nivel de abstracción</td>
                        <td>Alto</td>
                        <td>Operativo y cercano al código</td>
                    </tr>
                    <tr>
                        <td>Ejemplos</td>
                        <td>módulos, entidades, responsabilidades, relaciones</td>
                        <td>tablas, índices, clases concretas, tecnologías, despliegue</td>
                    </tr>
                </tbody>
            </table>
        </section>

        <section class="capitulo-section">
            <h2>Relación entre ambos</h2>
            <p>El diseño físico depende del diseño lógico. La organización conceptual establece qué debe construirse y bajo qué estructura. Si esta base es confusa, la implementación suele reproducir esa confusión.</p>
            <p>Al mismo tiempo, las decisiones físicas introducen restricciones reales que pueden obligar a ajustar el diseño lógico. Por ello, ambos niveles no son independientes, sino interdependientes.</p>
        </section>

        <section class="capitulo-section">
            <h2>Clasificación estructural</h2>
            <ul>
                <li><strong>Nivel lógico:</strong> define partes, relaciones, responsabilidades y flujo general del sistema.</li>
                <li><strong>Nivel físico:</strong> especifica estructuras reales, almacenamiento, tecnología, rendimiento y decisiones operativas.</li>
            </ul>
        </section>

        <section class="capitulo-section">
            <h2>Modelo conceptual</h2>
            <p>El diseño lógico establece la forma de la solución; el diseño físico la convierte en una implementación ejecutable bajo condiciones reales. Uno define la estructura; el otro la somete a restricciones técnicas y la hace operativa.</p>
        </section>

        <section class="capitulo-section">
            <h2>Relación interna con el capítulo</h2>
            <p>Esta distinción permite entender cómo el diseño puede analizarse en distintos niveles dentro del sistema. A partir de aquí se pueden abordar decisiones más específicas, como organización interna, distribución de responsabilidades y calidad estructural en distintos grados de concreción.</p>
        </section>

        <section class="capitulo-section">
            <h2>Precisión terminológica</h2>
            <ul>
                <li><strong>diseño lógico</strong> no implica abstracción vacía, sino definición estructural previa a la implementación detallada</li>
                <li><strong>diseño físico</strong> no se limita a bases de datos; incluye decisiones de código, componentes, infraestructura y ejecución</li>
                <li>ambos niveles no representan etapas separadas estrictas, sino perspectivas complementarias sobre la misma solución</li>
            </ul>
        </section>

        <section class="capitulo-section">
            <h2>Consideraciones y límites</h2>
            <p>La separación entre diseño lógico y físico no siempre es estricta. En sistemas pequeños puede diluirse, pero sigue siendo útil para distinguir entre decisiones de organización y decisiones de implementación.</p>
        </section>

        <section class="capitulo-section">
            <h2>Errores comunes o confusiones frecuentes</h2>
            <ul>
                <li>confundir el diseño lógico con una implementación incompleta</li>
                <li>pasar directamente al diseño físico sin una estructura conceptual definida</li>
                <li>mezclar decisiones de distintos niveles sin distinguir su impacto</li>
                <li>suponer que todos los detalles técnicos pertenecen al mismo nivel de abstracción</li>
            </ul>
        </section>

        <section class="capitulo-section">
            <h2>Síntesis conceptual</h2>
            <p>El diseño lógico define la estructura de la solución; el diseño físico la convierte en una implementación concreta. Distinguir ambos niveles permite tomar decisiones más claras y evitar saltos prematuros hacia la ejecución.</p>
        </section>

        <section class="capitulo-section">
            <h2>Aplicación conceptual</h2>
            <p>Este conocimiento permite identificar si una decisión pertenece a la organización del sistema o a su implementación concreta, facilitando el control sobre el nivel de abstracción en el que se está trabajando.</p>
        </section>
</article>';

    DECLARE @Html_04 NVARCHAR(MAX) = N'
<article class="capitulo">
        <header class="capitulo-section">
            <h1>Principios fundamentales del buen diseño</h1>
            <p>Un buen diseño de software no se construye a partir de intuiciones aisladas, sino de criterios que permiten organizar el sistema de forma coherente. Estos principios orientan decisiones sobre cómo dividir responsabilidades, cómo relacionar componentes y cómo mantener la estructura del sistema a lo largo del tiempo.</p>
        </header>

        <section class="capitulo-section">
            <h2>Principios base</h2>
            <ul>
                <li><strong>Cohesión:</strong> cada parte debe concentrarse en una finalidad clara y bien delimitada.</li>
                <li><strong>Bajo acoplamiento:</strong> las dependencias entre partes deben reducirse al mínimo necesario.</li>
                <li><strong>Separación de responsabilidades:</strong> cada decisión relevante debe ubicarse en el lugar que le corresponde.</li>
                <li><strong>Claridad estructural:</strong> la organización del sistema debe ser comprensible y rastreable.</li>
                <li><strong>Capacidad de cambio:</strong> el sistema debe poder modificarse sin generar efectos colaterales descontrolados.</li>
            </ul>
        </section>

        <section class="capitulo-section">
            <h2>Desarrollo de cada principio</h2>

            <h3>Cohesión</h3>
            <p>Una unidad con buena cohesión agrupa únicamente tareas relacionadas con un mismo propósito. Cuando una clase, módulo o componente mezcla responsabilidades distintas sin criterio, su cohesión disminuye y la comprensión del sistema se vuelve difusa.</p>

            <h3>Bajo acoplamiento</h3>
            <p>El acoplamiento mide cuánto depende una parte de otras. Un sistema con bajo acoplamiento permite modificar componentes individuales sin provocar cambios en cadena. Cuando las dependencias son excesivas, cualquier ajuste local se convierte en una intervención global.</p>

            <h3>Separación de responsabilidades</h3>
            <p>Este principio busca que cada decisión tenga un lugar claro dentro del sistema. Cuando una pieza concentra lógica que pertenece a distintos niveles o contextos, la estructura pierde definición y se dificulta su mantenimiento.</p>

            <h3>Claridad estructural</h3>
            <p>Un diseño claro permite identificar rápidamente qué hace cada parte, cómo se relaciona con otras y por qué está organizada de esa forma. No se trata solo de que el sistema funcione, sino de que su organización sea legible y justificable.</p>

            <h3>Capacidad de cambio</h3>
            <p>Un sistema bien diseñado no solo resuelve el problema actual, sino que permite adaptarse a cambios futuros. Esto implica aislar decisiones, reducir dependencias innecesarias y evitar estructuras rígidas que dificulten la evolución.</p>
        </section>

        <section class="capitulo-section">
            <h2>Relación interna entre principios</h2>
            <p>Estos principios se refuerzan entre sí. Una adecuada separación de responsabilidades favorece la cohesión. Una mayor cohesión suele reducir el acoplamiento. A su vez, esta combinación facilita la modificación del sistema y mejora su estabilidad a lo largo del tiempo.</p>
            <p>No deben aplicarse de forma aislada, sino como un conjunto de criterios que describen la calidad de la organización interna.</p>
        </section>

        <section class="capitulo-section">
            <h2>Estructura de evaluación rápida</h2>
            <ol>
                <li>identificar qué responsabilidad cumple cada parte del sistema</li>
                <li>verificar si una misma unidad mezcla funciones que no pertenecen al mismo propósito</li>
                <li>evaluar cuántas dependencias se ven afectadas al modificar un comportamiento puntual</li>
                <li>comprobar si la organización sigue siendo comprensible después de introducir cambios</li>
            </ol>
        </section>

        <section class="capitulo-section">
            <h2>Relación interna con el capítulo</h2>
            <p>Estos principios permiten evaluar la calidad del diseño definido en las secciones anteriores. A partir de ellos se puede analizar si una estructura propuesta mantiene coherencia interna, si sus niveles de abstracción están bien definidos y si su implementación futura será sostenible.</p>
        </section>

        <section class="capitulo-section">
            <h2>Precisión terminológica</h2>
            <ul>
                <li><strong>simple</strong> no equivale necesariamente a <strong>bien diseñado</strong>; un diseño puede ser simple pero incorrectamente estructurado</li>
                <li><strong>muchas unidades</strong> no garantizan <strong>modularidad</strong>; la calidad depende de su organización y relación</li>
                <li><strong>reutilización</strong> no debe forzarse si compromete la claridad o la cohesión</li>
            </ul>
        </section>

        <section class="capitulo-section">
            <h2>Consideraciones y límites</h2>
            <p>Estos principios no son reglas absolutas. Su aplicación depende del contexto: tamaño del sistema, complejidad del dominio, experiencia del equipo y tipo de evolución esperada. El objetivo no es cumplirlos de forma rígida, sino utilizarlos como guía para tomar mejores decisiones.</p>
        </section>

        <section class="capitulo-section">
            <h2>Errores comunes o confusiones frecuentes</h2>
            <ul>
                <li>memorizar principios sin traducirlos en decisiones concretas</li>
                <li>fragmentar el sistema en exceso para aparentar orden</li>
                <li>centrarse en la cantidad de componentes en lugar de la calidad de sus relaciones</li>
                <li>ignorar el impacto de las dependencias en la evolución del sistema</li>
            </ul>
        </section>

        <section class="capitulo-section">
            <h2>Síntesis conceptual</h2>
            <p>Un buen diseño se reconoce por la calidad de su organización interna. La cohesión, el bajo acoplamiento y la correcta separación de responsabilidades no son conceptos teóricos, sino criterios que determinan si un sistema puede entenderse, modificarse y mantenerse con eficacia.</p>
        </section>

        <section class="capitulo-section">
            <h2>Aplicación conceptual</h2>
            <p>Este conocimiento permite evaluar una solución existente y detectar si su estructura facilita el cambio o si, por el contrario, concentra responsabilidades, genera dependencias excesivas y se vuelve difícil de mantener.</p>
        </section>
</article>';

    DECLARE @Html_05 NVARCHAR(MAX) = N'
<article class="capitulo">
        <header class="capitulo-section">
            <h1>Impacto de un buen diseño y consecuencias de uno deficiente</h1>
            <p>El valor real del diseño de software se manifiesta cuando el sistema debe cambiar. Un buen diseño permite modificar, probar y mantener con control. Un diseño deficiente incrementa la fricción técnica, propaga errores y eleva el costo de cualquier ajuste.</p>
        </header>

        <section class="capitulo-section">
            <h2>Impactos positivos de un buen diseño</h2>
            <ul>
                <li>los cambios se mantienen localizados dentro de límites claros</li>
                <li>se reduce el riesgo de afectar partes no relacionadas</li>
                <li>el mantenimiento se vuelve más predecible</li>
                <li>la estructura es comprensible para nuevos integrantes</li>
                <li>las pruebas pueden enfocarse en unidades bien delimitadas</li>
            </ul>
        </section>

        <section class="capitulo-section">
            <h2>Consecuencias de un diseño deficiente</h2>
            <ul>
                <li>una modificación puntual requiere intervenir múltiples partes del sistema</li>
                <li>las responsabilidades se mezclan sin una delimitación clara</li>
                <li>aparecen dependencias implícitas y efectos colaterales</li>
                <li>la comprensión del sistema se vuelve progresivamente más difícil</li>
                <li>cada cambio incrementa la incertidumbre sobre el comportamiento global</li>
            </ul>
        </section>

        <section class="capitulo-section">
            <h2>Comparación estructural</h2>
            <p>En un sistema bien diseñado, una nueva regla o cambio funcional se incorpora en una zona acotada, respetando la organización existente. En un sistema mal diseñado, ese mismo cambio se dispersa entre múltiples puntos —clases, consultas, validaciones o interfaces— sin una frontera clara.</p>
            <p>La diferencia no es el tamaño del cambio, sino la forma en que la estructura lo absorbe.</p>
        </section>

        <section class="capitulo-section">
            <h2>Relación con mantenimiento y calidad</h2>
            <p>La calidad de un sistema no se limita a producir resultados correctos en un momento puntual. También implica sostener cambios sin degradar su estructura. En este sentido, el diseño es un factor directo de calidad, ya que determina cómo responde el sistema ante evolución, correcciones y nuevas necesidades.</p>
        </section>

        <section class="capitulo-section">
            <h2>Señales de alerta</h2>
            <ul>
                <li>una misma unidad crece continuamente absorbiendo nuevas responsabilidades</li>
                <li>una modificación requiere revisar múltiples módulos sin relación aparente</li>
                <li>los nombres de las piezas dejan de reflejar su comportamiento real</li>
                <li>los errores reaparecen en zonas similares debido a falta de estructura</li>
                <li>existe resistencia a modificar el sistema por miedo a romperlo</li>
            </ul>
        </section>

        <section class="capitulo-section">
            <h2>Relación interna con los principios de diseño</h2>
            <p>Los efectos descritos están directamente vinculados con los principios fundamentales del diseño. Una baja cohesión, un alto acoplamiento o una mala separación de responsabilidades suelen manifestarse en cambios dispersos, dependencias ocultas y dificultad de mantenimiento.</p>
        </section>

        <section class="capitulo-section">
            <h2>Precisión terminológica</h2>
            <ul>
                <li><strong>funciona actualmente</strong> no implica que el sistema esté bien diseñado</li>
                <li><strong>rápido de construir</strong> no equivale a <strong>sostenible en el tiempo</strong></li>
                <li><strong>frecuencia de cambios</strong> no siempre indica mala planificación; puede evidenciar falta de adaptabilidad estructural</li>
            </ul>
        </section>

        <section class="capitulo-section">
            <h2>Consideraciones y límites</h2>
            <p>No todos los problemas de mantenimiento se originan exclusivamente en el diseño. Factores como cambios en requisitos, decisiones previas, presión de tiempo o limitaciones técnicas también influyen. Sin embargo, la organización estructural sigue siendo uno de los elementos más determinantes.</p>
        </section>

        <section class="capitulo-section">
            <h2>Errores comunes o confusiones frecuentes</h2>
            <ul>
                <li>atribuir al lenguaje o a la tecnología problemas que son de organización</li>
                <li>considerar el desorden como una consecuencia inevitable del crecimiento</li>
                <li>suponer que la refactorización posterior puede corregir cualquier problema sin costo significativo</li>
                <li>evaluar el sistema solo por su funcionamiento actual y no por su capacidad de cambio</li>
            </ul>
        </section>

        <section class="capitulo-section">
            <h2>Síntesis conceptual</h2>
            <p>El buen diseño reduce la fricción técnica al permitir cambios controlados. El diseño deficiente la incrementa al dispersar responsabilidades y generar dependencias innecesarias. La diferencia se hace evidente en cada modificación, prueba y ajuste del sistema.</p>
        </section>

        <section class="capitulo-section">
            <h2>Aplicación conceptual</h2>
            <p>Este contenido permite evaluar una solución no solo por su funcionamiento actual, sino por su capacidad de evolucionar con un costo técnico controlado.</p>
        </section>
</article>';

    INSERT INTO acc_academic.ContenidoCapitulos
    (
        Tipo,
        Titulo,
        Subtitulo,
        Descripcion,
        Duracion,
        Dificultad,
        Nivel,
        IconoBadge,
        FechaActualizacion,
        FechaCreacion,
        CapituloId,
        HtmlBody
    )
    VALUES
    (
        @TipoConcepto,
        N'Qué es el diseño de software',
        N'Delimitación del dominio y función estructural',
        N'Explica el diseño de software como actividad de organización técnica: qué decide, qué relación guarda con otras etapas y por qué no debe confundirse con implementación o diseño visual.',
        N'12-15 min',
        NULL,
        @NivelPrincipiante,
        N'fas fa-sitemap',
        SYSUTCDATETIME(),
        SYSUTCDATETIME(),
        @CapituloId,
        @Html_01
    ),
    (
        @TipoDocumentacion,
        N'Diseño dentro del ciclo de vida',
        N'Ubicación del diseño entre análisis, implementación y pruebas',
        N'Explica dónde aparece el diseño dentro del trabajo de desarrollo y cómo conecta la comprensión del problema con la implementación y validación del sistema.',
        N'10-12 min',
        NULL,
        @NivelPrincipiante,
        N'fas fa-project-diagram',
        SYSUTCDATETIME(),
        SYSUTCDATETIME(),
        @CapituloId,
        @Html_02
    ),
    (
        @TipoConcepto,
        N'Diseño lógico y diseño físico',
        N'Dos niveles de organización con distinta abstracción',
        N'Distingue el nivel lógico del nivel físico del diseño para separar decisiones de estructura conceptual de decisiones de implementación concreta.',
        N'12-14 min',
        NULL,
        @NivelIntermedio,
        N'fas fa-layer-group',
        SYSUTCDATETIME(),
        SYSUTCDATETIME(),
        @CapituloId,
        @Html_03
    ),
    (
        @TipoBuenasPracticas,
        N'Principios fundamentales del buen diseño',
        N'Cohesión, bajo acoplamiento y separación de responsabilidades',
        N'Reúne los principios más importantes para evaluar la calidad estructural de una solución y entender cómo se relacionan entre sí.',
        N'14-16 min',
        NULL,
        @NivelIntermedio,
        N'fas fa-drafting-compass',
        SYSUTCDATETIME(),
        SYSUTCDATETIME(),
        @CapituloId,
        @Html_04
    ),
    (
        @TipoErroresComunes,
        N'Impacto de un buen diseño y consecuencias de uno deficiente',
        N'Cambios, mantenimiento, fricción técnica y calidad del sistema',
        N'Expone cómo afecta el diseño al mantenimiento, la estabilidad y la capacidad de cambio, comparando una estructura sana con una organización deficiente.',
        N'11-13 min',
        NULL,
        @NivelGeneral,
        N'fas fa-exclamation-triangle',
        SYSUTCDATETIME(),
        SYSUTCDATETIME(),
        @CapituloId,
        @Html_05
    );

    PRINT CONCAT('Se insertaron 5 contenidos para el capítulo IdCapitulo=', @CapituloId, '.');

    COMMIT;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;

    THROW;
END CATCH;
GO
