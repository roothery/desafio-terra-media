using FluentValidation;

namespace Desafio.TerraMedia.Application.Comentarios.CreateComentario
{
    public class CreateComentarioValidator : AbstractValidator<CreateComentarioCommand>
    {
        public CreateComentarioValidator()
        {
            RuleFor(c => c.UserId).NotEmpty().WithMessage("UserId is required.");
            RuleFor(c => c.LivroId).NotEmpty().WithMessage("LivroId is required.").MaximumLength(50).WithMessage("LivroId cannot be longer than 50 characters.");
            RuleFor(c => c.Texto).NotEmpty().WithMessage("Texto is required.").MaximumLength(500).WithMessage("Texto cannot be longer than 500 characters.");
            RuleFor(c => c.DataComentario).LessThanOrEqualTo(DateTime.Now).WithMessage("DataComentario must be less than or equal to the current date.");
        }
    }
}
