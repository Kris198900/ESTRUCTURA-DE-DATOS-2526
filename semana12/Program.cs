// ============================================================
// Titulo     : Torneo de Fútbol Universidad Estatal Amazónica
// Descripcion: Implementación de equipos y jugadores para el torneo
// Fecha      : [Coloca la fecha actual]
// ============================================================

using System;  // <-- Las declaraciones using deben ir al principio
using TorneoFutbol;  // <-- Las declaraciones using deben ir al principio

namespace TorneoFutbol
{
    class Program
    {
        static void Main(string[] args)
        {
            TorneoService torneoService = new TorneoService();
            bool seguir = true;

            while (seguir)
            {
                Console.WriteLine("\nTorneo de Futbol Universidad Estatal Amazonica");
                Console.WriteLine("1. Registrar Equipo");
                Console.WriteLine("2. Registrar Jugador");
                Console.WriteLine("3. Mostrar Jugadores de un Equipo");
                Console.WriteLine("4. Eliminar Jugador");
                Console.WriteLine("5. Eliminar Equipo");
                Console.WriteLine("6. Salir");
                Console.Write("Seleccione una opcion: ");
                
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("Ingrese el nombre del equipo:");
                        string nombreEquipo = Console.ReadLine();
                        torneoService.RegistrarEquipo(nombreEquipo);
                        break;

                    case "2":
                        Console.WriteLine("Ingrese el nombre del equipo para registrar un jugador:");
                        nombreEquipo = Console.ReadLine();
                        Console.WriteLine("Ingrese el nombre del jugador:");
                        string nombreJugador = Console.ReadLine();
                        Console.WriteLine("Ingrese la edad del jugador:");
                        int edad = int.Parse(Console.ReadLine());
                        Console.WriteLine("Ingrese la posicion del jugador:");
                        string posicion = Console.ReadLine();
                        torneoService.RegistrarJugador(nombreEquipo, nombreJugador, edad, posicion);
                        break;

                    case "3":
                        Console.WriteLine("Ingrese el nombre del equipo para mostrar los jugadores:");
                        nombreEquipo = Console.ReadLine();
                        torneoService.MostrarJugadoresEquipo(nombreEquipo);
                        break;

                    case "4":
                        Console.WriteLine("Ingrese el nombre del equipo para eliminar un jugador:");
                        nombreEquipo = Console.ReadLine();
                        Console.WriteLine("Ingrese el nombre del jugador a eliminar:");
                        string nombreJugadorEliminar = Console.ReadLine();
                        torneoService.EliminarJugador(nombreEquipo, nombreJugadorEliminar);
                        break;

                    case "5":
                        Console.WriteLine("Ingrese el nombre del equipo a eliminar:");
                        nombreEquipo = Console.ReadLine();
                        torneoService.EliminarEquipo(nombreEquipo);
                        break;

                    case "6":
                        seguir = false;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opcion no valida.");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}