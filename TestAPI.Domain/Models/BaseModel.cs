using TestAPI.Domain.Interfaces;

namespace TestAPI.Domain.Models;

public abstract class BaseModel : IBaseModel
{
    public int Id { get; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    protected BaseModel()
    {
    }
}