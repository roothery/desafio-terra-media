using AutoMapper;
using Desafio.TerraMedia.Domain.Entities;

namespace Desafio.TerraMedia.Application.Comentarios.UpdateComentario
{
    public class UpdateComentarioProfile : Profile
    {
        public UpdateComentarioProfile()
        {
            CreateMap<UpdateComentarioCommand, Comentario>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Comentario, UpdateComentarioResult>();
        }
    }
}
