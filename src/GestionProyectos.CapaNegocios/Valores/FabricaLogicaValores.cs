using GestionProyectos.Modelos.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.CapaNegocios.Valores
{
    public class FabricaLogicaValores : IFabricaLogicaValores
    {
        
        public ILogicaValoresEstados LogicaValoresEstados()
        {
            return new LogicaValoresEstados();
        }
    }
}
