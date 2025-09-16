using AutoMapper;
using Desafio.TerraMedia.Domain.Entities;

namespace Desafio.TerraMedia.Application.Comentarios.GetComentario
{
    public class GetComentarioProfile : Profile
    {
        public GetComentarioProfile()
        {
            CreateMap<Comentario, GetComentarioResult>();
        }
    }
}
