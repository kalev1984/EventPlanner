using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models;

public class EventVM
{
    public int EventId { get; set; }
    
    [MaxLength(50)]
    public string EventName { get; set; } = default!;

    public DateTime Time { get; set; }

    [MaxLength(50)]
    public string Place { get; set; } = default!;

    public ICollection<ParticipantVM> Participants { get; set; } = new List<ParticipantVM>();
    
    [MaxLength(50)]
    public string Name { get; set; } = default!;

    [MaxLength(50)]
    public string Identifier { get; set; } = default!;

    public long Number { get; set; }

    [MaxLength(50)]
    public string Payment { get; set; } = default!;

    [MaxLength(5000)]
    public string? Comment { get; set; }

    [MaxLength(50)]
    public string ClientType { get; set; } = default!;
}