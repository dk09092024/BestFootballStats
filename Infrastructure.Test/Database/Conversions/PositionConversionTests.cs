using Domain.Models.Enum;
using Infrastructure.Database.Conversions;

namespace Infrastructure.Test.Database.Conversions;
[TestFixture]
public class PositionConversionTests
{
    private PositionConversion _positionConversion;
    
    [SetUp]
    public void SetUp()
    {
        _positionConversion = new PositionConversion();
    }
    
    [Test]
    public void ToDatabase_ShouldReturnString()
    {
        var result = _positionConversion.ConvertToProvider(Position.Goalkeeper);
        Assert.That(result, Is.EqualTo("Goalkeeper"));
    }
    
    [Test]
    public void ToDomain_ShouldReturnPosition()
    {
        var result = _positionConversion.ConvertFromProvider("Goalkeeper");
        Assert.That(result, Is.EqualTo(Position.Goalkeeper));
    }
}