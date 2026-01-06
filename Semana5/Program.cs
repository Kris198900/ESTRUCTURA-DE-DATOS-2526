using System;
using System.Collections.Generic;
using System.Linq;

namespace Semana5
{
    class Program
    {
        static void Main(string[] args)
        {
            bool salir = false;

            while (!salir)
            {
                MostrarMenu();
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "5":
                        Ejercicio5();
                        break;
                    case "6":
                        Ejercicio6();
                        break;
                    case "7":
                        Ejercicio7();
                        break;
                    case "8":
                        Ejercicio8();
                        break;
                    case "9":
                        Ejercicio9();
                        break;
                    case "10":
                        Ejercicio10();
                        break;
                    case "11":
                        Ejercicio11();
                        break;
                    case "0":
                        salir = true;
                        Console.WriteLine("\n¡Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("\n❌ Opción no válida. Presione cualquier tecla...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║   EJERCICIOS DE LISTAS - NIVEL INTERMEDIO      ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║  5. Números en Orden Inverso                   ║");
            Console.WriteLine("║  6. Asignaturas a Repetir                      ║");
            Console.WriteLine("║  7. Abecedario sin Múltiplos de 3              ║");
            Console.WriteLine("║  8. Detector de Palíndromos                    ║");
            Console.WriteLine("║  9. Contador de Vocales                        ║");
            Console.WriteLine("║ 10. Mayor y Menor Precio                       ║");
            Console.WriteLine("║ 11. Producto Escalar de Vectores               ║");
            Console.WriteLine("║  0. Salir                                      ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
            Console.Write("\n➤ Seleccione una opción: ");
        }

        static void Ejercicio5()
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║      EJERCICIO 5 - NÚMEROS ORDEN INVERSO      ║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝\n");

            List<int> numeros = new List<int>();

            // Llenar lista del 1 al 10
            for (int i = 1; i <= 10; i++)
            {
                numeros.Add(i);
            }

            Console.WriteLine("📋 Lista original: " + string.Join(", ", numeros));

            // Invertir la lista
            numeros.Reverse();

            Console.WriteLine("🔄 Lista invertida: " + string.Join(", ", numeros));

            PausarYVolver();
        }

        static void Ejercicio6()
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║      EJERCICIO 6 - ASIGNATURAS A REPETIR      ║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝\n");

            List<string> asignaturas = new List<string>
            {
                "Matemáticas",
                "Física",
                "Química",
                "Historia",
                "Lengua"
            };

            Dictionary<string, double> notasAsignaturas = new Dictionary<string, double>();

            Console.WriteLine("📚 Ingrese la nota obtenida en cada asignatura:\n");

            // Solicitar notas
            foreach (string asignatura in asignaturas)
            {
                Console.Write($"   {asignatura}: ");
                double nota = Convert.ToDouble(Console.ReadLine());
                notasAsignaturas[asignatura] = nota;
            }

            // Filtrar asignaturas reprobadas (nota < 5)
            List<string> asignaturasRepetir = new List<string>();

            foreach (var item in notasAsignaturas)
            {
                if (item.Value < 5)
                {
                    asignaturasRepetir.Add(item.Key);
                }
            }

            Console.WriteLine("\n" + new string('─', 47));
            
            if (asignaturasRepetir.Count > 0)
            {
                Console.WriteLine("\n❌ Asignaturas que debes repetir:");
                foreach (string asignatura in asignaturasRepetir)
                {
                    Console.WriteLine($"   • {asignatura} (Nota: {notasAsignaturas[asignatura]})");
                }
            }
            else
            {
                Console.WriteLine("\n✅ ¡Felicidades! Has aprobado todas las asignaturas.");
            }

            PausarYVolver();
        }

        static void Ejercicio7()
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║   EJERCICIO 7 - ABECEDARIO SIN MÚLTIPLOS 3    ║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝\n");

            List<char> abecedario = new List<char>();

            // Llenar el abecedario
            for (char c = 'A'; c <= 'Z'; c++)
            {
                abecedario.Add(c);
            }

            Console.WriteLine("📖 Abecedario completo:");
            Console.WriteLine("   " + string.Join(", ", abecedario));

            // Eliminar posiciones múltiplos de 3 (posición 3, 6, 9, 12...)
            // Nota: Las posiciones empiezan en 1, no en 0
            List<char> abecedarioFiltrado = new List<char>();

            for (int i = 0; i < abecedario.Count; i++)
            {
                // i+1 porque las posiciones se cuentan desde 1
                if ((i + 1) % 3 != 0)
                {
                    abecedarioFiltrado.Add(abecedario[i]);
                }
            }

            Console.WriteLine("\n🔧 Abecedario sin múltiplos de 3:");
            Console.WriteLine("   " + string.Join(", ", abecedarioFiltrado));

            Console.WriteLine($"\n📊 Total de letras eliminadas: {abecedario.Count - abecedarioFiltrado.Count}");

            PausarYVolver();
        }

        static void Ejercicio8()
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║     EJERCICIO 8 - DETECTOR DE PALÍNDROMOS     ║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝\n");

            Console.Write("✏️  Ingrese una palabra: ");
            string palabra = Console.ReadLine().ToLower().Trim();

            // Eliminar espacios y caracteres especiales
            string palabraLimpia = "";
            foreach (char c in palabra)
            {
                if (char.IsLetterOrDigit(c))
                {
                    palabraLimpia += c;
                }
            }

            // Invertir la palabra
            char[] caracteres = palabraLimpia.ToCharArray();
            Array.Reverse(caracteres);
            string palabraInvertida = new string(caracteres);

            Console.WriteLine($"\n🔍 Palabra original: {palabraLimpia}");
            Console.WriteLine($"🔄 Palabra invertida: {palabraInvertida}");

            if (palabraLimpia == palabraInvertida)
            {
                Console.WriteLine("\n✅ ¡Es un PALÍNDROMO!");
            }
            else
            {
                Console.WriteLine("\n❌ NO es un palíndromo.");
            }

            PausarYVolver();
        }

        static void Ejercicio9()
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║      EJERCICIO 9 - CONTADOR DE VOCALES        ║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝\n");

            Console.Write("✏️  Ingrese una palabra: ");
            string palabra = Console.ReadLine().ToLower();

            Dictionary<char, int> contadorVocales = new Dictionary<char, int>
            {
                {'a', 0},
                {'e', 0},
                {'i', 0},
                {'o', 0},
                {'u', 0}
            };

            // Contar vocales
            foreach (char c in palabra)
            {
                if (contadorVocales.ContainsKey(c))
                {
                    contadorVocales[c]++;
                }
            }

            Console.WriteLine("\n📊 Resultado del conteo de vocales:\n");

            foreach (var vocal in contadorVocales)
            {
                string barra = new string('█', vocal.Value);
                Console.WriteLine($"   {vocal.Key.ToString().ToUpper()}: {barra} ({vocal.Value})");
            }

            int totalVocales = contadorVocales.Values.Sum();
            Console.WriteLine($"\n✓ Total de vocales: {totalVocales}");

            PausarYVolver();
        }

        static void Ejercicio10()
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║     EJERCICIO 10 - MAYOR Y MENOR PRECIO       ║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝\n");

            List<int> precios = new List<int> { 50, 75, 46, 22, 80, 65, 8 };

            Console.WriteLine("💰 Lista de precios:");
            Console.WriteLine("   " + string.Join(", ", precios));

            int precioMayor = precios.Max();
            int precioMenor = precios.Min();
            double promedio = precios.Average();

            Console.WriteLine("\n📈 Análisis de precios:");
            Console.WriteLine($"   ↑ Precio MAYOR: ${precioMayor}");
            Console.WriteLine($"   ↓ Precio MENOR: ${precioMenor}");
            Console.WriteLine($"   ≈ Promedio: ${promedio:F2}");
            Console.WriteLine($"   Δ Diferencia: ${precioMayor - precioMenor}");

            PausarYVolver();
        }

        static void Ejercicio11()
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║   EJERCICIO 11 - PRODUCTO ESCALAR VECTORES    ║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝\n");

            List<int> vector1 = new List<int> { 1, 2, 3 };
            List<int> vector2 = new List<int> { -1, 0, 2 };

            Console.WriteLine("📐 Vectores:");
            Console.WriteLine($"   Vector A = ({string.Join(", ", vector1)})");
            Console.WriteLine($"   Vector B = ({string.Join(", ", vector2)})");

            // Calcular producto escalar: (x1*x2) + (y1*y2) + (z1*z2)
            int productoEscalar = 0;

            Console.WriteLine("\n🔢 Cálculo del producto escalar:");
            for (int i = 0; i < vector1.Count; i++)
            {
                int producto = vector1[i] * vector2[i];
                productoEscalar += producto;
                Console.WriteLine($"   {vector1[i]} × {vector2[i]} = {producto}");
            }

            Console.WriteLine(new string('─', 47));
            Console.WriteLine($"\n✓ Producto escalar A·B = {productoEscalar}");

            // Información adicional
            double magnitudV1 = Math.Sqrt(vector1.Sum(x => x * x));
            double magnitudV2 = Math.Sqrt(vector2.Sum(x => x * x));

            Console.WriteLine($"\n📏 Magnitudes:");
            Console.WriteLine($"   |A| = {magnitudV1:F2}");
            Console.WriteLine($"   |B| = {magnitudV2:F2}");

            PausarYVolver();
        }

        static void PausarYVolver()
        {
            Console.WriteLine("\n" + new string('─', 47));
            Console.WriteLine("Presione cualquier tecla para volver al menú...");
            Console.ReadKey();
        }
    }
}