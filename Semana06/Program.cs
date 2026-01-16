using System;

namespace Semana06
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║        EJERCICIOS DE LISTAS ENLAZADAS - SEMANA 06         ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.WriteLine("\nSeleccione el ejercicio que desea ejecutar:");
            Console.WriteLine("1. Ejercicio 5 - Listas de Primos y Armstrong");
            Console.WriteLine("2. Ejercicio 7 - Sistema de Estacionamiento");
            Console.WriteLine("3. Salir");
            Console.Write("\nOpción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Clear();
                    Ejercicio5.Ejecutar();
                    break;
                case "2":
                    Console.Clear();
                    Ejercicio7.Ejecutar();
                    break;
                case "3":
                    Console.WriteLine("\n¡Hasta pronto!");
                    break;
                default:
                    Console.WriteLine("\nOpción no válida.");
                    break;
            }

            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}