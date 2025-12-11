using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.Core;
using ACC.Shared.Interfaces;
using ACC.Shared.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ACC.API.Services
{
    public class TareasService : ITareaService
    {
        private readonly ACCDbContext dbContext; // se declara el contexto de la base de datos
        private readonly IMapper _mapper; // se declara el mapeador

        public TareasService(ACCDbContext _Context, IMapper mapper) // se inicializa el contexto de la base de datos y el mapeador, usando el constructor
        {
            dbContext = _Context;
            _mapper = mapper;
        }

        /* ------------ Metodos para las tareas personales ----------------*/

        // Crear una tarea personal
        public async Task<ServiceResult<TareaPersonalDto>> CreateTareaPersonalAsync(TareaPersonalDto tareaPersonalDto, string IdUsuario)
        {
            try
            {
                // validacion basica o algo asi
                if (string.IsNullOrWhiteSpace(tareaPersonalDto.TareaPersonalTitulo))
                {
                    return ServiceResult<TareaPersonalDto>.Fail("El título de la tarea no puede estar vacío.");
                }

                var tareaPersonal = _mapper.Map<TareaPersonal>(tareaPersonalDto);
                if (tareaPersonal.Completada == true) // una tarea no puede estar ya completada al momento de crearse xd
                {
                    tareaPersonal.Completada = false; // la tarea se marca como incompleta, ya que pues no puede estar completada al crearse, eso no tiene sense XD
                }
                // Se asigna del usuario a la tarea personal
                tareaPersonal.IdUsuario = IdUsuario;

                // se almacena la nueva tarea personal en la base de datos
                dbContext.TareasPersonales.Add(tareaPersonal);
                await dbContext.SaveChangesAsync(); // se espera a guardar los cambios de manera asincrona

                var tareaPersonalDtoResult = _mapper.Map<TareaPersonalDto>(tareaPersonal);
                // se retorna el mensaje de exito
                return ServiceResult<TareaPersonalDto>.Ok(tareaPersonalDtoResult);
            }
            catch (Exception ex) // capturamos la exeption si algo sale mal
            {
                return ServiceResult<TareaPersonalDto>.Error(ex);// se retorna el mensaje de la exeption
            }
        }
        /// <summary>
        /// Metodo para eliminar una tarea personal por el id de la tarea y el id del usuario
        /// </summary>
        /// <param name="tareaPersonalId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<ServiceResult<bool>> DeleteTareaPersonalAsync(int tareaPersonalId, string UserId)
        {
            try
            {
                var tareaPersonal = await dbContext.TareasPersonales.FirstOrDefaultAsync(t => t.TareaPersonalId == tareaPersonalId && t.IdUsuario == UserId);
                if (tareaPersonal == null)
                {
                    return ServiceResult<bool>.Fail("La tarea personal no fue encontrada.");
                }

                // Se elimina la tarea personal de la base de datos
                dbContext.TareasPersonales.Remove(tareaPersonal);
                await dbContext.SaveChangesAsync();
                return ServiceResult<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Error(ex);
            }
        }

        // Obtener una tarea personal por el id de la tarea y el id del usuario
        public async Task<ServiceResult<TareaPersonalDto>> GetTareaPersonalByUserAsync(int IdTareaPersonal, string userId)
        {
            try
            {
                // Se busca la tarea personal por el id de la tarea y el id del usuario
                var tareaPersonal = await dbContext.TareasPersonales.FirstOrDefaultAsync(t => t.TareaPersonalId == IdTareaPersonal && t.IdUsuario == userId);
                // validacion para verificar si la tarea personal fue encontrada o no existe
                if (tareaPersonal == null)
                {
                    // se retorna el mensaje de error, osea que no fue encontrada.
                    return ServiceResult<TareaPersonalDto>.Fail("La tarea personal no fue encontrada.");
                }

                var tareaPersonalDto = _mapper.Map<TareaPersonalDto>(tareaPersonal);
                return ServiceResult<TareaPersonalDto>.Ok(tareaPersonalDto);// se retorna la tarea personal encontrada
            }
            catch (Exception ex) // capturamos la exeption si algo sale mal. btw no creo
            {
                return ServiceResult<TareaPersonalDto>.Error(ex);
            }
        }

        // Actualizar una tarea personal
        public async Task<ServiceResult<TareaPersonalDto>> UpdateTareaPersonalAsync(TareaPersonalDto tareaPersonalDto)
        {
            try
            {
                if (tareaPersonalDto.TareaPersonalTitulo == null || tareaPersonalDto.TareaPersonalId == 0)
                {
                    return ServiceResult<TareaPersonalDto>.Fail("El título de la tarea no puede estar vacío.");
                }

                var tareaPersonal = _mapper.Map<TareaPersonal>(tareaPersonalDto);
                // se actualiza la tarea personal en la base de datos
                dbContext.TareasPersonales.Update(tareaPersonal);// se actualiza la tarea personal
                await dbContext.SaveChangesAsync(); // se espera a guardar los cambios de manera asincrona

                var tareaPersonalDtoResult = _mapper.Map<TareaPersonalDto>(tareaPersonal);
                return ServiceResult<TareaPersonalDto>.Ok(tareaPersonalDtoResult);// se retorna la tarea personal actualizada, con el mensaje de exito.
            }
            catch (Exception ex)
            {
                return ServiceResult<TareaPersonalDto>.Error(ex);
            }
        }

        // Obtener todas las tareas personales de un usuario, se obtiene una lista
        public async Task<ServiceResult<List<TareaPersonalDto>>> GetTareasPersonalesByUserAsync(string userId)
        {
            try
            {
                // Se buscan todas las tareas personales del usuario por medio de su id de usuario
                var tareasPersonales = await dbContext.TareasPersonales.Where(t => t.IdUsuario == userId).ToListAsync();
                if (tareasPersonales == null || tareasPersonales.Count == 0) // si la lista esta vacia, se indica
                {
                    return ServiceResult<List<TareaPersonalDto>>.Fail("No se encontraron tareas personales.");
                }

                var tareasPersonalesDto = _mapper.Map<List<TareaPersonalDto>>(tareasPersonales);
                // se retorna la lista de tareas personales, con el mensaje de exito
                return ServiceResult<List<TareaPersonalDto>>.Ok(tareasPersonalesDto);
            }
            catch (Exception ex) // capturamos la exeption si algo sale mal
            {
                return ServiceResult<List<TareaPersonalDto>>.Error(ex);
            }
        }

        /* ------------ Metodos para las tareas asignadas por los docentes en las aulas ----------------*/

        // Creación de una tarea asignada por un docente en un aula
        public async Task<ServiceResult<TareaAsignadaDto>> CreateTareaAsignadaAsync(TareaAsignadaDto tareaAsignadaDto)
        {
            try
            {
                var tareaAsignada = _mapper.Map<TareaAsignada>(tareaAsignadaDto);
                tareaAsignada.Completada = false; // se establece como no completada
                if (tareaAsignada.TituloTareaAsignada == null) // validacion para verificar si el titulo de la tarea esta vacio
                {
                    return ServiceResult<TareaAsignadaDto>.Fail("El título de la tarea no puede estar vacío.");
                }

                await dbContext.TareasAsignadas.AddAsync(tareaAsignada); // se almacena la nueva tarea asignada en la base de datos
                await dbContext.SaveChangesAsync(); // se espera a guardar los cambios de manera asincrona

                var tareaAsignadaDtoResult = _mapper.Map<TareaAsignadaDto>(tareaAsignada);
                return ServiceResult<TareaAsignadaDto>.Ok(tareaAsignadaDtoResult);
            }
            catch (Exception ex)// capturamos la exeption si algo sale mal
            {
                return ServiceResult<TareaAsignadaDto>.Error(ex);
            }
        }

        // Borrar una tarea asignada por el id de la tarea
        public async Task<ServiceResult<bool>> DeleteTareaAsignadaAsync(int tareaAsignadaId)
        {
            try
            {
                var tareaBorrar = await dbContext.TareasAsignadas.FirstOrDefaultAsync(t => t.IdTareaAsignada == tareaAsignadaId);
                // validacion para verificar si la tarea asignada fue encontrada o no existe
                if (tareaBorrar == null)
                {
                    return ServiceResult<bool>.Fail("La tarea asignada no fue encontrada.");
                }
                // se elimina de la base de datos
                dbContext.TareasAsignadas.Remove(tareaBorrar);
                // se salvan los cambios de la base de datos
                await dbContext.SaveChangesAsync();// se espera a guardar los cambios de manera asincrona
                return ServiceResult<bool>.Ok(true);// se retorna el mensaje de exito
            }
            catch (Exception ex) // capturamos la exeption si algo sale mal
            {
                return ServiceResult<bool>.Error(ex);
            }
        }

        // Actualizar una tarea asignada
        public async Task<ServiceResult<TareaAsignadaDto>> UpdateTareaAsignadaAsync(TareaAsignadaDto tareaAsignadaDto)
        {
            try
            {
                if (tareaAsignadaDto.TituloTareaAsignada == null) // validacion basica
                {
                    // retornar el mensaje del error
                    return ServiceResult<TareaAsignadaDto>.Fail("El título de la tarea no puede estar vacío.");
                }

                var tareaAsignada = _mapper.Map<TareaAsignada>(tareaAsignadaDto);
                // actualizar el objeto en la base de datos
                dbContext.TareasAsignadas.Update(tareaAsignada);
                await dbContext.SaveChangesAsync(); // se espera a guardar los cambios de manera asincrona

                var tareaAsignadaDtoResult = _mapper.Map<TareaAsignadaDto>(tareaAsignada);
                return ServiceResult<TareaAsignadaDto>.Ok(tareaAsignadaDtoResult); // regresar el mensaje de exito con el objeto
            }
            catch (Exception ex) // capturar excepciones
            {
                return ServiceResult<TareaAsignadaDto>.Error(ex);
            }
        }

        // Obtener todas las tareas asignadas de un aula, se obtiene una lista
        public async Task<ServiceResult<List<TareaAsignadaDto>>> GetTareasAsignadasByAulaAsync(int aulaId)
        {
            try
            {
                // se crea la lista de tareas asignadas en el aula
                var TareasAula = await dbContext.TareasAsignadas.Where(t => t.AulaId == aulaId).ToListAsync();
                if (TareasAula.Count == 0)  // validacion, si la lista no tiene elementos se envia el mensaje del error
                {
                    return ServiceResult<List<TareaAsignadaDto>>.Fail("No se encontraron tareas asignadas en el aula.");
                }

                var tareasAulaDto = _mapper.Map<List<TareaAsignadaDto>>(TareasAula);
                // se retorna la lista de tareas asignadas, con el mensaje de exito
                return ServiceResult<List<TareaAsignadaDto>>.Ok(tareasAulaDto);
            }
            catch (Exception ex)// captura de excepciones
            {
                return ServiceResult<List<TareaAsignadaDto>>.Error(ex);
            }
        }

        // Obtener todas las tareas asignadas de un usuario por medio de su id
        public async Task<ServiceResult<List<TareaAsignadaDto>>> GetTareasAsignadasByUserAsync(string userId)
        {
            try
            {
                // Verificar si existen tareas asignadas para el usuario
                bool existenTareas = await dbContext.TareasAsignadas.AnyAsync(t => t.IdUsuario == userId);
                if (!existenTareas)
                {
                    return ServiceResult<List<TareaAsignadaDto>>.Ok([]); // éxito con lista vacía
                }

                // Obtener las tareas asignadas del usuario
                var tareas = await dbContext.TareasAsignadas
                    .Where(t => t.IdUsuario == userId)
                    .ToListAsync();

                // Mapear las tareas a DTOs
                var tareasDto = _mapper.Map<List<TareaAsignadaDto>>(tareas);

                // Retornar las tareas asignadas con éxito
                return ServiceResult<List<TareaAsignadaDto>>.Ok(tareasDto);
            }
            catch (Exception ex)
            {
                // Capturar y manejar cualquier excepción
                return ServiceResult<List<TareaAsignadaDto>>.Error(ex);
            }
        }

    }
}
