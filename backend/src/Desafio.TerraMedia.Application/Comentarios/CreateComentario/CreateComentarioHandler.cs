using AutoMapper;
using Desafio.TerraMedia.Domain.Entities;
using Desafio.TerraMedia.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Desafio.TerraMedia.Application.Comentarios.CreateComentario
{
    public class CreateComentarioHandler : IRequestHandler<CreateComentarioCommand, CreateComentarioResult>
    {
        private readonly IComentarioRepository _comentarioRepository;
        private readonly IMapper _mapper;

        public CreateComentarioHandler(IComentarioRepository comentarioRepository, IMapper mapper)
        {
            _comentarioRepository = comentarioRepository;
            _mapper = mapper;
        }

        public async Task<CreateComentarioResult> Handle(CreateComentarioCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateComentarioCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var comentarios = await _comentarioRepository.GetByUserAndLivroAsync(command.UserId, command.LivroId, cancellationToken);

            if (comentarios.Any())
            {
                if (comentarios.Count() >= 10)
                    throw new InvalidOperationException("The user has already reached the maximum number of 10 comments per book.");

                var comentariosPorMinuto = comentarios.Count(c => c.DataComentario >= DateTime.UtcNow.AddMinutes(-1));

                if (comentariosPorMinuto >= 3)
                    throw new InvalidOperationException("The user has already reached the maximum number of 3 comments per minute for this book.");
            }

            var comentario = await _comentarioRepository.CreateAsync(_mapper.Map<Comentario>(command), cancellationToken);

            var result = _mapper.Map<CreateComentarioResult>(comentario);
            return result;
        }
    }
}
