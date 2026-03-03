// ============================================================
// Archivo    : Equipo.cs
// Descripcion: Clase que representa un equipo de futbol
// Universidad Estatal Amazonica - Torneo de Futbol UEA
// ============================================================

namespace TorneoFutbol.Models
{
    /// <summary>
    /// Representa un equipo academico del torneo.
    /// Estructuras de datos utilizadas:
    ///   HashSet<Jugador>           -> jugadores sin duplicados, busqueda O(1)
    ///   Dictionary<string, Equipo> -> mapa global de equipos por nombre
    /// </summary>
    public class Equipo
    {
        // ─── Constante ────────────────────────────────────────────

        /// <summary>Maximo de jugadores permitidos por equipo.</summary>
        public const int MaxJugadores = 15;

        // ─── Propiedades ──────────────────────────────────────────

        /// <summary>Nombre unico del equipo.</summary>
        public string Nombre { get; private set; }

        /// <summary>
        /// Conjunto de jugadores del equipo.
        /// El HashSet garantiza que no existan jugadores con el mismo nombre.
        /// </summary>
        public HashSet<Jugador> Jugadores { get; private set; }

        // ─── Mapa global (Dictionary) ─────────────────────────────

        /// <summary>
        /// Diccionario que almacena todos los equipos registrados.
        /// Clave  : nombre del equipo (sin distinguir mayusculas)
        /// Valor  : instancia de Equipo
        /// </summary>
        public static Dictionary<string, Equipo> Equipos { get; private set; }
            = new Dictionary<string, Equipo>(StringComparer.OrdinalIgnoreCase);

        // ─── Constructor ──────────────────────────────────────────

        public Equipo(string nombre)
        {
            Nombre    = nombre;
            Jugadores = new HashSet<Jugador>();
        }

        // ─── Metodos de instancia ─────────────────────────────────

        /// <summary>
        /// Agrega un jugador al equipo si no existe ya y no se supero el limite.
        /// </summary>
        /// <returns>true si se agrego; false si ya existia o se llego al maximo.</returns>
        public bool AgregarJugador(Jugador jugador)
        {
            if (Jugadores.Count >= MaxJugadores) return false;
            return Jugadores.Add(jugador);  // Add devuelve false si el elemento ya existe
        }

        /// <summary>
        /// Elimina un jugador del equipo buscandolo por nombre.
        /// </summary>
        /// <returns>true si se encontro y elimino; false si no existia.</returns>
        public bool EliminarJugador(string nombre)
        {
            var jugador = Jugadores.FirstOrDefault(
                j => string.Equals(j.Nombre, nombre, StringComparison.OrdinalIgnoreCase));
            return jugador != null && Jugadores.Remove(jugador);
        }

        /// <summary>
        /// Imprime en consola todos los jugadores del equipo con colores:
        ///   Verde oscuro (DarkGreen) -> encabezado del equipo
        ///   Verde claro  (Green)     -> nombre del jugador
        ///   Blanco       (White)     -> resto de los datos
        /// </summary>
        public void MostrarJugadores()
        {
            // Encabezado en verde oscuro
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"\n  ** Equipo: {Nombre}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"  {"",62}".Replace(' ', '-').Substring(0, 60));
            Console.ResetColor();

            if (Jugadores.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("  (sin jugadores registrados)");
                Console.ResetColor();
                return;
            }

            // Ordenar por posicion y luego por nombre para mayor claridad
            foreach (var j in Jugadores.OrderBy(j => j.Posicion).ThenBy(j => j.Nombre))
                j.Mostrar();

            // Pie con total
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"  Total: {Jugadores.Count}/{MaxJugadores} jugadores");
            Console.ResetColor();
        }

        // ─── Metodos estaticos (operaciones sobre el mapa) ────────

        /// <summary>Registra un equipo en el diccionario global si el nombre es nuevo.</summary>
        public static bool RegistrarEquipo(string nombre)
        {
            if (Equipos.ContainsKey(nombre)) return false;
            Equipos[nombre] = new Equipo(nombre);
            return true;
        }

        /// <summary>Elimina un equipo completo del diccionario global.</summary>
        public static bool EliminarEquipo(string nombre) => Equipos.Remove(nombre);

        /// <summary>Obtiene un equipo por nombre; retorna null si no existe.</summary>
        public static Equipo? ObtenerEquipo(string nombre)
            => Equipos.TryGetValue(nombre, out var eq) ? eq : null;
    }
}