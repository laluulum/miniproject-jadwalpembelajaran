using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Mandiri.MiniProject.JadwalPembelajaran.API.Models;

public class JadwalDTO
{
    public int Id { get; set; }

    [Required]
    public int IdDosen { get; set; }

    [Required]
    public int IdRuangan { get; set; }

    [Required]
    public int IdKelas { get; set; }

    [Required]
    public int IdMataKuliah { get; set; }

    [Required]
    public DayOfWeek Day { get; set; }

    [NotMapped]
    public string? DayName { get; set; }

    [Required]
    [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Jam tidak valid. Gunakan format jj:mm")]
    public string? Time { get; set; }

    public DosenDTO? Dosen { get; set; }

    public RuanganDTO? Ruangan { get; set; }

    public KelasDTO? Kelas { get; set; }

    public MataKuliahDTO? MataKuliah { get; set; }

    public enum DayOfWeek
    {
        Senin,
        Selasa,
        Rabu,
        Kamis,
        Jumat,
        Sabtu,
        Minggu
    }
}