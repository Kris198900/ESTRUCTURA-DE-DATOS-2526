using System;

namespace FigurasGeometricas
{
    // Clase Rectángulo que encapsula los atributos y métodos para calcular área y perímetro
    public class Rectangulo
    {
        // Atributos
        public double Base { get; set; }   // La base del rectángulo
        public double Altura { get; set; } // La altura del rectángulo

        // Constructor
        public Rectangulo(double baseValor, double alturaValor)
        {
            this.Base = baseValor;      // Asigna el valor de la base
            this.Altura = alturaValor;  // Asigna el valor de la altura
        }

        // Calcular el área del rectángulo
        // La fórmula para el área es Base * Altura
        public double CalcularArea()
        {
            return Base * Altura; // Devuelve el valor del área
        }

        // Calcular el perímetro del rectángulo
        // La fórmula para el perímetro es 2 * (Base + Altura)
        public double CalcularPerimetro()
        {
            return 2 * (Base + Altura); // Devuelve el valor del perímetro
        }
    }

    // Clase Cuadrado que encapsula los atributos y métodos para calcular área y perímetro
    public class Cuadrado
    {
        // Atributo
        public double Lado { get; set; } // El lado del cuadrado

        // Constructor
        public Cuadrado(double ladoValor)
        {
            this.Lado = ladoValor; // Asigna el valor del lado
        }

        // Calcular el área del cuadrado
        // La fórmula para el área es Lado * Lado
        public double CalcularArea()
        {
            return Lado * Lado; // Devuelve el valor del área
        }

        // Calcular el perímetro del cuadrado
        // La fórmula para el perímetro es 4 * Lado
        public double CalcularPerimetro()
        {
            return 4 * Lado; // Devuelve el valor del perímetro
        }
    }

    // Clase principal para ejecutar el código
    class Program
    {
        static void Main(string[] args)
        {
            // Crear un rectángulo con base 5 y altura 10
            Rectangulo rectangulo = new Rectangulo(5, 10);

            // Crear un cuadrado con lado 6
            Cuadrado cuadrado = new Cuadrado(6);

            // Mostrar los resultados para el rectángulo
            Console.WriteLine("Rectángulo       | Valor");
            Console.WriteLine("-----------------|-----------");
            Console.WriteLine("Base             | " + rectangulo.Base);
            Console.WriteLine("Altura           | " + rectangulo.Altura);
            Console.WriteLine("Área             | " + rectangulo.CalcularArea());
            Console.WriteLine("Perímetro        | " + rectangulo.CalcularPerimetro());

            Console.WriteLine();

            // Mostrar los resultados para el cuadrado
            Console.WriteLine("Cuadrado         | Valor");
            Console.WriteLine("-----------------|-----------");
            Console.WriteLine("Lado             | " + cuadrado.Lado);
            Console.WriteLine("Área             | " + cuadrado.CalcularArea());
            Console.WriteLine("Perímetro        | " + cuadrado.CalcularPerimetro());
        }
    }
}
