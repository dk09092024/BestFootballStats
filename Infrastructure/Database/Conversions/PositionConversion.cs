using Domain.Models.Enum;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Database.Conversions;

public class PositionConversion : ValueConverter<Position, string>
{
    public PositionConversion()
        : base(
            domainPosition => ToDatabase(domainPosition),
            dbPosition => ToDomain(dbPosition))
    {
        
    }
    
    private static string ToDatabase(Position position)
    {
        return position.ToString();
    }
    private static Position ToDomain(string position)
    {
        if (!Enum.TryParse<Position>(position, out var domainPosition))
        {
            throw new InvalidOperationException($"The value {position} is not a valid position.");
        }
        return domainPosition;
    }
}