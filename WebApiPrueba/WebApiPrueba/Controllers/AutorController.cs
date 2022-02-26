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
    public class AutorController : ControllerBase
    {
        private readonly AutorRepository repository;

        public AutorController(AutorRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<ActionResult<List<Autor>>> GetAutores()
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
        public async Task<ActionResult<Autor>> GetById(int id)
        {
            try
            {
                var response = await repository.GetById(id);
                if (response == null) return NotFound();
                return response;
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
            }catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> InsertarAutor([FromBody] Autor autor)
        {
            try
            {
                await repository.Insert(autor);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditarAutor(int id, [FromBody] Autor autor)
        {
            try
            {
                await repository.Edit(id, autor);
            }
            catch
            {
                return BadRequest();
            }
            return BadRequest();
        }
    }
}
