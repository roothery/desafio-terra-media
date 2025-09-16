using MediatR;

namespace Desafio.TerraMedia.Application.Comentarios.DeleteComentario
{
    public class DeleteComentarioCommand : IRequest<DeleteComentarioResult>
    {
        public Guid Id { get; set; }
    }
}
