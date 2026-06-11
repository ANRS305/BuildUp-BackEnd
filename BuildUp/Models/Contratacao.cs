using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildUp.Models
{
    [Table("contratacoes")]
    public class Contratacao
    {
        [Key]
        [Column("id_contratacao")]
        public int Id_Contratacao { get; set; }

        [Column("data_inicio")]
        public DateTime Data_Inicio { get; set; }

        [Column("data_conclusao")]
        public DateTime? Data_Conclusao { get; set; }

        [Column("descricao_servico")]
        public string Descricao_Servico { get; set; }

        [Column("valor_combinado")]
        public decimal Valor_Combinado { get; set; }

        [Column("data_contratacao")]
        public DateTime Data_Contratacao { get; set; } = DateTime.Now;

        [Column("id_usuario")]
        public int Id_Usuario { get; set; }

        [Column("id_profissional")]
        public int Id_Profissional { get; set; }

        [Column("status")]
        public string Status { get; set; } = "Pendente";
    }
}