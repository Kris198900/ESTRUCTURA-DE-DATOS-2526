using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // 1. Conjunto universal: 500 ciudadanos
        HashSet<string> comunidad = new HashSet<string>();
        for (int i = 1; i <= 500; i++)
        {
            comunidad.Add("Ciudadano " + i);
        }

        // 2. Conjunto Pfizer: ciudadanos del 1 al 75
        HashSet<string> pfizer = new HashSet<string>();
        for (int i = 1; i <= 75; i++)
        {
            pfizer.Add("Ciudadano " + i);
        }

        // 3. Conjunto AstraZeneca: ciudadanos del 51 al 125
        //    Del 51 al 75 coinciden con Pfizer (dos dosis)
        //    Del 76 al 125 son solo AstraZeneca
        HashSet<string> astrazeneca = new HashSet<string>();
        for (int i = 51; i <= 125; i++)
        {
            astrazeneca.Add("Ciudadano " + i);
        }

        // Dos dosis: interseccion entre Pfizer y AstraZeneca
        HashSet<string> dosDosis = new HashSet<string>(pfizer);
        dosDosis.IntersectWith(astrazeneca);

        // Solo Pfizer: Pfizer menos AstraZeneca
        HashSet<string> soloPfizer = new HashSet<string>(pfizer);
        soloPfizer.ExceptWith(astrazeneca);

        // Solo AstraZeneca: AstraZeneca menos Pfizer
        HashSet<string> soloAstrazeneca = new HashSet<string>(astrazeneca);
        soloAstrazeneca.ExceptWith(pfizer);

        // Total vacunados: union de Pfizer y AstraZeneca
        HashSet<string> totalVacunados = new HashSet<string>(pfizer);
        totalVacunados.UnionWith(astrazeneca);

        // No vacunados: comunidad menos el total vacunados
        HashSet<string> noVacunados = new HashSet<string>(comunidad);
        noVacunados.ExceptWith(totalVacunados);

        // Mostrar resultados
        Console.WriteLine("========================================");
        Console.WriteLine("  SISTEMA DE VACUNACION COVID-19");
        Console.WriteLine("  Teoria de Conjuntos con HashSet");
        Console.WriteLine("========================================");

        Console.WriteLine("\n--- CONJUNTOS BASE ---");
        Console.WriteLine("Comunidad (Universo):      " + comunidad.Count + " ciudadanos");
        Console.WriteLine("Vacunados con Pfizer:      " + pfizer.Count + " ciudadanos");
        Console.WriteLine("Vacunados con AstraZeneca: " + astrazeneca.Count + " ciudadanos");

        MostrarResultado("DOS DOSIS (Pfizer interseccion AstraZeneca)", dosDosis);
        MostrarResultado("SOLO PFIZER (Pfizer menos AstraZeneca)", soloPfizer);
        MostrarResultado("SOLO ASTRAZENECA (AstraZeneca menos Pfizer)", soloAstrazeneca);
        MostrarResultado("NO VACUNADOS (Comunidad menos todos los vacunados)", noVacunados);

        int suma = dosDosis.Count + soloPfizer.Count + soloAstrazeneca.Count + noVacunados.Count;

        Console.WriteLine("\n========================================");
        Console.WriteLine("  VERIFICACION FINAL");
        Console.WriteLine("========================================");
        Console.WriteLine("Dos dosis:        " + dosDosis.Count);
        Console.WriteLine("Solo Pfizer:      " + soloPfizer.Count);
        Console.WriteLine("Solo AstraZeneca: " + soloAstrazeneca.Count);
        Console.WriteLine("No vacunados:     " + noVacunados.Count);
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("SUMA TOTAL:       " + suma + " (debe ser 500)");

        if (suma == 500)
            Console.WriteLine("\n[OK] Los conjuntos cubren exactamente el universo.");
        else
            Console.WriteLine("\n[ERROR] La suma no coincide con el universo.");

        Console.WriteLine("\nPresiona Enter para salir...");
        Console.ReadLine();
    }

    static void MostrarResultado(string nombre, HashSet<string> conjunto)
    {
        Console.WriteLine("\n--- " + nombre + " ---");

        List<string> lista = conjunto
            .OrderBy(c => int.Parse(c.Replace("Ciudadano ", "")))
            .ToList();

        int mostrados = 0;
        foreach (string ciudadano in lista)
        {
            if (mostrados >= 10)
            {
                Console.WriteLine("  ... y " + (lista.Count - 10) + " ciudadanos mas.");
                break;
            }
            Console.WriteLine("  - " + ciudadano);
            mostrados++;
        }

        Console.WriteLine("Total: " + conjunto.Count + " ciudadano(s)");
    }
}