using Mandiri.MiniProject.JadwalPembelajaran.API.Data;
using Mandiri.MiniProject.JadwalPembelajaran.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mandiri.MiniProject.JadwalPembelajaran.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MahasiswaController : ControllerBase
{
    private readonly JadwalPerkuliahanContext _context;

    public MahasiswaController(JadwalPerkuliahanContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<MahasiswaDTO> Get()
    {
        var rawData = _context.MahasiswaDTOs
            .FromSqlRaw("SELECT m.Id, m.Nim, m.Name, m.IdKelas, m.YearOfStudy, m.KelasId FROM MahasiswaDTOs m LEFT JOIN KelasDTOs k ON m.KelasId = k.Id")
            .Include(m => m.Kelas);
        
        var mahasiswas = rawData?.ToList();

        if (mahasiswas == null)
        {
            return new List<MahasiswaDTO>();
        }

        return mahasiswas;
    }

    [HttpGet("{id}")]
    public ActionResult<MahasiswaDTO> GetById(int id)
    {
        var rawData = _context.MahasiswaDTOs
            .FromSqlRaw("SELECT m.Id, m.Nim, m.Name, m.IdKelas, m.YearOfStudy FROM MahasiswaDTOs m LEFT JOIN KelasDTOs k ON m.KelasId = k.Id WHERE m.Id = " + id)
            .Include(m => m.Kelas);
        
        var mahasiswa = rawData?.FirstOrDefault();

        if (mahasiswa == null)
        {
            return NotFound();
        }

        return mahasiswa;
    }

    [HttpPost]
    public IActionResult Create(MahasiswaDTO newMahasiswa)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        using var transaction = _context.Database.BeginTransaction();
        int rowsAffected = _context.Database
            .ExecuteSqlInterpolated($"INSERT INTO MahasiswaDTOs (Nim, Name, IdKelas, KelasId, YearOfStudy) VALUES ({newMahasiswa.Nim}, {newMahasiswa.Name}, {newMahasiswa.IdKelas}, {newMahasiswa.IdKelas}, {newMahasiswa.YearOfStudy})");

        if (rowsAffected > 0)
        {
            transaction.Commit();
            return CreatedAtAction(nameof(GetById), new { id = newMahasiswa.Id }, newMahasiswa);
        }
        else
        {
            transaction.Rollback();
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, MahasiswaDTO updatedMahasiswa)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        using var transaction = _context.Database.BeginTransaction();
        int rowsAffected = _context.Database
            .ExecuteSqlInterpolated($"UPDATE MahasiswaDTOs SET Nim = {updatedMahasiswa.Nim}, Name = {updatedMahasiswa.Name}, IdKelas = {updatedMahasiswa.IdKelas}, KelasId = {updatedMahasiswa.IdKelas}, YearOfStudy = {updatedMahasiswa.YearOfStudy} WHERE id = {id}");

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
        int rowsAffected = _context.Database.ExecuteSqlInterpolated($"DELETE FROM MahasiswaDTOs WHERE id = {id}");

        if (rowsAffected == 0)
        {
            transaction.Rollback();
            return BadRequest();
        }

        transaction.Commit();

        return Ok();
    }
}