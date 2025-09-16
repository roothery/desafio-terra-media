using Desafio.TerraMedia.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Desafio.TerraMedia.Application.Comentarios.DeleteComentario
{
    public class DeleteComentarioHandler : IRequestHandler<DeleteComentarioCommand, DeleteComentarioResult>
    {
        private readonly IComentarioRepository _comentarioRepository;

        public DeleteComentarioHandler(IComentarioRepository comentarioRepository)
        {
            _comentarioRepository = comentarioRepository;
        }

        public async Task<DeleteComentarioResult> Handle(DeleteComentarioCommand command, CancellationToken cancellationToken)
        {
            var validator = new DeleteComentarioCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var result = await _comentarioRepository.DeleteAsync(command.Id, cancellationToken);
            if (!result)
                throw new KeyNotFoundException($"Comentario with Id {command.Id} not found.");

            return new DeleteComentarioResult { Sucesso = result };
        }
    }
}
