using Mandiri.MiniProject.JadwalPembelajaran.API.Data;
using Mandiri.MiniProject.JadwalPembelajaran.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mandiri.MiniProject.JadwalPembelajaran.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GedungController : ControllerBase
{
    private readonly JadwalPerkuliahanContext _context;

    public GedungController(JadwalPerkuliahanContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<GedungDTO> Get()
    {
        var rawData = _context.GedungDTOs
            .FromSqlRaw(@"
                SELECT g.Id, g.Name, g.LocationDescription
                FROM GedungDTOs g
                LEFT JOIN RuanganDTOs r
                ON g.Id = r.IdGedung
            ")
            .Include(g => g.Ruangans);

        var gedungs = rawData?.ToList();

        if (gedungs == null)
        {
            return new List<GedungDTO>();
        }

        return gedungs;
    }

    [HttpGet("{id}")]
    public ActionResult<GedungDTO> GetById(int id)
    {
        var rawData = _context.GedungDTOs
            .FromSqlRaw(@"
                SELECT g.Id, g.Name, g.LocationDescription
                FROM GedungDTOs g
                LEFT JOIN RuanganDTOs r
                ON g.Id = r.IdGedung
                WHERE g.id = {0}
            ", id)
            .Include(g => g.Ruangans);
        
        var gedung = rawData?.FirstOrDefault();

        if (gedung == null)
        {
            return NotFound();
        }

        return gedung;
    }

    [HttpPost]
    public IActionResult Create(GedungDTO newGedung)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        using var transaction = _context.Database.BeginTransaction();
        int rowsAffected = _context.Database
            .ExecuteSqlInterpolated($"INSERT INTO GedungDTOs (Name, LocationDescription) VALUES ({newGedung.Name}, {newGedung.LocationDescription})");

        if (rowsAffected > 0)
        {
            transaction.Commit();
            return CreatedAtAction(nameof(GetById), new { id = newGedung.Id }, newGedung);
        }
        else
        {
            transaction.Rollback();
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, GedungDTO updatedGedung)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        using var transaction = _context.Database.BeginTransaction();
        int rowsAffected = _context.Database
            .ExecuteSqlInterpolated($"UPDATE GedungDTOs SET Name = {updatedGedung.Name}, LocationDescription = {updatedGedung.LocationDescription} WHERE id = {id}");

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
        int rowsAffected = _context.Database.ExecuteSqlInterpolated($"DELETE FROM GedungDTOs WHERE id = {id}");

        if (rowsAffected == 0)
        {
            transaction.Rollback();
            return BadRequest();
        }

        transaction.Commit();

        return Ok();
    }
}