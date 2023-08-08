using Mandiri.MiniProject.JadwalPembelajaran.API.Data;
using Mandiri.MiniProject.JadwalPembelajaran.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mandiri.MiniProject.JadwalPembelajaran.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DosenController : ControllerBase
{
    private readonly JadwalPerkuliahanContext _context;

    public DosenController(JadwalPerkuliahanContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<DosenDTO> Get()
    {
        var rawData = _context.DosenDTOs.FromSqlRaw("SELECT * FROM DosenDTOs");
        var dosens = rawData?.ToList();

        if (dosens == null)
        {
            return new List<DosenDTO>();
        }

        return dosens;
    }

    [HttpGet("{id}")]
    public ActionResult<DosenDTO> GetById(int id)
    {
        var rawData = _context.DosenDTOs.FromSqlRaw("SELECT * FROM DosenDTOs WHERE id = " + id);
        var dosen = rawData?.FirstOrDefault();

        if (dosen == null)
        {
            return NotFound();
        }

        return dosen;
    }

    [HttpPost]
    public IActionResult Create(DosenDTO newDosen)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        using var transaction = _context.Database.BeginTransaction();
        int rowsAffected = _context.Database.ExecuteSqlInterpolated($"INSERT INTO DosenDTOs (NIP, Name) VALUES ({newDosen.Nip}, {newDosen.Name})");

        if (rowsAffected > 0)
        {
            transaction.Commit();
            return CreatedAtAction(nameof(GetById), new { id = newDosen.Id }, newDosen);
        }
        else
        {
            transaction.Rollback();
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, DosenDTO updatedDosen)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        using var transaction = _context.Database.BeginTransaction();
        int rowsAffected = _context.Database.ExecuteSqlInterpolated($"UPDATE DosenDTOs SET NIP = {updatedDosen.Nip}, Name = {updatedDosen.Name} WHERE id = {id}");

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
        int rowsAffected = _context.Database.ExecuteSqlInterpolated($"DELETE FROM DosenDTOs WHERE id = {id}");

        if (rowsAffected == 0)
        {
            transaction.Rollback();
            return BadRequest();
        }

        transaction.Commit();

        return Ok();
    }
}