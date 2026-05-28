using Microsoft.EntityFrameworkCore;
using BuildUp.Models;

namespace BuildUp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Material> Materiais { get; set; }
        public DbSet<Contratacao> Contratacoes { get; set; }
        public DbSet<Orcamento> Orcamentos { get; set; }
        public DbSet<ItemOrcamento> ItensOrcamentos { get; set; }
    }
}