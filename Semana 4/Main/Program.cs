using System;

namespace AgendaTurnosClinica
{
    /// <summary>
    /// Clase principal del Sistema de Agenda de Turnos
    /// Universidad Estatal Amazónica - Estructura de Datos
    /// </summary>
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
                    Console.WriteLine("\n▸ Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void MostrarBienvenida()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                            ║");
            Console.WriteLine("║    SISTEMA DE AGENDA DE TURNOS - CLÍNICA MÉDICA UEA       ║");
            Console.WriteLine("║                                                            ║");
            Console.WriteLine("║    Universidad Estatal Amazónica                           ║");
            Console.WriteLine("║    Asignatura: Estructura de Datos                         ║");
            Console.WriteLine("║    Guía de Prácticas #01                                   ║");
            Console.WriteLine("║                                                            ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.WriteLine("\n▸ Presione cualquier tecla para iniciar...");
            Console.ReadKey();
        }

        static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║      SISTEMA DE AGENDA DE TURNOS - MENÚ PRINCIPAL         ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.WriteLine("\n┌─── GESTIÓN DE PACIENTES ───────────────────────────────────┐");
            Console.WriteLine("│  1. Registrar paciente con Obra Social                     │");
            Console.WriteLine("│  2. Registrar paciente Particular                          │");
            Console.WriteLine("│  7. Ver todos los pacientes                                │");
            Console.WriteLine("│ 11. Buscar paciente                                        │");
            Console.WriteLine("└────────────────────────────────────────────────────────────┘");
            
            Console.WriteLine("\n┌─── GESTIÓN DE TURNOS ──────────────────────────────────────┐");
            Console.WriteLine("│  3. Agendar nuevo turno                                    │");
            Console.WriteLine("│  4. Consultar turnos por fecha                             │");
            Console.WriteLine("│  5. Consultar turnos de un paciente                        │");
            Console.WriteLine("│  6. Consultar turnos por especialidad                      │");
            Console.WriteLine("│  8. Ver agenda completa de turnos                          │");
            Console.WriteLine("│  9. Cancelar turno                                         │");
            Console.WriteLine("│ 10. Marcar turno como atendido                             │");
            Console.WriteLine("└────────────────────────────────────────────────────────────┘");
            
            Console.WriteLine("\n┌─── REPORTES Y ESTADÍSTICAS ────────────────────────────────┐");
            Console.WriteLine("│ 12. Ver estadísticas del sistema                           │");
            Console.WriteLine("└────────────────────────────────────────────────────────────┘");
            
            Console.WriteLine("\n┌────────────────────────────────────────────────────────────┐");
            Console.WriteLine("│ 13. Salir del sistema                                      │");
            Console.WriteLine("└────────────────────────────────────────────────────────────┘");
            
            Console.Write("\n▸ Seleccione una opción: ");
        }

        static void RegistrarPacienteObraSocial()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║     REGISTRAR PACIENTE CON OBRA SOCIAL                     ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

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
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║        REGISTRAR PACIENTE PARTICULAR                       ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

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
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║            AGENDAR NUEVO TURNO                             ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

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
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║        CONSULTAR TURNOS POR FECHA                          ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

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
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║      CONSULTAR TURNOS DE UN PACIENTE                       ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

            Console.Write("Cédula del paciente: ");
            string cedula = Console.ReadLine();

            agenda.ConsultarTurnosPaciente(cedula);
        }

        static void ConsultarTurnosPorEspecialidad()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║      CONSULTAR TURNOS POR ESPECIALIDAD                     ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

            Console.Write("Ingrese la especialidad: ");
            string especialidad = Console.ReadLine();

            agenda.ConsultarTurnosPorEspecialidad(especialidad);
        }

        static void CancelarTurno()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║             CANCELAR TURNO                                 ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

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
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║        MARCAR TURNO COMO ATENDIDO                          ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

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
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║            BUSCAR PACIENTE                                 ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

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
                    Console.WriteLine("\n" + new string('═', 60));
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
                    Console.WriteLine($"\n{'═',60}");
                    Console.WriteLine($"SE ENCONTRARON {pacientes.Count} PACIENTE(S):");
                    Console.WriteLine($"{'═',60}");
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
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                            ║");
            Console.WriteLine("║    ¡Gracias por usar el Sistema de Agenda de Turnos!      ║");
            Console.WriteLine("║                                                            ║");
            Console.WriteLine("║    Universidad Estatal Amazónica                           ║");
            Console.WriteLine("║    Estructura de Datos - 2025                              ║");
            Console.WriteLine("║                                                            ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
        }

        static void CargarDatosPrueba()
        {
            Console.WriteLine("\n▸ Cargando datos de prueba...");

            // Pacientes con Obra Social
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

            // Pacientes Particulares
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

            // Turnos de prueba
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