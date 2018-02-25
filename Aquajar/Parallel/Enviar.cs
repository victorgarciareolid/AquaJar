using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aquajar.Cola;
using Aquajar.Solicitudes;

namespace Aquajar.Parallel
{
    public class Enviar
    {
        ColaCircular<Solicitud> enviar;
        TcpListener server;
        IPAddress local = IPAddress.Parse("127.0.0.1");
        Int32 puerto = 13000;
        TcpClient cliente;

        public Enviar()
        {
            enviar = new ColaCircular<Solicitud>(1000);
        }

        public void run()
        {
            try
            {
                // Create tcp link
                server = new TcpListener(local, puerto);
                server.Start();

                Byte[] recepcion = new Byte[256];
                string data;
                Console.WriteLine("Esperando conexión...");
                cliente = server.AcceptTcpClient();
                Console.WriteLine("Conectado!");
                while (true)
                {
                    Stream s = cliente.GetStream();

                    data = null;

                    // Si la cola de envio esta vacia -> Esperar.
                    while (enviar.vacio())
                    {
                        Console.WriteLine("Esperando Solicitudes!");
                        Thread.Sleep(1500);
                    }

                    // Procesar Solictiud (Enviar)
                    Solicitud sol = enviar.sacar();
                    data = enviar.sacar().ToString();
                    byte[] datab = System.Text.Encoding.ASCII.GetBytes(data);
                    s.Write(datab, 0, datab.Length);

                    // Esperar Recepción
                    byte[] datar = new byte[256];
                    s.Read(datar, 0, datar.Length);

                    // Procesar Resultado
                    data = System.Text.Encoding.ASCII.GetString(datar);
                    sol.completar();
                    sol.res.parseResultado(data);
                    cliente.Close();
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha habido una excepción: {0}", e.Message);
            }
            finally
            {
                cliente.Close();
                server.Stop();
            }

            
            // Ir enviando solicitudes de enviar y poner recepciones en la cola circular recibir 
        }

        public void resolver(Solicitud sol)
        {
            enviar.poner(sol);
        }


    }
}
