using MediatR;

namespace Desafio.TerraMedia.Application.Comentarios.UpdateComentario
{
    public class UpdateComentarioCommand : IRequest<UpdateComentarioResult>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string LivroId { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
        public DateTime DataComentario { get; set; }
    }
}
