using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class ParticipantVM
{
    public int Id { get; set; }
    
    [MaxLength(50)]
    public string Name { get; set; } = default!;

    [MaxLength(50)]
    public string Identifier { get; set; } = default!;

    [MaxLength(50)]
    public string Type { get; set; } = default!;

    public int EventId { get; set; }
}