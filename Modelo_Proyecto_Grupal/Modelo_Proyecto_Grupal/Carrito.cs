using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo_Proyecto_Grupal
{
    public class Carrito
    {
        // Lista (arreglo dinámico) para guardar los productos que el cliente va escaneando
        private List<Producto> productosEnCarrito;

        // Constructor para inicializar el carrito vacío
        public Carrito()
        {
            productosEnCarrito = new List<Producto>();
        }

        // 1. CREATE (Insertar/Agregar): Añade un producto escaneado al carrito
        public void AgregarProducto(Producto p)
        {
            productosEnCarrito.Add(p);
            Console.WriteLine($"Agregado al carrito: {p.Nombre}");
        }

        // 2. READ (Buscar): Muestra todos los productos que están en el carrito
        public void MostrarCarrito()
        {
            Console.WriteLine("\n--- TUS PRODUCTOS ESCANEADOS ---");
            if (productosEnCarrito.Count == 0)
            {
                Console.WriteLine("El carrito está vacío.");
            }
            else
            {
                foreach (var item in productosEnCarrito)
                {
                    Console.WriteLine(item.MostrarDetalle());
                }
            }
        }

        // 3. CALCULAR TOTAL: Suma los precios de todos los productos del carrito
        public double CalcularTotal()
        {
            double total = 0;
            foreach (var item in productosEnCarrito)
            {
                total += item.Precio;
            }
            return total;
        }

        // 4. DELETE (Eliminar): Borra un producto del carrito según su código
        public void EliminarProducto(string codigo)
        {
            Producto productoAEliminar = null;
            foreach (var item in productosEnCarrito)
            {
                if (item.Codigo == codigo)
                {
                    productoAEliminar = item;
                    break;
                }
            }

            if (productoAEliminar != null)
            {
                productosEnCarrito.Remove(productoAEliminar);
                Console.WriteLine($"Se eliminó el producto: {productoAEliminar.Nombre}");
            }
            else
            {
                Console.WriteLine("No se encontró el producto en el carrito.");
            }
        }
    }
}
