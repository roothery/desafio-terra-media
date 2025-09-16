using AutoMapper;
using Desafio.TerraMedia.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Desafio.TerraMedia.Application.Comentarios.GetComentario
{
    public class GetComentarioHandler : IRequestHandler<GetComentarioCommand, IEnumerable<GetComentarioResult>>
    {
        private readonly IComentarioRepository _comentarioRepository;
        private readonly IMapper _mapper;

        public GetComentarioHandler(IComentarioRepository comentarioRepository, IMapper mapper)
        {
            _comentarioRepository = comentarioRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetComentarioResult>> Handle(GetComentarioCommand command, CancellationToken cancellationToken)
        {
            var validator = new GetComentarioCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var comentarios = await _comentarioRepository.GetByUserAndLivroAsync(command.UserId, command.LivroId, cancellationToken);

            if (comentarios == null || !comentarios.Any())
                return Enumerable.Empty<GetComentarioResult>();

            var result = _mapper.Map<IEnumerable<GetComentarioResult>>(comentarios);
            return result;
        }
    }
}
