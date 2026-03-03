using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ACC.API.Services;

public class TareasPersonalesService : ITareasPersonalesService
{
    private readonly ACCDbContext _dbContext;
    private readonly IMapper _mapper;

    public TareasPersonalesService(ACCDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ServiceResult<TareaPersonalDto>> CreateTareaPersonalAsync(TareaPersonalDto tareaPersonalDto, string idUsuario)
    {
        try
        {
            if (tareaPersonalDto == null)
            {
                return ServiceResult<TareaPersonalDto>.Fail("El cuerpo de la tarea es requerido.");
            }

            if (string.IsNullOrWhiteSpace(tareaPersonalDto.TareaPersonalTitulo))
            {
                return ServiceResult<TareaPersonalDto>.Fail("El título de la tarea no puede estar vacío.");
            }

            var tareaPersonal = _mapper.Map<TareaPersonal>(tareaPersonalDto);
            tareaPersonal.Completada = false;
            tareaPersonal.IdUsuario = idUsuario;

            _dbContext.TareasPersonales.Add(tareaPersonal);
            await _dbContext.SaveChangesAsync();

            return ServiceResult<TareaPersonalDto>.Ok(_mapper.Map<TareaPersonalDto>(tareaPersonal));
        }
        catch (Exception ex)
        {
            return ServiceResult<TareaPersonalDto>.Error(ex);
        }
    }

    public async Task<ServiceResult<bool>> DeleteTareaPersonalAsync(int tareaPersonalId, string userId)
    {
        try
        {
            var tareaPersonal = await _dbContext.TareasPersonales
                .FirstOrDefaultAsync(t => t.TareaPersonalId == tareaPersonalId && t.IdUsuario == userId);

            if (tareaPersonal == null)
            {
                return ServiceResult<bool>.Fail("La tarea personal no fue encontrada.");
            }

            _dbContext.TareasPersonales.Remove(tareaPersonal);
            await _dbContext.SaveChangesAsync();

            return ServiceResult<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>.Error(ex);
        }
    }

    public async Task<ServiceResult<TareaPersonalDto>> GetTareaPersonalByUserAsync(int idTareaPersonal, string userId)
    {
        try
        {
            var tareaPersonal = await _dbContext.TareasPersonales
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TareaPersonalId == idTareaPersonal && t.IdUsuario == userId);

            if (tareaPersonal == null)
            {
                return ServiceResult<TareaPersonalDto>.Fail("La tarea personal no fue encontrada.");
            }

            return ServiceResult<TareaPersonalDto>.Ok(_mapper.Map<TareaPersonalDto>(tareaPersonal));
        }
        catch (Exception ex)
        {
            return ServiceResult<TareaPersonalDto>.Error(ex);
        }
    }

    public async Task<ServiceResult<TareaPersonalDto>> UpdateTareaPersonalAsync(TareaPersonalDto tareaPersonalDto, string userId)
    {
        try
        {
            if (tareaPersonalDto == null)
            {
                return ServiceResult<TareaPersonalDto>.Fail("El cuerpo de la tarea es requerido.");
            }

            if (tareaPersonalDto.TareaPersonalId <= 0 || string.IsNullOrWhiteSpace(tareaPersonalDto.TareaPersonalTitulo))
            {
                return ServiceResult<TareaPersonalDto>.Fail("El título de la tarea no puede estar vacío.");
            }

            var tareaPersonal = await _dbContext.TareasPersonales
                .FirstOrDefaultAsync(t => t.TareaPersonalId == tareaPersonalDto.TareaPersonalId && t.IdUsuario == userId);

            if (tareaPersonal == null)
            {
                return ServiceResult<TareaPersonalDto>.NotFound("La tarea personal no fue encontrada.");
            }

            tareaPersonal.TareaPersonalTitulo = tareaPersonalDto.TareaPersonalTitulo;
            tareaPersonal.TareaPersonalDescripcion = tareaPersonalDto.TareaPersonalDescripcion;
            tareaPersonal.TareaPersonalFechaLimite = tareaPersonalDto.TareaPersonalFechaLimite;
            tareaPersonal.Completada = tareaPersonalDto.Completada ?? false;
            tareaPersonal.IdAgenda = tareaPersonalDto.IdAgenda;

            await _dbContext.SaveChangesAsync();

            return ServiceResult<TareaPersonalDto>.Ok(_mapper.Map<TareaPersonalDto>(tareaPersonal));
        }
        catch (Exception ex)
        {
            return ServiceResult<TareaPersonalDto>.Error(ex);
        }
    }

    public async Task<ServiceResult<List<TareaPersonalDto>>> GetTareasPersonalesByUserAsync(string userId)
    {
        try
        {
            var tareasPersonales = await _dbContext.TareasPersonales
                .AsNoTracking()
                .Where(t => t.IdUsuario == userId)
                .OrderBy(t => t.TareaPersonalFechaLimite)
                .ToListAsync();

            var tareasPersonalesDto = _mapper.Map<List<TareaPersonalDto>>(tareasPersonales);
            return ServiceResult<List<TareaPersonalDto>>.Ok(tareasPersonalesDto);
        }
        catch (Exception ex)
        {
            return ServiceResult<List<TareaPersonalDto>>.Error(ex);
        }
    }
}
