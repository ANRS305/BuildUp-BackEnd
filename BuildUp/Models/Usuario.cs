using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildUp.Models
{
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        public int Id_Usuario { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("senha")]
        public string Senha { get; set; }

        [Column("telefone")]
        public string Telefone { get; set; }

        [Column("token")]
        public string? Token { get; set; }

        [Column("data_cadastro")]
        public DateTime Data_Cadastro { get; set; }
    }
}