namespace SmartEvent.Backend.Core.Interfaces.IModels;

public interface IAuditableModel
{
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}