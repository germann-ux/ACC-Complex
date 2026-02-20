## Aprendiendo C# con Charp (ACC)

### Propósito
ACC es un sistema educativo orientado a estudiantes de programación que necesitan entender C# de forma progresiva y aplicada. Nace para reducir la brecha entre teoría y práctica, ofreciendo un itinerario guiado que evita que el alumno se pierda en recursos dispersos.

### Problema que resuelve
- Falta de rutas claras para avanzar en C# desde los fundamentos hasta escenarios intermedios.
- Escasa retroalimentación oportuna durante la práctica individual.
- Dificultad para conectar conceptos del lenguaje con situaciones de desarrollo reales.

### Visión general
ACC se concibe como un entorno integral: combina contenidos estructurados, acompañamiento inteligente y evaluación continua para asegurar progreso real. El sistema prioriza claridad conceptual, ejemplos accionables y práctica recurrente, manteniendo al estudiante en un ciclo constante de comprensión, aplicación y verificación.

### Experiencia de aprendizaje
- Lecciones modulares enlazadas a objetivos medibles.
- Ejercicios interactivos con compilación y ejecución seguras.
- Retroalimentación contextual a través de Charp, enfocada en guiar sin entregar soluciones completas.
- Seguimiento del avance académico para detectar brechas y recomendar próximos pasos.

#### Ejemplo rápido de práctica guiada
Un reto típico pide completar una función y enviarla al compilador seguro; el alumno recibe retroalimentación inmediata:

```csharp
// Completa para devolver true cuando el número sea par.
public static bool EsPar(int valor)
{
    return valor % 2 == 0;
}
```

La misma interfaz muestra la petición que se envía al servicio de compilación para validar la solución:

```http
POST https://localhost:7023/api/Compile
Content-Type: application/json

{
  "code": "Console.WriteLine(EsPar(4));",
  "input": ""
}
```

### Estado actual
El proyecto está en desarrollo activo; las funcionalidades centrales de contenido educativo, soporte de Charp y seguimiento de progreso se encuentran operativas, mientras que se sigue afinando la experiencia completa dentro de ACC-Complex.
