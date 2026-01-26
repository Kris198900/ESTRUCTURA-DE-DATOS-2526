/*
 * Práctico Experimental 2
 * Grupo: Cristian Chiquimba, Jordan Logroño, Isaac Caicedo, Xavier Lema
 * Materia: Estructura de Datos
 */

using System;
using System.Collections.Generic;

namespace SistemaAsignacionAsientos
{
    // Clase que representa a un asistente del congreso
    public class Asistente
    {
        public string Nombre { get; set; }
        public int NumeroAsiento { get; set; }

        public Asistente(string nombre, int numeroAsiento)
        {
            Nombre = nombre;
            NumeroAsiento = numeroAsiento;
        }

        public override string ToString()
        {
            return $"Asiento #{NumeroAsiento:D3} - {Nombre}";
        }
    }

    // Clase que gestiona el sistema de registro de asistentes al congreso
    public class SistemaRegistroCongreso
    {
        // Cola para gestionar el registro de asistentes en orden de llegada (FIFO)
        private Queue<Asistente> colaAsistentes;
        
        // Capacidad máxima del congreso
        private const int CAPACIDAD_MAXIMA = 100;
        
        // Contador para llevar el control de asientos asignados
        private int asientosAsignados;

        public SistemaRegistroCongreso()
        {
            colaAsistentes = new Queue<Asistente>();
            asientosAsignados = 0;
        }

        // Registra un nuevo asistente en el congreso y le asigna un asiento
        public bool RegistrarAsistente(string nombre)
        {
            // Validar que el nombre no esté vacío
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("\n[ERROR] El nombre del asistente no puede estar vacío.");
                return false;
            }

            // Verificar si hay capacidad disponible
            if (asientosAsignados >= CAPACIDAD_MAXIMA)
            {
                Console.WriteLine("\n[ERROR] No se puede registrar al asistente.");
                Console.WriteLine($"El congreso ha alcanzado su capacidad máxima de {CAPACIDAD_MAXIMA} asientos.");
                return false;
            }

            // Incrementar el contador de asientos y crear nuevo asistente
            asientosAsignados++;
            Asistente nuevoAsistente = new Asistente(nombre, asientosAsignados);
            
            // Agregar el asistente a la cola (al final)
            colaAsistentes.Enqueue(nuevoAsistente);
            
            Console.WriteLine($"\n[ÉXITO] Asistente registrado correctamente:");
            Console.WriteLine($"  Nombre: {nombre}");
            Console.WriteLine($"  Asiento asignado: #{asientosAsignados:D3}");
            Console.WriteLine($"  Asientos disponibles: {CAPACIDAD_MAXIMA - asientosAsignados}");
            
            return true;
        }

        // Muestra la lista completa de asistentes registrados con sus asientos asignados
        public void MostrarAsistentes()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("LISTA DE ASISTENTES REGISTRADOS EN EL CONGRESO");
            Console.WriteLine(new string('=', 60));

            // Verificar si hay asistentes registrados
            if (colaAsistentes.Count == 0)
            {
                Console.WriteLine("\nNo hay asistentes registrados actualmente.");
                Console.WriteLine(new string('=', 60));
                return;
            }

            Console.WriteLine($"\nTotal de asistentes: {colaAsistentes.Count} / {CAPACIDAD_MAXIMA}");
            Console.WriteLine($"Asientos disponibles: {CAPACIDAD_MAXIMA - asientosAsignados}\n");
            Console.WriteLine(new string('-', 60));

            // Recorrer la cola sin eliminar elementos
            int contador = 1;
            foreach (Asistente asistente in colaAsistentes)
            {
                Console.WriteLine($"{contador}. {asistente}");
                contador++;
            }

            Console.WriteLine(new string('=', 60));
        }

        public int ObtenerCantidadAsistentes()
        {
            return colaAsistentes.Count;
        }

        public int ObtenerAsientosDisponibles()
        {
            return CAPACIDAD_MAXIMA - asientosAsignados;
        }
    }
}