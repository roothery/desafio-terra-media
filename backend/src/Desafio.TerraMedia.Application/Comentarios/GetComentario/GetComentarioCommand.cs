using MediatR;

namespace Desafio.TerraMedia.Application.Comentarios.GetComentario
{
    public class GetComentarioCommand : IRequest<IEnumerable<GetComentarioResult>>
    {
        public Guid UserId { get; set; }
        public string LivroId { get; set; } = string.Empty;

        public GetComentarioCommand(Guid userId, string livroId)
        {
            UserId = userId;
            LivroId = livroId;
        }
    }
}
