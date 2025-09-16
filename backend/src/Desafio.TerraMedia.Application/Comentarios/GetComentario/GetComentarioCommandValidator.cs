using FluentValidation;

namespace Desafio.TerraMedia.Application.Comentarios.GetComentario
{
    public class GetComentarioCommandValidator : AbstractValidator<GetComentarioCommand>
    {
        public GetComentarioCommandValidator()
        {
            RuleFor(c => c.UserId).NotEmpty().WithMessage("UserId is required.");
            RuleFor(c => c.LivroId).NotEmpty().WithMessage("LivroId is required.").MaximumLength(50).WithMessage("LivroId cannot be longer than 50 characters.");
        }
    }
}