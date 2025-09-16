using FluentValidation;

namespace Desafio.TerraMedia.Application.Comentarios.DeleteComentario
{
    public class DeleteComentarioCommandValidator : AbstractValidator<DeleteComentarioCommand>
    {
        public DeleteComentarioCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        }
    }
}
