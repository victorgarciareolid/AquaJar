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
                System.Diagnostics.Debug.WriteLine("Esperando conexión...");
                cliente = server.AcceptTcpClient();
                System.Diagnostics.Debug.WriteLine("Conectado!");
                while (true)
                {
                    Stream s = cliente.GetStream();

                    data = null;

                    // Si la cola de envio esta vacia -> Esperar.
                    while (enviar.vacio())
                    {
                        System.Diagnostics.Debug.Print("Esperando Solicitudes!");
                        Thread.Sleep(1500);
                    }

                    // Procesar Solictiud (Enviar)
                    Solicitud sol = enviar.sacar();
                    data = sol.toString();
                    byte[] datab = System.Text.Encoding.ASCII.GetBytes(data);
                    s.Write(datab, 0, datab.Length);
                    System.Diagnostics.Debug.Print("Enviando: {0}", sol.toString());
                    // Esperar Recepción
                    byte[] datar = new byte[256];
                    s.Read(datar, 0, datar.Length);
                    
                    // Procesar Resultado
                    data = System.Text.Encoding.ASCII.GetString(datar);
                    System.Diagnostics.Debug.Print("Recibido: {0}", data);

                    sol.res.parseResultado(data);
                    sol.completar();
                    Solicitud.numUsuarios -= 1;
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
            if (enviar.lleno())
            {
                Console.WriteLine("No se pueden hacer más solicilictudes!! Pruebas más tarde");
            }
            else
            {
                enviar.poner(sol);
            }
            
        }


    }
}
