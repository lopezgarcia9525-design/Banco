using System;

namespace BancoConsola
{
// *********************** CLIENTES BANCO ****************************
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

// *********************** NODO DE LOS CLIENTES ************************
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

// ****************** LISTAS ENLAZADAS  *******************************
    class ListaClientes
    {
        private NodoCliente cabeza;

        public ListaClientes()
        {
            cabeza = null;
        }

        public bool ClienteExiste(string cedula, string cuenta)
        {
            NodoCliente actual = cabeza;

            while (actual != null)
            {
                if (actual.Datos.Cedula == cedula ||
                    actual.Datos.NumeroCuenta == cuenta)
                {
                    return true;
                }

                actual = actual.Siguiente;
            }

            return false;
        }

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
                Console.WriteLine("*********************************");
                Console.WriteLine("Cedula: " + actual.Datos.Cedula);
                Console.WriteLine("Nombre: " + actual.Datos.Nombre);
                Console.WriteLine("Cuenta: " + actual.Datos.NumeroCuenta);
                Console.WriteLine("Saldo: $" + actual.Datos.Saldo);

                actual = actual.Siguiente;
            }
        }

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

// ************* COLA ****************************
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

        public void MostrarCola()
        {
            if (frente == null)
            {
                Console.WriteLine("La cola esta vacia.");
                return;
            }

            NodoCola actual = frente;

            Console.WriteLine("===== COLA DE ATENCION =====");

            while (actual != null)
            {
                Console.WriteLine(actual.Datos.Nombre + " - Cuenta: " + actual.Datos.NumeroCuenta);
                actual = actual.Siguiente;
            }
        }
    }

// ********************** TRANSACCIONES DEL BANCO *************************
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

// ********************** PILA ******************
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

        public bool EstaVacia()
        {
            return cima == null;
        }
    }

// ********************** BANCO *************************
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
// **************** REGISTRO CLIENTE EN EL BANCO **********************
        public void RegistrarCliente()
        {
            Console.Write("Cedula: ");
            string cedula = Console.ReadLine();

            Console.Write("Nombre Completo: ");
            string nombre = Console.ReadLine();

            Console.Write("Numero de cuenta: ");
            string cuenta = Console.ReadLine();

            Console.Write("Saldo inicial: ");
            double saldo = Convert.ToDouble(Console.ReadLine());

            if (listaClientes.ClienteExiste(cedula, cuenta))
            {
                Console.WriteLine("Cliente ya registrado.");
                return;
            }

            Cliente nuevo = new Cliente(cedula, nombre, cuenta, saldo);
            listaClientes.AgregarCliente(nuevo);
            Console.WriteLine("Cliente registrado correctamente.");
        }

        public void BuscarCliente()
        {
            Console.Write("Ingrese numero de cuenta: ");
            string cuenta = Console.ReadLine();

            Cliente cliente = listaClientes.BuscarClientePorCuenta(cuenta);

            if (cliente == null)
            {
                Console.WriteLine("Cliente no encontrado.");
            }
            else
            {
                Console.WriteLine("Nombre: " + cliente.Nombre);
                Console.WriteLine("Cedula: " + cliente.Cedula);
                Console.WriteLine("Saldo: $" + cliente.Saldo);
            }
        }
// ****************** MOSTRAR LOS CLIENTES EN EL BANCO  *****************
        public void MostrarClientes()
        {
            listaClientes.MostrarClientes();
        }

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

        public void AtenderCliente()
        {
            cola.Desencolar();
        }

        public void MostrarCola()
        {
            cola.MostrarCola();
        }

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

            Console.Write("Monto a depositar: ");
            double monto = Convert.ToDouble(Console.ReadLine());

            cliente.Saldo += monto;

            pila.Push(new Transaccion("deposito", cliente, monto));

            Console.WriteLine("Deposito realizado.");
        }

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

            Console.Write("Monto a retirar: ");
            double monto = Convert.ToDouble(Console.ReadLine());

            if (monto > cliente.Saldo)
            {
                Console.WriteLine("Saldo insuficiente.");
                return;
            }

            cliente.Saldo -= monto;

            pila.Push(new Transaccion("retiro", cliente, monto));
            Console.WriteLine("Retiro realizado.");
        }

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
                Console.WriteLine("Deposito deshecho.");
            }
            else if (ultima.Tipo == "retiro")
            {
                ultima.Cliente.Saldo += ultima.Monto;
                Console.WriteLine("Retiro deshecho.");
            }
        }

        public void MostrarInformacion()
        {
            Console.WriteLine("**** INFORMACION DEL BANCO ****** ");
            Console.WriteLine("Clientes registrados: " + listaClientes.ContarClientes());

            Console.WriteLine("Dinero total almacenado: $" + listaClientes.TotalDinero());
        }
    }

    // ******************** METODO PRINCIPAL ************************
    class Program
    {
        static void Main(string[] args)
        {
            Banco banco = new Banco();
            int opcion;

            do
            {
                Console.WriteLine("\n***** BANCO ***** ");
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
                Console.WriteLine("11. Informacion del banco"); 
                
                Console.WriteLine("12. Salir");

                Console.Write("Seleccione una opcion: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Entrada invalida.");
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
                        Console.WriteLine("Saliendo...");
                        break;

                    default:
                        Console.WriteLine("Opcion invalida.");
                        break;
                }

            } while (opcion != 12);
        }
    }
}
