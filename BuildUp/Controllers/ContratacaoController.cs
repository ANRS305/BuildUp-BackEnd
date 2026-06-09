using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuildUp.Models;
using BuildUp.DTO;
using BuildUp.Data;

namespace BuildUp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContratacoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContratacoesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarContratacao dto)
        {
            var contratacao = new Contratacao
            {
                Data_Servico = dto.DataServico,
                Descricao_Servico = dto.DescricaoServico,
                Valor_Combinado = dto.ValorCombinado,
                Id_Usuario = dto.IdUsuario,
                Id_Profissional = dto.IdProfissional,
                Status = "Pendente"
            };

            _context.Contratacoes.Add(contratacao);
            await _context.SaveChangesAsync();

            return Ok(contratacao);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _context.Contratacoes.ToListAsync();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _context.Contratacoes.FindAsync(id);

            if (item == null)
            {
                return NotFound("Contratação não encontrada");
            }

            return Ok(item);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> AtualizarStatus(
            int id,
            [FromBody] StatusOrcamento dto)
        {
            var contratacao = await _context.Contratacoes.FindAsync(id);

            if (contratacao == null)
            {
                return NotFound("Contratação não encontrada");
            }

            contratacao.Status = dto.Status;

            await _context.SaveChangesAsync();

            return Ok(contratacao);
        }
    }
}