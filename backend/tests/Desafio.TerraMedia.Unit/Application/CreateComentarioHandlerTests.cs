using AutoMapper;
using Desafio.TerraMedia.Application.Comentarios.CreateComentario;
using Desafio.TerraMedia.Domain.Entities;
using Desafio.TerraMedia.Domain.Repositories;
using Desafio.TerraMedia.Unit.Application.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Desafio.TerraMedia.Unit.Application
{
    public class CreateComentarioHandlerTests
    {
        private readonly IComentarioRepository _comentarioRepository;
        private readonly IMapper _mapper;
        private readonly CreateComentarioHandler _handler;

        public CreateComentarioHandlerTests()
        {
            _comentarioRepository = Substitute.For<IComentarioRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new CreateComentarioHandler(_comentarioRepository, _mapper);
        }

        [Fact(DisplayName = "Given valid comentario data When creating comentario Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = CreateComentarioHandlerTestData.GenerateValidCommand();
            var comentario = CreateComentarioFromCommand(command);
            var result = new CreateComentarioResult { Id = comentario.Id };

            _mapper.Map<Comentario>(command).Returns(comentario);
            _mapper.Map<CreateComentarioResult>(comentario).Returns(result);
            _comentarioRepository.CreateAsync(Arg.Any<Comentario>(), Arg.Any<CancellationToken>())
                .Returns(comentario);

            // When
            var createComentarioResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            createComentarioResult.Should().NotBeNull();
            createComentarioResult.Id.Should().Be(comentario.Id);
            await _comentarioRepository.Received(1).CreateAsync(Arg.Any<Comentario>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given invalid comentario data When creating comentario Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Given
            var command = new CreateComentarioCommand();

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        }

        [Fact(DisplayName = "Given user already has 10 comments When creating comentario Then throws InvalidOperationException")]
        public async Task Handle_UserHasMaxComments_ThrowsInvalidOperationException()
        {
            // Given
            var command = CreateComentarioHandlerTestData.GenerateValidCommand();
            var comentarios = new List<Comentario>();

            for (int i = 0; i < 11; i++)
            {
                var comentario = new Comentario
                {
                    Id = command.UserId,
                    UserId = command.UserId,
                    LivroId = command.LivroId,
                    Texto = command.Texto,
                    DataComentario = command.DataComentario,
                };
                comentarios.Add(comentario);
            }

            _comentarioRepository.GetByUserAndLivroAsync(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(comentarios);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("The user has already reached the maximum number of 10 comments per book.");
        }

        [Fact(DisplayName = "Given user has more than 3 comments in the last minute When creating comentario Then throws InvalidOperationException")]
        public async Task Handle_UserHasMaxCommentsInLastMinute_ThrowsInvalidOperationException()
        {
            // Given
            var command = CreateComentarioHandlerTestData.GenerateValidCommand();
            var comentarios = new List<Comentario>();
            var random = new Random();

            for (int i = 0; i < 4; i++)
            {
                int segundosDeDiferenca = random.Next(0, 60);
                var comentario = new Comentario
                {
                    Id = command.UserId,
                    UserId = command.UserId,
                    LivroId = command.LivroId,
                    Texto = command.Texto,
                    DataComentario = DateTime.UtcNow.AddSeconds(-segundosDeDiferenca),
                };
                comentarios.Add(comentario);
            }

            _comentarioRepository.GetByUserAndLivroAsync(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(comentarios);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("The user has already reached the maximum number of 3 comments per minute for this book.");
        }

        private static Comentario CreateComentarioFromCommand(CreateComentarioCommand command)
        {
            return new Comentario
            {
                Id = Guid.NewGuid(),
                UserId = command.UserId,
                LivroId = command.LivroId,
                Texto = command.Texto,
                DataComentario = command.DataComentario,
            };
        }
    }
}
