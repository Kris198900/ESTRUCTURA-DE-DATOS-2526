using System;

namespace AgendaTurnosClinica
{
    /// <summary>
    /// Clase base que representa un paciente de la clínica
    /// </summary>
    public class Paciente
    {
        // Atributos privados
        private string cedula;
        private string nombre;
        private string apellido;
        private string telefono;
        private string email;
        private DateTime fechaNacimiento;

        // Propiedades públicas
        public string Cedula 
        { 
            get => cedula; 
            set => cedula = value; 
        }

        public string Nombre 
        { 
            get => nombre; 
            set => nombre = value; 
        }

        public string Apellido 
        { 
            get => apellido; 
            set => apellido = value; 
        }

        public string Telefono 
        { 
            get => telefono; 
            set => telefono = value; 
        }

        public string Email 
        { 
            get => email; 
            set => email = value; 
        }

        public DateTime FechaNacimiento 
        { 
            get => fechaNacimiento; 
            set => fechaNacimiento = value; 
        }

        // Constructor por defecto
        public Paciente()
        {
            cedula = "";
            nombre = "";
            apellido = "";
            telefono = "";
            email = "";
            fechaNacimiento = DateTime.Now;
        }

        // Constructor con parámetros
        public Paciente(string cedula, string nombre, string apellido, 
                       string telefono, string email, DateTime fechaNacimiento)
        {
            this.cedula = cedula;
            this.nombre = nombre;
            this.apellido = apellido;
            this.telefono = telefono;
            this.email = email;
            this.fechaNacimiento = fechaNacimiento;
        }

        // Método para calcular la edad del paciente
        public int ObtenerEdad()
        {
            int edad = DateTime.Now.Year - fechaNacimiento.Year;
            
            // Ajustar si aún no ha cumplido años este año
            if (DateTime.Now.DayOfYear < fechaNacimiento.DayOfYear)
                edad--;
            
            return edad;
        }

        // Método para obtener el nombre completo
        public string ObtenerNombreCompleto()
        {
            return $"{nombre} {apellido}";
        }

        // Método para validar si los datos son correctos
        public virtual bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(cedula) || cedula.Length < 10)
                return false;
            
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
                return false;
            
            if (string.IsNullOrWhiteSpace(telefono) || telefono.Length < 10)
                return false;
            
            if (fechaNacimiento > DateTime.Now)
                return false;
            
            return true;
        }

        // Override del método ToString para representación en texto
        public override string ToString()
        {
            return $"{cedula} - {nombre} {apellido} - Tel: {telefono} - Edad: {ObtenerEdad()} años";
        }

        // Método para mostrar información detallada
        public virtual void MostrarInformacion()
        {
            Console.WriteLine($"Cédula: {cedula}");
            Console.WriteLine($"Nombre: {nombre} {apellido}");
            Console.WriteLine($"Edad: {ObtenerEdad()} años");
            Console.WriteLine($"Teléfono: {telefono}");
            Console.WriteLine($"Email: {email}");
        }
    }
}