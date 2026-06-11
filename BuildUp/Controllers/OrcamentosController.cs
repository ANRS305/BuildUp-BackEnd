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
            {
                return BadRequest("Dados inválidos");
            }

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
                int quantidade = CalcularQuantidade(
                    material.Categoria,
                    orcamento.Metragem
                );

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
            orcamento.Tempo_Estimado =
                $"{Math.Ceiling(orcamento.Metragem / 25)} meses";

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensagem = "Orçamento gerado com sucesso",
                orcamento,
                valorTotal,
                itens = itensGerados
            });
        }

        private int CalcularQuantidade(
            string categoria,
            decimal metragem)
        {
            return categoria switch
            {
                "Casa" => (int)(metragem * 1.5m),
                "Apartamento" => (int)(metragem * 1.3m),
                "Hotel" => (int)(metragem * 2.5m),
                "Escritório" => (int)(metragem * 2.0m),
                "Comercial" => (int)(metragem * 2.4m),
                "Chácara" => (int)(metragem * 1.4m),
                "Área de Lazer" => (int)(metragem * 1.7m),
                _ => (int)(metragem * 1.0m)
            };
        }
    }
}