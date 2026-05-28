using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuildUp.Models;
using BuildUp.Data;

namespace BuildUp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrcamentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrcamentosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("simular")]
        public async Task<IActionResult> SimularOrcamento([FromBody] Orcamento orcamentoRequest)
        {
            if (orcamentoRequest == null)
                return BadRequest("Dados inválidos");

            var orcamento = new Orcamento
            {
                Tipo_Obra = orcamentoRequest.Tipo_Obra,
                Metragem = orcamentoRequest.Metragem,
                Quantidade_Quartos = orcamentoRequest.Quantidade_Quartos,
                Quantidade_Banheiros = orcamentoRequest.Quantidade_Banheiros,
                Id_Usuario = orcamentoRequest.Id_Usuario,
                Data_Orcamento = DateTime.Now
            };

            _context.Orcamentos.Add(orcamento);
            await _context.SaveChangesAsync();

            var materiais = await _context.Materiais.ToListAsync();

            decimal valorTotal = 0;

            var itensGerados = new List<ItemOrcamento>();

            foreach (var material in materiais)
            {
                int quantidade = CalcularQuantidade(material.Categoria, orcamento.Metragem);

                decimal preco = quantidade * material.Preco_Medio;

                var item = new ItemOrcamento
                {
                    Id_Orcamento = orcamento.Id_Orcamento,
                    Id_Material = material.Id_Material,
                    Quantidade = quantidade,
                    Preco_Estimado = preco
                };

                valorTotal += preco;

                itensGerados.Add(item);
                _context.ItensOrcamentos.Add(item);
            }

            orcamento.Valor_Estimado = valorTotal;
            orcamento.Tempo_Estimado = $"{Math.Ceiling(orcamento.Metragem / 25)} meses";

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensagem = "Orçamento gerado com sucesso",
                orcamento,
                valorTotal,
                itens = itensGerados
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _context.Orcamentos
                .ToListAsync();

            return Ok(lista);
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<IActionResult> GetByUsuario(int idUsuario)
        {
            var lista = await _context.Orcamentos
                .Where(o => o.Id_Usuario == idUsuario)
                .ToListAsync();

            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var orcamento = await _context.Orcamentos
                .FirstOrDefaultAsync(o => o.Id_Orcamento == id);

            if (orcamento == null)
                return NotFound("Orçamento não encontrado");

            var itens = await _context.ItensOrcamentos
                .Where(i => i.Id_Orcamento == id)
                .ToListAsync();

            var materiais = await _context.Materiais.ToListAsync();

            var resultado = itens.Select(i => new
            {
                i.Id_Item,
                i.Quantidade,
                i.Preco_Estimado,
                Material = materiais.FirstOrDefault(m => m.Id_Material == i.Id_Material)
            });

            return Ok(new
            {
                orcamento,
                itens = resultado
            });
        }
        private int CalcularQuantidade(string categoria, decimal metragem)
        {
            return categoria switch
            {
                "Construção" => (int)(metragem * 2.2m),
                "Acabamento" => (int)(metragem * 1.3m),
                "Estrutura" => (int)(metragem * 1.8m),
                _ => (int)(metragem * 0.8m)
            };
        }
    }
}