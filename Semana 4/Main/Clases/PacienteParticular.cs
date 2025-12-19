using System;

namespace AgendaTurnosClinica
{
    /// <summary>
    /// Clase que representa un paciente particular (sin obra social)
    /// Hereda de la clase Paciente
    /// </summary>
    public class PacienteParticular : Paciente
    {
        // Atributos específicos de paciente particular
        private decimal descuentoFrecuente;
        private int consultasRealizadas;
        private bool tienePlanPagos;
        private string metodoPagoPreferido;

        // Propiedades
        public decimal DescuentoFrecuente 
        { 
            get => descuentoFrecuente; 
            set => descuentoFrecuente = value; 
        }

        public int ConsultasRealizadas 
        { 
            get => consultasRealizadas; 
            set => consultasRealizadas = value; 
        }

        public bool TienePlanPagos 
        { 
            get => tienePlanPagos; 
            set => tienePlanPagos = value; 
        }

        public string MetodoPagoPreferido 
        { 
            get => metodoPagoPreferido; 
            set => metodoPagoPreferido = value; 
        }

        // Constructor por defecto
        public PacienteParticular() : base()
        {
            descuentoFrecuente = 0;
            consultasRealizadas = 0;
            tienePlanPagos = false;
            metodoPagoPreferido = "Efectivo";
        }

        // Constructor con parámetros
        public PacienteParticular(string cedula, string nombre, string apellido,
                                 string telefono, string email, DateTime fechaNacimiento,
                                 string metodoPagoPreferido = "Efectivo")
            : base(cedula, nombre, apellido, telefono, email, fechaNacimiento)
        {
            this.descuentoFrecuente = 0;
            this.consultasRealizadas = 0;
            this.tienePlanPagos = false;
            this.metodoPagoPreferido = metodoPagoPreferido;
        }

        // Método para calcular el descuento según consultas realizadas
        private void ActualizarDescuento()
        {
            if (consultasRealizadas >= 20)
                descuentoFrecuente = 0.20m; // 20% de descuento
            else if (consultasRealizadas >= 10)
                descuentoFrecuente = 0.15m; // 15% de descuento
            else if (consultasRealizadas >= 5)
                descuentoFrecuente = 0.10m; // 10% de descuento
            else
                descuentoFrecuente = 0;
        }

        // Método para registrar una consulta realizada
        public void RegistrarConsulta()
        {
            consultasRealizadas++;
            ActualizarDescuento();
            Console.WriteLine($"✓ Consulta registrada. Total: {consultasRealizadas}");
            
            if (descuentoFrecuente > 0)
                Console.WriteLine($"  Descuento actual: {descuentoFrecuente * 100}%");
        }

        // Método para calcular el costo de consulta
        public decimal CalcularCostoConsulta(decimal costoBase)
        {
            decimal costoFinal = costoBase;

            // Aplicar descuento por cliente frecuente
            if (descuentoFrecuente > 0)
            {
                decimal descuento = costoBase * descuentoFrecuente;
                costoFinal -= descuento;
            }

            return costoFinal;
        }

        // Método para calcular plan de pagos
        public decimal[] GenerarPlanPagos(decimal monto, int cuotas)
        {
            if (cuotas <= 0 || cuotas > 12)
            {
                Console.WriteLine("Error: El número de cuotas debe estar entre 1 y 12.");
                return new decimal[0];
            }

            decimal[] plan = new decimal[cuotas];
            decimal montoCuota = monto / cuotas;

            // Si tiene más de 3 cuotas, aplicar interés del 5%
            if (cuotas > 3)
            {
                decimal interes = monto * 0.05m;
                montoCuota = (monto + interes) / cuotas;
            }

            for (int i = 0; i < cuotas; i++)
            {
                plan[i] = Math.Round(montoCuota, 2);
            }

            tienePlanPagos = true;
            return plan;
        }

        // Método para mostrar el plan de pagos
        public void MostrarPlanPagos(decimal monto, int cuotas)
        {
            decimal[] plan = GenerarPlanPagos(monto, cuotas);
            
            if (plan.Length == 0)
                return;

            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║         PLAN DE PAGOS                  ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine($"Monto total: ${monto:F2}");
            Console.WriteLine($"Número de cuotas: {cuotas}");
            Console.WriteLine($"Monto por cuota: ${plan[0]:F2}");
            
            if (cuotas > 3)
                Console.WriteLine("(Incluye interés del 5%)");
            
            Console.WriteLine("\nDetalle de cuotas:");
            for (int i = 0; i < plan.Length; i++)
            {
                Console.WriteLine($"  Cuota {i + 1}: ${plan[i]:F2}");
            }
            Console.WriteLine($"Total a pagar: ${plan.Sum():F2}");
        }

        // Método para obtener categoría de cliente
        public string ObtenerCategoriaCliente()
        {
            if (consultasRealizadas >= 20)
                return "VIP";
            else if (consultasRealizadas >= 10)
                return "Premium";
            else if (consultasRealizadas >= 5)
                return "Regular";
            else
                return "Nuevo";
        }

        // Override del método MostrarInformacion
        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine($"Tipo: Paciente Particular");
            Console.WriteLine($"Categoría: {ObtenerCategoriaCliente()}");
            Console.WriteLine($"Consultas realizadas: {consultasRealizadas}");
            
            if (descuentoFrecuente > 0)
                Console.WriteLine($"Descuento actual: {descuentoFrecuente * 100}%");
            
            Console.WriteLine($"Método de pago preferido: {metodoPagoPreferido}");
            
            if (tienePlanPagos)
                Console.WriteLine("Estado: Con plan de pagos activo");
        }

        // Override del método ToString
        public override string ToString()
        {
            string categoria = ObtenerCategoriaCliente();
            return $"{base.ToString()} - Particular [{categoria}]";
        }

        // Método para cambiar método de pago
        public void CambiarMetodoPago(string nuevoMetodo)
        {
            metodoPagoPreferido = nuevoMetodo;
            Console.WriteLine($"✓ Método de pago actualizado a: {nuevoMetodo}");
        }

        // Método para verificar elegibilidad para plan de pagos
        public bool EsElegiblePlanPagos(decimal monto)
        {
            // Pacientes regulares o superiores pueden acceder a planes de pago
            // Para montos mayores a $100
            return consultasRealizadas >= 5 && monto >= 100;
        }
    }
}