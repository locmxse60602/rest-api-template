namespace Domain;

public record AuditLogValue
{
    public AuditLogValue(string name, string value)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; set; }
    public string Value { get; set; }
}

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