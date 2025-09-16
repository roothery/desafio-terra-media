namespace Desafio.TerraMedia.WebApi.Features.Comentarios.GetComentario
{
    public class GetComentarioResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string LivroId { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
        public DateTime DataComentario { get; set; }
    }
}
