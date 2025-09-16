using Desafio.TerraMedia.Application.Users.CreateUser;
using Desafio.TerraMedia.WebApi.Features.Users.CreateUser;
using AutoMapper;

namespace Desafio.TerraMedia.WebApi.Mappings;

public class CreateUserRequestProfile : Profile
{
    public CreateUserRequestProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
    }
}