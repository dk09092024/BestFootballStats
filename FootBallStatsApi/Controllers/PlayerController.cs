using Domain.Features.Players.Add;
using Domain.Features.Players.Delete;
using Domain.Features.Players.Get;
using Domain.Features.Players.Update;
using Domain.Models.Enum;
using FluentValidation;
using FootBallStatsApi.Controllers.DTOs.PlayerDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootBallStatsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private IMediator _mediator;

        public PlayerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPlayer([FromRoute] Guid id)
        {
            try
            {
                var result = await _mediator.Send(new GetPlayerQuery { Id = id });
                return Ok(new GetPlayerDto()
                {
                    Id = result.Player.Id,
                    Name = result.Player.Name,
                    Position = result.Player.Position,
                    TeamId = result.Player.TeamId
                });
            }
            catch (Exception e)
            {
                return NotFound();
            }
            
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePlayer([FromBody] CreatePlayerDto dto)
        {
            try
            {
                var result = await _mediator.Send(new AddPlayerRequest
                {
                    Name = dto.Name,
                    Position = dto.Position
                });
                return Ok(new PlayerCreatedDto()
                {
                    Id = result.Id
                });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            
        }
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePlayer([FromBody] UpdatePlayerDto dto)
        {
            try
            {
                await _mediator.Send(new UpdatePlayerRequest
                {
                    Id = dto.id,
                    Name = dto.Name,
                    Position = (Position)dto.Position
                });
                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePlayer([FromRoute] Guid id)
        {
            try
            {
                await _mediator.Send(new DeletePlayerRequest(id));
                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
        }
    }
}
