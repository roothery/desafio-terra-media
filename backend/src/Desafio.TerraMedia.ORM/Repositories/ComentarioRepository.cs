using Desafio.TerraMedia.Domain.Entities;
using Desafio.TerraMedia.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Desafio.TerraMedia.ORM.Repositories
{
    public class ComentarioRepository : IComentarioRepository
    {
        private readonly DefaultContext _context;

        public ComentarioRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Comentario> CreateAsync(Comentario comentario, CancellationToken cancellationToken = default)
        {
            await _context.Comentarios.AddAsync(comentario, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return comentario;
        }

        public async Task<Comentario?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Comentarios.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Comentario>> GetByUserAndLivroAsync(Guid userId, string livroId, CancellationToken cancellationToken = default)
        {
            var comentarios = await _context.Comentarios
                .Where(c => c.UserId == userId && c.LivroId == livroId)
                .ToListAsync(cancellationToken) ?? [];

            return comentarios;
        }

        public async Task<IEnumerable<Comentario>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Comentarios.ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(Comentario comentario, CancellationToken cancellationToken = default)
        {
            _context.Comentarios.Update(comentario);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var comentario = await GetByIdAsync(id, cancellationToken);

            if (comentario == null)
                return false;

            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
