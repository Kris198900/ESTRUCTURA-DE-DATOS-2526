using System;

namespace RegistroEstudiantes
{
    public class Estudiante
    {
        public string Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string[] Telefonos { get; set; }

        public Estudiante()
        {
            Telefonos = new string[3];
        }

        public void RegistrarDatos()
        {
            Console.WriteLine("\n--- REGISTRO DE ESTUDIANTE ---");
            
            Console.Write("ID: ");
            Id = Console.ReadLine();

            Console.Write("Nombres: ");
            Nombres = Console.ReadLine();

            Console.Write("Apellidos: ");
            Apellidos = Console.ReadLine();

            Console.Write("Dirección: ");
            Direccion = Console.ReadLine();

            Console.WriteLine("\nTeléfonos:");
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"Teléfono {i + 1}: ");
                Telefonos[i] = Console.ReadLine();
            }

            Console.WriteLine("\nRegistro completado.\n");
        }

        public void MostrarDatos()
        {
            Console.WriteLine("\n--- DATOS DEL ESTUDIANTE ---");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Nombres: {Nombres}");
            Console.WriteLine($"Apellidos: {Apellidos}");
            Console.WriteLine($"Dirección: {Direccion}");
            Console.WriteLine("\nTeléfonos:");
            
            for (int i = 0; i < 3; i++)
            {
                string telefono = string.IsNullOrWhiteSpace(Telefonos[i]) ? "(sin registrar)" : Telefonos[i];
                Console.WriteLine($"{i + 1}. {telefono}");
            }
            Console.WriteLine();
        }

        public bool TieneDatos()
        {
            return !string.IsNullOrWhiteSpace(Id);
        }

        public void ActualizarTelefono()
        {
            Console.WriteLine("\n--- ACTUALIZAR TELÉFONO ---");
            
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"{i + 1}. {Telefonos[i]}");
            }
            
            Console.Write("\n¿Cuál desea actualizar? (1-3): ");
            if (int.TryParse(Console.ReadLine(), out int num) && num >= 1 && num <= 3)
            {
                Console.Write($"Nuevo teléfono {num}: ");
                Telefonos[num - 1] = Console.ReadLine();
                Console.WriteLine("Actualizado.\n");
            }
            else
            {
                Console.WriteLine("Opción inválida.\n");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Estudiante estudiante = new Estudiante();
            bool continuar = true;

            Console.WriteLine("SISTEMA DE GESTIÓN DE ESTUDIANTES\n");

            while (continuar)
            {
                Console.WriteLine("--- MENÚ ---");
                Console.WriteLine("1. Registrar estudiante");
                Console.WriteLine("2. Mostrar información");
                Console.WriteLine("3. Actualizar teléfono");
                Console.WriteLine("4. Salir");
                
                Console.Write("\nOpción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        estudiante.RegistrarDatos();
                        break;

                    case "2":
                        if (estudiante.TieneDatos())
                            estudiante.MostrarDatos();
                        else
                            Console.WriteLine("\nNo hay estudiante registrado.\n");
                        break;

                    case "3":
                        if (estudiante.TieneDatos())
                            estudiante.ActualizarTelefono();
                        else
                            Console.WriteLine("\nNo hay estudiante registrado.\n");
                        break;

                    case "4":
                        Console.WriteLine("\n¡Hasta luego!\n");
                        continuar = false;
                        break;

                    default:
                        Console.WriteLine("\nOpción inválida.\n");
                        break;
                }
            }
        }
    }
}