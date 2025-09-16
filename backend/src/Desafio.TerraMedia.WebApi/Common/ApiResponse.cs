using Desafio.TerraMedia.Common.Validation;

namespace Desafio.TerraMedia.WebApi.Common;

public class ApiResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public IEnumerable<ValidationErrorDetail> Errors { get; set; } = [];
}
