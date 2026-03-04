namespace TorneoFutbol
{
    public class Jugador
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Posicion { get; set; }

        public Jugador(string nombre, int edad, string posicion)
        {
            Nombre = nombre;
            Edad = edad;
            Posicion = posicion;
        }

        public void Mostrar()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  - {Nombre}, Edad: {Edad}, Posicion: {Posicion}");
            Console.ResetColor();
        }
    }
}