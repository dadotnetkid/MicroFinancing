namespace MicroFinancing.Entities;

public class RefreshToken : BaseEntity<long>
{
    public string UserId { get; set; }
    public string Token { get; set; }
    public DateTimeOffset Expires { get; set; }
    public ApplicationUser User { get; set; }
}
