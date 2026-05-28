using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildUp.Models
{
    [Table("itens_orcamento")]
    public class ItemOrcamento
    {
        [Key]
        [Column("id_item")]
        public int Id_Item { get; set; }

        [Column("quantidade")]
        public int Quantidade { get; set; }

        [Column("preco_estimado")]
        public decimal Preco_Estimado { get; set; }

        [Column("id_orcamento")]
        public int Id_Orcamento { get; set; }

        [Column("id_material")]
        public int Id_Material { get; set; }
    }
}