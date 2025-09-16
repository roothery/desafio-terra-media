using Bogus;
using Desafio.TerraMedia.Application.Comentarios.CreateComentario;

namespace Desafio.TerraMedia.Unit.Application.TestData
{
    public static class CreateComentarioHandlerTestData
    {
        private static readonly Faker<CreateComentarioCommand> _faker = new Faker<CreateComentarioCommand>()
            .RuleFor(c => c.UserId, f => f.Random.Guid())
            .RuleFor(c => c.LivroId, f => f.Random.Char('A', 'Z') + f.Random.Number(10000000, 99999999).ToString())
            .RuleFor(c => c.Texto, f => f.Lorem.Text().PadRight(200, ' '))
            .RuleFor(c => c.DataComentario, f => DateTime.Now.AddHours(-1));

        public static CreateComentarioCommand GenerateValidCommand()
        {
            return _faker.Generate();
        }
    }
}
