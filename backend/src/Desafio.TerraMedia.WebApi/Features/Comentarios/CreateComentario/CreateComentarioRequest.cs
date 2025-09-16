namespace Desafio.TerraMedia.WebApi.Features.Comentarios.CreateComentario
{
    public class CreateComentarioRequest
    {
        public Guid UserId { get; set; }
        public string LivroId { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
        public DateTime DataComentario { get; set; }
    }
}
