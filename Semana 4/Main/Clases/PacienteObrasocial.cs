using System;

namespace AgendaTurnosClinica
{
    /// <summary>
    /// Clase que representa un paciente con obra social
    /// Hereda de la clase Paciente
    /// </summary>
    public class PacienteObraSocial : Paciente
    {
        // Atributos específicos de paciente con obra social
        private string nombreObraSocial;
        private string numeroAfiliado;
        private string planCobertura;
        private DateTime fechaVencimiento;
        private bool tieneCopago;
        private decimal montoCopago;

        // Propiedades
        public string NombreObraSocial 
        { 
            get => nombreObraSocial; 
            set => nombreObraSocial = value; 
        }

        public string NumeroAfiliado 
        { 
            get => numeroAfiliado; 
            set => numeroAfiliado = value; 
        }

        public string PlanCobertura 
        { 
            get => planCobertura; 
            set => planCobertura = value; 
        }

        public DateTime FechaVencimiento 
        { 
            get => fechaVencimiento; 
            set => fechaVencimiento = value; 
        }

        public bool TieneCopago 
        { 
            get => tieneCopago; 
            set => tieneCopago = value; 
        }

        public decimal MontoCopago 
        { 
            get => montoCopago; 
            set => montoCopago = value; 
        }

        // Constructor por defecto
        public PacienteObraSocial() : base()
        {
            nombreObraSocial = "";
            numeroAfiliado = "";
            planCobertura = "Básico";
            fechaVencimiento = DateTime.Now.AddYears(1);
            tieneCopago = false;
            montoCopago = 0;
        }

        // Constructor con parámetros
        public PacienteObraSocial(string cedula, string nombre, string apellido,
                                 string telefono, string email, DateTime fechaNacimiento,
                                 string nombreObraSocial, string numeroAfiliado,
                                 string planCobertura, DateTime fechaVencimiento,
                                 bool tieneCopago, decimal montoCopago)
            : base(cedula, nombre, apellido, telefono, email, fechaNacimiento)
        {
            this.nombreObraSocial = nombreObraSocial;
            this.numeroAfiliado = numeroAfiliado;
            this.planCobertura = planCobertura;
            this.fechaVencimiento = fechaVencimiento;
            this.tieneCopago = tieneCopago;
            this.montoCopago = montoCopago;
        }

        // Método para verificar si la obra social está vigente
        public bool EstaVigente()
        {
            return fechaVencimiento >= DateTime.Now;
        }

        // Método para verificar si tiene cobertura para una especialidad
        public bool TieneCobertura(string especialidad)
        {
            if (!EstaVigente())
                return false;

            // Lógica según el plan de cobertura
            switch (planCobertura.ToLower())
            {
                case "básico":
                    return especialidad.ToLower() == "medicina general";
                
                case "intermedio":
                    return especialidad.ToLower() != "cirugía estética";
                
                case "completo":
                case "premium":
                    return true;
                
                default:
                    return false;
            }
        }

        // Método para calcular el costo de consulta
        public decimal CalcularCostoConsulta(decimal costoBase)
        {
            if (!EstaVigente())
                return costoBase; // Sin cobertura, paga precio completo

            if (tieneCopago)
                return montoCopago;
            
            return 0; // Cobertura total sin copago
        }

        // Override del método ValidarDatos
        public override bool ValidarDatos()
        {
            if (!base.ValidarDatos())
                return false;

            if (string.IsNullOrWhiteSpace(nombreObraSocial))
                return false;

            if (string.IsNullOrWhiteSpace(numeroAfiliado))
                return false;

            return true;
        }

        // Override del método MostrarInformacion
        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine($"Obra Social: {nombreObraSocial}");
            Console.WriteLine($"Número de Afiliado: {numeroAfiliado}");
            Console.WriteLine($"Plan: {planCobertura}");
            Console.WriteLine($"Estado: {(EstaVigente() ? "VIGENTE" : "VENCIDA")}");
            Console.WriteLine($"Vencimiento: {fechaVencimiento:dd/MM/yyyy}");
            
            if (tieneCopago)
                Console.WriteLine($"Copago: ${montoCopago:F2}");
            else
                Console.WriteLine("Sin copago");
        }

        // Override del método ToString
        public override string ToString()
        {
            string estado = EstaVigente() ? "✓" : "✗";
            return $"{base.ToString()} - OS: {nombreObraSocial} [{estado}]";
        }

        // Método para renovar la obra social
        public void RenovarObraSocial(int meses)
        {
            fechaVencimiento = DateTime.Now.AddMonths(meses);
            Console.WriteLine($"✓ Obra social renovada hasta: {fechaVencimiento:dd/MM/yyyy}");
        }
    }
}