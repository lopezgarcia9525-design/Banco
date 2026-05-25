using System;

namespace BancoConsola
{
    // ********* CLIENTES ********************************
    class Cliente
    {
        public string Cedula;
        public string Nombre;
        public string NumeroCuenta;
        public double Saldo;

        public Cliente(string cedula, string nombre, string numeroCuenta, double saldo)
        {
            Cedula = cedula;
            Nombre = nombre;
            NumeroCuenta = numeroCuenta;
            Saldo = saldo;
        }
    }

    // ******** NODO CLIENTE ***************************************
    class NodoCliente
    {
        public Cliente Datos;
        public NodoCliente Siguiente;

        public NodoCliente(Cliente datos)
        {
            Datos = datos;
            Siguiente = null;
        }
    }

    // ************* LISTA ENLAZADA ************************************
    class ListaClientes
    {
        private NodoCliente cabeza;

        public ListaClientes()
        {
            cabeza = null;
        }

        // ****** VALIDAR CLIENTE DUPLICADO ***********************************
        public bool ClienteExiste(string cedula, string cuenta)
        {
            NodoCliente actual = cabeza;

            while (actual != null)
            {
                if (actual.Datos.Cedula == cedula || actual.Datos.NumeroCuenta == cuenta)
                {
                    return true;
                }

                actual = actual.Siguiente;
            }

            return false;
        }

        // ******* AGREGAR CLIENTE *******************************************
        public void AgregarCliente(Cliente cliente)
        {
            NodoCliente nuevo = new NodoCliente(cliente);

            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                NodoCliente actual = cabeza;

                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }

                actual.Siguiente = nuevo;
            }
        }

        // ********** BUSCAR LOS CLIENTE *************************************
        public Cliente BuscarClientePorCuenta(string cuenta)
        {
            NodoCliente actual = cabeza;

            while (actual != null)
            {
                if (actual.Datos.NumeroCuenta == cuenta)
                {
                    return actual.Datos;
                }

                actual = actual.Siguiente;
            }

            return null;
        }

        // ******** MOSTRAR CLIENTES REGISTRADOS O NO ******************
        public void MostrarClientes()
        {
            if (cabeza == null)
            {
                Console.WriteLine("No hay clientes registrados.");
                return;
            }

            NodoCliente actual = cabeza;

            while (actual != null)
            {
                Console.WriteLine("**************************************");
                Console.WriteLine("Cedula: " + actual.Datos.Cedula);
                Console.WriteLine("Nombre: " + actual.Datos.Nombre);
                Console.WriteLine("Cuenta: " + actual.Datos.NumeroCuenta);
                Console.WriteLine("Saldo: $" + actual.Datos.Saldo);

                actual = actual.Siguiente;
            }
        }

        // ********** CONTAR CLIENTES ******************************
        public int ContarClientes()
        {
            int contador = 0;

            NodoCliente actual = cabeza;

            while (actual != null)
            {
                contador++;
                actual = actual.Siguiente;
            }

            return contador;
        }

        // *******  TOTAL DINERO ********************************
        public double TotalDinero()
        {
            double total = 0;

            NodoCliente actual = cabeza;

            while (actual != null)
            {
                total += actual.Datos.Saldo;
                actual = actual.Siguiente;
            }

            return total;
        }
    }

    // ****** NODO COLA DE LOS CLIENTES *************************************** 
    class NodoCola
    {
        public Cliente Datos;
        public NodoCola Siguiente;

        public NodoCola(Cliente datos)
        {
            Datos = datos;
            Siguiente = null;
        }
    }

    // ******** COLA DE ATENCION DE LOS CLIENTES ****************************
    class ColaAtencion
    {
        private NodoCola frente;
        private NodoCola fin;

        public ColaAtencion()
        {
            frente = null;
            fin = null;
        }

        public void Encolar(Cliente cliente)
        {
            NodoCola nuevo = new NodoCola(cliente);

            if (frente == null)
            {
                frente = nuevo;
                fin = nuevo;
            }
            else
            {
                fin.Siguiente = nuevo;
                fin = nuevo;
            }
        }

        public void Desencolar()
        {
            if (frente == null)
            {
                Console.WriteLine("No hay clientes en la cola.");
                return;
            }

            Console.WriteLine("Atendiendo a: " + frente.Datos.Nombre);

            frente = frente.Siguiente;

            if (frente == null)
            {
                fin = null;
            }
        }

        // ************* MOSTRAR COLA DE LOS CLIENTES ********************************
       public void MostrarCola()
        {
            if (frente == null)
            {
                Console.WriteLine("La cola esta vacia.");
                return;
            }

            NodoCola actual = frente;

            Console.WriteLine(" ***** COLA DE ATENCION ******* ");

            while (actual != null)
            {
                Console.WriteLine(actual.Datos.Nombre + " - Cuenta: " + actual.Datos.NumeroCuenta);

                actual = actual.Siguiente;
            }
        }
    }

    // ******* TRANSACCIONES DE LOS  CLIENTE ********************
    class Transaccion
    {
        public string Tipo;
        public Cliente Cliente;
        public double Monto;

        public Transaccion(string tipo, Cliente cliente, double monto)
        {
            Tipo = tipo;
            Cliente = cliente;
            Monto = monto;
        }
    }

    // *********** NODO DE PILA *************************************
    class NodoPila
    {
        public Transaccion Datos;
        public NodoPila Siguiente;

        public NodoPila(Transaccion datos)
        {
            Datos = datos;
            Siguiente = null;
        }
    }

    // ******* PILA DE TRANSACCIONES *****************************************
    class PilaTransacciones
    {
        private NodoPila cima;

        public PilaTransacciones()
        {
            cima = null;
        }

        public void Push(Transaccion transaccion)
        {
            NodoPila nuevo = new NodoPila(transaccion);

            nuevo.Siguiente = cima;
            cima = nuevo;
        }

        public Transaccion Pop()
        {
            if (cima == null)
            {
                return null;
            }

            Transaccion temp = cima.Datos;
            cima = cima.Siguiente;
            return temp;
        }

    // ********* VALIDAR VACIA *****************************************
    public bool EstaVacia()
        {
            return cima == null;
        }
    }

    // ********* BANCO *********************************
    class Banco
    {
        private ListaClientes listaClientes;
        private ColaAtencion cola;
        private PilaTransacciones pila;

        public Banco()
        {
            listaClientes = new ListaClientes();
            cola = new ColaAtencion();
            pila = new PilaTransacciones();
        }

        // ***** VALIDACION DE NUMEROS *****************************
        private bool EsSoloNumeros(string texto)
        {
            for (int i = 0; i < texto.Length; i++)
            {
                if (texto[i] < '0' || texto[i] > '9')
                {
                    return false;
                }
            }

            return true;
        }

        // ********* REGISTRO DE LOS CLIENTE ***************************
        public void RegistrarCliente()
        {
            string cedula;

            do
            {
                Console.Write("Cedula (6 a 10 digitos): ");
                cedula = Console.ReadLine();

                if (!EsSoloNumeros(cedula))
                {
                    Console.WriteLine("La cedula solo puede contener numeros.");
                    continue;
                }

                if (cedula.Length < 6 || cedula.Length > 10)
                {
                    Console.WriteLine("La cedula debe tener entre 6 y 10 digitos.");
                }

            } while (!EsSoloNumeros(cedula) || cedula.Length < 6 || cedula.Length > 10);

            Console.Write("Nombre completo: ");
            string nombre = Console.ReadLine();
            

            string cuenta;

            do
            {
                Console.Write("Numero de cuenta (11 digitos): ");
                cuenta = Console.ReadLine();

                if (!EsSoloNumeros(cuenta))
                {
                    Console.WriteLine("La cuenta solo puede contener numeros.");
                    continue;
                }

                if (cuenta.Length != 11)
                {
                    Console.WriteLine("La cuenta debe tener exactamente 11 digitos.");
                }

            } while (!EsSoloNumeros(cuenta) || cuenta.Length != 11);

            double saldo;

            do
            {
                Console.Write("Saldo inicial: ");

                if (!double.TryParse(Console.ReadLine(), out saldo))
                {
                    Console.WriteLine("Ingrese un numero valido.");
                    saldo = -1;
                }

                if (saldo < 0)
                {
                    Console.WriteLine("El saldo no puede ser negativo.");
                }

            } while (saldo < 0);

            if (listaClientes.ClienteExiste(cedula, cuenta))
            {
                Console.WriteLine("El cliente ya existe.");
                return;
            }

            Cliente nuevo = new Cliente(cedula, nombre, cuenta, saldo);
            listaClientes.AgregarCliente(nuevo);
            Console.WriteLine("Cliente registrado correctamente.");
        }

        // ******* BUSCAR LOS CLIENTE YA REGISTRADOS ******************************
        public void BuscarCliente()
        {
            Console.Write("Numero de cuenta: ");
            string cuenta = Console.ReadLine();

            Cliente cliente = listaClientes.BuscarClientePorCuenta(cuenta);

            if (cliente == null)
            {
                Console.WriteLine("Cliente no encontrado.");
                return;
            }

            Console.WriteLine(" **************************** ");
            Console.WriteLine("Cedula: " + cliente.Cedula);
            Console.WriteLine("Nombre: " + cliente.Nombre);
            Console.WriteLine("Cuenta: " + cliente.NumeroCuenta);
            Console.WriteLine("Saldo: $" + cliente.Saldo);
        }

        // ******** MOSTRAR CLIENTE YA REGISTRADOS **********************
        public void MostrarClientes()
        {
            listaClientes.MostrarClientes();
        }

        // ******** AGREGAR CLIENTE EN COLA ******************
        public void AgregarCola()
        {
            Console.Write("Numero de cuenta: ");
            string cuenta = Console.ReadLine();

            Cliente cliente = listaClientes.BuscarClientePorCuenta(cuenta);

            if (cliente == null)
            {
                Console.WriteLine("Cliente no encontrado.");
                return;
            }

            cola.Encolar(cliente);
            Console.WriteLine("Cliente agregado a la cola.");
        }

        // ******* ATENDER LOS CLIENTE ***************************
        public void AtenderCliente()
        {
            cola.Desencolar();
        }

        // ******** MOSTRAR LOS CLIENTE EN COLA *****************************
        public void MostrarCola()
        {
            cola.MostrarCola();
        }

        // ******** DEPOSITAR DINERO ********************************
        public void Depositar()
        {
            Console.Write("Numero de cuenta: ");
            string cuenta = Console.ReadLine();

            Cliente cliente = listaClientes.BuscarClientePorCuenta(cuenta);

            if (cliente == null)
            {
                Console.WriteLine("Cliente no encontrado.");
                return;
            }

            double monto;

            do
            {
                Console.Write("Monto a depositar: ");

                if (!double.TryParse(Console.ReadLine(), out monto))
                {
                    Console.WriteLine("Ingrese un monto valido.");
                    monto = -1;
                }

                if (monto <= 0)
                {
                    Console.WriteLine("El monto debe ser mayor a 0.");
                }

            } while (monto <= 0);

            cliente.Saldo += monto;

            pila.Push(new Transaccion("deposito", cliente, monto));

            Console.WriteLine("Deposito realizado correctamente.");
        }

        // ****** RETIRO DEL DINERO DE LOS CLIENTES ************************
        public void Retirar()
        {
            Console.Write("Numero de cuenta: ");
            string cuenta = Console.ReadLine();

            Cliente cliente = listaClientes.BuscarClientePorCuenta(cuenta);

            if (cliente == null)
            {
                Console.WriteLine("Cliente no encontrado.");
                return;
            }

            double monto;

            do
            {
                Console.Write("Monto a retirar: ");

                if (!double.TryParse(Console.ReadLine(), out monto))
                {
                    Console.WriteLine("Ingrese un monto valido.");
                    monto = -1;
                }

                if (monto <= 0)
                {
                    Console.WriteLine("El monto debe ser mayor a 0.");
                }

            } while (monto <= 0);

            if (monto > cliente.Saldo)
            {
                Console.WriteLine("Saldo insuficiente.");
                return;
            }

            cliente.Saldo -= monto;

            pila.Push(new Transaccion("retiro", cliente, monto));

            Console.WriteLine("Retiro realizado correctamente.");
        }

        // ************* CONSULTAR LOS SALDOS DEL CLIENTE *****************
        public void ConsultarSaldo()
        {
            Console.Write("Numero de cuenta: ");
            string cuenta = Console.ReadLine();

            Cliente cliente = listaClientes.BuscarClientePorCuenta(cuenta);

            if (cliente == null)
            {
                Console.WriteLine("Cliente no encontrado.");
                return;
            }

            Console.WriteLine("Saldo actual: $" + cliente.Saldo);
        }

        //  ************ DESHACER LAS TRANSACCIONES **********
        public void DeshacerTransaccion()
        {
            if (pila.EstaVacia())
            {
                Console.WriteLine("No hay transacciones para deshacer.");
                return;
            }

            Transaccion ultima = pila.Pop();

            if (ultima.Tipo == "deposito")
            {
                ultima.Cliente.Saldo -= ultima.Monto;
                Console.WriteLine("Deposito deshecho correctamente.");
            }
            else if (ultima.Tipo == "retiro")
            {
                ultima.Cliente.Saldo += ultima.Monto;
                Console.WriteLine("Retiro deshecho correctamente.");
            }
        }

        // ****** INFORMACION GENERAL DEL BANCO **********
        public void MostrarInformacion()
        {
            Console.WriteLine(" ***** INFORMACION GENERAL ***** ");
            Console.WriteLine("Clientes registrados: " + listaClientes.ContarClientes());

            Console.WriteLine("Dinero total del banco: $" + listaClientes.TotalDinero());
        }
    }

    //***** METODO PRINCIPAL ******
    class Program
    {
        static void Main(string[] args)
        {
            Banco banco = new Banco();

            int opcion;

            do
            {
                Console.WriteLine("\n************ BANCO ************ ");
                Console.WriteLine("1. Registrar cliente");
                Console.WriteLine("2. Buscar cliente");
                Console.WriteLine("3. Mostrar clientes");
                Console.WriteLine("4. Agregar cliente a cola");
                Console.WriteLine("5. Atender cliente");
                Console.WriteLine("6. Mostrar cola");
                Console.WriteLine("7. Depositar");
                Console.WriteLine("8. Retirar");
                Console.WriteLine("9. Consultar saldo");
                Console.WriteLine("10. Deshacer transaccion");
                Console.WriteLine("11. Informacion general");
                Console.WriteLine("12. Salir");

                Console.Write("Seleccione una opcion: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Ingrese una opcion valida.");
                    opcion = 0;
                }

                switch (opcion)
                {
                    case 1:
                        banco.RegistrarCliente();
                        break;

                    case 2:
                        banco.BuscarCliente();
                        break;

                    case 3:
                        banco.MostrarClientes();
                        break;

                    case 4:
                        banco.AgregarCola();
                        break;

                    case 5:
                        banco.AtenderCliente();
                        break;

                    case 6:
                        banco.MostrarCola();
                        break;

                    case 7:
                        banco.Depositar();
                        break;

                    case 8:
                        banco.Retirar();
                        break;

                    case 9:
                        banco.ConsultarSaldo();
                        break;

                    case 10:
                        banco.DeshacerTransaccion();
                        break;

                    case 11:
                        banco.MostrarInformacion();
                        break;

                    case 12:
                        Console.WriteLine("Gracias por usar el sistema.");
                        break;

                    default:
                        Console.WriteLine("Opcion invalida.");
                        break;
                }

            } while (opcion != 12);
        }
    }
}
