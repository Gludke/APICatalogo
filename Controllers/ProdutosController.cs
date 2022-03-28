using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase //'ControllerBase' só inclui recursos para API, MVC não
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            return await _context.Produtos
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpGet("{id:int}", Name = "GetProduto")]//Nome da rota
        public async Task<ActionResult<Produto>> Get(int id)
        {
            var prodDb = await _context.Produtos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id.Equals(id));

            if (prodDb is null)
                return NotFound();

            return prodDb;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();

            //Retorno do resultado da criação do objeto usando a rota de GET(id) e do Produto criado
            return new CreatedAtRouteResult("GetProduto", 
                new { id = produto.Id },
                produto);
        }

        [HttpPut("{id:int}")]//Só é mapeado se for int
        public async Task<ActionResult> Put(int id, [FromBody] Produto produto)
        {
            if (id != produto.Id || produto is null)
                return BadRequest();

            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("remove/{id:int}")]//Só é mapeado se for int
        public async Task<ActionResult<Produto>> Remove(int id)
        {
            var prodDb = await _context.Produtos
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prodDb is null)
                return NotFound();

            _context.Produtos.Remove(prodDb);
            await _context.SaveChangesAsync();
            return prodDb;
        }
    }
}
