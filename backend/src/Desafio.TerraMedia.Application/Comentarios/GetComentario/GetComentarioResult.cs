namespace Desafio.TerraMedia.Application.Comentarios.GetComentario
{
    public class GetComentarioResult
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string LivroId { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
        public DateTime DataComentario { get; set; }
    }
}
