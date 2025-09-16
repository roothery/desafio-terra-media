using Desafio.TerraMedia.Domain.Entities;
using Desafio.TerraMedia.Domain.Enums;

namespace Desafio.TerraMedia.Domain.Specifications;

public class ActiveUserSpecification : ISpecification<User>
{
    public bool IsSatisfiedBy(User user)
    {
        return user.Status == UserStatus.Active;
    }
}
