using MediatR;

namespace Desafio.TerraMedia.Application.Comentarios.CreateComentario
{
    public class CreateComentarioCommand : IRequest<CreateComentarioResult>
    {
        public Guid UserId { get; set; }
        public string LivroId { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
        public DateTime DataComentario { get; set; }
    }
}
