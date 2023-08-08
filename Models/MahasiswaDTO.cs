using System.ComponentModel.DataAnnotations;

namespace Mandiri.MiniProject.JadwalPembelajaran.API.Models;

public class MahasiswaDTO
{
    public int Id { get; set; }

    [Required]
    public string? Nim { get; set; }

    [Required]
    [MaxLength(160)]
    public string? Name { get; set; }

    [Required]
    public int IdKelas { get; set; }

    [Required]
    [Range(2000, 2100, ErrorMessage = "Tahun angkatan tidak valid")]
    public int YearOfStudy { get; set; }
    
    public KelasDTO? Kelas { get; set; }

    public ICollection<JadwalDTO>? JadwalPerkuliahan { get; set; }
}