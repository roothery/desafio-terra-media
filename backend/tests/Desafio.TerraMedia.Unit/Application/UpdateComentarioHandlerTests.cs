using AutoMapper;
using Desafio.TerraMedia.Application.Comentarios.UpdateComentario;
using Desafio.TerraMedia.Domain.Entities;
using Desafio.TerraMedia.Domain.Repositories;
using Desafio.TerraMedia.Unit.Application.TestData;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Desafio.TerraMedia.Unit.Application
{
    public class UpdateComentarioHandlerTests
    {
        private readonly IComentarioRepository _comentarioRepository;
        private readonly IMapper _mapper;
        private readonly UpdateComentarioHandler _handler;

        public UpdateComentarioHandlerTests()
        {
            _comentarioRepository = Substitute.For<IComentarioRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new UpdateComentarioHandler(_comentarioRepository, _mapper);
        }

        [Fact(DisplayName = "Given valid comentario data When updating comentario Then returns updated comentario ID")]
        public async Task Handle_ValidCommand_ReturnsUpdatedComentarioId()
        {
            // Given
            var command = UpdateComentarioHandlerTestData.GenerateValidCommand();
            var existingComentario = new Comentario { Id = command.Id, Texto = command.Texto };
            var result = new UpdateComentarioResult { Id = command.Id };

            _comentarioRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(existingComentario);
            _mapper.Map(command, existingComentario).Returns(existingComentario);
            _mapper.Map<UpdateComentarioResult>(existingComentario).Returns(result);

            // When
            var updateResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            updateResult.Should().NotBeNull();
            updateResult.Id.Should().Be(command.Id);
            await _comentarioRepository.Received(1).UpdateAsync(existingComentario, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given invalid comentario data When updating comentario Then throws validation exception")]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Given
            var command = new UpdateComentarioCommand();

            // When
            var act = async () => await _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact(DisplayName = "Given non-existing comentario ID When updating comentario Then throws invalid operation exception")]
        public async Task Handle_NonExistingComentario_ThrowsInvalidOperationException()
        {
            // Given
            var command = UpdateComentarioHandlerTestData.GenerateValidCommand();

            _comentarioRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                .Returns((Comentario)null);

            // When
            var act = async () => await _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage($"Comentario with Id {command.Id} not found.");
        }
    }
}
