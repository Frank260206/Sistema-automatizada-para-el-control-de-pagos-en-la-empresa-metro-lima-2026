using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo_Proyecto_Grupal
{
    public class SistemaTienda
    {
        private productos[] Inventario;
        private int TotalInventario;
        private Carrito MiCarrito;

        public SistemaTienda()
        {
            Inventario = new productos[10]; // Tamaño fijo para el inventario
            TotalInventario = 0;

            Inventario[0] = new productos("77501", "Leche Gloria 1L", 4.50, false, 20);
            Inventario[1] = new productos("77502", "Agua San Mateo 700ml", 2.50, false, 15);
            Inventario[2] = new productos("77503", "Cerveza Cusqueña 620ml", 8.90, true, 10);
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

        public productos BuscarProducto(string codigo)
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
        // 1. ESCANEAR / AGREGAR CON PIN SUPERVISOR Y DNI
        public void EscanearProducto(string codigo)
        {
            productos productoEncontrado = BuscarProducto(codigo);

            if (productoEncontrado != null)
            {
                if (productoEncontrado.RequiereMayorEdad)
                {
                    Console.WriteLine($"[ALERTA +18]: '{productoEncontrado.Nombre}' requiere validación de edad.");
                    Console.Write("Solicite al supervisor ingresar PIN (4231): ");
                    string pin = Console.ReadLine();

                    if (pin == "4231")
                    {
                        Console.Write(" [SUPERVISOR]: PIN correcto ");
                        string dni = "";
                        bool dniValido = false;

                        while (!dniValido)
                        {
                            Console.WriteLine("Ingrese el DNI del cliente (8 dígitos):");
                            dni = Console.ReadLine();
                            if (dni != null && dni.Length == 8 && long.TryParse(dni, out _))
                            {
                                dniValido = true;
                                Console.WriteLine("[SISTEMA]: DNI registrado correctamente.");
                                MiCarrito.AgregarProducto(productoEncontrado);
                            }
                            else
                            {
                                Console.WriteLine(" [SUPERVISOR] X Error: El DNI debe tener exactamente 8 dígitos numéricos.");
                            }
                        }

                    }
                    else
                    {
                        Console.WriteLine(" [SUPERVISOR] X PIN Incorrecto. Venta denegada.");
                    }
                }
                else
                {
                    MiCarrito.AgregarProducto(productoEncontrado);
                    Console.WriteLine($"-> Producto '{productoEncontrado.Nombre}' agregado a la canasta.");
                }
            
            }
            else
            {
                Console.WriteLine(" [SUPERVISOR] X Código de barras no encontrado en el catálogo.");
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
        // 2. MODULARIZACIÓN DEL ORDENAMIENTO POR MÉTODO BURBUJA
        public void OrdenarCarritoPorPrecio()
        {
            MiCarrito.OrdenarCarritoPorBurbuja();
            Console.WriteLine("Carrito Ordenado por precio de menor a mayor.");
            VerCarritoActual();
        }
        // 3. PROCESO FISCAL COMPLETO (DESCUENTO, IGV 18% Y BONUS)
        public bool PagarCompra()
        {
            MiCarrito.MostrarCarrito();
            double totalBruto = MiCarrito.CalcularTotal();

            if (totalBruto == 0)
            {
                Console.WriteLine("No hay productos en el carrito para pagar.");
                return false;
            }
            //Descuento del 5% por compras mayores a S/. 150
            double descuento = (totalBruto > 150) ? totalBruto * 0.05 : 0;
            double totalPagar = totalBruto - descuento;
            double opGravada = totalPagar / 1.18;
            double igv = totalPagar - opGravada;
            int puntosBonus = (int)(totalPagar / 10);
            //Puntos Bonus (1 punto por cada S/. 10)

            Console.WriteLine("\n Ingrese tipo de comprobante (1: Boleta / 2: Factura: )");
            string tipoComprobante = Console.ReadLine();

            Console.Write("Ingrese numero de documento DNI o RUC: ");
            string docCliente = Console.ReadLine();
            Console.WriteLine("\n=====================================================");
            Console.WriteLine("        SUPERMERCADOS METRO - CENCOSUD PERU          ");
            Console.WriteLine("          COMPROBANTE DE PAGO ELECTRONICO            ");
            Console.WriteLine("=====================================================");
            Console.WriteLine($"Tipo / Doc. Cliente : {(tipoComprobante == "2" ? "Factura" : "Boleta")} - {docCliente}");
            Console.WriteLine($"Subtotal Bruto      : S/. {totalBruto:F2}");
            Console.WriteLine($"Descuento Aplicado  : S/. {descuento:F2}");
            Console.WriteLine($"Operación Gravada   : S/. {opGravada:F2}");
            Console.WriteLine($"IGV (18%)           : S/. {igv:F2}");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine($"TOTAL A PAGAR       : S/. {totalPagar:F2}");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine($"Puntos Bonus Ganados: +{puntosBonus} pts acumulados.");
            Console.WriteLine("Estado Transacción  : APROBADA - #METRO2026");
            Console.WriteLine("=====================================================");
            Console.WriteLine("¡Gracias por su compra en Metro! Vuelva pronto.");
            return true;
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
                Console.WriteLine("3. Buscar producto en el catalogo por codigo");
                Console.WriteLine("4. Ver carrito de compras actual y subtotal acumulado");
                Console.WriteLine("5. Eliminar producto del carrito");
                Console.WriteLine("6. Ordenar Productos por precio");
                Console.WriteLine("7. Procesar el pago y emitir comprobante electronico");
                Console.WriteLine("8. Salir");
                Console.Write("\nElige una opción (1-8): ");

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
                        Console.Write("Ingresa el codigo del producto a buscar: ");
                        string codBuscar = Console.ReadLine();
                        productos p = BuscarProducto(codBuscar);
                        if (p != null)
                            Console.WriteLine($"Producto encontrado: {p.MostrarDetalle()}");
                        else
                            Console.WriteLine("X Producto no encontrado en el catálogo.");
                        break;
                    case "4":
                        VerCarritoActual();
                        break;
                    case "5":
                        Console.Write("Ingresa el código del producto a eliminar: ");
                        string codEliminar = Console.ReadLine();
                        EliminarDelCarrito(codEliminar);
                        break;
                    case "6":
                        OrdenarCarritoPorPrecio();
                        break;
                    case "7":
                        bool exito = PagarCompra();
                        if (exito)
                        {
                            salir = true;                     
                        }
                        break;
                    case "8":
                        salir = true;
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("X Opción inválida. Elige un número del 1 al 8.");
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