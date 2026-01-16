using System;

namespace Semana06
{
    // Clase Vehículo
    public class Vehiculo
    {
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Año { get; set; }
        public decimal Precio { get; set; }

        public Vehiculo(string placa, string marca, string modelo, int año, decimal precio)
        {
            Placa = placa;
            Marca = marca;
            Modelo = modelo;
            Año = año;
            Precio = precio;
        }

        public void MostrarDatos()
        {
            Console.WriteLine($"Placa: {Placa}");
            Console.WriteLine($"Marca: {Marca}");
            Console.WriteLine($"Modelo: {Modelo}");
            Console.WriteLine($"Año: {Año}");
            Console.WriteLine($"Precio: ${Precio:N2}");
        }
    }

    // Clase Nodo para la lista enlazada de vehículos
    public class NodoVehiculo
    {
        public Vehiculo Vehiculo { get; set; }
        public NodoVehiculo Siguiente { get; set; }

        public NodoVehiculo(Vehiculo vehiculo)
        {
            Vehiculo = vehiculo;
            Siguiente = null;
        }
    }

    // Clase Lista Enlazada de Vehículos
    public class ListaVehiculos
    {
        private NodoVehiculo cabeza;

        public ListaVehiculos()
        {
            cabeza = null;
        }

        // a. Agregar vehículo
        public void AgregarVehiculo(Vehiculo vehiculo)
        {
            NodoVehiculo nuevoNodo = new NodoVehiculo(vehiculo);

            if (cabeza == null)
            {
                cabeza = nuevoNodo;
            }
            else
            {
                NodoVehiculo actual = cabeza;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevoNodo;
            }

            Console.WriteLine($"\n✓ Vehículo con placa {vehiculo.Placa} agregado exitosamente.");
        }

        // b. Buscar vehículo por placa
        public void BuscarPorPlaca(string placa)
        {
            NodoVehiculo actual = cabeza;

            while (actual != null)
            {
                if (actual.Vehiculo.Placa.ToUpper() == placa.ToUpper())
                {
                    Console.WriteLine("\n=== VEHÍCULO ENCONTRADO ===");
                    actual.Vehiculo.MostrarDatos();
                    Console.WriteLine(new string('=', 30));
                    return;
                }
                actual = actual.Siguiente;
            }

            Console.WriteLine($"\n✗ No se encontró ningún vehículo con la placa: {placa}");
        }

        // c. Ver vehículos por año
        public void VerVehiculosPorAño(int año)
        {
            NodoVehiculo actual = cabeza;
            bool encontrado = false;
            int contador = 0;

            Console.WriteLine($"\n=== VEHÍCULOS DEL AÑO {año} ===");

            while (actual != null)
            {
                if (actual.Vehiculo.Año == año)
                {
                    contador++;
                    Console.WriteLine($"\n{contador}.");
                    actual.Vehiculo.MostrarDatos();
                    Console.WriteLine(new string('-', 30));
                    encontrado = true;
                }
                actual = actual.Siguiente;
            }

            if (!encontrado)
            {
                Console.WriteLine($"No hay vehículos registrados del año {año}.");
            }
            else
            {
                Console.WriteLine($"\nTotal de vehículos del año {año}: {contador}");
            }
        }

        // d. Ver todos los vehículos registrados
        public void VerTodosLosVehiculos()
        {
            if (cabeza == null)
            {
                Console.WriteLine("\n✗ No hay vehículos registrados en el estacionamiento.");
                return;
            }

            NodoVehiculo actual = cabeza;
            int contador = 0;

            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("TODOS LOS VEHÍCULOS REGISTRADOS");
            Console.WriteLine(new string('=', 60));

            while (actual != null)
            {
                contador++;
                Console.WriteLine($"\n{contador}.");
                actual.Vehiculo.MostrarDatos();
                Console.WriteLine(new string('-', 60));
                actual = actual.Siguiente;
            }

            Console.WriteLine($"\nTotal de vehículos registrados: {contador}");
        }

        // e. Eliminar vehículo por placa
        public void EliminarVehiculo(string placa)
        {
            if (cabeza == null)
            {
                Console.WriteLine("\n✗ La lista está vacía.");
                return;
            }

            // Si el vehículo a eliminar es el primero
            if (cabeza.Vehiculo.Placa.ToUpper() == placa.ToUpper())
            {
                Console.WriteLine($"\n✓ Vehículo con placa {cabeza.Vehiculo.Placa} eliminado exitosamente.");
                cabeza = cabeza.Siguiente;
                return;
            }

            // Buscar el vehículo en el resto de la lista
            NodoVehiculo actual = cabeza;
            while (actual.Siguiente != null)
            {
                if (actual.Siguiente.Vehiculo.Placa.ToUpper() == placa.ToUpper())
                {
                    Console.WriteLine($"\n✓ Vehículo con placa {actual.Siguiente.Vehiculo.Placa} eliminado exitosamente.");
                    actual.Siguiente = actual.Siguiente.Siguiente;
                    return;
                }
                actual = actual.Siguiente;
            }

            Console.WriteLine($"\n✗ No se encontró ningún vehículo con la placa: {placa}");
        }
    }

    public class Ejercicio7
    {
        public static void Ejecutar()
        {
            ListaVehiculos estacionamiento = new ListaVehiculos();
            bool continuar = true;

            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║   SISTEMA DE REGISTRO DE ESTACIONAMIENTO                   ║");
            Console.WriteLine("║   Área de Ingeniería de Sistemas                           ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");

            while (continuar)
            {
                Console.WriteLine("\n" + new string('=', 60));
                Console.WriteLine("MENÚ PRINCIPAL");
                Console.WriteLine(new string('=', 60));
                Console.WriteLine("1. Agregar vehículo");
                Console.WriteLine("2. Buscar vehículo por placa");
                Console.WriteLine("3. Ver vehículos por año");
                Console.WriteLine("4. Ver todos los vehículos registrados");
                Console.WriteLine("5. Eliminar vehículo");
                Console.WriteLine("6. Salir");
                Console.WriteLine(new string('=', 60));
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        AgregarVehiculo(estacionamiento);
                        break;
                    case "2":
                        BuscarVehiculo(estacionamiento);
                        break;
                    case "3":
                        VerPorAño(estacionamiento);
                        break;
                    case "4":
                        estacionamiento.VerTodosLosVehiculos();
                        break;
                    case "5":
                        EliminarVehiculo(estacionamiento);
                        break;
                    case "6":
                        continuar = false;
                        Console.WriteLine("\n✓ Gracias por usar el sistema. ¡Hasta pronto!");
                        break;
                    default:
                        Console.WriteLine("\n✗ Opción no válida. Intente nuevamente.");
                        break;
                }

                if (continuar)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void AgregarVehiculo(ListaVehiculos estacionamiento)
        {
            Console.WriteLine("\n--- AGREGAR NUEVO VEHÍCULO ---");

            Console.Write("Placa: ");
            string placa = Console.ReadLine();

            Console.Write("Marca: ");
            string marca = Console.ReadLine();

            Console.Write("Modelo: ");
            string modelo = Console.ReadLine();

            Console.Write("Año: ");
            int año = int.Parse(Console.ReadLine());

            Console.Write("Precio: $");
            decimal precio = decimal.Parse(Console.ReadLine());

            Vehiculo nuevoVehiculo = new Vehiculo(placa, marca, modelo, año, precio);
            estacionamiento.AgregarVehiculo(nuevoVehiculo);
        }

        static void BuscarVehiculo(ListaVehiculos estacionamiento)
        {
            Console.WriteLine("\n--- BUSCAR VEHÍCULO ---");
            Console.Write("Ingrese la placa del vehículo: ");
            string placa = Console.ReadLine();
            estacionamiento.BuscarPorPlaca(placa);
        }

        static void VerPorAño(ListaVehiculos estacionamiento)
        {
            Console.WriteLine("\n--- VER VEHÍCULOS POR AÑO ---");
            Console.Write("Ingrese el año: ");
            int año = int.Parse(Console.ReadLine());
            estacionamiento.VerVehiculosPorAño(año);
        }

        static void EliminarVehiculo(ListaVehiculos estacionamiento)
        {
            Console.WriteLine("\n--- ELIMINAR VEHÍCULO ---");
            Console.Write("Ingrese la placa del vehículo a eliminar: ");
            string placa = Console.ReadLine();
            estacionamiento.EliminarVehiculo(placa);
        }
    }
}
