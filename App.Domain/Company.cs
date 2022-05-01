using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Company : DomainEntityId
{
    [MaxLength(50)]
    public string Name { get; set; } = default!;

    [MaxLength(20)]
    public string RegistryCode { get; set; } = default!;

    public int NumberOfParticipants { get; set; }
    
    [MaxLength(12), MinLength(8)]
    public string Payment { get; set; } = default!;

    [MaxLength(5000)]
    public string? Comment { get; set; }
    
    public int EventId { get; set; }
    public Event? Event { get; set; }
}