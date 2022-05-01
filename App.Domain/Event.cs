using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Event : DomainEntityId
{
    [MaxLength(50)]
    public string EventName { get; set; } = default!;
    
    public DateTime Time { get; set; }

    [MaxLength(50)]
    public string Place { get; set; } = default!;

    [MaxLength(1000)]
    public string? Comments { get; set; }

    public ICollection<Company> Companies { get; set; } = new List<Company>();

    public ICollection<Person> Persons { get; set; } = new List<Person>();
}