using Domain.Model.Common;

namespace Domain.Model;

public class League : Entity
{
    public required string Name { get; set; }
    public required List<Team> Teams { get; set; }
}