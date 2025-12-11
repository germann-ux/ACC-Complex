using ACC.Data;
using ACC.Shared.DTOs;
using AutoMapper;

namespace ACC.API.Interfaces
{
    public interface ICapitulosService
    {
        /// <summary>
        /// metodo para obtener los capitulos seleccionados aleatoriamente para recomendacion en el resumen
        /// </summary>
        /// <param name="seleccionados"></param>
        /// <returns></returns>
        Task<List<ContenidoCapituloDto>> CapitulosRecomendados(List<int> seleccionados);
        /// <summary>
        /// metodo para obtener los ids de los capitulos random, para recomendacion en el resumen
        /// </summary>
        /// <param name="maxIdIncluido"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<List<ContenidoCapituloDto>> CapitulosRecomendadosRandom(int maxIdIncluido, int count);
    }
}
