using System.Net;
using System.Net.Sockets;
using System.Text;
using Raylib_cs;

namespace Game.Develop;

class Connection
{
    TcpListener server;
    TcpClient blenderSocket;
    bool connected = false;

    public Connection()
    {
        server = new TcpListener(IPAddress.Any, 9228);
        server.Start();
        NewConnection();
    }

    public void NewConnection()
    {
        WaitForConnection();
        while (!connected)
        {
            ConnectionWaiter.WaitingConnectionScreen();
        }
    }

    async public void WaitForConnection()
    {
        blenderSocket = await server.AcceptTcpClientAsync();
        NetworkStream ns = blenderSocket.GetStream();
        byte[] connMesage = new byte[4096];
        connMesage = Encoding.Default.GetBytes("CONNECTED");
        ns.Write(connMesage, 0, connMesage.Length);
        connected = true;

        Raylib.GetTime();
    }
    
    public void Update()
    {
        NetworkStream ns = blenderSocket.GetStream();
        byte[] connMesage = new byte[1];
        ns.Write(connMesage, 0, connMesage.Length);
        ReadCommands();
        /*if (!blenderSocket.Connected)
        {
            Console.WriteLine("NOT CONNECTED");
            return;
        }
        NetworkStream ns = blenderSocket.GetStream();
        byte[] msg = new byte[1024];
        ns.Read(msg, 0, msg.Length);
        Console.WriteLine("MESSAGE RECIVED");*/
    }

    public void ReadCommands()
    {
        NetworkStream ns = blenderSocket.GetStream();
        byte[] data = new byte[1];
        ns.Read(data, 0, data.Length);
        for (int i = 0; i < data.Length; i++)
        {
            if(data[i] != 0)
            {
                //Console.WriteLine("READING DATA");
            }
        }

    }

    
}
