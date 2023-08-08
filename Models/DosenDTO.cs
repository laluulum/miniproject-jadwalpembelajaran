using System.ComponentModel.DataAnnotations;

namespace Mandiri.MiniProject.JadwalPembelajaran.API.Models;

public class DosenDTO
{
    public int Id { get; set; }

    [Required]
    public string? Nip { get; set; }

    [Required]
    [MaxLength(160)]
    public string? Name { get; set; }

    public ICollection<JadwalDTO>? JadwalMengajars { get; set; }
}