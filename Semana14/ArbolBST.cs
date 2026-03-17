using System;

namespace Semana14
{
    public class ArbolBST
    {
        private Nodo? raiz;

        public ArbolBST()
        {
            raiz = null;
        }

        // Verifica si el árbol está vacío
        public bool EstaVacio()
        {
            return raiz == null;
        }

        // Método para insertar un valor en el árbol
        public void Insertar(int valor)
        {
            raiz = InsertarRec(raiz, valor);
        }

        private Nodo? InsertarRec(Nodo? nodo, int valor)
        {
            if (nodo == null) return new Nodo(valor);
            if (valor < nodo.Valor)
                nodo.Izquierdo = InsertarRec(nodo.Izquierdo, valor);
            else if (valor > nodo.Valor)
                nodo.Derecho = InsertarRec(nodo.Derecho, valor);
            else
                Console.WriteLine("  >> El valor " + valor + " ya existe en el árbol.");
            return nodo;
        }

        // Método para buscar un valor en el árbol
        public bool Buscar(int valor)
        {
            return BuscarRec(raiz, valor);
        }

        private bool BuscarRec(Nodo? nodo, int valor)
        {
            if (nodo == null) return false;
            if (valor == nodo.Valor) return true;
            if (valor < nodo.Valor) return BuscarRec(nodo.Izquierdo, valor);
            return BuscarRec(nodo.Derecho, valor);
        }

        // Método para eliminar un valor del árbol
        public void Eliminar(int valor)
        {
            if (!Buscar(valor))
            {
                Console.WriteLine("  >> El valor " + valor + " no existe en el árbol.");
                return;
            }
            raiz = EliminarRec(raiz, valor);
            Console.WriteLine("  >> Valor " + valor + " eliminado correctamente.");
        }

        private Nodo? EliminarRec(Nodo? nodo, int valor)
        {
            if (nodo == null) return null;
            if (valor < nodo.Valor)
                nodo.Izquierdo = EliminarRec(nodo.Izquierdo, valor);
            else if (valor > nodo.Valor)
                nodo.Derecho = EliminarRec(nodo.Derecho, valor);
            else
            {
                if (nodo.Izquierdo == null) return nodo.Derecho;
                if (nodo.Derecho == null) return nodo.Izquierdo;
                int sucesor = ObtenerMinValor(nodo.Derecho);
                nodo.Valor = sucesor;
                nodo.Derecho = EliminarRec(nodo.Derecho, sucesor);
            }
            return nodo;
        }

        // Métodos para mostrar el árbol en diferentes recorridos
        public void MostrarPreorden()
        {
            Console.Write("Preorden (Raíz > Izq > Der): ");
            PreordenRec(raiz);
            Console.WriteLine();
        }

        private void PreordenRec(Nodo? nodo)
        {
            if (nodo == null) return;
            Console.Write("[" + nodo.Valor + "] ");
            PreordenRec(nodo.Izquierdo);
            PreordenRec(nodo.Derecho);
        }

        public void MostrarInorden()
        {
            Console.Write("Inorden (Izq > Raíz > Der): ");
            InordenRec(raiz);
            Console.WriteLine();
        }

        private void InordenRec(Nodo? nodo)
        {
            if (nodo == null) return;
            InordenRec(nodo.Izquierdo);
            Console.Write("[" + nodo.Valor + "] ");
            InordenRec(nodo.Derecho);
        }

        public void MostrarPostorden()
        {
            Console.Write("Postorden (Izq > Der > Raíz): ");
            PostordenRec(raiz);
            Console.WriteLine();
        }

        private void PostordenRec(Nodo? nodo)
        {
            if (nodo == null) return;
            PostordenRec(nodo.Izquierdo);
            PostordenRec(nodo.Derecho);
            Console.Write("[" + nodo.Valor + "] ");
        }

        // Métodos para obtener el valor mínimo y máximo
        public int ObtenerMinimo()
        {
            if (raiz == null) throw new InvalidOperationException("El árbol está vacío.");
            return ObtenerMinValor(raiz);
        }

        private int ObtenerMinValor(Nodo? nodo)
        {
            while (nodo.Izquierdo != null)
                nodo = nodo.Izquierdo;
            return nodo.Valor;
        }

        public int ObtenerMaximo()
        {
            if (raiz == null) throw new InvalidOperationException("El árbol está vacío.");
            Nodo actual = raiz;
            while (actual.Derecho != null)
                actual = actual.Derecho;
            return actual.Valor;
        }

        // Obtener la altura del árbol
        public int ObtenerAltura()
        {
            return AlturaRec(raiz);
        }

        private int AlturaRec(Nodo? nodo)
        {
            if (nodo == null) return 0;
            int izq = AlturaRec(nodo.Izquierdo);
            int der = AlturaRec(nodo.Derecho);
            return 1 + Math.Max(izq, der);
        }

        // Limpiar el árbol
        public void Limpiar()
        {
            raiz = null;
            Console.WriteLine("  >> Árbol limpiado. Todos los nodos eliminados.");
        }
    }
}