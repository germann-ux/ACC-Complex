## Arquitectura General de ACC-Complex

### Principios de diseño
- Separación clara de responsabilidades entre experiencia de usuario, servicios de dominio educativo y utilidades compartidas.
- Orquestación centralizada para desplegar, observar y coordinar servicios sin acoplarlos.
- Seguridad y aislamiento durante la ejecución de código del estudiante.

### Componentes principales
- **Clientes**: Blazor WebAssembly para la experiencia web y MAUI Blazor para escritorio/móvil, ambos consumen servicios mediante API segura.
- **ACC.WebApp**: Servicio de autenticación y administración de usuarios; emite tokens y aplica políticas de autorización.
- **ACC.API**: Servicio de dominio académico que maneja módulos, lecciones, actividades y seguimiento de progreso.
- **API_CompilerACC**: Servicio de compilación y ejecución controlada de código C#, diseñado para entregar retroalimentación inmediata sin comprometer la seguridad.
- **ACC.Data**: Capa de acceso a datos y modelos persistentes; mantiene la coherencia entre identidades y perfiles académicos.
- **ACC.Shared**: Tipos, contratos y utilidades transversales que evitan duplicación entre servicios.
- **ACC.ExternalClients**: Integraciones hacia proveedores externos de IA que potencian a Charp.
- **ACC.ServiceDefaults**: Configuraciones comunes de resiliencia, telemetría y salud de servicios.
- **ACC.AppHost (Aspire)**: Orquestador que agrupa servicios, bases de datos y caché, facilitando observabilidad y despliegue coordinado.
- **Infraestructura**: SQL Server para identidad y datos académicos; Redis como caché y soporte a operaciones de baja latencia.

### Interacción entre módulos
- Los clientes solicitan autenticación a ACC.WebApp y usan los tokens emitidos para acceder a ACC.API y API_CompilerACC.
- ACC.API consulta y actualiza datos académicos a través de ACC.Data, manteniendo sincronía con la información de identidad.
- ACC.WebApp y ACC.API reutilizan ACC.Shared y ACC.ServiceDefaults para asegurar consistencia de contratos, registro y telemetría.
- API_CompilerACC opera de forma aislada, apoyándose en Redis para gestionar sesiones de ejecución y limitar efectos colaterales.
- ACC.AppHost coordina el inicio, configuración y monitoreo de cada servicio y sus dependencias de base de datos y caché.

### Motivación del diseño
La distribución en servicios desacoplados permite evolucionar de forma independiente la experiencia de aprendizaje, la autenticación y la ejecución de código. El uso de Aspire centraliza la observabilidad y simplifica la operación conjunta de los componentes. La combinación de SQL Server y Redis equilibra persistencia consistente y respuesta rápida para interacciones frecuentes.

### Estado
La arquitectura está implementada y operativa; se encuentran en ajuste fino las políticas de resiliencia y las integraciones externas que alimentan a Charp.
