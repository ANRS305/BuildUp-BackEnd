using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildUp.Models
{
    public class Profissional
    {
        [Key]
        [Column("id_profissional")]
        public int Id_Profissional { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("foto_perfil")]
        public string? Foto_Perfil { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("cidade")]
        public string Cidade { get; set; }

        [Column("valor_diaria")]
        public decimal Valor_Diaria { get; set; }

        [Column("avaliacao_media")]
        public decimal Avaliacao_Media { get; set; }

        [Column("experiencia_anos")]
        public int Experiencia_Anos { get; set; }

        [Column("telefone")]
        public string Telefone { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("disponivel")]
        public bool Disponivel { get; set; }

        [Column("id_especialidade")]
        public int Id_Especialidade { get; set; }

        [ForeignKey(nameof(Id_Especialidade))]
        public virtual Especialidade Especialidade { get; set; }

    }
}