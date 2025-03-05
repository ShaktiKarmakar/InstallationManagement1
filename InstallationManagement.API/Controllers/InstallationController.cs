using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using InstallationManagement.Shared.Models;

namespace InstallationManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstallationController : ControllerBase
    {
        private readonly string _connectionString = "your_connection_string_here"; // Replace with actual connection string

        private IDbConnection Connection => new SqlConnection(_connectionString);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstallationModel>>> GetAll()
        {
            using var db = Connection;
            var installations = await db.QueryAsync<InstallationModel>("SELECT * FROM Installations");
            return Ok(installations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InstallationModel>> GetById(Guid id)
        {
            using var db = Connection;
            var installation = await db.QuerySingleOrDefaultAsync<InstallationModel>(
                "SELECT * FROM Installations WHERE Id = @Id", new { Id = id });

            if (installation == null)
                return NotFound();

            return Ok(installation);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] InstallationModel model)
        {
            using var db = Connection;
            var result = await db.ExecuteAsync(
                "INSERT INTO Installations (Id, Name, Description) VALUES (@Id, @Name, @Description)",
                model);

            if (result == 0)
                return BadRequest("Failed to insert");

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] InstallationModel model)
        {
            using var db = Connection;
            var result = await db.ExecuteAsync(
                "UPDATE Installations SET Name = @Name, Description = @Description WHERE Id = @Id",
                new { model.Name, model.Description, Id = id });

            if (result == 0)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            using var db = Connection;
            var result = await db.ExecuteAsync("DELETE FROM Installations WHERE Id = @Id", new { Id = id });

            if (result == 0)
                return NotFound();

            return NoContent();
        }
    }
}