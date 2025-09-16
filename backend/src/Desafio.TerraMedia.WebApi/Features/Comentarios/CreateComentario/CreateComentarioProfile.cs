using AutoMapper;
using Desafio.TerraMedia.Application.Comentarios.CreateComentario;

namespace Desafio.TerraMedia.WebApi.Features.Comentarios.CreateComentario
{
    public class CreateComentarioProfile : Profile
    {
        public CreateComentarioProfile()
        {
            CreateMap<CreateComentarioRequest, CreateComentarioCommand>();
            CreateMap<CreateComentarioResult, CreateComentarioResponse>();
        }
    }
}
