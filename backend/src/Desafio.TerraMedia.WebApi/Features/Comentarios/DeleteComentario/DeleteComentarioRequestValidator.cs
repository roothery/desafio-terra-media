using FluentValidation;

namespace Desafio.TerraMedia.WebApi.Features.Comentarios.DeleteComentario
{
    public class DeleteComentarioRequestValidator : AbstractValidator<DeleteComentarioRequest>
    {
        public DeleteComentarioRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        }
    }
}
