using Microsoft.AspNetCore.Builder;

namespace Desafio.TerraMedia.IoC;

public interface IModuleInitializer
{
    void Initialize(WebApplicationBuilder builder);
}
