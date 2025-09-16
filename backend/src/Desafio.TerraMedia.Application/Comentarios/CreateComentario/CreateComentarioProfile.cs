using AutoMapper;
using Desafio.TerraMedia.Domain.Entities;

namespace Desafio.TerraMedia.Application.Comentarios.CreateComentario
{
    public class CreateComentarioProfile : Profile
    {
        public CreateComentarioProfile()
        {
            CreateMap<CreateComentarioCommand, Comentario>();
            CreateMap<Comentario, CreateComentarioResult>();
        }
    }
}
