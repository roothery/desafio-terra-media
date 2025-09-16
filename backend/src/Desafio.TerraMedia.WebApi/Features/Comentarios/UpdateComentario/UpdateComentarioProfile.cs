using AutoMapper;
using Desafio.TerraMedia.Application.Comentarios.UpdateComentario;

namespace Desafio.TerraMedia.WebApi.Features.Comentarios.UpdateComentario
{
    public class UpdateComentarioProfile : Profile
    {
        public UpdateComentarioProfile()
        {
            CreateMap<UpdateComentarioRequest, UpdateComentarioCommand>();
            CreateMap<UpdateComentarioResult, UpdateComentarioResponse>();
        }
    }
}
