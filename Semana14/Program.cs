using System;

namespace Semana14
{
    class Program
    {
        static void Main(string[] args)
        {
            ArbolBST arbol = new ArbolBST();
            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("Menú Árbol Binario de Búsqueda:");
                Console.WriteLine("1. Insertar un valor");
                Console.WriteLine("2. Buscar un valor");
                Console.WriteLine("3. Eliminar un valor");
                Console.WriteLine("4. Mostrar Preorden");
                Console.WriteLine("5. Mostrar Inorden");
                Console.WriteLine("6. Mostrar Postorden");
                Console.WriteLine("7. Obtener valor mínimo");
                Console.WriteLine("8. Obtener valor máximo");
                Console.WriteLine("9. Obtener altura del árbol");
                Console.WriteLine("10. Limpiar el árbol");
                Console.WriteLine("11. Salir");
                Console.Write("Seleccione una opción: ");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.Write("Ingrese el valor a insertar: ");
                        int valorInsertar = int.Parse(Console.ReadLine());
                        arbol.Insertar(valorInsertar);
                        break;

                    case 2:
                        Console.Write("Ingrese el valor a buscar: ");
                        int valorBuscar = int.Parse(Console.ReadLine());
                        bool encontrado = arbol.Buscar(valorBuscar);
                        Console.WriteLine(encontrado ? "Valor encontrado." : "Valor no encontrado.");
                        break;

                    case 3:
                        Console.Write("Ingrese el valor a eliminar: ");
                        int valorEliminar = int.Parse(Console.ReadLine());
                        arbol.Eliminar(valorEliminar);
                        break;

                    case 4:
                        arbol.MostrarPreorden();
                        break;

                    case 5:
                        arbol.MostrarInorden();
                        break;

                    case 6:
                        arbol.MostrarPostorden();
                        break;

                    case 7:
                        try
                        {
                            Console.WriteLine("Valor mínimo: " + arbol.ObtenerMinimo());
                        }
                        catch (InvalidOperationException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    case 8:
                        try
                        {
                            Console.WriteLine("Valor máximo: " + arbol.ObtenerMaximo());
                        }
                        catch (InvalidOperationException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    case 9:
                        Console.WriteLine("Altura del árbol: " + arbol.ObtenerAltura());
                        break;

                    case 10:
                        arbol.Limpiar();
                        break;

                    case 11:
                        Console.WriteLine("Saliendo...");
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();

            } while (opcion != 11);
        }
    }
}