using System.ComponentModel.DataAnnotations;
using Domain.Features.Matches.Add;
using Domain.Features.Matches.ComputeStatistics;
using Domain.Features.Matches.Delete;
using Domain.Features.Matches.Get;
using Domain.Features.Matches.Update;
using FootBallStatsApi.Controllers.DTOs.MatchDtos;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FootBallStatsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private IMediator _mediator;

        public MatchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMatch([FromRoute] Guid id)
        {
            try
            {
                var result = await _mediator.Send(new GetMatchQuery { Id = id });
                return Ok(new GetMatchDto
                {
                    Id = result.Match.Id,
                    HomeTeamId = result.Match.HomeTeamId,
                    AwayTeamId = result.Match.AwayTeamId,
                    TotalPasses = result.Match.TotalPasses
                });
            }
            catch (ValidationException e)
            {
                return NotFound(e);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMatch([FromBody] CreateMatchDto dto)
        {
            try
            {
                var result = await _mediator.Send(new AddMatchRequest
                {
                    HomeTeamId = dto.HomeTeamId,
                    AwayTeamId = dto.AwayTeamId,
                });
                return Ok(new MatchCreatedDto
                {
                    Id = result.Id
                });
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
        }
        
        
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMatch([FromBody] UpdateMatchDto dto)
        {
            try
            {
                await _mediator.Send(new UpdateMatchRequest
                {
                    Id = dto.Id,
                    HomeTeamId = dto.HomeTeamId,
                    AwayTeamId = dto.AwayTeamId,
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
        public async Task<IActionResult> DeleteMatch([FromRoute] Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteMatchRequest
                {
                    Id = id
                });
                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
        }
    }
}
