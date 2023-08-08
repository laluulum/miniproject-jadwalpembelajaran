using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mandiri.MiniProject.JadwalPembelajaran.API.Models;

public class GedungDTO
{
    public int Id { get; set; }

    [Required]
    [MaxLength(24)]
    public string? Name { get; set; }

    public string? LocationDescription { get; set; }

    public ICollection<RuanganDTO>? Ruangans { get; set; }
}