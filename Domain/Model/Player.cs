using Domain.Model.Common;

namespace Domain.Model;

public class Player : Entity
{
    public required string Name { get; set; }
    public required Position Position { get; set; }
    public required Guid TeamId { get; set; }
}