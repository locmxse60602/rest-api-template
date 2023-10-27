namespace Domain;

public class AuditLog
{
    public int Id { get; set; }
    public string EntityName { get; set; }
    public string Action { get; set; }

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    public string? ModifiedByUserId { get; set; }
    
    public string? ModifiedByUsername { get; set; }
    public DateTime TimeStamp { get; set; }
}