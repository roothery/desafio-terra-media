using Bogus;
using Desafio.TerraMedia.Application.Comentarios.GetComentario;
using Desafio.TerraMedia.Domain.Entities;

namespace Desafio.TerraMedia.Unit.Application.TestData
{
    public static class GetComentarioHandlerTestData
    {
        private static readonly Faker<GetComentarioCommand> _faker = new Faker<GetComentarioCommand>()
            .RuleFor(c => c.UserId, f => f.Random.Guid())
            .RuleFor(c => c.LivroId, f => f.Random.AlphaNumeric(10));

        public static GetComentarioCommand GenerateValidCommand()
        {
            return _faker.Generate();
        }

        public static List<Comentario> GenerateValidFakeComentarios(int quantidade = 5)
        {
            var comentarioFaker = new Faker<Comentario>()
                .RuleFor(c => c.UserId, f => f.Random.Guid())
                .RuleFor(c => c.LivroId, f => f.Random.AlphaNumeric(10))
                .RuleFor(c => c.Texto, f => f.Lorem.Text().PadRight(200, ' '))
                .RuleFor(c => c.DataComentario, f => DateTime.UtcNow.AddHours(-1));

            return comentarioFaker.Generate(quantidade);
        }
    }
}
