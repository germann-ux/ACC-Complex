using ACC.API.Interfaces;
using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using ACC.Shared.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

#region Contexto pedagógico (referencia rápida)
/*
 * 1) Exámenes de Módulo:
 *    - Se aplican al finalizar un módulo completo.
 *    - Son obligatorios para avanzar al siguiente módulo.
 *
 * 2) Exámenes de Submódulo:
 *    - Se realizan para desbloquear el examen de módulo.
 *    - Se desbloquean al completar actividades del submódulo.
 *
 * 3) Exámenes de Evaluación:
 *    - Pueden ser creados por docentes/administradores en cualquier momento.
 *    - Evalúan a los estudiantes de forma diagnóstica o adicional.
 */
#endregion

namespace ACC.API.Services
{
    /// <summary>
    /// Servicio de exámenes que concentra operaciones de:
    /// <list type="bullet">
    ///   <item><description><see cref="IExamenesSubMService"/>: consultas de exámenes de submódulo.</description></item>
    ///   <item><description><see cref="IExamenesUserService"/>: intentos por usuario (consulta/registro).</description></item>
    ///   <item><description><see cref="IExamenesModService"/>: consultas de exámenes de módulo.</description></item>
    /// </list>
    ///
    /// <para>
    /// Este servicio implementa la **lógica y validación de dominio**. Los controladores deben limitarse a
    /// delegar la operación y retornar el <see cref="ServiceResult{T}"/> sin revalidar.
    /// </para>
    /// </summary>
    public class ExamenesService : IExamenesSubMService, IExamenesUserService, IExamenesModService
    {
        private readonly ACCDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPrerrequisitosService _prerrequisitos;

        /// <summary>
        /// Crea una nueva instancia del servicio de exámenes.
        /// </summary>
        /// <param name="context">DbContext principal de ACC para acceso a datos.</param>
        /// <param name="mapper">Componente de mapeo entre entidades y DTOs.</param>
        /// <param name="prerrequisitos">Servicio de prerrequisitos para evaluar desbloqueos.</param>
        public ExamenesService(ACCDbContext context, IMapper mapper, IPrerrequisitosService prerrequisitos)
        {
            _context = context;
            _mapper = mapper;
            _prerrequisitos = prerrequisitos;
        }

        // =====================================================================================
        // SECCIÓN: IExamenesSubMService (Exámenes de Submódulo)
        // =====================================================================================

        /// <summary>
        /// Obtiene la lista completa de exámenes de submódulo.
        /// </summary>
        /// <returns>
        /// <see cref="ServiceResult{T}"/> con la lista de <see cref="ExamenSubModuloDto"/>.
        /// Si no existen registros, retorna <c>Fail</c> con un mensaje descriptivo.
        /// </returns>
        /// <remarks>
        /// Optimización: usa <see cref="EntityFrameworkQueryableExtensions.AsNoTracking{TEntity}(IQueryable{TEntity})"/>
        /// para lecturas sin tracking. Capa de controlador no debe filtrar/interpretar el resultado.
        /// </remarks>
        public async Task<ServiceResult<List<ExamenSubModuloDto>>> ObtenerExamenesSubMAsync()
        {
            // ExamenesSubModulo
            var examenes = await _context.ExamenesSubModulo
                                         .AsNoTracking()
                                         .Include(e => e.SubModulo)
                                         .ToListAsync()
                                         .ConfigureAwait(false);

            var dto = _mapper.Map<List<ExamenSubModuloDto>>(examenes);
            return ServiceResult<List<ExamenSubModuloDto>>.Ok(dto);
        }

        /// <summary>
        /// Obtiene un examen de submódulo por su identificador.
        /// </summary>
        /// <param name="id">Identificador del examen de submódulo. Debe ser &gt; 0.</param>
        /// <returns>
        /// <see cref="ServiceResult{T}"/> con <see cref="ExamenSubModuloDto"/> si existe y es válido;
        /// de lo contrario, <c>Fail</c> con causa (id inválido, no encontrado o contenido inválido).
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Nunca se lanza: los errores se reportan vía <see cref="ServiceResult{T}"/>.</exception>
        /// <remarks>
        /// Valida consistencia mínima del contenido: nombre o HTML no vacíos.
        /// Reglas más estrictas pueden agregarse aquí (longitudes mínimas, HTML permitido, etc.).
        /// </remarks>
        public async Task<ServiceResult<ExamenSubModuloDto?>> ObtenerExamenSubMAsync(int id)
        {
            if (id <= 0)
                return ServiceResult<ExamenSubModuloDto?>.Fail("El id del examen debe ser mayor a cero.");

            var examen = await _context.ExamenesSubModulo
                                       .AsNoTracking()
                                       .Include(e => e.SubModulo)
                                       .FirstOrDefaultAsync(e => e.Id == id)
                                       .ConfigureAwait(false);

            if (examen is null)
                return ServiceResult<ExamenSubModuloDto?>.Fail("Examen no encontrado.");

            var contenidoValido = !string.IsNullOrWhiteSpace(examen.Nombre)
                                  || !string.IsNullOrWhiteSpace(examen.ContenidoHtml);

            if (!contenidoValido)
                return ServiceResult<ExamenSubModuloDto?>.Fail("El examen no contiene datos válidos.");

            var dto = _mapper.Map<ExamenSubModuloDto>(examen);
            return ServiceResult<ExamenSubModuloDto?>.Ok(dto);
        }

        // =====================================================================================
        // SECCIÓN: IExamenesUserService (Intentos / Usuario)
        // =====================================================================================

        /// <summary>
        /// Obtiene todos los intentos registrados por un usuario (en cualquier examen).
        /// </summary>
        /// <param name="userId">Identificador del usuario. No puede ser nulo ni vacío.</param>
        /// <returns>
        /// <see cref="ServiceResult{T}"/> con la lista de <see cref="ExamenIntentoDto"/> si hay datos;
        /// en caso contrario, <c>Fail</c> con explicación.
        /// </returns>
        /// <remarks>
        /// Usa <see cref="AsNoTracking"/> para lectura. Si necesitas paginación, añádela aquí
        /// (no en el controlador) para mantener consistencia del dominio.
        /// </remarks>
        public async Task<ServiceResult<List<ExamenIntentoDto>>> ObtenerIntentosPorUsuarioAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return ServiceResult<List<ExamenIntentoDto>>.Fail("El id del usuario no puede estar vacío.");

            var intentos = await _context.ExamenesIntentos
                                         .AsNoTracking()
                                         .Where(e => e.IdUsuario == userId)
                                         .ToListAsync()
                                         .ConfigureAwait(false);

            if (intentos.Count == 0)
                return ServiceResult<List<ExamenIntentoDto>>.Fail("No se encontraron intentos para el usuario solicitado.");

            var dto = _mapper.Map<List<ExamenIntentoDto>>(intentos);
            return ServiceResult<List<ExamenIntentoDto>>.Ok(dto);
        }

        /// <summary>
        /// Obtiene el último intento del usuario en un examen de submódulo específico.
        /// </summary>
        /// <param name="userId">Identificador del usuario. No puede ser nulo ni vacío.</param>
        /// <param name="examenSubModuloId">Identificador del examen de submódulo. Debe ser &gt; 0.</param>
        /// <returns>
        /// <see cref="ServiceResult{T}"/> con el último <see cref="ExamenIntentoDto"/> si existe; de lo contrario, <c>Fail</c>.
        /// </returns>
        /// <remarks>
        /// “Último” se define cronológicamente por <c>FechaIntento</c> descendente.
        /// Asegúrate de que <see cref="ExamenIntento"/> tenga una marca temporal confiable
        /// (idealmente asignada en servidor).
        /// </remarks>
        public Task<ServiceResult<ExamenIntentoDto?>> ObtenerUltimoIntentoPorUsuarioYExamenAsync(string userId, int examenSubModuloId)
            => ObtenerUltimoIntentoSubModuloAsync(userId, examenSubModuloId);

        public Task<ServiceResult<ExamenIntentoDto?>> ObtenerUltimoIntentoPorUsuarioYExamenAsync(
            string userId,
            ExamenTipo tipo,
            int examenId)
            => tipo switch
            {
                ExamenTipo.SubModulo => ObtenerUltimoIntentoSubModuloAsync(userId, examenId),
                ExamenTipo.Modulo => ObtenerUltimoIntentoModuloAsync(userId, examenId),
                ExamenTipo.Libre => ObtenerUltimoIntentoLibreAsync(userId, examenId),
                _ => Task.FromResult(ServiceResult<ExamenIntentoDto?>.Fail("Tipo de examen no válido."))
            };

        public async Task<ServiceResult<ExamenEstadoDto?>> ObtenerEstadoExamenAsync(string userId, ExamenTipo tipo, int examenId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return ServiceResult<ExamenEstadoDto?>.Fail("El id del usuario no puede estar vacio.");

            var configuracion = await ObtenerConfiguracionExamenAsync(tipo, examenId).ConfigureAwait(false);
            if (configuracion is null)
                return ServiceResult<ExamenEstadoDto?>.NotFound("El examen solicitado no existe.");

            var intentosQuery = CrearConsultaIntentosMismoExamen(userId, configuracion.Tipo, configuracion.ExamenId);
            var intentosRealizados = await intentosQuery.CountAsync().ConfigureAwait(false);
            var ultimoIntento = await intentosQuery
                .OrderByDescending(ie => ie.FechaIntento)
                .ThenByDescending(ie => ie.NumeroIntento)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            var aprobadoRegistrado = await _context.ExamenesAprobatorios
                .AsNoTracking()
                .AnyAsync(a => a.UsuarioId == userId
                               && a.Tipo == configuracion.Tipo
                               && a.ExamenId == configuracion.ExamenId)
                .ConfigureAwait(false);

            var estaHabilitado = configuracion.Tipo == ExamenTipo.Libre
                || await _prerrequisitos
                    .EstaHabilitadoAsync(userId, new ExamenRef(configuracion.Tipo, configuracion.ExamenId))
                    .ConfigureAwait(false);

            var intentosRestantes = configuracion.IntentosMaximos == int.MaxValue
                ? int.MaxValue
                : Math.Max(configuracion.IntentosMaximos - intentosRealizados, 0);

            var estado = new ExamenEstadoDto
            {
                Tipo = configuracion.Tipo,
                ExamenId = configuracion.ExamenId,
                EstaHabilitado = estaHabilitado,
                IntentosRealizados = intentosRealizados,
                IntentosMaximos = configuracion.IntentosMaximos,
                IntentosRestantes = intentosRestantes,
                PuedePresentar = estaHabilitado && intentosRealizados < configuracion.IntentosMaximos,
                EstaAprobado = aprobadoRegistrado || ultimoIntento?.Aprobado == true,
                UltimoIntento = ultimoIntento is null ? null : _mapper.Map<ExamenIntentoDto>(ultimoIntento)
            };

            return ServiceResult<ExamenEstadoDto?>.Ok(estado);
        }

        /// <summary>
        /// Registra un nuevo intento de examen para un usuario.
        /// </summary>
        /// <param name="intentoDto">Datos del intento a registrar. No puede ser nulo.</param>
        /// <returns>
        /// <see cref="ServiceResult{T}"/> con el <see cref="ExamenIntentoDto"/> realmente **persistido** (incluye claves/fechas),
        /// o <c>Fail</c> si la operación no se concreta.
        /// </returns>
        /// <remarks>
        /// Validaciones mínimas:
        /// <list type="bullet">
        ///   <item><description><see cref="ExamenIntentoDto.ExamenSubModuloId"/> &gt; 0.</description></item>
        ///   <item><description><see cref="ExamenIntentoDto.IdUsuario"/> no vacío.</description></item>
        ///   <item><description><see cref="ExamenIntentoDto.Calificacion"/> ≥ 0.</description></item>
        ///   <item><description>El examen de submódulo debe existir.</description></item>
        /// </list>
        /// Considera setear <c>FechaIntento</c> en servidor si no viene informado.
        /// </remarks>
        public async Task<ServiceResult<ExamenIntentoDto>> RegistrarIntentoAsync(ExamenIntentoDto intentoDto)
        {
            if (intentoDto is null)
                return ServiceResult<ExamenIntentoDto>.Fail("El intento no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(intentoDto.IdUsuario))
                return ServiceResult<ExamenIntentoDto>.Fail("El id del usuario no puede estar vacío.");

            if (intentoDto.NumeroAciertos < 0)
                return ServiceResult<ExamenIntentoDto>.Fail("El numero de aciertos no puede ser negativo.");

            if (intentoDto.TotalPreguntas <= 0)
                return ServiceResult<ExamenIntentoDto>.Fail("El total de preguntas debe ser mayor a cero.");

            if (intentoDto.NumeroAciertos > intentoDto.TotalPreguntas)
                return ServiceResult<ExamenIntentoDto>.Fail("El numero de aciertos no puede exceder el total de preguntas.");

            if (intentoDto.TiempoSegundos < 0)
                return ServiceResult<ExamenIntentoDto>.Fail("El tiempo del examen no puede ser negativo.");

            // 1) Debe referenciar EXACTAMENTE un examen
            int fkCount = 0;
            if (intentoDto.ExamenSubModuloId.HasValue) fkCount++;
            if (intentoDto.ExamenModuloId.HasValue) fkCount++;
            if (intentoDto.ExamenId.HasValue) fkCount++;
            if (fkCount != 1)
                return ServiceResult<ExamenIntentoDto>.Fail("Debe referenciar exactamente un examen (SubMódulo, Módulo o Libre).");

            var examenRef = ObtenerReferenciaExamen(intentoDto);
            var configuracion = await ObtenerConfiguracionExamenAsync(examenRef.tipo, examenRef.examenId).ConfigureAwait(false);
            if (configuracion is null)
                return ServiceResult<ExamenIntentoDto>.NotFound("El examen solicitado no existe.");

            if (intentoDto.TotalPreguntas != configuracion.NumeroPreguntas)
                return ServiceResult<ExamenIntentoDto>.Fail("El total de preguntas enviado no coincide con la configuracion del examen.");

            // 3) Mapear a entidad
            var entity = _mapper.Map<ExamenIntento>(intentoDto);
            entity.Calificacion = Math.Round((double)intentoDto.NumeroAciertos * 100.0 / intentoDto.TotalPreguntas, 2);
            entity.PorcentajeObtenido = entity.Calificacion;
            entity.TotalPreguntas = intentoDto.TotalPreguntas;
            entity.NumeroAciertos = intentoDto.NumeroAciertos;
            entity.TiempoSegundos = intentoDto.TiempoSegundos;

            // 4) Sello de tiempo en servidor
            if (entity.FechaIntento == default)
                entity.FechaIntento = DateTimeOffset.UtcNow;

            // 5) Numeración secuencial por (Usuario + Examen lógico)
            var sameExamQuery = CrearConsultaIntentosMismoExamen(entity.IdUsuario, configuracion.Tipo, configuracion.ExamenId);

            var intentosRealizados = await sameExamQuery.CountAsync().ConfigureAwait(false);
            if (intentosRealizados >= configuracion.IntentosMaximos)
                return ServiceResult<ExamenIntentoDto>.Fail("El usuario ya alcanzo el numero maximo de intentos para este examen.");

            var nextN = (await sameExamQuery
                            .Select(i => (int?)i.NumeroIntento)
                            .MaxAsync()
                            .ConfigureAwait(false)) ?? 0;

            entity.NumeroIntento = nextN + 1;

            // 6) Determinar aprobación en servidor (con umbral real)
            entity.Aprobado = EstaAprobado(
                intentoDto.NumeroAciertos,
                entity.PorcentajeObtenido,
                configuracion.PuntajeAprobacion,
                configuracion.NumeroPreguntas);

            try
            {
                // 7) Persistir intento
                await _context.ExamenesIntentos.AddAsync(entity).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                // 8) Si aprobó: registrar PRIMER APROBATORIO (si no existía) + PRERREQUISITOS
                if (entity.Aprobado)
                {
                    using var tx = await _context.Database.BeginTransactionAsync().ConfigureAwait(false);

                    var yaExisteAprobatorio = await _context.ExamenesAprobatorios
                        .AsNoTracking()
                        .AnyAsync(a => a.UsuarioId == entity.IdUsuario
                                       && a.Tipo == configuracion.Tipo
                                       && a.ExamenId == configuracion.ExamenId)
                        .ConfigureAwait(false);

                    if (!yaExisteAprobatorio)
                    {
                        _context.ExamenesAprobatorios.Add(new ExamenAprobatorio
                        {
                            UsuarioId = entity.IdUsuario,
                            Tipo = configuracion.Tipo,
                            ExamenId = configuracion.ExamenId,
                            ExamenIntentoId = entity.Id,
                            FechaAprobacion = DateTimeOffset.UtcNow,
                            Calificacion = entity.Calificacion
                        });

                        await _context.SaveChangesAsync().ConfigureAwait(false);
                    }

                    await tx.CommitAsync().ConfigureAwait(false);

                    // 9) PRERREQUISITOS según tu servicio:
                    // - Aprobación de SubMódulo → podría habilitar el Examen de Módulo (ReglaB)
                    if (configuracion.Tipo == ExamenTipo.SubModulo)
                        await _prerrequisitos.EvaluarDesbloqueosPorAprobacionAsync(entity.IdUsuario, configuracion.ExamenId);

                    // Nota: Si en el futuro defines desbloqueos para Módulo o Examen Libre,
                    // añade aquí su invocación explícita usando ExamenRef si aplica.
                }

                // 10) Devolver intento persistido
                var dtoPersistido = _mapper.Map<ExamenIntentoDto>(entity);
                return ServiceResult<ExamenIntentoDto>.Ok(dtoPersistido);
            }
            catch (DbUpdateException ex)
            {
                return ServiceResult<ExamenIntentoDto>.Error(ex);
            }
        }

        // =====================================================================================
        // SECCIÓN: IExamenesModService (Exámenes de Módulo)
        // =====================================================================================

        /// <summary>
        /// Obtiene la lista completa de exámenes de módulo.
        /// </summary>
        /// <returns>
        /// <see cref="ServiceResult{T}"/> con la lista de <see cref="ExamenModuloDto"/> o <c>Fail</c> si no hay registros.
        /// </returns>
        public async Task<ServiceResult<List<ExamenModuloDto>>> ObtenerExamenesModAsync()
        {
            var examenes = await _context.ExamenesModulos
                                         .AsNoTracking()
                                         .ToListAsync()
                                         .ConfigureAwait(false);

            var examenesDto = _mapper.Map<List<ExamenModuloDto>>(examenes);
            return ServiceResult<List<ExamenModuloDto>>.Ok(examenesDto);
        }

        /// <summary>
        /// Obtiene un examen de módulo por su identificador.
        /// </summary>
        /// <param name="id">Identificador del examen de módulo. Debe ser &gt; 0.</param>
        /// <returns>
        /// <see cref="ServiceResult{T}"/> con <see cref="ExamenModuloDto"/> si existe; de lo contrario, <c>Fail</c>.
        /// </returns>
        public async Task<ServiceResult<ExamenModuloDto?>> ObtenerExamenModAsync(int id)
        {
            if (id <= 0)
                return ServiceResult<ExamenModuloDto?>.Fail("El id del examen debe ser mayor a cero.");

            var examen = await _context.ExamenesModulos
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(e => e.Id == id)
                                       .ConfigureAwait(false);

            if (examen is null)
                return ServiceResult<ExamenModuloDto?>.Fail($"Examen con el id: {id} no encontrado.");

            var examenDto = _mapper.Map<ExamenModuloDto>(examen);
            return ServiceResult<ExamenModuloDto?>.Ok(examenDto);
        }

        public async Task<ServiceResult<int?>> ObtenerUmbralAprobacionAsync(ExamenIntento examen)
        {
            if (examen.ExamenSubModuloId is int esmId)
            {
                    var result = await _context.ExamenesSubModulo
                    .AsNoTracking()
                    .Where(x => x.Id == esmId)
                    .Select(x => (int?)x.PuntajeAprobacion)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                return ServiceResult<int?>.Ok(result);
            }
                
            if (examen.ExamenModuloId is int emId)
            {
                var result = await _context.ExamenesModulos
                    .AsNoTracking()
                    .Where(x => x.Id == emId)
                    .Select(x => (int?)x.PuntajeAprobacion)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                return ServiceResult<int?>.Ok(result);
            }

            if (examen.ExamenId is int egId)
            {
                var result = await _context.Examenes
                    .AsNoTracking()
                    .Where(x => x.Id == egId)
                    .Select(x => (int?)x.PuntajeAprobacion)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
                return ServiceResult<int?>.Ok(result);
            }

            return ServiceResult<int?>.Ok(null);
        }

        private async Task<ServiceResult<ExamenIntentoDto?>> ObtenerUltimoIntentoSubModuloAsync(string userId, int examenSubModuloId)
        {
            if (examenSubModuloId <= 0)
                return ServiceResult<ExamenIntentoDto?>.Fail("El id del examen debe ser mayor a cero.");

            if (string.IsNullOrWhiteSpace(userId))
                return ServiceResult<ExamenIntentoDto?>.Fail("El id del usuario no puede estar vacío.");

            var existeExamen = await _context.ExamenesSubModulo
                                             .AsNoTracking()
                                             .AnyAsync(e => e.Id == examenSubModuloId)
                                             .ConfigureAwait(false);

            if (!existeExamen)
                return ServiceResult<ExamenIntentoDto?>.Fail("El examen de submódulo no existe.");

            return await ObtenerUltimoIntentoAsync(
                    userId,
                    query => query.Where(ie => ie.ExamenSubModuloId == examenSubModuloId),
                    "No se encontraron intentos para el examen y usuario solicitados.")
                .ConfigureAwait(false);
        }

        private async Task<ServiceResult<ExamenIntentoDto?>> ObtenerUltimoIntentoModuloAsync(string userId, int examenModuloId)
        {
            if (examenModuloId <= 0)
                return ServiceResult<ExamenIntentoDto?>.Fail("El id del examen debe ser mayor a cero.");

            if (string.IsNullOrWhiteSpace(userId))
                return ServiceResult<ExamenIntentoDto?>.Fail("El id del usuario no puede estar vacío.");

            var existeExamen = await _context.ExamenesModulos
                                             .AsNoTracking()
                                             .AnyAsync(e => e.Id == examenModuloId)
                                             .ConfigureAwait(false);

            if (!existeExamen)
                return ServiceResult<ExamenIntentoDto?>.Fail("El examen de módulo no existe.");

            return await ObtenerUltimoIntentoAsync(
                    userId,
                    query => query.Where(ie => ie.ExamenModuloId == examenModuloId),
                    "No se encontraron intentos para el examen de módulo y usuario solicitados.")
                .ConfigureAwait(false);
        }

        private async Task<ServiceResult<ExamenIntentoDto?>> ObtenerUltimoIntentoLibreAsync(string userId, int examenId)
        {
            if (examenId <= 0)
                return ServiceResult<ExamenIntentoDto?>.Fail("El id del examen debe ser mayor a cero.");

            if (string.IsNullOrWhiteSpace(userId))
                return ServiceResult<ExamenIntentoDto?>.Fail("El id del usuario no puede estar vacío.");

            var existeExamen = await _context.Examenes
                                             .AsNoTracking()
                                             .AnyAsync(e => e.Id == examenId)
                                             .ConfigureAwait(false);

            if (!existeExamen)
                return ServiceResult<ExamenIntentoDto?>.Fail("El examen libre no existe.");

            return await ObtenerUltimoIntentoAsync(
                    userId,
                    query => query.Where(ie => ie.ExamenId == examenId),
                    "No se encontraron intentos para el examen libre y usuario solicitados.")
                .ConfigureAwait(false);
        }

        private async Task<ServiceResult<ExamenIntentoDto?>> ObtenerUltimoIntentoAsync(
            string userId,
            Func<IQueryable<ExamenIntento>, IQueryable<ExamenIntento>> filtroExamen,
            string mensajeNoEncontrado)
        {
            var ultimo = await filtroExamen(
                    _context.ExamenesIntentos
                        .AsNoTracking()
                        .Where(ie => ie.IdUsuario == userId))
                .OrderByDescending(ie => ie.FechaIntento)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (ultimo is null)
                return ServiceResult<ExamenIntentoDto?>.Fail(mensajeNoEncontrado);

            var ultimoDto = _mapper.Map<ExamenIntentoDto>(ultimo);
            return ServiceResult<ExamenIntentoDto?>.Ok(ultimoDto);
        }

        private static (ExamenTipo tipo, int examenId) ObtenerReferenciaExamen(ExamenIntentoDto intentoDto)
        {
            if (intentoDto.ExamenSubModuloId is int examenSubModuloId)
                return (ExamenTipo.SubModulo, examenSubModuloId);

            if (intentoDto.ExamenModuloId is int examenModuloId)
                return (ExamenTipo.Modulo, examenModuloId);

            if (intentoDto.ExamenId is int examenId)
                return (ExamenTipo.Libre, examenId);

            throw new InvalidOperationException("El intento no referencia ningun examen.");
        }

        private IQueryable<ExamenIntento> CrearConsultaIntentosMismoExamen(string userId, ExamenTipo tipo, int examenId)
        {
            var query = _context.ExamenesIntentos
                .AsNoTracking()
                .Where(i => i.IdUsuario == userId);

            return tipo switch
            {
                ExamenTipo.SubModulo => query.Where(i => i.ExamenSubModuloId == examenId),
                ExamenTipo.Modulo => query.Where(i => i.ExamenModuloId == examenId),
                ExamenTipo.Libre => query.Where(i => i.ExamenId == examenId),
                _ => query.Where(_ => false)
            };
        }

        private async Task<ExamenConfiguracion?> ObtenerConfiguracionExamenAsync(ExamenTipo tipo, int examenId)
        {
            if (examenId <= 0)
                return null;

            if (tipo == ExamenTipo.SubModulo)
            {
                return await _context.ExamenesSubModulo
                    .AsNoTracking()
                    .Where(e => e.Id == examenId)
                    .Select(e => new ExamenConfiguracion(
                        ExamenTipo.SubModulo,
                        e.Id,
                        e.NumeroPreguntas,
                        e.PuntajeAprobacion,
                        e.IntentosMaximos,
                        e.TiempoLimiteSegundos))
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
            }

            if (tipo == ExamenTipo.Modulo)
            {
                return await _context.ExamenesModulos
                    .AsNoTracking()
                    .Where(e => e.Id == examenId)
                    .Select(e => new ExamenConfiguracion(
                        ExamenTipo.Modulo,
                        e.Id,
                        e.NumeroPreguntas,
                        e.PuntajeAprobacion,
                        e.IntentosMaximos,
                        e.TiempoLimiteSegundos))
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
            }

            return await _context.Examenes
                .AsNoTracking()
                .Where(e => e.Id == examenId)
                .Select(e => new ExamenConfiguracion(
                    ExamenTipo.Libre,
                    e.Id,
                    e.NumeroPreguntas,
                    e.PuntajeAprobacion,
                    int.MaxValue,
                    null))
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        private static bool EstaAprobado(int numeroAciertos, double porcentajeObtenido, int puntajeAprobacion, int numeroPreguntas)
        {
            if (puntajeAprobacion <= numeroPreguntas)
                return numeroAciertos >= puntajeAprobacion;

            return porcentajeObtenido >= puntajeAprobacion;
        }

        private sealed record ExamenConfiguracion(
            ExamenTipo Tipo,
            int ExamenId,
            int NumeroPreguntas,
            int PuntajeAprobacion,
            int IntentosMaximos,
            int? TiempoLimiteSegundos);

    }
}
