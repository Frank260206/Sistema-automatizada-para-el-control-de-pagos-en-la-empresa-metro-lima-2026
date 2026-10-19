using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo_Proyecto_Grupal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Creamos la instancia de la tercera clase y llamamos a su método desde el MAIN
            SistemaTienda miTienda = new SistemaTienda();
            miTienda.IniciarMenu();
        }
    }
}
