using Desafio.TerraMedia.Domain.Entities;

namespace Desafio.TerraMedia.Domain.Events
{
    public class UserRegisteredEvent
    {
        public User User { get; }

        public UserRegisteredEvent(User user)
        {
            User = user;
        }
    }
}
