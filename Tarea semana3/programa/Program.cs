using System;

namespace RegistroEstudiantes
{
    // Clase Estudiante que encapsula todos los datos del estudiante
    public class Estudiante
    {
        // Propiedades del estudiante
        public string Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        
        // Array para almacenar exactamente 3 números de teléfono
        public string[] Telefonos { get; set; }

        // Constructor que inicializa el array de teléfonos con capacidad para 3 elementos
        public Estudiante()
        {
            Telefonos = new string[3];
        }

        // Método para capturar todos los datos del estudiante
        public void RegistrarDatos()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════════════");
            Console.WriteLine("       REGISTRO DE ESTUDIANTE - NUEVO          ");
            Console.WriteLine("═══════════════════════════════════════════════\n");

            Console.Write("► ID del estudiante: ");
            Id = Console.ReadLine();

            Console.Write("► Nombres: ");
            Nombres = Console.ReadLine();

            Console.Write("► Apellidos: ");
            Apellidos = Console.ReadLine();

            Console.Write("► Dirección: ");
            Direccion = Console.ReadLine();

            // Capturar los 3 números de teléfono usando el array
            Console.WriteLine("\n─── NÚMEROS DE TELÉFONO ───");
            for (int i = 0; i < Telefonos.Length; i++)
            {
                Console.Write($"► Teléfono #{i + 1}: ");
                Telefonos[i] = Console.ReadLine();
            }

            Console.WriteLine("\n✓ ¡Registro completado exitosamente!\n");
        }

        // Método para mostrar toda la información del estudiante
        public void MostrarDatos()
        {
            Console.WriteLine("\n═══════════════════════════════════════════════");
            Console.WriteLine("         INFORMACIÓN DEL ESTUDIANTE            ");
            Console.WriteLine("═══════════════════════════════════════════════\n");

            Console.WriteLine($"ID:          {Id}");
            Console.WriteLine($"Nombres:     {Nombres}");
            Console.WriteLine($"Apellidos:   {Apellidos}");
            Console.WriteLine($"Dirección:   {Direccion}");
            
            Console.WriteLine("\n─── TELÉFONOS ───");
            
            // Recorrer el array de teléfonos usando un bucle for
            for (int i = 0; i < Telefonos.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(Telefonos[i]))
                {
                    Console.WriteLine($"  {i + 1}. {Telefonos[i]}");
                }
                else
                {
                    Console.WriteLine($"  {i + 1}. (no registrado)");
                }
            }
            
            Console.WriteLine("\n═══════════════════════════════════════════════\n");
        }

        // Método para verificar si hay datos registrados
        public bool TieneDatos()
        {
            return !string.IsNullOrWhiteSpace(Id);
        }

        // Método para actualizar un teléfono específico
        public void ActualizarTelefono()
        {
            Console.WriteLine("\n─── ACTUALIZAR TELÉFONO ───");
            Console.WriteLine("\nTeléfonos actuales:");
            
            for (int i = 0; i < Telefonos.Length; i++)
            {
                string valor = string.IsNullOrWhiteSpace(Telefonos[i]) ? "(vacío)" : Telefonos[i];
                Console.WriteLine($"  {i + 1}. {valor}");
            }
            
            Console.Write("\n¿Cuál teléfono desea actualizar? (1-3): ");
            string opcion = Console.ReadLine();
            
            if (int.TryParse(opcion, out int indice) && indice >= 1 && indice <= 3)
            {
                Console.Write($"Ingrese el nuevo número para Teléfono #{indice}: ");
                Telefonos[indice - 1] = Console.ReadLine();
                Console.WriteLine("\n✓ Teléfono actualizado correctamente.\n");
            }
            else
            {
                Console.WriteLine("\n✗ Opción inválida. Debe ser un número entre 1 y 3.\n");
            }
        }
    }

    // Clase principal del programa
    class Program
    {
        static void Main(string[] args)
        {
            // Crear una instancia del estudiante
            Estudiante estudiante = new Estudiante();
            bool continuar = true;

            // Mensaje de bienvenida
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║                                               ║");
            Console.WriteLine("║    SISTEMA DE GESTIÓN DE ESTUDIANTES - C#     ║");
            Console.WriteLine("║                                               ║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝");
            
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();

            // Bucle principal del programa
            while (continuar)
            {
                Console.Clear();
                MostrarMenu();
                
                Console.Write("\n► Seleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        estudiante.RegistrarDatos();
                        break;

                    case "2":
                        if (!estudiante.TieneDatos())
                        {
                            Console.WriteLine("\n✗ Error: No hay ningún estudiante registrado aún.");
                            Console.WriteLine("   Por favor, registre un estudiante primero (Opción 1).\n");
                        }
                        else
                        {
                            estudiante.MostrarDatos();
                        }
                        break;

                    case "3":
                        if (!estudiante.TieneDatos())
                        {
                            Console.WriteLine("\n✗ Error: No hay ningún estudiante registrado aún.");
                            Console.WriteLine("   Por favor, registre un estudiante primero (Opción 1).\n");
                        }
                        else
                        {
                            estudiante.ActualizarTelefono();
                        }
                        break;

                    case "4":
                        Console.Clear();
                        Console.WriteLine("\n╔═══════════════════════════════════════════════╗");
                        Console.WriteLine("║                                               ║");
                        Console.WriteLine("║   ¡Gracias por usar el sistema!               ║");
                        Console.WriteLine("║   Programa desarrollado en C#                 ║");
                        Console.WriteLine("║                                               ║");
                        Console.WriteLine("╚═══════════════════════════════════════════════╝\n");
                        continuar = false;
                        break;

                    default:
                        Console.WriteLine("\n✗ Opción inválida. Por favor, seleccione una opción del 1 al 4.\n");
                        break;
                }

                // Pausa antes de volver al menú
                if (continuar)
                {
                    Console.WriteLine("Presione cualquier tecla para volver al menú...");
                    Console.ReadKey();
                }
            }
        }

        // Método para mostrar el menú principal
        static void MostrarMenu()
        {
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║              MENÚ PRINCIPAL                   ║");
            Console.WriteLine("╠═══════════════════════════════════════════════╣");
            Console.WriteLine("║                                               ║");
            Console.WriteLine("║  1. Registrar nuevo estudiante                ║");
            Console.WriteLine("║  2. Mostrar información del estudiante        ║");
            Console.WriteLine("║  3. Actualizar número de teléfono             ║");
            Console.WriteLine("║  4. Salir del programa                        ║");
            Console.WriteLine("║                                               ║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝");
        }
    }
}