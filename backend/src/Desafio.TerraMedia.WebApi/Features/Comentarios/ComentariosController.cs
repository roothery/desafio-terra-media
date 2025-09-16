using AutoMapper;
using Desafio.TerraMedia.Application.Comentarios.CreateComentario;
using Desafio.TerraMedia.WebApi.Common;
using Desafio.TerraMedia.WebApi.Features.Comentarios.CreateComentario;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.TerraMedia.WebApi.Features.Comentarios
{
    public class ComentariosController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ComentariosController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateComentarioResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateComentario(
            [FromBody] CreateComentarioRequest request,
            CancellationToken cancellationToken)
        {
            var validator = new CreateComentarioRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateComentarioCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateComentarioResponse>
            {
                Success = true,
                Message = "Comentário created successfully",
                Data = _mapper.Map<CreateComentarioResponse>(response)
            });
        }
    }
}
