using Mandiri.MiniProject.JadwalPembelajaran.API.Data;
using Mandiri.MiniProject.JadwalPembelajaran.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mandiri.MiniProject.JadwalPembelajaran.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JadwalController : ControllerBase
{
    private readonly JadwalPerkuliahanContext _context;

    public JadwalController(JadwalPerkuliahanContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<JadwalDTO> Get()
    {
        var rawData = _context.JadwalDTOs.FromSqlRaw(@"
            SELECT j.Id, j.IdDosen, j.DosenId, j.IdRuangan, j.RuanganId, j.IdKelas, j.KelasId, j.IdMataKuliah, j.MataKuliahId, j.Day, j.Time FROM JadwalDTOs j
            JOIN DosenDTOs d ON j.DosenId = d.Id
            JOIN RuanganDTOs r ON j.RuanganId = r.Id
            JOIN KelasDTOs k ON j.KelasId = k.Id
            JOIN MataKuliahDTOs mk ON j.MataKuliahId = mk.Id")
            .Include(j => j.Dosen)
            .Include(j => j.Ruangan)
            .Include(j => j.Kelas)
            .Include(j => j.MataKuliah);

        var jadwals = rawData?.ToList();

        if (jadwals == null)
        {
            return new List<JadwalDTO>();
        }

        return jadwals;
    }

    [HttpGet("{id}")]
    public ActionResult<JadwalDTO> GetById(int id)
    {
        var rawData = _context.JadwalDTOs.FromSqlRaw(@"
            SELECT j.Id, j.IdDosen, j.DosenId, j.IdRuangan, j.RuanganId, j.IdKelas, j.KelasId, j.IdMataKuliah, j.MataKuliahId, j.Day, j.Time FROM JadwalDTOs j
            JOIN DosenDTOs d ON j.DosenId = d.Id
            JOIN RuanganDTOs r ON j.RuanganId = r.Id
            JOIN KelasDTOs k ON j.KelasId = k.Id
            JOIN MataKuliahDTOs mk ON j.MataKuliahId = mk.Id
            WHERE j.Id = " + id)
            .Include(j => j.Dosen)
            .Include(j => j.Ruangan)
            .Include(j => j.Kelas)
            .Include(j => j.MataKuliah);

        
        var jadwal = rawData?.FirstOrDefault();

        if (jadwal == null)
        {
            return NotFound();
        }

        return jadwal;
    }

    [HttpPost]
    public IActionResult Create(JadwalDTO newJadwal)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        using var transaction = _context.Database.BeginTransaction();
        int rowsAffected = _context.Database
            .ExecuteSqlInterpolated($"INSERT INTO JadwalDTOs (IdDosen, DosenId, IdRuangan, RuanganId, IdKelas, KelasId, IdMataKuliah, MataKuliahId, Day, Time) VALUES ({newJadwal.IdDosen}, {newJadwal.IdDosen}, {newJadwal.IdRuangan}, {newJadwal.IdRuangan}, {newJadwal.IdKelas}, {newJadwal.IdKelas}, {newJadwal.IdMataKuliah}, {newJadwal.IdMataKuliah}, {newJadwal.Day}, {newJadwal.Time})");

        if (rowsAffected > 0)
        {
            transaction.Commit();
            return CreatedAtAction(nameof(GetById), new { id = newJadwal.Id }, newJadwal);
        }
        else
        {
            transaction.Rollback();
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, JadwalDTO updatedJadwal)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        using var transaction = _context.Database.BeginTransaction();
        int rowsAffected = _context.Database.ExecuteSqlInterpolated($"UPDATE DosenDTOs SET JadwalDTOs = {updatedJadwal.IdDosen}, DosenId = {updatedJadwal.IdDosen}, IdRuangan = {updatedJadwal.IdRuangan}, RuanganId = {updatedJadwal.IdRuangan}, IdKelas = {updatedJadwal.IdKelas}, KelasId = {updatedJadwal.IdKelas}, IdMataKuliah = {updatedJadwal.IdMataKuliah}, MataKuliahId = {updatedJadwal.IdMataKuliah}, Day = {updatedJadwal.Day}, Time = {updatedJadwal.Time} WHERE id = {id}");

        if (rowsAffected == 0)
        {
            transaction.Rollback();
            return BadRequest();
        }

        transaction.Commit();

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        using var transaction = _context.Database.BeginTransaction();
        int rowsAffected = _context.Database.ExecuteSqlInterpolated($"DELETE FROM JadwalDTOs WHERE id = {id}");

        if (rowsAffected == 0)
        {
            transaction.Rollback();
            return BadRequest();
        }

        transaction.Commit();

        return Ok();
    }
}