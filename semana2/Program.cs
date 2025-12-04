Console.WriteLine("CRISTIAN CHIQUIMBA");
Console.WriteLine();

// Crear un rectángulo con base 8 y altura 9
Rectangulo rectangulo = new Rectangulo(8, 9);

// Encabezado de tabla
Console.WriteLine("Rectangulo       | Valor");
Console.WriteLine("-----------------|-----------");

// Valores del rectángulo
Console.WriteLine("Base             | " + rectangulo.Base);
Console.WriteLine("Altura           | " + rectangulo.Altura);
Console.WriteLine("Área             | " + rectangulo.CalcularArea());
Console.WriteLine("Perímetro        | " + rectangulo.CalcularPerimetro());

Console.WriteLine();