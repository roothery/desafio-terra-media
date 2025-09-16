using AutoMapper;
using Desafio.TerraMedia.Application.Comentarios.GetComentario;
using Desafio.TerraMedia.Domain.Entities;
using Desafio.TerraMedia.Domain.Repositories;
using Desafio.TerraMedia.Unit.Application.TestData;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Desafio.TerraMedia.Unit.Application
{
    public class GetComentarioHandlerTests
    {
        private readonly IComentarioRepository _comentarioRepository;
        private readonly IMapper _mapper;
        private readonly GetComentarioHandler _handler;

        public GetComentarioHandlerTests()
        {
            _comentarioRepository = Substitute.For<IComentarioRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetComentarioHandler(_comentarioRepository, _mapper);
        }

        [Fact(DisplayName = "Given valid comentario data When getting comentarios Then returns comentarios list")]
        public async Task Handle_ValidCommand_ReturnsComentariosList()
        {
            // Given
            var command = GetComentarioHandlerTestData.GenerateValidCommand();
            var comentarios = GetComentarioHandlerTestData.GenerateValidFakeComentarios();

            var comentarioResults = comentarios.Select(c => new GetComentarioResult { Id = c.Id, Texto = c.Texto }).ToList();
            _comentarioRepository.GetByUserAndLivroAsync(command.UserId, command.LivroId, Arg.Any<CancellationToken>())
                .Returns(comentarios);

            _mapper.Map<IEnumerable<GetComentarioResult>>(comentarios).Returns(comentarioResults);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Should().HaveCount(comentarios.Count);
            result.First().Id.Should().Be(comentarios.First().Id);
            await _comentarioRepository.Received(1).GetByUserAndLivroAsync(command.UserId, command.LivroId, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given invalid comentario data When getting comentarios Then throws validation exception")]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Given
            var command = new GetComentarioCommand();

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact(DisplayName = "Given no comentarios found When getting comentarios Then returns empty list")]
        public async Task Handle_NoComentariosFound_ReturnsEmptyList()
        {
            // Given
            var command = GetComentarioHandlerTestData.GenerateValidCommand();

            _comentarioRepository.GetByUserAndLivroAsync(command.UserId, command.LivroId, Arg.Any<CancellationToken>())
                .Returns(Enumerable.Empty<Comentario>());

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Should().BeEmpty();
            await _comentarioRepository.Received(1).GetByUserAndLivroAsync(command.UserId, command.LivroId, Arg.Any<CancellationToken>());
        }
    }
}
