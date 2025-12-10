using System;

class Cola
{
    private int[] colaArray;
    private int frente;
    private int fin;
    private int tamaño;

    public Cola(int tamaño)
    {
        this.tamaño = tamaño;
        colaArray = new int[tamaño];
        frente = -1;
        fin = -1;
    }

    // Verificar si la cola está llena
    public bool EstaLlena()
    {
        return fin == tamaño - 1;
    }

    // Verificar si la cola está vacía
    public bool EstaVacia()
    {
        return frente == -1 || frente > fin;
    }

    // Agregar un elemento a la cola
    public void Enqueue(int valor)
    {
        if (EstaLlena())
        {
            Console.WriteLine("La cola está llena. No se puede agregar el valor.");
            return;
        }
        if (frente == -1) frente = 0; // Si la cola está vacía, inicializar el frente
        fin++;
        colaArray[fin] = valor;
    }

    // Eliminar un elemento de la cola
    public int Dequeue()
    {
        if (EstaVacia())
        {
            Console.WriteLine("La cola está vacía. No se puede eliminar el valor.");
            return -1; // Retorna un valor inválido si la cola está vacía
        }
        int valor = colaArray[frente];
        frente++;
        return valor;
    }

    // Mostrar los elementos de la cola
    public void MostrarCola()
    {
        if (EstaVacia())
        {
            Console.WriteLine("La cola está vacía.");
            return;
        }
        Console.WriteLine("Elementos en la cola:");
        for (int i = frente; i <= fin; i++)
        {
            Console.Write(colaArray[i] + " ");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Crear una cola de tamaño 5
        Cola miCola = new Cola(5);

        // Agregar elementos a la cola
        miCola.Enqueue(10);
        miCola.Enqueue(20);
        miCola.Enqueue(30);
        miCola.Enqueue(40);
        miCola.Enqueue(50);

        // Mostrar la cola
        miCola.MostrarCola();

        // Eliminar un elemento de la cola
        Console.WriteLine("Elemento eliminado: " + miCola.Dequeue());

        // Mostrar la cola después de eliminar un elemento
        miCola.MostrarCola();

        // Crear una matriz de enteros de 2x3
        int[,] matriz = new int[2, 3] { { 1, 2, 3 }, { 4, 5, 6 } };
        Console.WriteLine("Matriz:");
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(matriz[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
