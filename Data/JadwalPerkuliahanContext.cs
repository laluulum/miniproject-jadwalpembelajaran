using Mandiri.MiniProject.JadwalPembelajaran.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Mandiri.MiniProject.JadwalPembelajaran.API.Data;

public class JadwalPerkuliahanContext : DbContext
{
    public JadwalPerkuliahanContext(DbContextOptions<JadwalPerkuliahanContext> options)
        : base(options)
    {

    }

    public DbSet<DosenDTO> DosenDTOs => Set<DosenDTO>();
    public DbSet<MahasiswaDTO> MahasiswaDTOs => Set<MahasiswaDTO>();
    public DbSet<KelasDTO> KelasDTOs => Set<KelasDTO>();
    public DbSet<RuanganDTO> RuanganDTOs => Set<RuanganDTO>();
    public DbSet<GedungDTO> GedungDTOs => Set<GedungDTO>();
    public DbSet<MataKuliahDTO> MataKuliahDTOs => Set<MataKuliahDTO>();
    public DbSet<JadwalDTO> JadwalDTOs => Set<JadwalDTO>();
}