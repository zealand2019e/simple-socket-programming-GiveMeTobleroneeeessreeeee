using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;

namespace EchoServer
{
    /// <summary>
    /// Eksemplet fra https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.tcplistener?redirectedfrom=MSDN&view=netcore-3.1
    /// </summary>
    public static class Server
    {
        public static void Start()
        {
            //IP = IPAddress.Loopback
            //Port = 7 eller 7777 (port 7 er standard for echo server)
            TcpListener server = null;
            try
            {
                Int32 port = 7;
                IPAddress localAddr = IPAddress.Parse("192.168.24.207");

                server = new TcpListener(localAddr, port);

                server.Start();

                Byte[] bytes = new byte[256];
                String data = null;

                while (true)
                {
                    Console.Write("Waiting for a connection...");

                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;
                    NetworkStream stream = client.GetStream();

                    int i;

                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);

                        data = data.ToUpper();

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);
                    }

                    client.Close();
                }

            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
                throw;
            }
            finally
            {
                server.Stop();
                
            }
                Console.WriteLine("/nHit enter to continue...");
                Console.Read();
        }

    }
}
