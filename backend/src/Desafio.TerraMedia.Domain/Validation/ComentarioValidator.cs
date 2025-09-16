using Desafio.TerraMedia.Domain.Entities;
using FluentValidation;

namespace Desafio.TerraMedia.Domain.Validation
{
    public class ComentarioValidator : AbstractValidator<Comentario>
    {
        public ComentarioValidator()
        {
            RuleFor(comentario => comentario.UserId)
                .NotEmpty().WithMessage("UserId is required.");
            RuleFor(comentario => comentario.LivroId)
                .NotEmpty().WithMessage("LivroId is required.")
                .MaximumLength(50).WithMessage("LivroId cannot be longer than 50 characters.");
            RuleFor(comentario => comentario.Texto)
                .NotEmpty().WithMessage("Texto is required.")
                .MaximumLength(500).WithMessage("Texto cannot be longer than 500 characters.");
            RuleFor(comentario => comentario.DataComentario)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("DataComentario must be less than or equal to the current date.");
        }
    }
}
