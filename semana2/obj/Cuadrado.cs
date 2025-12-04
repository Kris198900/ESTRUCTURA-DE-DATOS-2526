// Clase Cuadrado que hereda de Figura
public class Cuadrado : Figura
{
    // Atributo
    public double Lado { get; set; }

    // Constructor
    public Cuadrado(double Lado)
    {
        this.Lado = Lado;
    }

    // Métodos
    public override double CalcularArea()
    {
        // El área de un cuadrado es Lado * Lado
        return Lado * Lado;
    }

    public override double CalcularPerimetro()
    {
        // El perímetro de un cuadrado es 4 veces el lado
        return 4 * Lado;
    }
}
