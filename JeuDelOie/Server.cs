// See https://aka.ms/new-console-template for more information


using System.Net.Sockets;
using System.Net;

public class Server
{
    public event EventHandler<MessageReceivedEventArgs>
        MessageReceived;

    private readonly IPEndPoint ep;
    private readonly TcpListener srv;
    private readonly CancellationTokenSource source;

    const int PORT = 11000;

    public Server()
    {
        source = new CancellationTokenSource();
        ep = new IPEndPoint(IPAddress.Any, PORT);
        srv = new TcpListener(ep);
    }

    public async Task StartAsync(
        CancellationToken token = default)
    {
        srv.Start();

        Console.WriteLine("Server stated");
        while (true)
        {
            if (token.IsCancellationRequested)
            {
                Console.WriteLine("Stopping the listener");
                srv.Stop();
                break;
            }

            var client = await srv.AcceptTcpClientAsync(token);

            Console.WriteLine("Client Connected...");

            await Task.Run(() => HandleClient(client));

        }
    }
    private static async Task HandleClient(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        client.SendTimeout = 60 * 1000;
        client.ReceiveTimeout = 60 * 1000;  
        while (client.Connected)
        {
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            if (bytesRead == 0)
            {
                break;
            }

            string message = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Message from client: {message}");

            byte[] response = System.Text.Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(response, 0, response.Length);
            Console.WriteLine("Confirmation sent to client");
        }

        stream.Close();
        client.Close();
        Console.WriteLine("Client disconnected...");
    }

    public void Stop()
    {
        {
            source.Cancel();
        }
    }
}






