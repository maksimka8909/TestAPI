namespace TestAPI.Domain.Interfaces;

public interface IBaseModel
{
    public int Id { get; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}