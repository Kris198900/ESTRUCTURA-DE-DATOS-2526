using System;

namespace SistemaAsignacionAsientos
{
    class Program
    {
        static void Main(string[] args)
        {
            SistemaRegistroCongreso sistema = new SistemaRegistroCongreso();
            int opcion;
            
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║   SISTEMA DE ASIGNACIÓN DE ASIENTOS - CONGRESO 2026       ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");

            do
            {
                Console.WriteLine("\n┌────────────────────────────────────────┐");
                Console.WriteLine("│           MENÚ PRINCIPAL               │");
                Console.WriteLine("├────────────────────────────────────────┤");
                Console.WriteLine("│ 1. Registrar nuevo asistente           │");
                Console.WriteLine("│ 2. Mostrar lista de asistentes         │");
                Console.WriteLine("│ 3. Ver estadísticas                    │");
                Console.WriteLine("│ 4. Salir                               │");
                Console.WriteLine("└────────────────────────────────────────┘");
                Console.Write("\nSeleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("\n[ERROR] Por favor ingrese un número válido.");
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        Console.Write("\nIngrese el nombre del asistente: ");
                        string nombre = Console.ReadLine();
                        sistema.RegistrarAsistente(nombre);
                        break;

                    case 2:
                        sistema.MostrarAsistentes();
                        break;

                    case 3:
                        Console.WriteLine("\n┌────────────────────────────────────────┐");
                        Console.WriteLine("│         ESTADÍSTICAS DEL CONGRESO      │");
                        Console.WriteLine("├────────────────────────────────────────┤");
                        Console.WriteLine($"│ Asistentes registrados: {sistema.ObtenerCantidadAsistentes(),14} │");
                        Console.WriteLine($"│ Asientos disponibles:   {sistema.ObtenerAsientosDisponibles(),14} │");
                        Console.WriteLine($"│ Capacidad total:        {100,14} │");
                        Console.WriteLine("└────────────────────────────────────────┘");
                        break;

                    case 4:
                        Console.WriteLine("\n¡Gracias por utilizar el sistema!");
                        Console.WriteLine("Saliendo...\n");
                        break;

                    default:
                        Console.WriteLine("\n[ERROR] Opción no válida. Intente nuevamente.");
                        break;
                }

            } while (opcion != 4);
        }
    }
}