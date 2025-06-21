using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace ACC.API.Services
{
    public class AulaService : IAulaService
    {
        private readonly ACCDbContext _context;
        private readonly IMapper _mapper;

        public AulaService(ACCDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// METODO ENCARGADO DE ACTUALIZAR UN AVISO EN EL AULA DE MANERA ASINCRONA
        /// </summary>
        /// <param name="Aviso"></param>
        /// <returns>El resultado de la operacion(ServiceResult) y el objeto(AvisoDto)</returns>
        public async Task<ServiceResult<AvisoDto>> UpdateAvisoAsync(AvisoDto Aviso)
        {
            try // captura de erres
            {
                // se evalua si es que el aviso esta nulo
                if (Aviso == null)
                {
                    return ServiceResult<AvisoDto>.Fail("El aviso no tiene contenido");
                }
                // si no es nulo
                else
                {
                    // se mapea el avisoDto a aviso para poder usarlo con la db
                    var AvisoActualizado = _mapper.Map<Aviso>(Aviso);
                    _context.Avisos.Update(AvisoActualizado);
                    // se guardan cambios
                    await _context.SaveChangesAsync();
                    // se retorna el exito de la operacion
                    return ServiceResult<AvisoDto>.Ok(Aviso, "Aviso Actualizado de manera correcta");
                }
            }
            // se captura la exepcion y se retorna el mensaje de esta misma.
            catch (Exception ex)
            {
                return ServiceResult<AvisoDto>.Error(ex);
            }
        }
        /// <summary>
        /// METODO ENCARGADO DE ASIGNAR UN AULA A LOS ALUMNOS EN UN AULA DE MANERA ASINCRONA
        /// </summary>
        /// <param name="tarea"></param>
        /// <param name="IdAula"></param>
        /// <returns>El resultado de la operacion(ServiceResult) y el objeto(TareaAsignadaDto)</returns>
        public async Task<ServiceResult<TareaAsignadaDto>> AsignarTareaAulaAsync(TareaAsignadaDto Tarea, int IdAula)
        {
            try
            {
                // se convierte la tarea a un objeto tarea asignada 
                var tareaAsignada = _mapper.Map<TareaAsignada>(Tarea);
                // se hace la lista de los estudiantes en el aula para luego agregarle la tarea a todos a su agenda.
                var estudiantes = await _context.AulaEstudiantes
                    .Include(e => e.Usuario)
                    .ThenInclude(u => u.Agenda)
                    .Where(es => es.AulaId == IdAula)
                    .ToListAsync(); 
                
                // evaluacion basica para evitar referencias nulas
                if (estudiantes.Count == 0)
                {
                    return ServiceResult<TareaAsignadaDto>.Fail("No hay alumnos en el Aula");
                }
                // si pasa la evaluacion se procede con agregar las tareas a las agendas de cada usuario
                else
                {
                    // para cada usuario que este en la lista que se hizo anteriormente
                    foreach (var estudiante in estudiantes)
                    {
                        // lo agrega a la agenda de cada usuario
                        estudiante.Usuario.Agenda.TareasAsignadas.Add(tareaAsignada);
                    }
                    // se guardan los datos en la base de datos
                    await _context.SaveChangesAsync();
                    // se retorna el resultado(un resultado de exito obvio)
                    return ServiceResult<TareaAsignadaDto>.Ok(Tarea, "Tarea asignada a los alumnos con exito");
                }
            }
            catch(Exception ex)
            {
                return ServiceResult<TareaAsignadaDto>.Error(ex); 
            }
        }
        /// <summary>
        /// METODO ENCARGADO DE CREAR UN AULA DE MANERA ASINCRONA
        /// </summary>
        /// <param name="aula"></param>
        /// <returns>El resultado de la operacion(ServiceResult) y el objeto(AulaDto)</returns>
        public async Task<ServiceResult<AulaDto>> CreateAulaAsync(AulaDto Aula)
        {
            // captura de exepciones 
            try
            {
                // evaluacion basica para comprobar seguridad de la creacion del aula desde la plataforma.
                if (Aula.Nombre == null || Aula.DocenteId == null)
                {
                    // retornar el error si es que no cumple con alguna condicion
                    return ServiceResult<AulaDto>.Fail("Error al crear el aula, El aula debe tener un nombre");
                }
                // si se cumple procede el metodo
                else
                {
                    // se crea un objeto, se mapea y se guarda en la base de datos
                    var AulaCreada = _mapper.Map<Aula>(Aula);
                    await _context.Aulas.AddAsync(AulaCreada);
                    await _context.SaveChangesAsync();
                    // se retorna el exito de la operacion
                    return ServiceResult<AulaDto>.Ok(Aula, $"Se creo el aula {Aula.Nombre} con exito");
                }
            }
            // si es que se captura una ex se retorna el mensaje de esta misma
            catch (Exception ex)
            {
                return ServiceResult<AulaDto>.Error(ex);
            }
        }
        /// <summary>
        /// METODO ENCARGADO DE CREAR UNA AVISO
        /// </summary>
        /// <param name="aviso"></param>
        /// <returns>El resultado de la operacion(ServiceResult) y el objeto(AvisoDto)</returns>
        public async Task<ServiceResult<AvisoDto>> CreateAvisoAsync(AvisoDto Aviso)
        {
            // captura de errores
            try
            {
                // evaluacion para ver si el aviso tiene los campos nesesarios
                if (Aviso.TituloAviso == null || Aviso.ContenidoAviso == null || Aviso.DocenteId == null)
                {
                    // retornar el mensaje de error si no se cumple con alguna condicion
                    return ServiceResult<AvisoDto>.Fail($"El aviso no cumple con los contenidos: Titulo del aviso: {Aviso.TituloAviso}, Contenido del aviso: {Aviso.ContenidoAviso}, Docente ID: {Aviso.DocenteId}"); 
                }
                // si cumple las condiciones se procede
                else
                {
                    // se crea mapea el objeto a la entidad Aviso para poder usarse en la db
                    var AvisoEntity = _mapper.Map<Aviso>(Aviso);
                    // se agrega el aviso a la base de datos y se guardan los cambios en la base de datos
                    await _context.Avisos.AddAsync(AvisoEntity);
                    await _context.SaveChangesAsync();
                    // retorna el exito de la operacion
                    return ServiceResult<AvisoDto>.Ok(Aviso, "Aviso creado con exito");
                }
            }
            // captura de la exepcion
            catch (Exception ex)
            {
                // retorna el mensaje de la exepcion
                return ServiceResult<AvisoDto>.Error(ex);
            }
        }

        public async Task<bool> DeleteAulaAsync(int aulaId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<bool>> EliminarAulmnoAulaAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<AulaDto>> GetAulaByIdAsync(int aulaId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<IEnumerable<AulaDto>>> GetAulasByModuloAsync(int moduloId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetClaveAula(int IdDocente)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<bool>> MarcarComoCompletadaAsync(int IdTarea, string UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<AulaDto>> UpdateAulaAsync(AulaDto aula)
        {
            throw new NotImplementedException();
        }
    }
}



