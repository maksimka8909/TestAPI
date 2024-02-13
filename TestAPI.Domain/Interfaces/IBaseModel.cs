namespace TestAPI.Domain.Interfaces;

public interface IBaseModel
{
    public int Id { get; }

    public DateTime? CreatedAt { get; }

    public DateTime? UpdatedAt { get; }

    public DateTime? DeletedAt { get; }

    public void SetCreateDate();

    public void SetUpdateDate();

    public void SetDeleteDate();
}