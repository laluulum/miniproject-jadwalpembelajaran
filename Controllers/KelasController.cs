using Mandiri.MiniProject.JadwalPembelajaran.API.Data;
using Mandiri.MiniProject.JadwalPembelajaran.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mandiri.MiniProject.JadwalPembelajaran.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KelasController : ControllerBase
{
    private readonly JadwalPerkuliahanContext _context;

    public KelasController(JadwalPerkuliahanContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<KelasDTO> Get()
    {
        var rawData = _context.KelasDTOs.FromSqlRaw("SELECT * FROM KelasDTOs");
        var kelasses = rawData?.ToList();

        if (kelasses == null)
        {
            return new List<KelasDTO>();
        }

        return kelasses;
    }

    [HttpGet("{id}")]
    public ActionResult<KelasDTO> GetById(int id)
    {
        var rawData = _context.KelasDTOs.FromSqlRaw("SELECT * FROM KelasDTOs WHERE id = " + id);
        var kelas = rawData?.FirstOrDefault();

        if (kelas == null)
        {
            return NotFound();
        }

        return kelas;
    }

    [HttpPost]
    public IActionResult Create(KelasDTO newKelas)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        using var transaction = _context.Database.BeginTransaction();
        int rowsAffected = _context.Database.ExecuteSqlInterpolated($"INSERT INTO KelasDTOs (Name) VALUES ({newKelas.Name})");

        if (rowsAffected > 0)
        {
            transaction.Commit();
            return CreatedAtAction(nameof(GetById), new { id = newKelas.Id }, newKelas);
        }
        else
        {
            transaction.Rollback();
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, KelasDTO updatedKelas)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        using var transaction = _context.Database.BeginTransaction();
        int rowsAffected = _context.Database.ExecuteSqlInterpolated($"UPDATE KelasDTOs SET Name = {updatedKelas.Name} WHERE id = {id}");

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
        int rowsAffected = _context.Database.ExecuteSqlInterpolated($"DELETE FROM KelasDTOs WHERE id = {id}");

        if (rowsAffected == 0)
        {
            transaction.Rollback();
            return BadRequest();
        }

        transaction.Commit();

        return Ok();
    }
}