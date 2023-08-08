using Mandiri.MiniProject.JadwalPembelajaran.API.Data;
using Mandiri.MiniProject.JadwalPembelajaran.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mandiri.MiniProject.JadwalPembelajaran.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MataKuliahController : ControllerBase
{
    private readonly JadwalPerkuliahanContext _context;

    public MataKuliahController(JadwalPerkuliahanContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<MataKuliahDTO> Get()
    {
        var rawData = _context.MataKuliahDTOs.FromSqlRaw("SELECT * FROM MataKuliahDTOs");
        var mataKuliahs = rawData?.ToList();

        if (mataKuliahs == null)
        {
            return new List<MataKuliahDTO>();
        }

        return mataKuliahs;
    }

    [HttpGet("{id}")]
    public ActionResult<MataKuliahDTO> GetById(int id)
    {
        var rawData = _context.MataKuliahDTOs.FromSqlRaw("SELECT * FROM MataKuliahDTOs WHERE id = " + id);
        var mataKuliah = rawData?.FirstOrDefault();

        if (mataKuliah == null)
        {
            return NotFound();
        }

        return mataKuliah;
    }

    [HttpPost]
    public IActionResult Create(MataKuliahDTO newMataKuliah)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        using var transaction = _context.Database.BeginTransaction();
        int rowsAffected = _context.Database.ExecuteSqlInterpolated($"INSERT INTO MataKuliahDTOs (Name, Sks) VALUES ({newMataKuliah.Name}, {newMataKuliah.Sks})");

        if (rowsAffected > 0)
        {
            transaction.Commit();
            return CreatedAtAction(nameof(GetById), new { id = newMataKuliah.Id }, newMataKuliah);
        }
        else
        {
            transaction.Rollback();
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, MataKuliahDTO updatedMataKuliah)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        using var transaction = _context.Database.BeginTransaction();
        int rowsAffected = _context.Database.ExecuteSqlInterpolated($"UPDATE MataKuliahDTOs SET Name = {updatedMataKuliah.Name}, Sks = {updatedMataKuliah.Sks} WHERE id = {id}");

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
        int rowsAffected = _context.Database.ExecuteSqlInterpolated($"DELETE FROM MataKuliahDTOs WHERE id = {id}");

        if (rowsAffected == 0)
        {
            transaction.Rollback();
            return BadRequest();
        }

        transaction.Commit();

        return Ok();
    }
}