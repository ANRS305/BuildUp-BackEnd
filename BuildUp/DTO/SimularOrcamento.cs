using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildUp.DTO
{
    public class SimularOrcamento
    {
        public string TipoObra { get; set; }
        public decimal Metragem { get; set; }
        public int QuantidadeQuartos { get; set; }
        public int QuantidadeBanheiros { get; set; }
        public int IdUsuario { get; set; }

    }
}