using Mandiri.MiniProject.JadwalPembelajaran.API.Data;
using Mandiri.MiniProject.JadwalPembelajaran.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mandiri.MiniProject.JadwalPembelajaran.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RuanganController : ControllerBase
{
    private readonly JadwalPerkuliahanContext _context;

    public RuanganController(JadwalPerkuliahanContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<RuanganDTO> Get()
    {
        var rawData = _context.RuanganDTOs
            .FromSqlRaw($"SELECT r.Id, r.Name, r.IdGedung, r.GedungId FROM RuanganDTOs r LEFT JOIN GedungDTOs g ON r.GedungId = g.Id")
            .Include(r => r.Gedung);

        var ruangans = rawData?.ToList();

        if (ruangans == null)
        {
            return new List<RuanganDTO>();
        }

        return ruangans;
    }

    [HttpGet("{id}")]
    public ActionResult<RuanganDTO> GetById(int id)
    {
        var rawData = _context.RuanganDTOs
            .FromSqlRaw($"SELECT r.Id, r.Name, r.IdGedung, r.GedungId FROM RuanganDTOs r LEFT JOIN GedungDTOs g ON r.GedungId = g.Id WHERE r.id = {id}")
            .Include(r => r.Gedung);

        var ruangan = rawData?.FirstOrDefault();

        if (ruangan == null)
        {
            return NotFound();
        }

        return ruangan;
    }

    [HttpPost]
    public IActionResult Create(RuanganDTO newRuangan)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        using var transaction = _context.Database.BeginTransaction();
        int rowsAffected = _context.Database
            .ExecuteSqlInterpolated($"INSERT INTO RuanganDTOs (Name, IdGedung, GedungId) VALUES ({newRuangan.Name}, {newRuangan.IdGedung}, {newRuangan.IdGedung})");

        if (rowsAffected > 0)
        {
            transaction.Commit();
            return CreatedAtAction(nameof(GetById), new { id = newRuangan.Id }, newRuangan);
        }
        else
        {
            transaction.Rollback();
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, RuanganDTO updatedRuangan)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        using var transaction = _context.Database.BeginTransaction();
        int rowsAffected = _context.Database
            .ExecuteSqlInterpolated($"UPDATE RuanganDTOs SET Name = {updatedRuangan.Name}, IdGedung = {updatedRuangan.IdGedung} WHERE id = {id}");

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
        int rowsAffected = _context.Database.ExecuteSqlInterpolated($"DELETE FROM RuanganDTOs WHERE id = {id}");

        if (rowsAffected == 0)
        {
            transaction.Rollback();
            return BadRequest();
        }

        transaction.Commit();

        return Ok();
    }
}