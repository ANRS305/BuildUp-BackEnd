using BuildUp.Data;
using BuildUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuildUp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemOrcamentoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemOrcamentoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ItemOrcamento>>> Get()
        {
            return await _context.ItensOrcamentos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _context.ItensOrcamentos.FindAsync(id);

            if (item == null)
                return NotFound($"Item do orçamento com o id = {id} não encontrado");

            return Ok(item);
        }
    }
}