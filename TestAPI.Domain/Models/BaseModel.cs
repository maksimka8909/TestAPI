namespace TestAPI.Domain.Models;

public abstract class BaseModel
{
    public int Id { get; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}