using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildUp.DTO
{
    public class CriarContratacao
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataConclusao { get; set; }
        public string DescricaoServico { get; set; } = string.Empty;
        public decimal ValorCombinado { get; set; }
        public int IdUsuario { get; set; }
        public int IdProfissional { get; set; }
    }
}