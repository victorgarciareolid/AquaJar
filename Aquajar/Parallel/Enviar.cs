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
        ColaCircular<Resultado> recibir;
        TcpListener server;
        IPAddress local = IPAddress.Parse("127.0.0.1");
        Int32 puerto = 13000;

        public Enviar(ColaCircular<Resultado> recibir)
        {
            enviar = new ColaCircular<Solicitud>(1000);
            this.recibir = recibir;
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

                while (true)
                {
                    Console.WriteLine("Esperando conexión...");
                    TcpClient cliente = server.AcceptTcpClient();
                    Console.WriteLine("Conectado!");

                    Stream s = cliente.GetStream();

                    data = null;
                    int i;

                    // Si la cola de envio esta vacia -> Esperar.
                    while (enviar.vacio())
                    {
                        Console.WriteLine("Esperando Solicitudes!");
                        Thread.Sleep(1500);
                    }

                    // Procesar Solictiud (Enviar)
                    data = enviar.sacar().ToString();
                    byte[] datab = System.Text.Encoding.ASCII.GetBytes(data);
                    s.Write(datab, 0, datab.Length);

                    // Esperar Recepción
                    byte[] datar = new byte[256];
                    s.Read(datar, 0, datar.Length);

                    // Procesar Resultado
                    data = System.Text.Encoding.ASCII.GetString(datar);
                    Resultado res = new Resultado();
                    res.parseResultado(data);
                    recibir.poner(res);

                    /*
                    while ((i = s.Read(recepcion, 0, recepcion.Length)) != 0) {
                        data = System.Text.Encoding.ASCII.GetString(recepcion, 0, i);
                        Console.WriteLine("Recibido: {0}", data);

                        data = data.ToLower();
                        string msge = "lo que quiiero enviar al rpi";
                        byte[] msgr = System.Text.Encoding.ASCII.GetBytes(msge);
                        s.Write(msgr, 0, msgr.Length);
                        Console.WriteLine("Enviado: {0}", msgr);
                    }
                    */

                    // cliente.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha habido una excepción: {0}", e.Message);
            }
            finally
            {
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
