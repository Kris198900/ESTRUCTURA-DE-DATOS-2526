// ============================================================
// Archivo    : TorneoService.cs
// Descripcion: Clase de servicio que gestiona el registro de equipos y jugadores
// Universidad Estatal Amazonica - Torneo de Futbol UEA
// ============================================================

using System;
using System.Collections.Generic;
using TorneoFutbol.Models;

namespace TorneoFutbol.Services
{
    public class TorneoService
    {
        private Dictionary<string, Equipo> equipos;

        public TorneoService()
        {
            equipos = new Dictionary<string, Equipo>();
        }

        // Método para registrar un equipo
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

        // Método para registrar un jugador en un equipo
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

        // Método para mostrar los jugadores de un equipo
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

        // Método para eliminar un jugador de un equipo
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

        // Método para eliminar un equipo
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