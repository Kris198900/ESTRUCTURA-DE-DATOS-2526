using System;

namespace Semana06
{
    // Clase Nodo para la lista enlazada
    public class Nodo
    {
        public int Dato { get; set; }
        public Nodo Siguiente { get; set; }

        public Nodo(int dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }

    // Clase Lista Enlazada
    public class ListaEnlazada
    {
        private Nodo cabeza;

        public ListaEnlazada()
        {
            cabeza = null;
        }

        // Agregar por el inicio
        public void AgregarPorInicio(int dato)
        {
            Nodo nuevoNodo = new Nodo(dato);
            nuevoNodo.Siguiente = cabeza;
            cabeza = nuevoNodo;
        }

        // Agregar por el final
        public void AgregarPorFinal(int dato)
        {
            Nodo nuevoNodo = new Nodo(dato);

            if (cabeza == null)
            {
                cabeza = nuevoNodo;
                return;
            }

            Nodo actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevoNodo;
        }

        // Contar elementos
        public int ContarElementos()
        {
            int contador = 0;
            Nodo actual = cabeza;

            while (actual != null)
            {
                contador++;
                actual = actual.Siguiente;
            }

            return contador;
        }

        // Mostrar datos
        public void MostrarDatos()
        {
            if (cabeza == null)
            {
                Console.WriteLine("La lista está vacía.");
                return;
            }

            Nodo actual = cabeza;
            while (actual != null)
            {
                Console.Write(actual.Dato + " ");
                actual = actual.Siguiente;
            }
            Console.WriteLine();
        }
    }

    public class Utilidades
    {
        // Verificar si un número es primo
        public static bool EsPrimo(int numero)
        {
            if (numero < 2) return false;
            if (numero == 2) return true;
            if (numero % 2 == 0) return false;

            for (int i = 3; i <= Math.Sqrt(numero); i += 2)
            {
                if (numero % i == 0)
                    return false;
            }
            return true;
        }

        // Verificar si un número es Armstrong
        public static bool EsArmstrong(int numero)
        {
            int suma = 0;
            int temp = numero;
            int digitos = numero.ToString().Length;

            while (temp > 0)
            {
                int digito = temp % 10;
                suma += (int)Math.Pow(digito, digitos);
                temp /= 10;
            }

            return suma == numero;
        }
    }

    public class Ejercicio5
    {
        public static void Ejecutar()
        {
            ListaEnlazada listaPrimos = new ListaEnlazada();
            ListaEnlazada listaArmstrong = new ListaEnlazada();

            Console.WriteLine("=== EJERCICIO 5: LISTAS DE NÚMEROS PRIMOS Y ARMSTRONG ===\n");

            Console.Write("Ingrese la cantidad de números a procesar: ");
            int cantidad = int.Parse(Console.ReadLine());

            Console.WriteLine("\nIngrese los números:");

            for (int i = 0; i < cantidad; i++)
            {
                Console.Write($"Número {i + 1}: ");
                int numero = int.Parse(Console.ReadLine());

                if (Utilidades.EsPrimo(numero))
                {
                    listaPrimos.AgregarPorFinal(numero);
                    Console.WriteLine($"  → {numero} es PRIMO (agregado al final de la lista de primos)");
                }

                if (Utilidades.EsArmstrong(numero))
                {
                    listaArmstrong.AgregarPorInicio(numero);
                    Console.WriteLine($"  → {numero} es ARMSTRONG (agregado al inicio de la lista de Armstrong)");
                }
            }

            // Mostrar resultados
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("RESULTADOS:");
            Console.WriteLine(new string('=', 60));

            // a. Número de datos insertados en cada lista
            int cantidadPrimos = listaPrimos.ContarElementos();
            int cantidadArmstrong = listaArmstrong.ContarElementos();

            Console.WriteLine($"\na) Número de datos insertados:");
            Console.WriteLine($"   - Lista de Primos: {cantidadPrimos} elementos");
            Console.WriteLine($"   - Lista de Armstrong: {cantidadArmstrong} elementos");

            // b. Lista con más elementos
            Console.WriteLine($"\nb) Lista con más elementos:");
            if (cantidadPrimos > cantidadArmstrong)
                Console.WriteLine($"   La lista de PRIMOS tiene más elementos ({cantidadPrimos} vs {cantidadArmstrong})");
            else if (cantidadArmstrong > cantidadPrimos)
                Console.WriteLine($"   La lista de ARMSTRONG tiene más elementos ({cantidadArmstrong} vs {cantidadPrimos})");
            else
                Console.WriteLine($"   Ambas listas tienen la misma cantidad de elementos ({cantidadPrimos})");

            // c. Mostrar todos los datos
            Console.WriteLine($"\nc) Datos insertados en las listas:");
            Console.WriteLine("\n   Lista de Números PRIMOS:");
            Console.Write("   ");
            listaPrimos.MostrarDatos();

            Console.WriteLine("\n   Lista de Números ARMSTRONG:");
            Console.Write("   ");
            listaArmstrong.MostrarDatos();

            Console.WriteLine("\n" + new string('=', 60));
        }
    }
}