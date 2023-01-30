// See https://aka.ms/new-console-template for more information


using System.Net;

public class MessageReceivedEventArgs : EventArgs
{
    public byte[] RawData { get; set; }
    public IPEndPoint ClientEndPoint { get; set; }
}
