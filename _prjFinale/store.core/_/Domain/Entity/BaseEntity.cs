namespace store.core._.Domain.Entity;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

    protected BaseEntity() { }
    public BaseEntity(int id, bool isDeleted, DateTime createdAt, DateTime modifiedAt) { 
        Id = id; IsDeleted = isDeleted; CreatedAt = createdAt; ModifiedAt = modifiedAt;
    }
}