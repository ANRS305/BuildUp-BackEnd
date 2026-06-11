using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildUp.DTO
{
    public class CriarContratacao
    {
        public DateTime DataServico { get; set; }
        public string DescricaoServico { get; set; }
        public decimal ValorCombinado { get; set; }
        public int IdUsuario { get; set; }
        public int IdProfissional { get; set; }
    }
}