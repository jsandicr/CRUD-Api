using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPrueba.Entities;
using WebApiPrueba.Repository;

namespace WebApiPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly LibrosRepository repository;

        public LibrosController(LibrosRepository librosRepository)
        {
            this.repository = librosRepository ?? throw new ArgumentException(nameof(repository));
        }
        [HttpGet]
        public async Task<ActionResult<List<Libros>>> GetAllValues()
        {
            try
            {
                return await repository.GetAll();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Libros>> GetById(int id)
        {
            try
            {
                return await repository.GetById(id);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> InsertLibro([FromBody] Libros libros)
        {
            try
            {
                await repository.Insert(libros);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            try
            {
                await repository.DeleteById(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditLibro(int id, [FromBody] Libros libro)
        {
            try
            {
                await repository.Edit(id, libro);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
