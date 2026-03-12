# Politica de Deprecacion de Rutas

## Objetivo
Definir una ventana corta y controlada de compatibilidad para contratos legacy.

## Regla general
1. Toda nueva ruta canónica se publica con prefijo/version estable.
2. La ruta legacy se mantiene solo 1 fase.
3. La ruta legacy debe emitir warning de deprecacion en cada llamada.
4. En la siguiente fase se elimina la ruta legacy y se valida no-regresion.

## Caso actual: compilador
- Ruta canónica: `/api/compile`
- Ruta legacy temporal: `/api/acc-compile`
- Estado actual: activa con header `Warning` en Fase 2.
- Cierre esperado: eliminar `/api/acc-compile` en Fase 5.

## Checklist de retiro
1. Confirmar que cliente UI usa ruta canónica.
2. Verificar telemetria sin trafico legacy.
3. Remover endpoint legacy y pruebas asociadas.
4. Registrar cambio en guia tecnica y changelog interno.

