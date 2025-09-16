using AutoMapper;
using Desafio.TerraMedia.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Desafio.TerraMedia.Application.Comentarios.UpdateComentario
{
    public class UpdateComentarioHandler : IRequestHandler<UpdateComentarioCommand, UpdateComentarioResult>
    {
        private readonly IComentarioRepository _comentarioRepository;
        private readonly IMapper _mapper;

        public UpdateComentarioHandler(IComentarioRepository comentarioRepository, IMapper mapper)
        {
            _comentarioRepository = comentarioRepository;
            _mapper = mapper;
        }

        public async Task<UpdateComentarioResult> Handle(UpdateComentarioCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateComentarioCommandValitor();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingComentario = await _comentarioRepository.GetByIdAsync(command.Id);
            if (existingComentario == null)
                throw new KeyNotFoundException($"Comentario with Id {command.Id} not found.");

            await _comentarioRepository.UpdateAsync(_mapper.Map(command, existingComentario));

            var result = _mapper.Map<UpdateComentarioResult>(existingComentario);
            return result;
        }
    }
}
