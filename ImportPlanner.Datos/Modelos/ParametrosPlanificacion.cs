using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportacionesPlanner.Datos.Modelos
{
    public class ParametrosPlanificacion
    {
        public string Proveedor { get; set; }

        public int Familia { get; set; }

        public int DiasLead { get; set; }

        public int DiasFrecuencia { get; set; }

        public int BaseCalculo { get; set; }

        public decimal Crecimiento { get; set; }
    }
}
