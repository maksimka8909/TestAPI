using TestAPI.Domain.Interfaces;

namespace TestAPI.Domain.Models;

public abstract class BaseModel : IBaseModel
{
    public int Id { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    public DateTime? DeletedAt { get; private set; }

    public void SetCreateDate()
    {
        CreatedAt = DateTime.Now;
    }

    public void SetUpdateDate()
    {
        UpdatedAt = DateTime.Now;
    }

    public void SetDeleteDate()
    {
        DeletedAt = DateTime.Now;
    }
}