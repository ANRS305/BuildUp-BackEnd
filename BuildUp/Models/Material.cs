using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildUp.Models
{
    [Table("materiais")]
    public class Material
    {
        [Key]
        [Column("id_material")]
        public int Id_Material { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("unidade")]
        public string Unidade { get; set; }

        [Column("preco_medio")]
        public decimal Preco_Medio { get; set; }

        [Column("categoria")]
        public string? Categoria { get; set; }

        [Column("id_fornecedor")]
        public int Id_Fornecedor { get; set; }

        [ForeignKey(nameof(Id_Fornecedor))]
        public virtual Fornecedor Fornecedor { get; set; }
    }
}