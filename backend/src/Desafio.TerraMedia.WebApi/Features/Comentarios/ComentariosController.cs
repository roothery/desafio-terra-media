using AutoMapper;
using Desafio.TerraMedia.Application.Comentarios.CreateComentario;
using Desafio.TerraMedia.Application.Comentarios.DeleteComentario;
using Desafio.TerraMedia.Application.Comentarios.GetComentario;
using Desafio.TerraMedia.Application.Comentarios.UpdateComentario;
using Desafio.TerraMedia.WebApi.Common;
using Desafio.TerraMedia.WebApi.Features.Comentarios.CreateComentario;
using Desafio.TerraMedia.WebApi.Features.Comentarios.DeleteComentario;
using Desafio.TerraMedia.WebApi.Features.Comentarios.GetComentario;
using Desafio.TerraMedia.WebApi.Features.Comentarios.UpdateComentario;
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

        [HttpGet("{userId}/{livroId}")]
        [ProducesResponseType(typeof(ApiResponseWithData<IEnumerable<GetComentarioResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetComentarios([FromRoute] Guid userId, [FromRoute] string livroId, CancellationToken cancellationToken)
        {
            if (userId == Guid.Empty || string.IsNullOrWhiteSpace(livroId))
                return BadRequest(new ApiResponse { Success = false, Message = "Invalid parameters." });

            var command = new GetComentarioCommand(userId, livroId);
            var comentarios = await _mediator.Send(command, cancellationToken);

            if (comentarios == null || !comentarios.Any())
                return NotFound(new ApiResponse { Success = false, Message = "Comentários not found." });

            return Ok(new ApiResponseWithData<IEnumerable<GetComentarioResponse>>
            {
                Success = true,
                Message = "Comentários retrieved successfully.",
                Data = _mapper.Map<IEnumerable<GetComentarioResponse>>(comentarios)
            });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateComentarioResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateComentario([FromRoute] Guid id, [FromBody] UpdateComentarioRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateComentarioRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateComentarioCommand>(request);
            command.Id = id;

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponseWithData<UpdateComentarioResponse>
            {
                Success = true,
                Message = "Comentário updated successfully.",
                Data = _mapper.Map<UpdateComentarioResponse>(result)
            });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteComentario([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteComentarioRequest { Id = id };

            var validator = new DeleteComentarioRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<DeleteComentarioCommand>(request);
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
