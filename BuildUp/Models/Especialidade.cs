using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildUp.Models
{
    [Table("especialidades")]
    public class Especialidade
    {
        [Key]
        [Column("id_especialidade")]
        public int Id_Especialidade { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }
    }
}