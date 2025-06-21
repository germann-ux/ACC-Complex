using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACC.Shared.DTOs;
using ACC.Data.Entities;
using ACC.Data;
using ACC.Shared.Interfaces;

namespace ACC.API.Services
{
    public class TagService : ITagService
    {
        private readonly ACCDbContext _context;

        public TagService(ACCDbContext context)
        {
            _context = context;
        }

        public Task<TagDto> CreateTagAsync(TagDto tag)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTagAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TagDto>> GetAllTagsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TagDto> GetTagByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TagDto>> GetTagsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TagDto>> GetTagsByModuloIdAsync(int moduloId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTagAsync(TagDto tag)
        {
            throw new NotImplementedException();
        }
    }
}
