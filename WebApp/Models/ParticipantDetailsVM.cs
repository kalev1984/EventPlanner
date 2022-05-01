using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models;

public class ParticipantDetailsVm
{
    public int Id { get; set; }
    
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
    public string Type { get; set; } = default!;

    public int EventId { get; set; }
}