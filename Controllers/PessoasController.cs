using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testes_webapi.Models;

namespace testes_webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PessoasController : Controller
    {
        private readonly PessoasContext _context;

        public PessoasController(PessoasContext context)
            => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Pessoa>> RetornaTodasAsPessoas()
        {
            return await _context.Pessoas.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id:int}", Name = "RetornaPessoa")]
        public async Task<IActionResult> RetornaPessoa(int id)
        {
            var pessoaEncontrada = await _context.Pessoas.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if(pessoaEncontrada == null)
                return NotFound();

            return new ObjectResult(pessoaEncontrada);
        }

        [HttpPost]
        public async Task<IActionResult> GravarPessoa([FromBody] Pessoa pessoa)
        {
            if(pessoa == null)
                return BadRequest();

            await _context.Pessoas.AddAsync(pessoa);
            await _context.SaveChangesAsync();
            return CreatedAtRoute("RetornaPessoa", new { id = pessoa.Id }, pessoa);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> AtualizarPessoa(int id, [FromBody] Pessoa pessoa)
        {
            var pessoaEncontrada = await _context.Pessoas.FirstOrDefaultAsync(p => p.Id == id);
            if(pessoaEncontrada == null)
                return NotFound();
            else if(pessoa == null)
                return BadRequest();

            pessoaEncontrada.Nome = pessoa.Nome;
            pessoaEncontrada.Idade = pessoa.Idade;
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> ApagarPessoa(int id)
        {
            var pessoaEncontrada = await _context.Pessoas.FirstOrDefaultAsync(p => p.Id == id);
            if(pessoaEncontrada == null)
                return NotFound();

            _context.Pessoas.Remove(pessoaEncontrada);
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }
    }
}