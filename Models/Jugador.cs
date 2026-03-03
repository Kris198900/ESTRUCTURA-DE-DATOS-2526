// ============================================================
// Archivo    : Jugador.cs
// Descripcion: Clase que representa un jugador de futbol
// Universidad Estatal Amazonica - Torneo de Futbol UEA
// ============================================================

namespace TorneoFutbol.Models
{
    /// <summary>
    /// Representa un jugador dentro del torneo.
    /// </summary>
    public class Jugador
    {
        // ─── Propiedades ──────────────────────────────────────────

        /// <summary>Nombre del jugador.</summary>
        public string Nombre { get; set; }

        /// <summary>Edad del jugador.</summary>
        public int Edad { get; set; }

        /// <summary>Posicion del jugador en el campo.</summary>
        public string Posicion { get; set; }

        // ─── Constructor ──────────────────────────────────────────

        public Jugador(string nombre, int edad, string posicion)
        {
            Nombre = nombre;
            Edad = edad;
            Posicion = posicion;
        }

        // ─── Metodos ──────────────────────────────────────────────

        /// <summary>
        /// Muestra la informacion del jugador con formato.
        /// </summary>
        public void Mostrar()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  - {Nombre}, Edad: {Edad}, Posicion: {Posicion}");
            Console.ResetColor();
        }
    }
}