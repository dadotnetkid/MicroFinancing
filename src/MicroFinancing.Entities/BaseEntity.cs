namespace MicroFinancing.Entities;

public interface IBaseEntity<TKey>
{
    TKey Id { get; set; }
    DateTimeOffset CreatedAt { get; set; }
    DateTimeOffset? UpdateAt { get; set; }
    DateTimeOffset? DeletionAt { get; set; }
    bool IsDeleted { get; set; }
    string DeleterUserId { get; set; }
    string LastModifierUserId { get; set; }
    string CreatorUserId { get; set; }
    ApplicationUser Creator { get; set; }
    ApplicationUser LastModifier { get; set; }
    ApplicationUser DeleterUser { get; set; }
}

public class BaseEntity<TKey> : IBaseEntity<TKey>
{
    public virtual TKey Id { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdateAt { get; set; }
    public DateTimeOffset? DeletionAt { get; set; }

    public bool IsDeleted { get; set; }

    public string? DeleterUserId { get; set; }
    public string? LastModifierUserId { get; set; }
    public string CreatorUserId { get; set; }

    public ApplicationUser Creator { get; set; }
    public ApplicationUser LastModifier { get; set; }
    public ApplicationUser DeleterUser { get; set; }
}