using System;
using System.Collections.Generic;
using System.Linq;

namespace AgendaTurnosClinica
{
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
        public string Estado { get; set; } // Programado, Atendido, Cancelado

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

    /// <summary>
    /// Clase Agenda - Gestiona los turnos y pacientes de la clínica
    /// </summary>
    public class Agenda
    {
        // ====== ESTRUCTURAS DE DATOS UTILIZADAS ======
        
        // 1. VECTOR DINÁMICO - Lista de pacientes
        private List<Paciente> pacientes;
        
        // 2. VECTOR DINÁMICO - Lista de turnos
        private List<Turno> turnos;
        
        // 3. ESTRUCTURA HASH - Índice para búsqueda rápida por cédula
        private Dictionary<string, Paciente> indicePacientes;
        
        // 4. MATRIZ ASOCIATIVA - Organiza turnos por fecha
        private Dictionary<string, List<Turno>> turnosPorFecha;
        
        // 5. ESTRUCTURA HASH - Índice de turnos por especialidad
        private Dictionary<string, List<Turno>> turnosPorEspecialidad;

        private int contadorTurnos;

        // Constructor
        public Agenda()
        {
            pacientes = new List<Paciente>();
            turnos = new List<Turno>();
            indicePacientes = new Dictionary<string, Paciente>();
            turnosPorFecha = new Dictionary<string, List<Turno>>();
            turnosPorEspecialidad = new Dictionary<string, List<Turno>>();
            contadorTurnos = 1;
        }

        // ====== MÉTODOS DE GESTIÓN DE PACIENTES ======

        /// <summary>
        /// Registra un nuevo paciente en el sistema
        /// </summary>
        public bool RegistrarPaciente(Paciente paciente)
        {
            // Validar que no exista la cédula
            if (indicePacientes.ContainsKey(paciente.Cedula))
            {
                Console.WriteLine("✗ Error: Ya existe un paciente con esa cédula.");
                return false;
            }

            // Validar datos del paciente
            if (!paciente.ValidarDatos())
            {
                Console.WriteLine("✗ Error: Los datos del paciente no son válidos.");
                return false;
            }

            // Agregar a las estructuras de datos
            pacientes.Add(paciente);
            indicePacientes[paciente.Cedula] = paciente;

            string tipo = paciente is PacienteObraSocial ? "con Obra Social" : "Particular";
            Console.WriteLine($"✓ Paciente {tipo} registrado: {paciente.Nombre} {paciente.Apellido}");
            return true;
        }

        /// <summary>
        /// Busca un paciente por cédula - Búsqueda O(1)
        /// </summary>
        public Paciente BuscarPaciente(string cedula)
        {
            if (indicePacientes.ContainsKey(cedula))
                return indicePacientes[cedula];
            return null;
        }

        /// <summary>
        /// Busca pacientes por apellido - Búsqueda O(n)
        /// </summary>
        public List<Paciente> BuscarPacientesPorApellido(string apellido)
        {
            return pacientes.Where(p => p.Apellido.ToLower().Contains(apellido.ToLower())).ToList();
        }

        // ====== MÉTODOS DE GESTIÓN DE TURNOS ======

        /// <summary>
        /// Agenda un nuevo turno médico
        /// </summary>
        public bool AgendarTurno(string cedula, DateTime fechaHora, string especialidad, 
                                string doctor, string motivo)
        {
            // Buscar paciente
            Paciente paciente = BuscarPaciente(cedula);
            if (paciente == null)
            {
                Console.WriteLine("✗ Error: Paciente no encontrado. Debe registrarlo primero.");
                return false;
            }

            // Validar fecha futura
            if (fechaHora <= DateTime.Now)
            {
                Console.WriteLine("✗ Error: La fecha del turno debe ser futura.");
                return false;
            }

            // Verificar disponibilidad del doctor
            if (!VerificarDisponibilidad(fechaHora, doctor))
            {
                Console.WriteLine("✗ Error: Ya existe un turno para ese doctor en ese horario.");
                return false;
            }

            // Verificar cobertura si es paciente con obra social
            if (paciente is PacienteObraSocial pacienteOS)
            {
                if (!pacienteOS.EstaVigente())
                {
                    Console.WriteLine("⚠ Advertencia: La obra social del paciente está vencida.");
                    Console.Write("¿Desea continuar como paciente particular? (S/N): ");
                    // En producción, aquí se debería esperar respuesta del usuario
                }
                else if (!pacienteOS.TieneCobertura(especialidad))
                {
                    Console.WriteLine($"⚠ Advertencia: La obra social no cubre {especialidad}.");
                }
            }

            // Crear el turno
            Turno nuevoTurno = new Turno(contadorTurnos++, paciente, fechaHora, 
                                        especialidad, doctor, motivo);
            turnos.Add(nuevoTurno);

            // Agregar a matriz de fechas
            string claveFecha = fechaHora.Date.ToString("yyyy-MM-dd");
            if (!turnosPorFecha.ContainsKey(claveFecha))
                turnosPorFecha[claveFecha] = new List<Turno>();
            turnosPorFecha[claveFecha].Add(nuevoTurno);

            // Agregar a índice de especialidades
            if (!turnosPorEspecialidad.ContainsKey(especialidad))
                turnosPorEspecialidad[especialidad] = new List<Turno>();
            turnosPorEspecialidad[especialidad].Add(nuevoTurno);

            Console.WriteLine($"✓ Turno agendado exitosamente - Turno #{nuevoTurno.NumeroTurno}");
            return true;
        }

        /// <summary>
        /// Verifica disponibilidad de horario para un doctor
        /// </summary>
        private bool VerificarDisponibilidad(DateTime fechaHora, string doctor)
        {
            return !turnos.Any(t => t.FechaHora == fechaHora && 
                                    t.Doctor.ToLower() == doctor.ToLower() && 
                                    t.Estado != "Cancelado");
        }

        // ====== MÉTODOS DE CONSULTA Y REPORTERÍA ======

        /// <summary>
        /// Consulta turnos por fecha específica
        /// </summary>
        public void ConsultarTurnosPorFecha(DateTime fecha)
        {
            string claveFecha = fecha.Date.ToString("yyyy-MM-dd");
            
            Console.WriteLine($"\n{'═',60}");
            Console.WriteLine($"TURNOS PARA EL DÍA: {fecha:dd/MM/yyyy}");
            Console.WriteLine($"{'═',60}");

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
                Console.WriteLine(new string('─', 60));
            }
        }

        /// <summary>
        /// Consulta turnos de un paciente específico
        /// </summary>
        public void ConsultarTurnosPaciente(string cedula)
        {
            Paciente paciente = BuscarPaciente(cedula);
            if (paciente == null)
            {
                Console.WriteLine("✗ Error: Paciente no encontrado.");
                return;
            }

            Console.WriteLine($"\n{'═',60}");
            Console.WriteLine($"TURNOS DEL PACIENTE: {paciente.Nombre} {paciente.Apellido}");
            Console.WriteLine($"{'═',60}");

            var turnosPaciente = turnos
                .Where(t => t.Paciente.Cedula == cedula)
                .OrderBy(t => t.FechaHora)
                .ToList();

            if (turnosPaciente.Count == 0)
            {
                Console.WriteLine("Este paciente no tiene turnos programados.");
                return;
            }

            // Mostrar información del paciente
            paciente.MostrarInformacion();
            Console.WriteLine(new string('─', 60));

            foreach (var turno in turnosPaciente)
            {
                Console.WriteLine($"\n{turno}");
                Console.WriteLine(new string('─', 60));
            }
        }

        /// <summary>
        /// Consulta turnos por especialidad
        /// </summary>
        public void ConsultarTurnosPorEspecialidad(string especialidad)
        {
            Console.WriteLine($"\n{'═',60}");
            Console.WriteLine($"TURNOS DE {especialidad.ToUpper()}");
            Console.WriteLine($"{'═',60}");

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
                Console.WriteLine(new string('─', 60));
            }
        }

        /// <summary>
        /// Visualiza todos los pacientes registrados
        /// </summary>
        public void VisualizarPacientes()
        {
            Console.WriteLine($"\n{'═',60}");
            Console.WriteLine($"LISTA DE PACIENTES REGISTRADOS ({pacientes.Count})");
            Console.WriteLine($"{'═',60}");

            if (pacientes.Count == 0)
            {
                Console.WriteLine("No hay pacientes registrados.");
                return;
            }

            // Separar por tipo
            var pacientesOS = pacientes.OfType<PacienteObraSocial>().ToList();
            var pacientesParticulares = pacientes.OfType<PacienteParticular>().ToList();

            if (pacientesOS.Count > 0)
            {
                Console.WriteLine($"\n▸ PACIENTES CON OBRA SOCIAL ({pacientesOS.Count}):");
                foreach (var p in pacientesOS.OrderBy(p => p.Apellido))
                {
                    Console.WriteLine($"  {p}");
                }
            }

            if (pacientesParticulares.Count > 0)
            {
                Console.WriteLine($"\n▸ PACIENTES PARTICULARES ({pacientesParticulares.Count}):");
                foreach (var p in pacientesParticulares.OrderBy(p => p.Apellido))
                {
                    Console.WriteLine($"  {p}");
                }
            }
        }

        /// <summary>
        /// Visualiza la agenda completa de turnos
        /// </summary>
        public void VisualizarTodosTurnos()
        {
            Console.WriteLine($"\n{'═',60}");
            Console.WriteLine($"AGENDA COMPLETA DE TURNOS ({turnos.Count})");
            Console.WriteLine($"{'═',60}");

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
                Console.WriteLine(new string('─', 60));
            }
        }

        // ====== MÉTODOS DE MODIFICACIÓN ======

        /// <summary>
        /// Cancela un turno específico
        /// </summary>
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

        /// <summary>
        /// Marca un turno como atendido
        /// </summary>
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
            
            // Si es paciente particular, registrar la consulta
            if (turno.Paciente is PacienteParticular pacParticular)
            {
                pacParticular.RegistrarConsulta();
            }

            Console.WriteLine($"✓ Turno #{numeroTurno} marcado como atendido.");
            return true;
        }

        // ====== MÉTODOS DE ESTADÍSTICAS ======

        /// <summary>
        /// Muestra estadísticas generales del sistema
        /// </summary>
        public void MostrarEstadisticas()
        {
            Console.WriteLine($"\n{'═',60}");
            Console.WriteLine("ESTADÍSTICAS DEL SISTEMA");
            Console.WriteLine($"{'═',60}");

            // Estadísticas de pacientes
            int totalOS = pacientes.OfType<PacienteObraSocial>().Count();
            int totalParticulares = pacientes.OfType<PacienteParticular>().Count();
            
            Console.WriteLine($"\n▸ PACIENTES:");
            Console.WriteLine($"  Total registrados: {pacientes.Count}");
            Console.WriteLine($"  Con Obra Social: {totalOS}");
            Console.WriteLine($"  Particulares: {totalParticulares}");

            // Estadísticas de turnos
            Console.WriteLine($"\n▸ TURNOS:");
            Console.WriteLine($"  Total: {turnos.Count}");
            Console.WriteLine($"  Programados: {turnos.Count(t => t.Estado == "Programado")}");
            Console.WriteLine($"  Atendidos: {turnos.Count(t => t.Estado == "Atendido")}");
            Console.WriteLine($"  Cancelados: {turnos.Count(t => t.Estado == "Cancelado")}");

            // Especialidad más solicitada
            if (turnos.Any())
            {
                var especialidadTop = turnos
                    .GroupBy(t => t.Especialidad)
                    .OrderByDescending(g => g.Count())
                    .First();
                
                Console.WriteLine($"\n▸ ESPECIALIDAD MÁS SOLICITADA:");
                Console.WriteLine($"  {especialidadTop.Key}: {especialidadTop.Count()} turnos");

                // Doctor con más turnos
                var doctorTop = turnos
                    .GroupBy(t => t.Doctor)
                    .OrderByDescending(g => g.Count())
                    .First();
                
                Console.WriteLine($"\n▸ DOCTOR CON MÁS TURNOS:");
                Console.WriteLine($"  {doctorTop.Key}: {doctorTop.Count()} turnos");
            }
        }
    }
}