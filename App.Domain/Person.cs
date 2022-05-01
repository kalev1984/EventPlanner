using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Person : DomainEntityId
{
    [MaxLength(50)]
    public string FirstName { get; set; } = default!;

    [MaxLength(50)]
    public string LastName { get; set; } = default!;

    [Range(10000000000, 100000000000)]
    public long PersonalId { get; set; }

    [MaxLength(12), MinLength(8)]
    public string Payment { get; set; } = default!;

    [MaxLength(1500)]
    public string? Comment { get; set; }

    public int EventId { get; set; }
    public Event? Event { get; set; }
}