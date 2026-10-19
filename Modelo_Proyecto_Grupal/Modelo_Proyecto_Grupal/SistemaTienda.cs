using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo_Proyecto_Grupal
{
    public class SistemaTienda
    {
        private Producto[] Inventario;
        private int TotalInventario;
        private Carrito MiCarrito;

        public SistemaTienda()
        {
            Inventario = new Producto[10]; // Tamaño fijo para el inventario
            TotalInventario = 0;

            Inventario[0] = new Producto("77501", "Leche Gloria 1L", 4.50, false, 20);
            Inventario[1] = new Producto("77502", "Agua San Mateo 700ml", 2.50, false, 15);
            Inventario[2] = new Producto("77503", "Cerveza Cusqueña 620ml", 8.90, true, 10);
            TotalInventario = 3;

            MiCarrito = new Carrito();
        }

        public void MostrarCatalogo()
        {
            Console.WriteLine("--- PRODUCTOS EN EL SISTEMA ---");
            for (int i = 0; i < TotalInventario; i++)
            {
                Console.WriteLine($"{Inventario[i].MostrarDetalle()} | Stock: {Inventario[i].Stock}");
            }
        }

        public Producto BuscarProducto(string codigo)
        {
            for (int i = 0; i < TotalInventario; i++)
            {
                if (Inventario[i].Codigo == codigo)
                {
                    return Inventario[i];
                }
            }
            return null;
        }

        public void EscanearProducto(string codigo)
        {
            Producto productoEncontrado = BuscarProducto(codigo);

            if (productoEncontrado != null)
            {
                if (productoEncontrado.RequiereMayorEdad)
                {
                    Console.WriteLine($"[ALERTA]: '{productoEncontrado.Nombre}' requiere validación de edad.");
                    Console.Write("¿El cliente es mayor de edad? (s/n): ");
                    string respuesta = Console.ReadLine();

                    if (respuesta.ToLower() == "s")
                    {
                        //--- VALIDACION DE DNI DENTRO DEL ESCANEO ---
                        string dni = "";
                        bool dniValido = false;

                        while (!dniValido)
                        {
                            Console.WriteLine("Ingrese su DNI porfavor:");
                            dni = Console.ReadLine();
                            if (dni != null && dni.Length == 8)
                            {
                                dniValido = true;
                                Console.WriteLine("[SUPERVISOR]: Aprobado ✓ | dni { dni} registrado.");
                                MiCarrito.AgregarProducto(productoEncontrado);
                            }
                            else
                            {
                                Console.WriteLine(" [SUPERVISOR] X Error. DNI inválido. Debe tener 8 dígitos.");
                            }
                        }

                    }
                    else
                    {
                        Console.WriteLine(" [SUPERVISOR] Venta denegada. Producto no agregado.");
                    }
                }
                else
                {
                    MiCarrito.AgregarProducto(productoEncontrado);
                }
            }
            else
            {
                Console.WriteLine(" [SUPERVISOR] X Código no encontrado en el sistema.");
            }
        }
        public void VerCarritoActual()
        {
            MiCarrito.MostrarCarrito();
            Console.WriteLine($"\nTotal parcial hasta el momento: S/. {MiCarrito.CalcularTotal():F2}");
        }

        public void EliminarDelCarrito(string codigo)
        {
            MiCarrito.EliminarProducto(codigo);
        }

        public void PagarCompra()
        {
            MiCarrito.MostrarCarrito();
            double totalFinal = MiCarrito.CalcularTotal();
            Console.WriteLine($"\n========================================");
            Console.WriteLine($" TOTAL A PAGAR: S/. {totalFinal:F2}");
            Console.WriteLine($"========================================");
            Console.WriteLine("¡Gracias por su compra en Metro! Vuelva pronto.");
        }

        public void IniciarMenu()
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("    SISTEMA AUTOCOBRO - METRO          ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Ver catálogo de productos");
                Console.WriteLine("2. Escanear / Agregar producto");
                Console.WriteLine("3. Ver carrito de compras actual");
                Console.WriteLine("4. Eliminar producto del carrito");
                Console.WriteLine("5. Pagar y finalizar compra");
                Console.WriteLine("6. Salir");
                Console.Write("\nElige una opción (1-6): ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        MostrarCatalogo();
                        break;
                    case "2":
                        Console.Write("Ingresa el código del producto a escanear: ");
                        string codAgregar = Console.ReadLine();
                        EscanearProducto(codAgregar);
                        break;
                    case "3":
                        VerCarritoActual();
                        break;
                    case "4":
                        Console.Write("Ingresa el código del producto a eliminar: ");
                        string codEliminar = Console.ReadLine();
                        EliminarDelCarrito(codEliminar);
                        break;
                    case "5":
                        PagarCompra();
                        salir = true;
                        break;
                    case "6":
                        salir = true;
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("X Opción inválida. Elige un número del 1 al 6.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
    }
}