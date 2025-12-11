using ACC.API.Interfaces;
using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using ACC.Shared.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

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
                                         .ToListAsync()
                                         .ConfigureAwait(false);

            if (examenes.Count == 0)
                return ServiceResult<List<ExamenSubModuloDto>>.Fail("No se encontraron exámenes de submódulo.");

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
        public async Task<ServiceResult<ExamenIntentoDto?>> ObtenerUltimoIntentoPorUsuarioYExamenAsync(string userId, int examenSubModuloId)
        {
            if (examenSubModuloId <= 0)
                return ServiceResult<ExamenIntentoDto?>.Fail("El id del examen debe ser mayor a cero.");

            if (string.IsNullOrWhiteSpace(userId))
                return ServiceResult<ExamenIntentoDto?>.Fail("El id del usuario no puede estar vacío.");

            // Validar existencia del examen para cortar temprano.
            var existeExamen = await _context.ExamenesSubModulo
                                             .AsNoTracking()
                                             .AnyAsync(e => e.Id == examenSubModuloId)
                                             .ConfigureAwait(false);

            if (!existeExamen)
                return ServiceResult<ExamenIntentoDto?>.Fail("El examen de submódulo no existe.");

            var ultimo = await _context.ExamenesIntentos
                                       .AsNoTracking()
                                       .Where(ie => ie.IdUsuario == userId && ie.ExamenSubModuloId == examenSubModuloId)
                                       .OrderByDescending(ie => ie.FechaIntento) // Cambia el nombre si tu propiedad difiere.
                                       .FirstOrDefaultAsync()
                                       .ConfigureAwait(false);

            if (ultimo is null)
                return ServiceResult<ExamenIntentoDto?>.Fail("No se encontraron intentos para el examen y usuario solicitados.");

            var ultimoDto = _mapper.Map<ExamenIntentoDto>(ultimo);
            return ServiceResult<ExamenIntentoDto?>.Ok(ultimoDto);
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

            if (intentoDto.Calificacion < 0)
                return ServiceResult<ExamenIntentoDto>.Fail("El puntaje obtenido no puede ser negativo.");

            // 1) Debe referenciar EXACTAMENTE un examen
            int fkCount = 0;
            if (intentoDto.ExamenSubModuloId.HasValue) fkCount++;
            if (intentoDto.ExamenModuloId.HasValue) fkCount++;
            if (intentoDto.ExamenId.HasValue) fkCount++;
            if (fkCount != 1)
                return ServiceResult<ExamenIntentoDto>.Fail("Debe referenciar exactamente un examen (SubMódulo, Módulo o Libre).");

            // 2) Verificar existencia del examen y obtener umbral real
            //    (ligero y seguro; no confiamos ciegamente en 'Aprobado' del cliente)
            int umbral;
            ExamenTipo tipo;
            int examenId;

            if (intentoDto.ExamenSubModuloId is int esmId)
            {
                var exam = await _context.ExamenesSubModulo
                    .AsNoTracking()
                    .Where(e => e.Id == esmId)
                    .Select(e => new { e.Id, e.PuntajeAprobacion })
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                if (exam is null)
                    return ServiceResult<ExamenIntentoDto>.Fail("El examen de submódulo no existe.");

                umbral = exam.PuntajeAprobacion;
                tipo = ExamenTipo.SubModulo;
                examenId = exam.Id;
            }
            else if (intentoDto.ExamenModuloId is int emId)
            {
                var exam = await _context.ExamenesModulos
                    .AsNoTracking()
                    .Where(e => e.Id == emId)
                    .Select(e => new { e.Id, e.PuntajeAprobacion })
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                if (exam is null)
                    return ServiceResult<ExamenIntentoDto>.Fail("El examen de módulo no existe.");

                umbral = exam.PuntajeAprobacion;
                tipo = ExamenTipo.Modulo;
                examenId = exam.Id;
            }
            else // intentoDto.ExamenId
            {
                var exam = await _context.Examenes
                    .AsNoTracking()
                    .Where(e => e.Id == intentoDto.ExamenId)
                    .Select(e => new { e.Id, e.PuntajeAprobacion })
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                if (exam is null)
                    return ServiceResult<ExamenIntentoDto>.Fail("El examen genérico no existe.");

                umbral = exam.PuntajeAprobacion;
                tipo = ExamenTipo.Libre;
                examenId = exam.Id;
            }

            // 3) Mapear a entidad
            var entity = _mapper.Map<ExamenIntento>(intentoDto);

            // 4) Sello de tiempo en servidor
            if (entity.FechaIntento == default)
                entity.FechaIntento = DateTimeOffset.UtcNow;

            // 5) Numeración secuencial por (Usuario + Examen lógico)
            var sameExamQuery = _context.ExamenesIntentos.AsNoTracking()
                .Where(i => i.IdUsuario == entity.IdUsuario);

            sameExamQuery = tipo switch
            {
                ExamenTipo.SubModulo => sameExamQuery.Where(i => i.ExamenSubModuloId == examenId),
                ExamenTipo.Modulo => sameExamQuery.Where(i => i.ExamenModuloId == examenId),
                ExamenTipo.Libre => sameExamQuery.Where(i => i.ExamenId == examenId),
                _ => sameExamQuery.Where(_ => false)
            };

            var nextN = (await sameExamQuery
                            .Select(i => (int?)i.NumeroIntento)
                            .MaxAsync()
                            .ConfigureAwait(false)) ?? 0;

            entity.NumeroIntento = nextN + 1;

            // 6) Determinar aprobación en servidor (con umbral real)
            entity.Aprobado = entity.Calificacion >= umbral;

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
                                       && a.Tipo == tipo
                                       && a.ExamenId == examenId)
                        .ConfigureAwait(false);

                    if (!yaExisteAprobatorio)
                    {
                        _context.ExamenesAprobatorios.Add(new ExamenAprobatorio
                        {
                            UsuarioId = entity.IdUsuario,
                            Tipo = tipo,
                            ExamenId = examenId,
                            ExamenIntentoId = entity.Id,
                            FechaAprobacion = DateTimeOffset.UtcNow,
                            Calificacion = entity.Calificacion
                        });

                        await _context.SaveChangesAsync().ConfigureAwait(false);
                    }

                    await tx.CommitAsync().ConfigureAwait(false);

                    // 9) PRERREQUISITOS según tu servicio:
                    // - Aprobación de SubMódulo → podría habilitar el Examen de Módulo (ReglaB)
                    if (tipo == ExamenTipo.SubModulo)
                        await _prerrequisitos.EvaluarDesbloqueosPorAprobacionAsync(entity.IdUsuario, examenId);

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

            if (examenes.Count == 0)
                return ServiceResult<List<ExamenModuloDto>>.Fail("No se encontraron exámenes de módulo.");

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

    }
}
