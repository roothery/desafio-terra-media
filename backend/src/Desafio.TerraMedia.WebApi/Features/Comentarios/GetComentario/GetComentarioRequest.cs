namespace Desafio.TerraMedia.WebApi.Features.Comentarios.GetComentario
{
    public class GetComentarioRequest
    {
        public Guid UserId { get; set; }
        public string LivroId { get; set; } = string.Empty;
    }
}
