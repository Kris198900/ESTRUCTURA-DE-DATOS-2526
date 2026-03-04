using System.Collections.Generic;

namespace TorneoFutbol
{
    public class Equipo
    {
        public const int MaxJugadores = 15;
        public string Nombre { get; private set; }
        public HashSet<Jugador> Jugadores { get; private set; }

        public static Dictionary<string, Equipo> Equipos { get; private set; }
            = new Dictionary<string, Equipo>(StringComparer.OrdinalIgnoreCase);

        public Equipo(string nombre)
        {
            Nombre = nombre;
            Jugadores = new HashSet<Jugador>();
        }

        public bool AgregarJugador(Jugador jugador)
        {
            if (Jugadores.Count >= MaxJugadores) return false;
            return Jugadores.Add(jugador);
        }

        public void MostrarJugadores()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"\n  ** Equipo: {Nombre}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"  {"",62}".Replace(' ', '-').Substring(0, 60));
            Console.ResetColor();

            foreach (var j in Jugadores)
            {
                j.Mostrar();
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"  Total: {Jugadores.Count}/{MaxJugadores} jugadores");
            Console.ResetColor();
        }

        public static bool RegistrarEquipo(string nombre)
        {
            if (Equipos.ContainsKey(nombre)) return false;
            Equipos[nombre] = new Equipo(nombre);
            return true;
        }
    }
}