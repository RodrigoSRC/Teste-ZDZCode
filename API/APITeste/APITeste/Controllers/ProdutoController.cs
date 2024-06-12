using APITeste.Models;
using Microsoft.AspNetCore.Mvc;

namespace APITeste.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {

        [HttpGet()]
        public IEnumerable<Produto> Get()
        {
            return ProdutoLista.ToList();
        }


        public static List<Produto> ProdutoLista = new List<Produto>();

        [HttpPost()]
        public IActionResult Post([FromBody] Produto produto)
        {
            if (ProdutoLista.Any(p => p.Nome == produto.Nome))
            {
                return Conflict("Já existe uma categoria com o mesmo nome.");
            }

            produto.DataCadastrada = DateTime.Now;
            produto.Estoque = 0;
            ProdutoLista.Add(produto);

            return Ok(produto);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (!Guid.TryParse(id, out Guid idGuid))
            {
                return BadRequest("O id fornecido não é um GUID válido.");
            }

            var produto = ProdutoLista.FirstOrDefault(p => p.Id == idGuid);
            if (produto == null)
            {
                return NotFound();
            }

            ProdutoLista.Remove(produto);
            return NoContent();
        }


        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Produto produtoAtualizado)
        {
            if (!Guid.TryParse(id, out Guid idGuid))
            {
                return BadRequest("O id fornecido não é um GUID válido.");
            }

            var produto = ProdutoLista.FirstOrDefault(p => p.Id == idGuid);
            if (produto == null)
            {
                return NotFound();
            }

            if (produtoAtualizado == null)
            {
                return BadRequest("O produto atualizado não pode ser nula.");
            }

            produto.Nome = produtoAtualizado.Nome ?? produto.Nome;
            produto.Descricao = produtoAtualizado.Descricao;

            return Ok(produto);

        }
    }
}
