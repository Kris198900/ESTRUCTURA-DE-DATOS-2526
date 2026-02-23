/*
 * Cristian Chiquimba - Semana 11
 * Estructura de Datos
 * Universidad Estatal Amazonica
 *
 * Hice este traductor pensando en algo simple pero que funcione bien.
 * La idea es que el usuario escriba una frase en espanol y el programa
 * reemplace las palabras que conoce por su version en ingles.
 * Las que no conoce, simplemente las deja como estan.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Semana11
{
    class Traductor
    {
        // Cargue estas 10 palabras al inicio porque son de las mas comunes
        // en el idioma espanol segun lo que vi en la lista de la tarea
        static Dictionary<string, string> mispalabras = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "tiempo",   "Time"   },
            { "persona",  "Person" },
            { "dia",      "Day"    },
            { "cosa",     "Thing"  },
            { "mundo",    "World"  },
            { "vida",     "Life"   },
            { "mano",     "Hand"   },
            { "ojo",      "Eye"    },
            { "mujer",    "Woman"  },
            { "trabajo",  "Work"   }
        };

        // Estos son los signos que pueden estar pegados a una palabra
        // y que no deben interferir cuando busco en el diccionario
        static char[] signos = { ',', '.', '!', '?', ';', ':', '"', '\'' };

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool seguir = true;

            while (seguir)
            {
                Menu();
                string opcion = Console.ReadLine() ?? "";

                if (opcion == "1")
                {
                    Traducir();
                }
                else if (opcion == "2")
                {
                    AgregarPalabra();
                }
                else if (opcion == "0")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nHasta luego!");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    seguir = false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEsa opcion no existe, intenta otra vez.");
                    Console.ResetColor();
                    Thread.Sleep(800);
                }
            }
        }

        static void Menu()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==================================================");
            Console.WriteLine("        UNIVERSIDAD ESTATAL AMAZONICA             ");
            Console.WriteLine("    Traductor Basico  -  Cristian Chiquimba       ");
            Console.WriteLine("==================================================");
            Console.ResetColor();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("==================== MENU ====================");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1. Traducir una frase");
            Console.WriteLine("2. Agregar palabras al diccionario");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. Salir");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("==============================================");
            Console.ResetColor();
            Console.Write("Seleccione una opcion: ");
        }

        static void Traducir()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("============== TRADUCIR FRASE ==============");
            Console.ResetColor();

            Console.Write("\nEscribe tu frase en espanol: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string frase = Console.ReadLine() ?? "";
            Console.ResetColor();

            if (string.IsNullOrWhiteSpace(frase))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNo escribiste nada.");
                Console.ResetColor();
                Thread.Sleep(1000);
                return;
            }

            // Le doy un pequeño tiempo para que no se vea tan instantaneo
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("\nBuscando palabras");
            for (int i = 0; i < 3; i++) { Thread.Sleep(350); Console.Write("."); }
            Console.ResetColor();

            // Separo la frase por espacios y reviso cada palabra
            string[] palabras = frase.Split(' ');

            for (int i = 0; i < palabras.Length; i++)
            {
                // Guardo lo que haya antes y despues de la palabra (comas, puntos, etc.)
                string antes   = "";
                string despues = "";
                string palabra = palabras[i];

                while (palabra.Length > 0 && Array.IndexOf(signos, palabra[0]) >= 0)
                {
                    antes   += palabra[0];
                    palabra  = palabra.Substring(1);
                }

                while (palabra.Length > 0 && Array.IndexOf(signos, palabra[palabra.Length - 1]) >= 0)
                {
                    despues  = palabra[palabra.Length - 1] + despues;
                    palabra  = palabra.Substring(0, palabra.Length - 1);
                }

                // Si la encuentro en el diccionario, la cambio
                if (mispalabras.TryGetValue(palabra, out string? enIngles))
                    palabras[i] = antes + enIngles + despues;

                // Si no la encuentro, la dejo tal cual estaba
            }

            string resultado = string.Join(" ", palabras);

            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("--------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("  Original  : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(frase);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("  Resultado : ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(resultado);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("--------------------------------------------");
            Console.ResetColor();
            Console.WriteLine("  Las palabras que no conozco las deje igual.");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPresiona cualquier tecla para volver...");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        static void AgregarPalabra()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========== NUEVA PALABRA ===================");
            Console.ResetColor();

            Console.Write("\nPalabra en espanol : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string esp = Console.ReadLine()?.Trim() ?? "";
            Console.ResetColor();

            Console.Write("Traduccion en ingles: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string ing = Console.ReadLine()?.Trim() ?? "";
            Console.ResetColor();

            Console.WriteLine();

            if (string.IsNullOrEmpty(esp) || string.IsNullOrEmpty(ing))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  Dejaste algo vacio, intenta de nuevo.");
                Console.ResetColor();
            }
            else if (mispalabras.ContainsKey(esp))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  Esa palabra ya la tenia guardada.");
                Console.ResetColor();
            }
            else
            {
                mispalabras.Add(esp, ing);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  Listo, ya quedo guardada: " + esp + " = " + ing);
                Console.ResetColor();
            }

            Thread.Sleep(1600);
        }
    }
}