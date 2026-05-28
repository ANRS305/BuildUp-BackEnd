using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuildUp.DTO;
using BuildUp.Models;
using BuildUp.Data;

namespace BuildUpAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("cadastro")]
        public async Task<IActionResult> Cadastro(CadastrarUsuario dto)
        {
            var usuarioExistente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (usuarioExistente != null)
            {
                return BadRequest("Email já cadastrado.");
            }

            Usuario usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha,
                Telefone = dto.Telefone,
                Data_Cadastro = DateTime.Now
            };

            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensagem = "Usuário cadastrado com sucesso."
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUsuario dto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u =>
                    u.Email == dto.Email &&
                    u.Senha == dto.Senha);

            if (usuario == null)
            {
                return Unauthorized("Email ou senha inválidos.");
            }

            return Ok(new
            {
                mensagem = "Login realizado com sucesso.",
                usuario.Id_Usuario,
                usuario.Nome,
                usuario.Email
            });
        }
    }
}