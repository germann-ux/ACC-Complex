## Enfoque Pedagógico de ACC

### Estructura del aprendizaje
El recorrido se organiza en módulos que escalan en complejidad siguiendo niveles cognitivos: comprensión, aplicación, análisis y evaluación. Cada módulo combina teoría breve, ejemplos centrados en casos de uso y práctica guiada antes de pasar a retos autónomos.

### Rol del estudiante
- Asume decisiones sobre ritmo y ruta, eligiendo prácticas y evaluaciones según su progreso.
- Documenta sus intentos y reflexiona sobre los errores detectados por el sistema.
- Repite ejercicios con variaciones para consolidar patrones de diseño y sintaxis.

### Rol de Charp
Charp actúa como guía educativo: contextualiza conceptos, formula preguntas que orientan el razonamiento y sugiere ajustes incrementales. Evita entregar soluciones completas, privilegiando que el estudiante derive la respuesta por sí mismo.

### Fomento del razonamiento y la práctica deliberada
- Preguntas de andamiaje que obligan a justificar decisiones de diseño y de sintaxis.
- Retroalimentación inmediata en ejecuciones, enfocada en causas y efectos del código.
- Ejercicios escalonados que repiten la idea clave con ligeras variaciones para reforzarla.

#### Ejemplo de reto guiado
Charp presenta el objetivo, expone casos de prueba mínimos y pide explicar la decisión tomada:

```csharp
// Objetivo: devolver la suma de números pares
public static class Calculadora
{
    public static int SumarPares(IEnumerable<int> valores)
        => valores.Where(v => v % 2 == 0).Sum();
}
```

Casos con los que se valida en el compilador seguro:

```csharp
[Fact]
public void SumarPares_SoloPares_RegresaSuma()
{
    var resultado = Calculadora.SumarPares(new[] { 2, 4, 6 });
    Assert.Equal(12, resultado);
}
```

El estudiante debe justificar por qué filtra antes de sumar y qué sucede con secuencias vacías, reforzando la reflexión sobre la solución.

### Autonomía y progresión
- Rutas personalizables según desempeño registrado en evaluaciones.
- Actividades independientes que requieren integrar varios conceptos sin guía explícita.
- Indicadores de logro que muestran el estado actual y las brechas por cerrar.

### Estado
La estrategia pedagógica está definida y en uso; se continúan ajustando las reglas de recomendación de contenido para mejorar la adaptación al ritmo individual.
