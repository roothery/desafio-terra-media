using AutoMapper;
using Desafio.TerraMedia.Application.Comentarios.DeleteComentario;

namespace Desafio.TerraMedia.WebApi.Features.Comentarios.DeleteComentario
{
    public class DeleteComentarioProfile : Profile
    {
        public DeleteComentarioProfile()
        {
            CreateMap<DeleteComentarioRequest, DeleteComentarioCommand>();
        }
    }
}
