using Desafio.TerraMedia.Domain.Entities;

namespace Desafio.TerraMedia.Domain.Repositories
{
    public interface IComentarioRepository
    {
        Task<Comentario> CreateAsync(Comentario comentario, CancellationToken cancellationToken = default);

        Task<Comentario?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Comentario>> GetByUserAndLivroAsync(Guid userId, string livroId, CancellationToken cancellationToken = default);

        Task<IEnumerable<Comentario>> GetAllAsync(CancellationToken cancellationToken = default);

        Task UpdateAsync(Comentario comentario, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
