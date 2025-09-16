using Desafio.TerraMedia.Common.Validation;
using Desafio.TerraMedia.Domain.Common;
using Desafio.TerraMedia.Domain.Validation;

namespace Desafio.TerraMedia.Domain.Entities
{
    public class Comentario : BaseEntity
    {
        public Guid UserId { get; set; }
        public string LivroId { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
        public DateTime DataComentario { get; set; }

        public Comentario(Guid userId, string livroId, string texto)
        {
            UserId = userId;
            LivroId = livroId;
            Texto = texto;
            DataComentario = DateTime.Now;
            Validate();
        }

        public void Update(Guid userId, string livroId, string texto)
        {
            UserId = userId;
            LivroId = livroId;
            Texto = texto;
            DataComentario = DateTime.Now;
            Validate();
        }

        public ValidationResultDetail Validate()
        {
            var validator = new ComentarioValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
