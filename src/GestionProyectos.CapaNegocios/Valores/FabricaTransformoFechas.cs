using GestionProyectos.Modelos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.CapaNegocios.Valores
{
    public class FabricaTransformoFechas : IFabricaTransformoFechas
    {
        public ITransformoFechas TransformoFechas()
        {
            return new TransformoFechas();
        }
    }
}
