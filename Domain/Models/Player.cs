using Domain.Models.Common;
using Domain.Models.Enum;

namespace Domain.Models;

public class Player : Entity
{
    public required string Name { get; set; }
    public required Position Position { get; set; }
    public Guid TeamId { get; set; }
}