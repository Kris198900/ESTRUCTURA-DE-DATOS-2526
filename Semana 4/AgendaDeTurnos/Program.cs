using System;
using System.Collections.Generic;
using System.Linq;

namespace AgendaTurnosClinica
{
    // ==================== CLASE PACIENTE (BASE) ====================
    /// <summary>
    /// Clase base que representa un paciente de la clínica
    /// </summary>
    public class Paciente
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public Paciente()
        {
            Cedula = "";
            Nombre = "";
            Apellido = "";
            Telefono = "";
            Email = "";
            FechaNacimiento = DateTime.Now;
        }

        public Paciente(string cedula, string nombre, string apellido, 
                       string telefono, string email, DateTime fechaNacimiento)
        {
            Cedula = cedula;
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
            Email = email;
            FechaNacimiento = fechaNacimiento;
        }

        public int ObtenerEdad()
        {
            int edad = DateTime.Now.Year - FechaNacimiento.Year;
            if (DateTime.Now.DayOfYear < FechaNacimiento.DayOfYear)
                edad--;
            return edad;
        }

        public string ObtenerNombreCompleto()
        {
            return $"{Nombre} {Apellido}";
        }

        public virtual bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(Cedula) || Cedula.Length < 10)
                return false;
            if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Apellido))
                return false;
            if (string.IsNullOrWhiteSpace(Telefono) || Telefono.Length < 10)
                return false;
            if (FechaNacimiento > DateTime.Now)
                return false;
            return true;
        }

        public override string ToString()
        {
            return $"{Cedula} - {Nombre} {Apellido} - Tel: {Telefono} - Edad: {ObtenerEdad()} años";
        }

        public virtual void MostrarInformacion()
        {
            Console.WriteLine($"Cédula: {Cedula}");
            Console.WriteLine($"Nombre: {Nombre} {Apellido}");
            Console.WriteLine($"Edad: {ObtenerEdad()} años");
            Console.WriteLine($"Teléfono: {Telefono}");
            Console.WriteLine($"Email: {Email}");
        }
    }

    // ==================== CLASE PACIENTE OBRA SOCIAL ====================
    /// <summary>
    /// Clase que representa un paciente con obra social
    /// </summary>
    public class PacienteObraSocial : Paciente
    {
        public string NombreObraSocial { get; set; }
        public string NumeroAfiliado { get; set; }
        public string PlanCobertura { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public bool TieneCopago { get; set; }
        public decimal MontoCopago { get; set; }

        public PacienteObraSocial() : base()
        {
            NombreObraSocial = "";
            NumeroAfiliado = "";
            PlanCobertura = "Básico";
            FechaVencimiento = DateTime.Now.AddYears(1);
            TieneCopago = false;
            MontoCopago = 0;
        }

        public PacienteObraSocial(string cedula, string nombre, string apellido,
                                 string telefono, string email, DateTime fechaNacimiento,
                                 string nombreObraSocial, string numeroAfiliado,
                                 string planCobertura, DateTime fechaVencimiento,
                                 bool tieneCopago, decimal montoCopago)
            : base(cedula, nombre, apellido, telefono, email, fechaNacimiento)
        {
            NombreObraSocial = nombreObraSocial;
            NumeroAfiliado = numeroAfiliado;
            PlanCobertura = planCobertura;
            FechaVencimiento = fechaVencimiento;
            TieneCopago = tieneCopago;
            MontoCopago = montoCopago;
        }

        public bool EstaVigente()
        {
            return FechaVencimiento >= DateTime.Now;
        }

        public bool TieneCobertura(string especialidad)
        {
            if (!EstaVigente())
                return false;

            switch (PlanCobertura.ToLower())
            {
                case "básico":
                case "basico":
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

        public decimal CalcularCostoConsulta(decimal costoBase)
        {
            if (!EstaVigente())
                return costoBase;
            if (TieneCopago)
                return MontoCopago;
            return 0;
        }

        public override bool ValidarDatos()
        {
            if (!base.ValidarDatos())
                return false;
            if (string.IsNullOrWhiteSpace(NombreObraSocial))
                return false;
            if (string.IsNullOrWhiteSpace(NumeroAfiliado))
                return false;
            return true;
        }

        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine($"Obra Social: {NombreObraSocial}");
            Console.WriteLine($"Número de Afiliado: {NumeroAfiliado}");
            Console.WriteLine($"Plan: {PlanCobertura}");
            Console.WriteLine($"Estado: {(EstaVigente() ? "VIGENTE" : "VENCIDA")}");
            Console.WriteLine($"Vencimiento: {FechaVencimiento:dd/MM/yyyy}");
            if (TieneCopago)
                Console.WriteLine($"Copago: ${MontoCopago:F2}");
            else
                Console.WriteLine("Sin copago");
        }

        public override string ToString()
        {
            string estado = EstaVigente() ? "✓" : "✗";
            return $"{base.ToString()} - OS: {NombreObraSocial} [{estado}]";
        }

        public void RenovarObraSocial(int meses)
        {
            FechaVencimiento = DateTime.Now.AddMonths(meses);
            Console.WriteLine($"✓ Obra social renovada hasta: {FechaVencimiento:dd/MM/yyyy}");
        }
    }

    // ==================== CLASE PACIENTE PARTICULAR ====================
    /// <summary>
    /// Clase que representa un paciente particular (sin obra social)
    /// </summary>
    public class PacienteParticular : Paciente
    {
        public decimal DescuentoFrecuente { get; set; }
        public int ConsultasRealizadas { get; set; }
        public bool TienePlanPagos { get; set; }
        public string MetodoPagoPreferido { get; set; }

        public PacienteParticular() : base()
        {
            DescuentoFrecuente = 0;
            ConsultasRealizadas = 0;
            TienePlanPagos = false;
            MetodoPagoPreferido = "Efectivo";
        }

        public PacienteParticular(string cedula, string nombre, string apellido,
                                 string telefono, string email, DateTime fechaNacimiento,
                                 string metodoPagoPreferido = "Efectivo")
            : base(cedula, nombre, apellido, telefono, email, fechaNacimiento)
        {
            DescuentoFrecuente = 0;
            ConsultasRealizadas = 0;
            TienePlanPagos = false;
            MetodoPagoPreferido = metodoPagoPreferido;
        }

        private void ActualizarDescuento()
        {
            if (ConsultasRealizadas >= 20)
                DescuentoFrecuente = 0.20m;
            else if (ConsultasRealizadas >= 10)
                DescuentoFrecuente = 0.15m;
            else if (ConsultasRealizadas >= 5)
                DescuentoFrecuente = 0.10m;
            else
                DescuentoFrecuente = 0;
        }

        public void RegistrarConsulta()
        {
            ConsultasRealizadas++;
            ActualizarDescuento();
            Console.WriteLine($"✓ Consulta registrada. Total: {ConsultasRealizadas}");
            if (DescuentoFrecuente > 0)
                Console.WriteLine($"  Descuento actual: {DescuentoFrecuente * 100}%");
        }

        public decimal CalcularCostoConsulta(decimal costoBase)
        {
            decimal costoFinal = costoBase;
            if (DescuentoFrecuente > 0)
            {
                decimal descuento = costoBase * DescuentoFrecuente;
                costoFinal -= descuento;
            }
            return costoFinal;
        }

        public string ObtenerCategoriaCliente()
        {
            if (ConsultasRealizadas >= 20)
                return "VIP";
            else if (ConsultasRealizadas >= 10)
                return "Premium";
            else if (ConsultasRealizadas >= 5)
                return "Regular";
            else
                return "Nuevo";
        }

        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine($"Tipo: Paciente Particular");
            Console.WriteLine($"Categoría: {ObtenerCategoriaCliente()}");
            Console.WriteLine($"Consultas realizadas: {ConsultasRealizadas}");
            if (DescuentoFrecuente > 0)
                Console.WriteLine($"Descuento actual: {DescuentoFrecuente * 100}%");
            Console.WriteLine($"Método de pago preferido: {MetodoPagoPreferido}");
        }

        public override string ToString()
        {
            string categoria = ObtenerCategoriaCliente();
            return $"{base.ToString()} - Particular [{categoria}]";
        }
    }

    // ==================== CLASE TURNO ====================
    /// <summary>
    /// Clase Turno - Representa una cita médica
    /// </summary>
    public class Turno
    {
        public int NumeroTurno { get; set; }
        public Paciente Paciente { get; set; }
        public DateTime FechaHora { get; set; }
        public string Especialidad { get; set; }
        public string Doctor { get; set; }
        public string Motivo { get; set; }
        public string Estado { get; set; }

        public Turno(int numeroTurno, Paciente paciente, DateTime fechaHora, 
                     string especialidad, string doctor, string motivo)
        {
            NumeroTurno = numeroTurno;
            Paciente = paciente;
            FechaHora = fechaHora;
            Especialidad = especialidad;
            Doctor = doctor;
            Motivo = motivo;
            Estado = "Programado";
        }

        public override string ToString()
        {
            string tipoPaciente = Paciente is PacienteObraSocial ? "OS" : "Particular";
            return $"Turno #{NumeroTurno} - {FechaHora:dd/MM/yyyy HH:mm} - {Especialidad}\n" +
                   $"   Paciente: {Paciente.Nombre} {Paciente.Apellido} ({Paciente.Cedula}) [{tipoPaciente}]\n" +
                   $"   Doctor: {Doctor} - Estado: {Estado}\n" +
                   $"   Motivo: {Motivo}";
        }
    }

    // ==================== CLASE AGENDA ====================
    /// <summary>
    /// Clase Agenda - Gestiona los turnos y pacientes de la clínica
    /// </summary>
    public class Agenda
    {
        private List<Paciente> pacientes;
        private List<Turno> turnos;
        private Dictionary<string, Paciente> indicePacientes;
        private Dictionary<string, List<Turno>> turnosPorFecha;
        private Dictionary<string, List<Turno>> turnosPorEspecialidad;
        private int contadorTurnos;

        public Agenda()
        {
            pacientes = new List<Paciente>();
            turnos = new List<Turno>();
            indicePacientes = new Dictionary<string, Paciente>();
            turnosPorFecha = new Dictionary<string, List<Turno>>();
            turnosPorEspecialidad = new Dictionary<string, List<Turno>>();
            contadorTurnos = 1;
        }

        public bool RegistrarPaciente(Paciente paciente)
        {
            if (indicePacientes.ContainsKey(paciente.Cedula))
            {
                Console.WriteLine("✗ Error: Ya existe un paciente con esa cédula.");
                return false;
            }

            if (!paciente.ValidarDatos())
            {
                Console.WriteLine("✗ Error: Los datos del paciente no son válidos.");
                return false;
            }

            pacientes.Add(paciente);
            indicePacientes[paciente.Cedula] = paciente;

            string tipo = paciente is PacienteObraSocial ? "con Obra Social" : "Particular";
            Console.WriteLine($"✓ Paciente {tipo} registrado: {paciente.Nombre} {paciente.Apellido}");
            return true;
        }

        public Paciente BuscarPaciente(string cedula)
        {
            if (indicePacientes.ContainsKey(cedula))
                return indicePacientes[cedula];
            return null;
        }

        public List<Paciente> BuscarPacientesPorApellido(string apellido)
        {
            return pacientes.Where(p => p.Apellido.ToLower().Contains(apellido.ToLower())).ToList();
        }

        public bool AgendarTurno(string cedula, DateTime fechaHora, string especialidad, 
                                string doctor, string motivo)
        {
            Paciente paciente = BuscarPaciente(cedula);
            if (paciente == null)
            {
                Console.WriteLine("✗ Error: Paciente no encontrado. Debe registrarlo primero.");
                return false;
            }

            if (fechaHora <= DateTime.Now)
            {
                Console.WriteLine("✗ Error: La fecha del turno debe ser futura.");
                return false;
            }

            if (!VerificarDisponibilidad(fechaHora, doctor))
            {
                Console.WriteLine("✗ Error: Ya existe un turno para ese doctor en ese horario.");
                return false;
            }

            if (paciente is PacienteObraSocial pacienteOS)
            {
                if (!pacienteOS.EstaVigente())
                {
                    Console.WriteLine("⚠ Advertencia: La obra social del paciente está vencida.");
                }
                else if (!pacienteOS.TieneCobertura(especialidad))
                {
                    Console.WriteLine($"⚠ Advertencia: La obra social no cubre {especialidad}.");
                }
            }

            Turno nuevoTurno = new Turno(contadorTurnos++, paciente, fechaHora, 
                                        especialidad, doctor, motivo);
            turnos.Add(nuevoTurno);

            string claveFecha = fechaHora.Date.ToString("yyyy-MM-dd");
            if (!turnosPorFecha.ContainsKey(claveFecha))
                turnosPorFecha[claveFecha] = new List<Turno>();
            turnosPorFecha[claveFecha].Add(nuevoTurno);

            if (!turnosPorEspecialidad.ContainsKey(especialidad))
                turnosPorEspecialidad[especialidad] = new List<Turno>();
            turnosPorEspecialidad[especialidad].Add(nuevoTurno);

            Console.WriteLine($"✓ Turno agendado exitosamente - Turno #{nuevoTurno.NumeroTurno}");
            return true;
        }

        private bool VerificarDisponibilidad(DateTime fechaHora, string doctor)
        {
            return !turnos.Any(t => t.FechaHora == fechaHora && 
                                    t.Doctor.ToLower() == doctor.ToLower() && 
                                    t.Estado != "Cancelado");
        }

        public void ConsultarTurnosPorFecha(DateTime fecha)
        {
            string claveFecha = fecha.Date.ToString("yyyy-MM-dd");
            
            Console.WriteLine("\n============================================================");
            Console.WriteLine($"TURNOS PARA EL DÍA: {fecha:dd/MM/yyyy}");
            Console.WriteLine("============================================================");

            if (!turnosPorFecha.ContainsKey(claveFecha) || turnosPorFecha[claveFecha].Count == 0)
            {
                Console.WriteLine("No hay turnos programados para esta fecha.");
                return;
            }

            var turnosOrdenados = turnosPorFecha[claveFecha]
                .Where(t => t.Estado != "Cancelado")
                .OrderBy(t => t.FechaHora)
                .ToList();

            if (turnosOrdenados.Count == 0)
            {
                Console.WriteLine("Todos los turnos de esta fecha fueron cancelados.");
                return;
            }

            foreach (var turno in turnosOrdenados)
            {
                Console.WriteLine($"\n{turno}");
                Console.WriteLine("------------------------------------------------------------");
            }
        }

        public void ConsultarTurnosPaciente(string cedula)
        {
            Paciente paciente = BuscarPaciente(cedula);
            if (paciente == null)
            {
                Console.WriteLine("✗ Error: Paciente no encontrado.");
                return;
            }

            Console.WriteLine("\n============================================================");
            Console.WriteLine($"TURNOS DEL PACIENTE: {paciente.Nombre} {paciente.Apellido}");
            Console.WriteLine("============================================================");

            var turnosPaciente = turnos
                .Where(t => t.Paciente.Cedula == cedula)
                .OrderBy(t => t.FechaHora)
                .ToList();

            if (turnosPaciente.Count == 0)
            {
                Console.WriteLine("Este paciente no tiene turnos programados.");
                return;
            }

            paciente.MostrarInformacion();
            Console.WriteLine("------------------------------------------------------------");

            foreach (var turno in turnosPaciente)
            {
                Console.WriteLine($"\n{turno}");
                Console.WriteLine("------------------------------------------------------------");
            }
        }

        public void ConsultarTurnosPorEspecialidad(string especialidad)
        {
            Console.WriteLine("\n============================================================");
            Console.WriteLine($"TURNOS DE {especialidad.ToUpper()}");
            Console.WriteLine("============================================================");

            if (!turnosPorEspecialidad.ContainsKey(especialidad))
            {
                Console.WriteLine($"No hay turnos para la especialidad: {especialidad}");
                return;
            }

            var turnosEsp = turnosPorEspecialidad[especialidad]
                .Where(t => t.Estado != "Cancelado")
                .OrderBy(t => t.FechaHora)
                .ToList();

            if (turnosEsp.Count == 0)
            {
                Console.WriteLine("No hay turnos activos para esta especialidad.");
                return;
            }

            foreach (var turno in turnosEsp)
            {
                Console.WriteLine($"\n{turno}");
                Console.WriteLine("------------------------------------------------------------");
            }
        }

        public void VisualizarPacientes()
        {
            Console.WriteLine("\n============================================================");
            Console.WriteLine($"LISTA DE PACIENTES REGISTRADOS ({pacientes.Count})");
            Console.WriteLine("============================================================");

            if (pacientes.Count == 0)
            {
                Console.WriteLine("No hay pacientes registrados.");
                return;
            }

            var pacientesOS = pacientes.OfType<PacienteObraSocial>().ToList();
            var pacientesParticulares = pacientes.OfType<PacienteParticular>().ToList();

            if (pacientesOS.Count > 0)
            {
                Console.WriteLine($"\nPACIENTES CON OBRA SOCIAL ({pacientesOS.Count}):");
                foreach (var p in pacientesOS.OrderBy(p => p.Apellido))
                {
                    Console.WriteLine($"  {p}");
                }
            }

            if (pacientesParticulares.Count > 0)
            {
                Console.WriteLine($"\nPACIENTES PARTICULARES ({pacientesParticulares.Count}):");
                foreach (var p in pacientesParticulares.OrderBy(p => p.Apellido))
                {
                    Console.WriteLine($"  {p}");
                }
            }
        }

        public void VisualizarTodosTurnos()
        {
            Console.WriteLine("\n============================================================");
            Console.WriteLine($"AGENDA COMPLETA DE TURNOS ({turnos.Count})");
            Console.WriteLine("============================================================");

            if (turnos.Count == 0)
            {
                Console.WriteLine("No hay turnos programados.");
                return;
            }

            var turnosActivos = turnos
                .Where(t => t.Estado != "Cancelado")
                .OrderBy(t => t.FechaHora)
                .ToList();

            if (turnosActivos.Count == 0)
            {
                Console.WriteLine("No hay turnos activos.");
                return;
            }

            foreach (var turno in turnosActivos)
            {
                Console.WriteLine($"\n{turno}");
                Console.WriteLine("------------------------------------------------------------");
            }
        }

        public bool CancelarTurno(int numeroTurno)
        {
            var turno = turnos.FirstOrDefault(t => t.NumeroTurno == numeroTurno);
            
            if (turno == null)
            {
                Console.WriteLine("✗ Error: Turno no encontrado.");
                return false;
            }

            if (turno.Estado == "Cancelado")
            {
                Console.WriteLine("⚠ Este turno ya fue cancelado.");
                return false;
            }

            turno.Estado = "Cancelado";
            Console.WriteLine($"✓ Turno #{numeroTurno} cancelado exitosamente.");
            return true;
        }

        public bool MarcarTurnoAtendido(int numeroTurno)
        {
            var turno = turnos.FirstOrDefault(t => t.NumeroTurno == numeroTurno);
            
            if (turno == null)
            {
                Console.WriteLine("✗ Error: Turno no encontrado.");
                return false;
            }

            if (turno.Estado == "Cancelado")
            {
                Console.WriteLine("✗ Error: No se puede atender un turno cancelado.");
                return false;
            }

            turno.Estado = "Atendido";
            
            if (turno.Paciente is PacienteParticular pacParticular)
            {
                pacParticular.RegistrarConsulta();
            }

            Console.WriteLine($"✓ Turno #{numeroTurno} marcado como atendido.");
            return true;
        }

        public void MostrarEstadisticas()
        {
            Console.WriteLine("\n============================================================");
            Console.WriteLine("ESTADÍSTICAS DEL SISTEMA");
            Console.WriteLine("============================================================");

            int totalOS = pacientes.OfType<PacienteObraSocial>().Count();
            int totalParticulares = pacientes.OfType<PacienteParticular>().Count();
            
            Console.WriteLine($"\nPACIENTES:");
            Console.WriteLine($"  Total registrados: {pacientes.Count}");
            Console.WriteLine($"  Con Obra Social: {totalOS}");
            Console.WriteLine($"  Particulares: {totalParticulares}");

            Console.WriteLine($"\nTURNOS:");
            Console.WriteLine($"  Total: {turnos.Count}");
            Console.WriteLine($"  Programados: {turnos.Count(t => t.Estado == "Programado")}");
            Console.WriteLine($"  Atendidos: {turnos.Count(t => t.Estado == "Atendido")}");
            Console.WriteLine($"  Cancelados: {turnos.Count(t => t.Estado == "Cancelado")}");

            if (turnos.Any())
            {
                var especialidadTop = turnos
                    .GroupBy(t => t.Especialidad)
                    .OrderByDescending(g => g.Count())
                    .First();
                
                Console.WriteLine($"\nESPECIALIDAD MÁS SOLICITADA:");
                Console.WriteLine($"  {especialidadTop.Key}: {especialidadTop.Count()} turnos");

                var doctorTop = turnos
                    .GroupBy(t => t.Doctor)
                    .OrderByDescending(g => g.Count())
                    .First();
                
                Console.WriteLine($"\nDOCTOR CON MÁS TURNOS:");
                Console.WriteLine($"  {doctorTop.Key}: {doctorTop.Count()} turnos");
            }
        }
    }

    // ==================== CLASE PROGRAM (MAIN) ====================
    class Program
    {
        static Agenda agenda = new Agenda();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            MostrarBienvenida();
            CargarDatosPrueba();

            bool continuar = true;
            while (continuar)
            {
                MostrarMenu();
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        RegistrarPacienteObraSocial();
                        break;
                    case "2":
                        RegistrarPacienteParticular();
                        break;
                    case "3":
                        AgendarNuevoTurno();
                        break;
                    case "4":
                        ConsultarTurnosFecha();
                        break;
                    case "5":
                        ConsultarTurnosPaciente();
                        break;
                    case "6":
                        ConsultarTurnosPorEspecialidad();
                        break;
                    case "7":
                        agenda.VisualizarPacientes();
                        break;
                    case "8":
                        agenda.VisualizarTodosTurnos();
                        break;
                    case "9":
                        CancelarTurno();
                        break;
                    case "10":
                        MarcarTurnoAtendido();
                        break;
                    case "11":
                        BuscarPaciente();
                        break;
                    case "12":
                        agenda.MostrarEstadisticas();
                        break;
                    case "13":
                        continuar = false;
                        MostrarDespedida();
                        break;
                    default:
                        Console.WriteLine("\n✗ Opción inválida. Intente nuevamente.");
                        break;
                }

                if (continuar)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void MostrarBienvenida()
        {
            Console.Clear();
            Console.WriteLine("============================================================");
            Console.WriteLine("                                                            ");
            Console.WriteLine("    SISTEMA DE AGENDA DE TURNOS - CLÍNICA MÉDICA UEA       ");
            Console.WriteLine("                                                            ");
            Console.WriteLine("    Universidad Estatal Amazónica                           ");
            Console.WriteLine("    Asignatura: Estructura de Datos                         ");
            Console.WriteLine("    Guía de Prácticas #01                                   ");
            Console.WriteLine("                                                            ");
            Console.WriteLine("============================================================");
            Console.WriteLine("\nPresione cualquier tecla para iniciar...");
            Console.ReadKey();
        }

        static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("============================================================");
            Console.WriteLine("      SISTEMA DE AGENDA DE TURNOS - MENÚ PRINCIPAL         ");
            Console.WriteLine("============================================================");
            Console.WriteLine("\n--- GESTIÓN DE PACIENTES ---");
            Console.WriteLine("  1. Registrar paciente con Obra Social");
            Console.WriteLine("  2. Registrar paciente Particular");
            Console.WriteLine("  7. Ver todos los pacientes");
            Console.WriteLine(" 11. Buscar paciente");
            
            Console.WriteLine("\n--- GESTIÓN DE TURNOS ---");
            Console.WriteLine("  3. Agendar nuevo turno");
            Console.WriteLine("  4. Consultar turnos por fecha");
            Console.WriteLine("  5. Consultar turnos de un paciente");
            Console.WriteLine("  6. Consultar turnos por especialidad");
            Console.WriteLine("  8. Ver agenda completa de turnos");
            Console.WriteLine("  9. Cancelar turno");
            Console.WriteLine(" 10. Marcar turno como atendido");
            
            Console.WriteLine("\n--- REPORTES Y ESTADÍSTICAS ---");
            Console.WriteLine(" 12. Ver estadísticas del sistema");
            
            Console.WriteLine("\n 13. Salir del sistema");
            
            Console.Write("\nSeleccione una opción: ");
        }

        static void RegistrarPacienteObraSocial()
        {
            Console.Clear();
            Console.WriteLine("============================================================");
            Console.WriteLine("     REGISTRAR PACIENTE CON OBRA SOCIAL                     ");
            Console.WriteLine("============================================================\n");

            try
            {
                Console.Write("Cédula: ");
                string cedula = Console.ReadLine();

                Console.Write("Nombre: ");
                string nombre = Console.ReadLine();

                Console.Write("Apellido: ");
                string apellido = Console.ReadLine();

                Console.Write("Teléfono: ");
                string telefono = Console.ReadLine();

                Console.Write("Email: ");
                string email = Console.ReadLine();

                Console.Write("Fecha de nacimiento (dd/MM/yyyy): ");
                DateTime fechaNacimiento;
                while (!DateTime.TryParse(Console.ReadLine(), out fechaNacimiento))
                {
                    Console.Write("✗ Formato inválido. Intente nuevamente (dd/MM/yyyy): ");
                }

                Console.WriteLine("\n--- DATOS DE OBRA SOCIAL ---");
                Console.Write("Nombre de la Obra Social: ");
                string nombreOS = Console.ReadLine();

                Console.Write("Número de Afiliado: ");
                string numeroAfiliado = Console.ReadLine();

                Console.WriteLine("\nPlanes disponibles:");
                Console.WriteLine("  1. Básico");
                Console.WriteLine("  2. Intermedio");
                Console.WriteLine("  3. Completo");
                Console.WriteLine("  4. Premium");
                Console.Write("Seleccione plan (1-4): ");
                string[] planes = { "Básico", "Intermedio", "Completo", "Premium" };
                int planSeleccionado;
                while (!int.TryParse(Console.ReadLine(), out planSeleccionado) || 
                       planSeleccionado < 1 || planSeleccionado > 4)
                {
                    Console.Write("✗ Opción inválida. Seleccione (1-4): ");
                }
                string plan = planes[planSeleccionado - 1];

                Console.Write("Fecha de vencimiento (dd/MM/yyyy): ");
                DateTime fechaVencimiento;
                while (!DateTime.TryParse(Console.ReadLine(), out fechaVencimiento))
                {
                    Console.Write("✗ Formato inválido. Intente nuevamente (dd/MM/yyyy): ");
                }

                Console.Write("¿Tiene copago? (S/N): ");
                bool tieneCopago = Console.ReadLine()?.ToUpper() == "S";

                decimal montoCopago = 0;
                if (tieneCopago)
                {
                    Console.Write("Monto del copago: $");
                    while (!decimal.TryParse(Console.ReadLine(), out montoCopago) || montoCopago < 0)
                    {
                        Console.Write("✗ Monto inválido. Intente nuevamente: $");
                    }
                }

                PacienteObraSocial paciente = new PacienteObraSocial(
                    cedula, nombre, apellido, telefono, email, fechaNacimiento,
                    nombreOS, numeroAfiliado, plan, fechaVencimiento, tieneCopago, montoCopago
                );

                agenda.RegistrarPaciente(paciente);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error al registrar paciente: {ex.Message}");
            }
        }

        static void RegistrarPacienteParticular()
        {
            Console.Clear();
            Console.WriteLine("============================================================");
            Console.WriteLine("        REGISTRAR PACIENTE PARTICULAR                       ");
            Console.WriteLine("============================================================\n");

            try
            {
                Console.Write("Cédula: ");
                string cedula = Console.ReadLine();

                Console.Write("Nombre: ");
                string nombre = Console.ReadLine();

                Console.Write("Apellido: ");
                string apellido = Console.ReadLine();

                Console.Write("Teléfono: ");
                string telefono = Console.ReadLine();

                Console.Write("Email: ");
                string email = Console.ReadLine();

                Console.Write("Fecha de nacimiento (dd/MM/yyyy): ");
                DateTime fechaNacimiento;
                while (!DateTime.TryParse(Console.ReadLine(), out fechaNacimiento))
                {
                    Console.Write("✗ Formato inválido. Intente nuevamente (dd/MM/yyyy): ");
                }

                Console.WriteLine("\nMétodos de pago disponibles:");
                Console.WriteLine("  1. Efectivo");
                Console.WriteLine("  2. Tarjeta de crédito");
                Console.WriteLine("  3. Tarjeta de débito");
                Console.WriteLine("  4. Transferencia");
                Console.Write("Seleccione método preferido (1-4): ");
                string[] metodos = { "Efectivo", "Tarjeta de crédito", "Tarjeta de débito", "Transferencia" };
                int metodoSeleccionado;
                while (!int.TryParse(Console.ReadLine(), out metodoSeleccionado) || 
                       metodoSeleccionado < 1 || metodoSeleccionado > 4)
                {
                    Console.Write("✗ Opción inválida. Seleccione (1-4): ");
                }
                string metodoPago = metodos[metodoSeleccionado - 1];

                PacienteParticular paciente = new PacienteParticular(
                    cedula, nombre, apellido, telefono, email, fechaNacimiento, metodoPago
                );

                agenda.RegistrarPaciente(paciente);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error al registrar paciente: {ex.Message}");
            }
        }

        static void AgendarNuevoTurno()
        {
            Console.Clear();
            Console.WriteLine("============================================================");
            Console.WriteLine("            AGENDAR NUEVO TURNO                             ");
            Console.WriteLine("============================================================\n");

            try
            {
                Console.Write("Cédula del paciente: ");
                string cedula = Console.ReadLine();

                Console.Write("Fecha y hora del turno (dd/MM/yyyy HH:mm): ");
                DateTime fechaHora;
                while (!DateTime.TryParse(Console.ReadLine(), out fechaHora))
                {
                    Console.Write("✗ Formato inválido. Intente nuevamente (dd/MM/yyyy HH:mm): ");
                }

                Console.WriteLine("\nEspecialidades disponibles:");
                Console.WriteLine("  1. Medicina General");
                Console.WriteLine("  2. Cardiología");
                Console.WriteLine("  3. Pediatría");
                Console.WriteLine("  4. Oftalmología");
                Console.WriteLine("  5. Traumatología");
                Console.WriteLine("  6. Otra");
                Console.Write("Seleccione especialidad (1-6): ");
                
                string[] especialidades = { "Medicina General", "Cardiología", "Pediatría", 
                                          "Oftalmología", "Traumatología", "Otra" };
                int espSeleccionada;
                while (!int.TryParse(Console.ReadLine(), out espSeleccionada) || 
                       espSeleccionada < 1 || espSeleccionada > 6)
                {
                    Console.Write("✗ Opción inválida. Seleccione (1-6): ");
                }
                
                string especialidad = especialidades[espSeleccionada - 1];
                if (especialidad == "Otra")
                {
                    Console.Write("Ingrese la especialidad: ");
                    especialidad = Console.ReadLine();
                }

                Console.Write("Nombre del doctor: ");
                string doctor = Console.ReadLine();

                Console.Write("Motivo de la consulta: ");
                string motivo = Console.ReadLine();

                agenda.AgendarTurno(cedula, fechaHora, especialidad, doctor, motivo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error al agendar turno: {ex.Message}");
            }
        }

        static void ConsultarTurnosFecha()
        {
            Console.Clear();
            Console.WriteLine("============================================================");
            Console.WriteLine("        CONSULTAR TURNOS POR FECHA                          ");
            Console.WriteLine("============================================================\n");

            Console.Write("Ingrese la fecha (dd/MM/yyyy): ");
            DateTime fecha;
            while (!DateTime.TryParse(Console.ReadLine(), out fecha))
            {
                Console.Write("✗ Formato inválido. Intente nuevamente (dd/MM/yyyy): ");
            }

            agenda.ConsultarTurnosPorFecha(fecha);
        }

        static void ConsultarTurnosPaciente()
        {
            Console.Clear();
            Console.WriteLine("============================================================");
            Console.WriteLine("      CONSULTAR TURNOS DE UN PACIENTE                       ");
            Console.WriteLine("============================================================\n");

            Console.Write("Cédula del paciente: ");
            string cedula = Console.ReadLine();

            agenda.ConsultarTurnosPaciente(cedula);
        }

        static void ConsultarTurnosPorEspecialidad()
        {
            Console.Clear();
            Console.WriteLine("============================================================");
            Console.WriteLine("      CONSULTAR TURNOS POR ESPECIALIDAD                     ");
            Console.WriteLine("============================================================\n");

            Console.Write("Ingrese la especialidad: ");
            string especialidad = Console.ReadLine();

            agenda.ConsultarTurnosPorEspecialidad(especialidad);
        }

        static void CancelarTurno()
        {
            Console.Clear();
            Console.WriteLine("============================================================");
            Console.WriteLine("             CANCELAR TURNO                                 ");
            Console.WriteLine("============================================================\n");

            Console.Write("Número de turno a cancelar: ");
            int numeroTurno;
            while (!int.TryParse(Console.ReadLine(), out numeroTurno))
            {
                Console.Write("✗ Número inválido. Intente nuevamente: ");
            }

            agenda.CancelarTurno(numeroTurno);
        }

        static void MarcarTurnoAtendido()
        {
            Console.Clear();
            Console.WriteLine("============================================================");
            Console.WriteLine("        MARCAR TURNO COMO ATENDIDO                          ");
            Console.WriteLine("============================================================\n");

            Console.Write("Número de turno: ");
            int numeroTurno;
            while (!int.TryParse(Console.ReadLine(), out numeroTurno))
            {
                Console.Write("✗ Número inválido. Intente nuevamente: ");
            }

            agenda.MarcarTurnoAtendido(numeroTurno);
        }

        static void BuscarPaciente()
        {
            Console.Clear();
            Console.WriteLine("============================================================");
            Console.WriteLine("            BUSCAR PACIENTE                                 ");
            Console.WriteLine("============================================================\n");

            Console.WriteLine("Buscar por:");
            Console.WriteLine("1. Cédula");
            Console.WriteLine("2. Apellido");
            Console.Write("\nSeleccione opción: ");
            
            string opcion = Console.ReadLine();

            if (opcion == "1")
            {
                Console.Write("\nCédula: ");
                string cedula = Console.ReadLine();
                var paciente = agenda.BuscarPaciente(cedula);
                
                if (paciente != null)
                {
                    Console.WriteLine("\n============================================================");
                    paciente.MostrarInformacion();
                }
                else
                {
                    Console.WriteLine("\n✗ Paciente no encontrado.");
                }
            }
            else if (opcion == "2")
            {
                Console.Write("\nApellido: ");
                string apellido = Console.ReadLine();
                var pacientes = agenda.BuscarPacientesPorApellido(apellido);
                
                if (pacientes.Count > 0)
                {
                    Console.WriteLine("\n============================================================");
                    Console.WriteLine($"SE ENCONTRARON {pacientes.Count} PACIENTE(S):");
                    Console.WriteLine("============================================================");
                    foreach (var p in pacientes)
                    {
                        Console.WriteLine($"\n{p}");
                    }
                }
                else
                {
                    Console.WriteLine("\n✗ No se encontraron pacientes con ese apellido.");
                }
            }
        }

        static void MostrarDespedida()
        {
            Console.Clear();
            Console.WriteLine("============================================================");
            Console.WriteLine("                                                            ");
            Console.WriteLine("    ¡Gracias por usar el Sistema de Agenda de Turnos!      ");
            Console.WriteLine("                                                            ");
            Console.WriteLine("    Universidad Estatal Amazónica                           ");
            Console.WriteLine("    Estructura de Datos - 2025                              ");
            Console.WriteLine("                                                            ");
            Console.WriteLine("============================================================");
        }

        static void CargarDatosPrueba()
        {
            Console.WriteLine("\nCargando datos de prueba...");

            var p1 = new PacienteObraSocial(
                "1725648392", "María", "González", "0987654321", "maria.g@email.com",
                new DateTime(1985, 3, 15), "IESS", "12345678", "Completo",
                new DateTime(2026, 12, 31), true, 5.00m
            );
            agenda.RegistrarPaciente(p1);

            var p2 = new PacienteObraSocial(
                "1723456789", "Juan", "Pérez", "0998765432", "juan.p@email.com",
                new DateTime(1990, 7, 22), "Salud S.A.", "87654321", "Premium",
                new DateTime(2026, 6, 30), false, 0
            );
            agenda.RegistrarPaciente(p2);

            var p3 = new PacienteParticular(
                "1734567890", "Ana", "Rodríguez", "0976543210", "ana.r@email.com",
                new DateTime(1978, 11, 8), "Tarjeta de crédito"
            );
            agenda.RegistrarPaciente(p3);

            var p4 = new PacienteParticular(
                "1745678901", "Carlos", "Méndez", "0965432109", "carlos.m@email.com",
                new DateTime(1995, 5, 20), "Efectivo"
            );
            agenda.RegistrarPaciente(p4);

            agenda.AgendarTurno("1725648392", new DateTime(2025, 12, 20, 9, 0, 0),
                "Medicina General", "Dr. Carlos Mendoza", "Control de presión arterial");

            agenda.AgendarTurno("1723456789", new DateTime(2025, 12, 20, 10, 30, 0),
                "Cardiología", "Dra. Patricia Luna", "Dolor en el pecho");

            agenda.AgendarTurno("1734567890", new DateTime(2025, 12, 21, 14, 0, 0),
                "Pediatría", "Dr. Roberto Silva", "Control de niño sano");

            agenda.AgendarTurno("1745678901", new DateTime(2025, 12, 22, 11, 0, 0),
                "Oftalmología", "Dra. Laura Vega", "Revisión de vista");

            agenda.AgendarTurno("1725648392", new DateTime(2025, 12, 23, 15, 30, 0),
                "Traumatología", "Dr. Miguel Torres", "Dolor en rodilla");

            Console.WriteLine("✓ Datos de prueba cargados exitosamente.\n");
        }
    }
}
