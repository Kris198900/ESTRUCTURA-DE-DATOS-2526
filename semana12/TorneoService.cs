using TorneoFutbol;

namespace TorneoFutbol
{
    public class TorneoService
    {
        private Dictionary<string, Equipo> equipos;

        public TorneoService()
        {
            equipos = new Dictionary<string, Equipo>();
        }

        public void RegistrarEquipo(string nombreEquipo)
        {
            if (!equipos.ContainsKey(nombreEquipo))
            {
                equipos[nombreEquipo] = new Equipo(nombreEquipo);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Equipo {nombreEquipo} registrado con exito.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"El equipo {nombreEquipo} ya esta registrado.");
                Console.ResetColor();
            }
        }

        public void RegistrarJugador(string nombreEquipo, string nombreJugador, int edad, string posicion)
        {
            if (equipos.ContainsKey(nombreEquipo))
            {
                Jugador jugador = new Jugador(nombreJugador, edad, posicion);
                equipos[nombreEquipo].AgregarJugador(jugador);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"El equipo {nombreEquipo} no esta registrado.");
                Console.ResetColor();
            }
        }

        public void MostrarJugadoresEquipo(string nombreEquipo)
        {
            if (equipos.ContainsKey(nombreEquipo))
            {
                equipos[nombreEquipo].MostrarJugadores();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"El equipo {nombreEquipo} no esta registrado.");
                Console.ResetColor();
            }
        }

        public void EliminarJugador(string nombreEquipo, string nombreJugador)
        {
            if (equipos.ContainsKey(nombreEquipo))
            {
                bool eliminado = equipos[nombreEquipo].Jugadores.RemoveWhere(j => j.Nombre == nombreJugador) > 0;
                if (eliminado)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Jugador {nombreJugador} eliminado con exito del equipo {nombreEquipo}.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"No se encontro al jugador {nombreJugador} en el equipo {nombreEquipo}.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"El equipo {nombreEquipo} no esta registrado.");
                Console.ResetColor();
            }
        }

        public void EliminarEquipo(string nombreEquipo)
        {
            if (equipos.ContainsKey(nombreEquipo))
            {
                equipos.Remove(nombreEquipo);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Equipo {nombreEquipo} eliminado con exito.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"El equipo {nombreEquipo} no esta registrado.");
                Console.ResetColor();
            }
        }
    }
}