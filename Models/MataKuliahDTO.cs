using System.ComponentModel.DataAnnotations;

namespace Mandiri.MiniProject.JadwalPembelajaran.API.Models;

public class MataKuliahDTO
{
    public int Id { get; set; }

    [Required]
    [MaxLength(32)]
    public string? Name { get; set; }

    [Required]
    [Range(1, 8, ErrorMessage = "SKS tidak valid")]
    public int Sks { get; set; }
}