using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildUp.Models
{
    public class Fornecedor
    {
        [Key]
        [Column("id_fornecedor")]
        public int Id_Fornecedor { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("logo")]
        public string? Logo { get; set; }

        [Column("cidade")]
        public string? Cidade { get; set; }

        [Column("telefone")]
        public string Telefone { get; set; }

        [Column("email")]
        public string Email { get; set; }
    }
}