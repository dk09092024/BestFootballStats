namespace Domain.Models.Common;

public abstract class Entity
{
    public required Guid Id { get; init; } = Guid.NewGuid();
    public required DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}