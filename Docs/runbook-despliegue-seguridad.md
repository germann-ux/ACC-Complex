# Runbook de Despliegue y Seguridad Operativa

## Variables y secretos obligatorios
- `SqlPassword`
- `Jwt:Key`
- `ConnectionStrings__acc-redis` (si se habilita Redis real)
- `ExternalIntegrations__Chatbase__ChatbotId` (si aplica)

## Pre-deploy
1. Ejecutar `dotnet build ACC.sln`.
2. Ejecutar `dotnet test tests/ACC.Tests/ACC.Tests.csproj`.
3. Verificar que no existan secretos hardcodeados en `appsettings*.json`.

## Deploy
1. Publicar `ACC.API`, `ACC.WebApp`, `ACC.Compiler`.
2. Inyectar secretos por entorno (KeyVault, variable de entorno o secrets manager).
3. Validar health checks y arranque de AppHost.

## Post-deploy smoke
1. Login.
2. Navegacion de guia.
3. Examenes y prerrequisitos.
4. Compilador en `/api/compile`.
5. Aula y tareas.

## Operacion segura
1. Rotacion periodica de `Jwt:Key` y `SqlPassword`.
2. Minimizar permisos de cuentas SQL/Redis.
3. Mantener TLS obligatorio en frontend y APIs.
4. Alertar sobre incremento de errores 5xx y misses de cache anormales.

