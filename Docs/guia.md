## Guía Breve de Puesta en Marcha de ACC

### 1. Requisitos
- .NET 8 SDK o superior.
- Docker Desktop activo para contenedores de Redis y SQL Server.
- Entorno de desarrollo compatible con .NET (Visual Studio 2022 o VS Code).
- Acceso a una instancia de SQL Server o contenedor equivalente.

### 2. Pasos generales de instalación
1. Clonar el repositorio ACC-Complex y ubicarse en la raíz del proyecto.
2. Restaurar las dependencias de la solución.
3. Configurar las cadenas de conexión y secretos de la aplicación para los servicios web y API.
4. Aplicar las migraciones de base de datos académica e identidad.
5. Iniciar la solución mediante el orquestador ACC.AppHost (Aspire) para levantar servicios, bases de datos y caché.
6. Verificar acceso a la aplicación web y al panel de observabilidad para confirmar que los servicios responden.

### 3. Comandos rápidos (PowerShell)

```pwsh
# En la raíz del repo
dotnet restore ACC.sln

# Aplicar migraciones sobre la base académica local (puerto 1435 por defecto)
dotnet ef database update `
  --project src/ACC.Data `
  --startup-project src/ACC.API `
  --context ACCDbContext

# Levantar toda la topología con Aspire
dotnet run --project src/ACC.AppHost/ACC.AppHost.csproj
```

Cuando `ACC.AppHost` esté en ejecución, la API quedará disponible y se puede probar con Swagger en la URL expuesta por el orquestador.

### Estado
La guía cubre el flujo mínimo para ambientes locales; pasos adicionales de endurecimiento y despliegue productivo están en elaboración.
