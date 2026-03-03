using System;
using TorneoFutbol.Services;

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

                string opcion = Console.ReadLine() ?? string.Empty;

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("Ingrese el nombre del equipo:");
                        string nombreEquipo = Console.ReadLine() ?? string.Empty;

                        if (string.IsNullOrEmpty(nombreEquipo))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("El nombre del equipo no puede estar vacio.");
                            Console.ResetColor();
                            break;
                        }
                        torneoService.RegistrarEquipo(nombreEquipo);
                        break;

                    case "2":
                        Console.WriteLine("Ingrese el nombre del equipo para registrar un jugador:");
                        nombreEquipo = Console.ReadLine() ?? string.Empty;

                        if (string.IsNullOrEmpty(nombreEquipo))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("El nombre del equipo no puede estar vacio.");
                            Console.ResetColor();
                            break;
                        }

                        Console.WriteLine("Ingrese el nombre del jugador:");
                        string nombreJugador = Console.ReadLine() ?? string.Empty;

                        if (string.IsNullOrEmpty(nombreJugador))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("El nombre del jugador no puede estar vacio.");
                            Console.ResetColor();
                            break;
                        }

                        Console.WriteLine("Ingrese la edad del jugador:");
                        int edad;
                        if (!int.TryParse(Console.ReadLine() ?? string.Empty, out edad) || edad <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Por favor ingrese una edad valida.");
                            Console.ResetColor();
                            break;
                        }

                        Console.WriteLine("Ingrese la posicion del jugador:");
                        string posicion = Console.ReadLine() ?? string.Empty;

                        if (string.IsNullOrEmpty(posicion))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("La posicion del jugador no puede estar vacia.");
                            Console.ResetColor();
                            break;
                        }

                        torneoService.RegistrarJugador(nombreEquipo, nombreJugador, edad, posicion);
                        break;

                    case "3":
                        Console.WriteLine("Ingrese el nombre del equipo para mostrar los jugadores:");
                        nombreEquipo = Console.ReadLine() ?? string.Empty;

                        if (string.IsNullOrEmpty(nombreEquipo))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("El nombre del equipo no puede estar vacio.");
                            Console.ResetColor();
                            break;
                        }
                        torneoService.MostrarJugadoresEquipo(nombreEquipo);
                        break;

                    case "4":
                        Console.WriteLine("Ingrese el nombre del equipo para eliminar un jugador:");
                        nombreEquipo = Console.ReadLine() ?? string.Empty;

                        if (string.IsNullOrEmpty(nombreEquipo))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("El nombre del equipo no puede estar vacio.");
                            Console.ResetColor();
                            break;
                        }

                        Console.WriteLine("Ingrese el nombre del jugador a eliminar:");
                        string nombreJugadorEliminar = Console.ReadLine() ?? string.Empty;

                        if (string.IsNullOrEmpty(nombreJugadorEliminar))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("El nombre del jugador no puede estar vacio.");
                            Console.ResetColor();
                            break;
                        }
                        torneoService.EliminarJugador(nombreEquipo, nombreJugadorEliminar);
                        break;

                    case "5":
                        Console.WriteLine("Ingrese el nombre del equipo a eliminar:");
                        nombreEquipo = Console.ReadLine() ?? string.Empty;

                        if (string.IsNullOrEmpty(nombreEquipo))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("El nombre del equipo no puede estar vacio.");
                            Console.ResetColor();
                            break;
                        }
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