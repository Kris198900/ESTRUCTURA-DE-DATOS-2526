using System;
using System.Collections.Generic;

namespace EjerciciosPilas
{
    /// <summary>
    /// Clase para verificar si los paréntesis, llaves y corchetes 
    /// están balanceados en una expresión matemática
    /// </summary>
    public class VerificacionParentesis
    {
        /// <summary>
        /// Ejecuta el ejercicio de verificación de paréntesis balanceados
        /// </summary>
        public static void Ejecutar()
        {
            string[] expresiones = {
                "{7 + (8 * 5) - [(9 - 7) + (4 + 1)]}",
                "{[(3 + 2) * 5]}",
                "{7 + (8 * 5) - [(9 - 7) + (4 + 1)]",  // Falta llave de cierre
                "((2 + 3) * [5 - 1))]"  // Paréntesis mal cerrado
            };
            
            foreach (string expresion in expresiones)
            {
                bool balanceada = VerificarBalance(expresion);
                Console.WriteLine($"Expresión: {expresion}");
                Console.WriteLine($"Resultado: {(balanceada ? "Fórmula balanceada" : "Fórmula NO balanceada")}\n");
            }
        }
        
        /// <summary>
        /// Verifica si los paréntesis, llaves y corchetes están balanceados en una expresión
        /// </summary>
        /// <param name="expresion">Expresión matemática a verificar</param>
        /// <returns>true si está balanceada, false en caso contrario</returns>
        private static bool VerificarBalance(string expresion)
        {
            // Pila para almacenar los símbolos de apertura
            Stack<char> pila = new Stack<char>();
            
            // Recorrer cada carácter de la expresión
            foreach (char c in expresion)
            {
                // Si es un símbolo de apertura, agregarlo a la pila
                if (c == '(' || c == '[' || c == '{')
                {
                    pila.Push(c);
                }
                // Si es un símbolo de cierre, verificar que coincida con el último de apertura
                else if (c == ')' || c == ']' || c == '}')
                {
                    // Si la pila está vacía, no hay símbolo de apertura correspondiente
                    if (pila.Count == 0)
                    {
                        return false;
                    }
                    
                    // Obtener el último símbolo de apertura
                    char apertura = pila.Pop();
                    
                    // Verificar que el símbolo de cierre coincida con el de apertura
                    if (!CoincidenSimbolos(apertura, c))
                    {
                        return false;
                    }
                }
            }
            
            // La expresión está balanceada solo si la pila quedó vacía
            return pila.Count == 0;
        }
        
        /// <summary>
        /// Verifica si un símbolo de apertura coincide con uno de cierre
        /// </summary>
        /// <param name="apertura">Símbolo de apertura</param>
        /// <param name="cierre">Símbolo de cierre</param>
        /// <returns>true si coinciden, false en caso contrario</returns>
        private static bool CoincidenSimbolos(char apertura, char cierre)
        {
            return (apertura == '(' && cierre == ')') ||
                   (apertura == '[' && cierre == ']') ||
                   (apertura == '{' && cierre == '}');
        }
    }

    /// <summary>
    /// Clase para resolver el problema de las Torres de Hanoi utilizando pilas
    /// </summary>
    public class TorresHanoi
    {
        /// <summary>
        /// Ejecuta el ejercicio de las Torres de Hanoi
        /// </summary>
        public static void Ejecutar()
        {
            Console.Write("Ingrese el número de discos (recomendado: 3-5): ");
            int numDiscos = 3; // Valor por defecto
            
            try
            {
                string input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    numDiscos = int.Parse(input);
                }
            }
            catch
            {
                Console.WriteLine("Valor inválido. Usando 3 discos por defecto.");
            }
            
            // Crear las tres torres (pilas)
            Stack<int> torreOrigen = new Stack<int>();
            Stack<int> torreAuxiliar = new Stack<int>();
            Stack<int> torreDestino = new Stack<int>();
            
            // Inicializar la torre origen con los discos (del más grande al más pequeño)
            for (int i = numDiscos; i >= 1; i--)
            {
                torreOrigen.Push(i);
            }
            
            Console.WriteLine($"\nEstado inicial:");
            MostrarTorres(torreOrigen, torreAuxiliar, torreDestino);
            Console.WriteLine("\nMovimientos:\n");
            
            // Resolver el problema
            int numeroMovimiento = 0;
            ResolverHanoi(numDiscos, torreOrigen, torreDestino, torreAuxiliar, 
                         "Origen", "Destino", "Auxiliar", ref numeroMovimiento);
            
            Console.WriteLine($"\nEstado final:");
            MostrarTorres(torreOrigen, torreAuxiliar, torreDestino);
            Console.WriteLine($"\nTotal de movimientos: {numeroMovimiento}");
        }
        
        /// <summary>
        /// Resuelve el problema de las Torres de Hanoi de forma recursiva
        /// Estrategia divide y vencerás:
        /// 1. Mover n-1 discos de origen a auxiliar
        /// 2. Mover el disco más grande de origen a destino
        /// 3. Mover n-1 discos de auxiliar a destino
        /// </summary>
        /// <param name="n">Número de discos a mover</param>
        /// <param name="origen">Torre origen</param>
        /// <param name="destino">Torre destino</param>
        /// <param name="auxiliar">Torre auxiliar</param>
        /// <param name="nombreOrigen">Nombre de la torre origen</param>
        /// <param name="nombreDestino">Nombre de la torre destino</param>
        /// <param name="nombreAuxiliar">Nombre de la torre auxiliar</param>
        /// <param name="numeroMovimiento">Contador de movimientos</param>
        private static void ResolverHanoi(int n, Stack<int> origen, Stack<int> destino, 
                                         Stack<int> auxiliar, string nombreOrigen, 
                                         string nombreDestino, string nombreAuxiliar,
                                         ref int numeroMovimiento)
        {
            // Caso base: si solo hay un disco, moverlo directamente
            if (n == 1)
            {
                int disco = origen.Pop();
                destino.Push(disco);
                numeroMovimiento++;
                Console.WriteLine($"{numeroMovimiento}. Mover disco {disco} de {nombreOrigen} a {nombreDestino}");
                return;
            }
            
            // Paso 1: Mover n-1 discos de origen a auxiliar usando destino como auxiliar
            ResolverHanoi(n - 1, origen, auxiliar, destino, 
                         nombreOrigen, nombreAuxiliar, nombreDestino, ref numeroMovimiento);
            
            // Paso 2: Mover el disco más grande de origen a destino
            int discoGrande = origen.Pop();
            destino.Push(discoGrande);
            numeroMovimiento++;
            Console.WriteLine($"{numeroMovimiento}. Mover disco {discoGrande} de {nombreOrigen} a {nombreDestino}");
            
            // Paso 3: Mover n-1 discos de auxiliar a destino usando origen como auxiliar
            ResolverHanoi(n - 1, auxiliar, destino, origen, 
                         nombreAuxiliar, nombreDestino, nombreOrigen, ref numeroMovimiento);
        }
        
        /// <summary>
        /// Muestra el estado actual de las tres torres
        /// </summary>
        /// <param name="origen">Torre origen</param>
        /// <param name="auxiliar">Torre auxiliar</param>
        /// <param name="destino">Torre destino</param>
        private static void MostrarTorres(Stack<int> origen, Stack<int> auxiliar, Stack<int> destino)
        {
            Console.WriteLine($"Torre Origen:   [{string.Join(", ", origen)}]");
            Console.WriteLine($"Torre Auxiliar: [{string.Join(", ", auxiliar)}]");
            Console.WriteLine($"Torre Destino:  [{string.Join(", ", destino)}]");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== EJERCICIOS DE PILAS ===\n");
            
            // Ejercicio 1: Verificación de paréntesis balanceados
            Console.WriteLine("EJERCICIO 1: Verificación de paréntesis balanceados");
            Console.WriteLine("---------------------------------------------------");
            VerificacionParentesis.Ejecutar();
            
            Console.WriteLine("\n\n");
            
            // Ejercicio 2: Torres de Hanoi
            Console.WriteLine("EJERCICIO 2: Torres de Hanoi");
            Console.WriteLine("-----------------------------");
            TorresHanoi.Ejecutar();
            
            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
