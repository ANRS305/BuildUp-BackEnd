using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildUp.Models
{
    [Table("orcamentos")]
    public class Orcamento
    {
        [Key]
        [Column("id_orcamento")]
        public int Id_Orcamento { get; set; }

        [Column("tipo_obra")]
        public string Tipo_Obra { get; set; }

        [Column("metragem")]
        public decimal Metragem { get; set; }

        [Column("quantidade_quartos")]
        public int Quantidade_Quartos { get; set; }

        [Column("quantidade_banheiros")]
        public int Quantidade_Banheiros { get; set; }

        [Column("valor_estimado")]
        public decimal? Valor_Estimado { get; set; }

        [Column("tempo_estimado")]
        public string Tempo_Estimado { get; set; }

        [Column("data_orcamento")]
        public DateTime Data_Orcamento { get; set; } = DateTime.Now;

        [Column("id_usuario")]
        public int Id_Usuario { get; set; }
    }
}