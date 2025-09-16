using Desafio.TerraMedia.Application.Comentarios.DeleteComentario;
using Desafio.TerraMedia.Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Desafio.TerraMedia.Unit.Application
{
    public class DeleteComentarioHandlerTests
    {
        private readonly IComentarioRepository _comentarioRepository;
        private readonly DeleteComentarioHandler _handler;

        public DeleteComentarioHandlerTests()
        {
            _comentarioRepository = Substitute.For<IComentarioRepository>();
            _handler = new DeleteComentarioHandler(_comentarioRepository);
        }

        [Fact(DisplayName = "Given valid comentario id When deleting comentario Then operation is successful")]
        public async Task Handle_ValidId_DeletesSale()
        {
            // Given
            var comentarioId = Guid.NewGuid();
            _comentarioRepository.DeleteAsync(comentarioId, Arg.Any<CancellationToken>()).Returns(Task.FromResult(true));

            // When
            var result = await _handler.Handle(new DeleteComentarioCommand { Id = comentarioId }, CancellationToken.None);

            // Then
            result.Sucesso.Should().BeTrue();
            await _comentarioRepository.Received(1).DeleteAsync(comentarioId, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given invalid comentario id When deleting comentario Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Given
            var command = new DeleteComentarioCommand();

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact(DisplayName = "Given non-existing comentario id When deleting comentario Then throws KeyNotFoundException")]
        public async Task Handle_NonExistingId_ThrowsKeyNotFoundException()
        {
            // Given
            var comentarioId = Guid.NewGuid();
            _comentarioRepository.DeleteAsync(comentarioId, Arg.Any<CancellationToken>()).Returns(Task.FromResult(false));

            // When
            var act = () => _handler.Handle(new DeleteComentarioCommand { Id = comentarioId }, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}
