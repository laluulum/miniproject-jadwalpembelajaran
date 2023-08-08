using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mandiri.MiniProject.JadwalPembelajaran.API.Models;

public class KelasDTO
{
    public int Id { get; set; }

    [Required]
    [MaxLength(18)]
    public string? Name { get; set; }

    [JsonIgnore]
    public ICollection<MahasiswaDTO>? Mahasiswas { get; set; }
}