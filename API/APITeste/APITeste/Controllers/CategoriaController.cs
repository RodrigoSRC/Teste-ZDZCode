using APITeste.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APITeste.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {

        [HttpGet()]
        public IEnumerable<Categoria> Get()
        {

            return CategoriaLista.ToList();
        }


        public static List<Categoria> CategoriaLista = new List<Categoria>();


        [HttpPost()]
        public IActionResult Post([FromBody] Categoria categoria)
        {

            if (CategoriaLista.Any(c => c.Nome == categoria.Nome))
            {
                return Conflict("Já existe uma categoria com o mesmo nome.");
            }


            CategoriaLista.Add(categoria);

            return Ok(categoria);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (!Guid.TryParse(id, out Guid idGuid))
            {
                return BadRequest("O id fornecido não é um GUID válido.");
            }

            var categoria = CategoriaLista.FirstOrDefault(c => c.Id == idGuid);
            if (categoria == null)
            {
                return NotFound();
            }

            CategoriaLista.Remove(categoria);
            return NoContent();
        }


        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Categoria categoriaAtualizada)
        {
            if (!Guid.TryParse(id, out Guid idGuid))
            {
                return BadRequest("O id fornecido não é um GUID válido.");
            }

            var categoria = CategoriaLista.FirstOrDefault(c => c.Id == idGuid);
            if (categoria == null)
            {
                return NotFound();
            }

            if (categoriaAtualizada == null)
            {
                return BadRequest("A categoria atualizada não pode ser nula.");
            }

            categoria.Nome = categoriaAtualizada.Nome ?? categoria.Nome;
            categoria.Descricao = categoriaAtualizada.Descricao;

            return Ok(categoria);

        }
    }
}



/*using APITeste.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APITeste.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly EFDBContext _context;

        public CategoriaController(EFDBContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<IEnumerable<Categoria>> Get()
        {
            return await _context.Categoria.ToListAsync();
        }

        [HttpPost()]
        public async Task<ActionResult<Categoria>> Post([FromBody] Categoria categoria)
        {
            _context.Categoria.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = categoria.Id }, categoria);
        }
    }
}*/
