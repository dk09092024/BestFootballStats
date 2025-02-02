using Domain.Features.Matches.ComputeStatistics;
using Domain.Models;
using Domain.Repositories;
using FluentValidation.TestHelper;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Exceptions;
using Moq;
using Match = System.Text.RegularExpressions.Match;

namespace Domain.Test.Features.Matches.ComputeStatstics;
[TestFixture]
public class ComputeStatisticsValidatorTests
{
    private ComputeStatisticsValidator _validator;
    private Mock<IMatchRepository> _matchRepository;

    [SetUp]
    public void SetUp()
    {
        _matchRepository = new Mock<IMatchRepository>();
        _validator = new ComputeStatisticsValidator(_matchRepository.Object);
    }

    [Test]
    public async Task Should_ReturnError_When_MatchId_Is_Empty()
    {
        var request = new ComputeStatisticsRequest
        {
            MatchId = Guid.Empty
        };

        var result = await _validator.TestValidateAsync(request);

        result.ShouldHaveValidationErrorFor(x => x.MatchId);
    }
    
    [Test]
    public async Task Should_ReturnError_When_Match_Not_Exist()
    {
        var request = new ComputeStatisticsRequest
        {
            MatchId = Guid.NewGuid()
        };
        _matchRepository.Setup(x => x.ExistsAsync(request.MatchId, default))
            .ReturnsAsync(false);

        var result = await _validator.TestValidateAsync(request);
        _matchRepository.Verify(x => x.ExistsAsync(request.MatchId, default), Times.Once);
        result.ShouldHaveValidationErrorFor(x => x.MatchId);
    }
    
    [Test]
    public async Task Should_Not_ReturnError_When_Match_Exist()
    {
        var request = new ComputeStatisticsRequest
        {
            MatchId = Guid.NewGuid()
        };
        _matchRepository.Setup(x => x.ExistsAsync(request.MatchId, default))
            .ReturnsAsync(true);

        var result = await _validator.TestValidateAsync(request);
        _matchRepository.Verify(x => x.ExistsAsync(request.MatchId, default), Times.Once);
        result.ShouldNotHaveValidationErrorFor(x => x.MatchId);
    }
    
}