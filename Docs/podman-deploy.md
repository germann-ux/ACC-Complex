# Podman Deploy (Init Jobs + Runtime)

## 1. Variables
1. Copy `.env.example` to `.env`.
2. Set a strong `JWT_KEY`.
3. Keep or rotate `SQL_PASSWORD` as needed.

## 2. Start stack
```powershell
podman compose --env-file .env up -d --build
```

This compose includes two one-shot init jobs:
- `acc-migrate-academic` (migrates `ACCDbContext`)
- `acc-migrate-identity` (migrates `ApplicationDbContext`)

## 3. Verify init jobs
```powershell
podman ps -a --format "{{.Names}} {{.Status}}" | findstr /I "acc-migrate"
```

Expected: both migration containers finish with `Exited (0)`.

## 4. Verify services
```powershell
podman ps --format "{{.Names}} {{.Status}}"
```

## 5. Logs (if needed)
```powershell
podman logs acc-migrate-academic
podman logs acc-migrate-identity
podman logs acc-api
podman logs acc-webapp
podman logs acc-compiler
```

## Remote DB mode (2 servers)
Use this mode when SQL runs on another server:

1. Set `DB_SQL_USER`, `DB_IDENTITY_HOST`, `DB_IDENTITY_PORT`, `DB_ACADEMIC_HOST`, `DB_ACADEMIC_PORT`, `SQL_PASSWORD` in `.env`.
2. Ensure firewall on DB server allows `DB_IDENTITY_PORT` and `DB_ACADEMIC_PORT` from the app server.
3. Start using:

```powershell
podman compose -f podman-compose.remote-db.yml --env-file .env up -d --build
```
