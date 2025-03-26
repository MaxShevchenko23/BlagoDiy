namespace BlagoDiy.DataAccessLayer.Entites;

public interface IEntity
{
    public DateTime CreatedAt { get; set; }

    public void SetCreatedAt()
    {
        CreatedAt = DateTime.Now;
    }
}