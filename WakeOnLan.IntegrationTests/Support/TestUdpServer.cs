namespace WakeOnLan.IntegrationTests.Support
{
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using UdpServer = NetCoreServer.UdpServer;

    class TestUdpServer : UdpServer
    {
        public StringBuilder ErrorsBuffer = new StringBuilder();
        public StringBuilder ReceivedBuffer = new StringBuilder();
        public StringBuilder EventsBuffer = new StringBuilder();

        public TestUdpServer(IPAddress address, int port) : base(address, port) { }

        protected override void OnStarted()
        {
            EventsBuffer.AppendLine("Udp Server Started");
            // Start receive datagrams
            ReceiveAsync();
        }

        protected override void OnReceived(EndPoint endpoint, byte[] buffer, long offset, long size)
        {
            ReceivedBuffer.Append(Encoding.UTF8.GetString(buffer, (int)offset, (int)size));

            // Echo the message back to the sender
            SendAsync(endpoint, buffer, 0, size);
        }

        protected override void OnSent(EndPoint endpoint, long sent)
        {
            // Continue receive datagrams
            ReceiveAsync();
        }

        protected override void OnError(SocketError error)
        {
            ErrorsBuffer.AppendLine($"Echo UDP server caught an error with code {error}");
        }
    }
}
