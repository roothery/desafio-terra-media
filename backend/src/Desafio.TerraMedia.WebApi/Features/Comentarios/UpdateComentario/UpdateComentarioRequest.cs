namespace Desafio.TerraMedia.WebApi.Features.Comentarios.UpdateComentario
{
    public class UpdateComentarioRequest
    {
        public Guid UserId { get; set; }
        public string LivroId { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
        public DateTime DataComentario { get; set; }
    }
}
