using System;

namespace RegistroEstudiantes
{
    class Program
    {
        // Definición de la clase Estudiante
        public class Estudiante
        {
            public int ID { get; set; }
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string Direccion { get; set; }
            public string[] Telefonos { get; set; } // Array para los teléfonos
        }

        static void Main(string[] args)
        {
            // Crear un objeto estudiante con datos predefinidos
            Estudiante[] estudiantes = new Estudiante[4];

            // Estudiante 1: Cristian Chiquimba
            estudiantes[0] = new Estudiante
            {
                ID = 1,
                Nombres = "Cristian",
                Apellidos = "Chiquimba",
                Direccion = "Quito, Ecuador",
                Telefonos = new string[] { "0991234567", "0987654321", "0976543210" }
            };

            // Estudiante 2: Isaac Caicedo
            estudiantes[1] = new Estudiante
            {
                ID = 2,
                Nombres = "Isaac",
                Apellidos = "Caicedo",
                Direccion = "Loja, Ecuador",
                Telefonos = new string[] { "0992345678", "0988765432", "0965432109" }
            };

            // Estudiante 3: Daniela Lamiña
            estudiantes[2] = new Estudiante
            {
                ID = 3,
                Nombres = "Daniela",
                Apellidos = "Lamiña",
                Direccion = "Ambato, Ecuador",
                Telefonos = new string[] { "0993456789", "0989876543", "0954321098" }
            };

            // Estudiante 4: Jordan Lema
            estudiantes[3] = new Estudiante
            {
                ID = 4,
                Nombres = "Jordan",
                Apellidos = "Lema",
                Direccion = "Cuenca, Ecuador",
                Telefonos = new string[] { "0994567890", "0986543210", "0973210987" }
            };

            // Mostrar los datos de los estudiantes
            foreach (var estudiante in estudiantes)
            {
                Console.WriteLine("\nDatos del estudiante:");
                Console.WriteLine($"ID: {estudiante.ID}");
                Console.WriteLine($"Nombres: {estudiante.Nombres}");
                Console.WriteLine($"Apellidos: {estudiante.Apellidos}");
                Console.WriteLine($"Dirección: {estudiante.Direccion}");
                Console.WriteLine("Teléfonos:");
                foreach (var telefono in estudiante.Telefonos)
                {
                    Console.WriteLine(telefono);
                }
            }
        }
    }
}
