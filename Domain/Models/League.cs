using Domain.Models.Common;

namespace Domain.Models;

public class League : Entity
{
    public required string Name { get; set; }
    public List<Team> Teams { get; set; } = [];
}