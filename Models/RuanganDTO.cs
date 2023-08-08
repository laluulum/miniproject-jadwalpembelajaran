using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mandiri.MiniProject.JadwalPembelajaran.API.Models;

public class RuanganDTO
{
    public int Id { get; set; }

    [Required]
    [MaxLength(6)]
    public string? Name { get; set; }

    [Required]
    public int IdGedung { get; set; }

    [JsonIgnore]
    public GedungDTO? Gedung { get; set; }
}