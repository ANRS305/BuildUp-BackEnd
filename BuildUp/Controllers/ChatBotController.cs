using BuildUp.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ChatBotTeste.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public ChatController(
        IConfiguration configuration,
        IHttpClientFactory factory)
    {
        _configuration = configuration;
        _httpClient = factory.CreateClient();
    }

    [HttpPost]
    public async Task<IActionResult> Perguntar([FromBody] MensagemChat dto)
    {
        var token = Environment.GetEnvironmentVariable("HF_TOKEN");

        if (string.IsNullOrEmpty(token))
            return BadRequest("Token não configurado.");

        using var client = new HttpClient();

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var request = new
        {
            inputs = dto.Mensagem
        };

        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json"
        );

        var response = await client.PostAsync(
            "https://api-inference.huggingface.co/models/mistralai/Mistral-7B-Instruct-v0.3",
            content
        );

        var result = await response.Content.ReadAsStringAsync();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Chat([FromBody] MensagemChat dto)
    {
        try
        {
            var apiKey = _configuration["HuggingFace:ApiKey"];

            _httpClient.DefaultRequestHeaders.Clear();

            _httpClient.DefaultRequestHeaders.Add(
                "Authorization",
                $"Bearer {apiKey}"
            );

            var requestBody = new
            {
                messages = new object[]
                {
                new
                {
                    role = "system",
                    content = """
                    Você é o assistente oficial da plataforma BuildUp.

                    A BuildUp é uma plataforma web que conecta proprietários de obras e reformas com profissionais da construção civil (pedreiros, eletricistas, pintores e outros profissionais), além de oferecer ferramentas de planejamento de obras, simulação de custos e contratação de serviços.

                    Sua função é ajudar o usuário a entender e utilizar a plataforma.

                    FUNCIONALIDADES DA PLATAFORMA:
                    - Cadastro e login de usuários
                    - Listagem de profissionais da construção civil
                    - Visualização de especialidades (pedreiro, eletricista, pintor, etc.)
                    - Simulação de orçamento de obras (materiais, custos e metragem)
                    - Planejamento de obras com estimativa de gastos
                    - Contratação de profissionais dentro da plataforma
                    - Acompanhamento de status de serviços (pendente, em andamento, concluído)

                    COMO FUNCIONA A CONTRATAÇÃO:
                    - O usuário visualiza profissionais disponíveis na plataforma
                    - Analisa informações como especialidade, experiência, avaliação e preço
                    - Escolhe um profissional adequado para sua obra
                    - Realiza a contratação dentro do sistema
                    - O sistema registra o serviço com data, valor combinado e descrição
                    - O andamento do serviço pode ser acompanhado pelo status

                    COMO FUNCIONA O PLANEJAMENTO DE OBRA:
                    - O usuário informa dados da obra (tipo, metragem, quantidade de cômodos e acabamento)
                    - O sistema gera uma simulação de custos
                    - São listados materiais necessários e seus valores estimados
                    - O usuário pode comparar cenários e tomar decisões mais econômicas

                    REGRAS:
                    - Responda apenas perguntas relacionadas à plataforma BuildUp
                    - Explique sempre de forma clara, simples e educativa
                    - Se o usuário perguntar algo fora do contexto da plataforma, recuse educadamente dizendo:
                    "Posso ajudar apenas com informações relacionadas à plataforma BuildUp."
                    - Não responda temas externos como política, medicina, direito ou assuntos gerais
                    - Não forneça instruções ilegais ou fora do contexto do sistema

                    ESTILO DE RESPOSTA:
                    - Seja didático, como um guia explicando o sistema
                    - Use linguagem simples e objetiva
                    - Sempre ajude o usuário a entender como usar a plataforma
                    - Foque em orientar o fluxo de uso (planejamento → escolha → contratação → acompanhamento)
                    """
                },

                new
                {
                    role = "user",
                    content = dto.Mensagem
                }
                },

                model = "meta-llama/Llama-3.1-8B-Instruct",
                max_tokens = 200
            };

            var json = JsonSerializer.Serialize(requestBody);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(
                "https://router.huggingface.co/v1/chat/completions",
                content
            );

            var responseContent =
    await response.Content.ReadAsStringAsync();

            using JsonDocument doc =
                JsonDocument.Parse(responseContent);

            var mensagem =
                doc.RootElement
                   .GetProperty("choices")[0]
                   .GetProperty("message")
                   .GetProperty("content")
                   .GetString();

            return Ok(new
            {
                pergunta = dto.Mensagem,
                resposta = mensagem
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                erro = ex.Message
            });
        }
    }
}