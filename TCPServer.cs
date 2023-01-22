using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class TCPServer
{
    // Thread signal.
    public static ManualResetEvent allDone = new ManualResetEvent(false);

    public static void Main(string[] args)
    {
        // Set the TcpListener on port 13000.
        Int32 port = 13000;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");

        // TcpListener server = new TcpListener(port);
        TcpListener server = new TcpListener(localAddr, port);

        // Start listening for client requests.
        server.Start();

        while (true)
        {
            // Set the event to nonsignaled state.
            allDone.Reset();

            // Start an asynchronous socket to listen for connections.
            Console.WriteLine("Waiting for a connection...");
            server.BeginAcceptTcpClient(new AsyncCallback(AcceptCallback), server);

            // Wait until a connection is made before continuing.
            allDone.WaitOne();
        }
    }

    public static void AcceptCallback(IAsyncResult ar)
    {
        // Signal the main thread to continue.
        allDone.Set();

        // Get the listener that handles the client request.
        TcpListener listener = (TcpListener)ar.AsyncState;

        // End the operation and get the connected TcpClient.
        TcpClient client = listener.EndAcceptTcpClient(ar);

        // Create a new thread to handle communication
        // with connected client
        Thread t = new Thread(() => HandleClient(client));
        t.Start();
    }

    public static void HandleClient(TcpClient client)
    {
        // Buffer for reading data
        Byte[] bytes = new Byte[256];
        String data = null;

        // Get a stream object for reading and writing
        NetworkStream stream = client.GetStream();

        int i;

        // Loop to receive all the data sent by the client.
        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
        {
            // Translate data bytes to a ASCII string.
            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
            Console.WriteLine("Received: {0}", data);

            // Process the data sent by the client.
            data = data.ToUpper();

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

            // Send back a response.
            stream.Write(msg, 0, msg.Length);
            Console.WriteLine("Sent: {0}", data);
        }

        // Shutdown and end connection
        client.Close();
    }
}
