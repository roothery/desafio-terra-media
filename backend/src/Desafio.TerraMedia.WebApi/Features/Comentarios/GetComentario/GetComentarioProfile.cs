using AutoMapper;
using Desafio.TerraMedia.Application.Comentarios.GetComentario;

namespace Desafio.TerraMedia.WebApi.Features.Comentarios.GetComentario
{
    public class GetComentarioProfile : Profile
    {
        public GetComentarioProfile()
        {
            CreateMap<GetComentarioRequest, GetComentarioCommand>();
            CreateMap<GetComentarioResult, GetComentarioResponse>();
        }
    }
}
