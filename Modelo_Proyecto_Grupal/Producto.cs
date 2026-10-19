using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo_Proyecto_Grupal
{
    public class productos
    {
        // Datos del producto
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public bool RequiereMayorEdad { get; set; } // True si es alcohol, False si no
        public int Stock { get; set; }

        // Metodo para crear el producto 
        public productos(string codigo, string nombre, double precio, bool requiereMayorEdad, int stock)
        {
            Codigo = codigo;
            Nombre = nombre;
            Precio = precio;
            RequiereMayorEdad = requiereMayorEdad;
            Stock = stock;
        }

        // Mostrar datos del producto
        public string MostrarDetalle()
        {
            return $"[{Codigo}] {Nombre} - S/. {Precio:F2}";
        }
    }
}