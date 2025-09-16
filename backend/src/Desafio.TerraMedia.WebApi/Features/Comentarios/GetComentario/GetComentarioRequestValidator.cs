using FluentValidation;

namespace Desafio.TerraMedia.WebApi.Features.Comentarios.GetComentario
{
    public class GetComentarioRequestValidator : AbstractValidator<GetComentarioRequest>
    {
        public GetComentarioRequestValidator()
        {
            RuleFor(c => c.UserId).NotEmpty().WithMessage("UserId is required.");
            RuleFor(c => c.LivroId).NotEmpty().WithMessage("LivroId is required.").MaximumLength(50).WithMessage("LivroId cannot be longer than 50 characters.");
        }
    }
}
