namespace WakeOnLan.IntegrationTests.Support
{
    using System.Net;
    using System.Net.Sockets;
    using UdpClient = NetCoreServer.UdpClient;

    public class TestUdpClient : UdpClient
    {
        public bool Connected { get; set; }
        public bool Disconnected { get; set; }
        public bool Errors { get; set; }

        public TestUdpClient(string address, int port) : base(address, port) { }

        protected override void OnConnected() { Connected = true; ReceiveAsync(); }
        protected override void OnDisconnected() { Disconnected = true; }
        protected override void OnReceived(EndPoint endpoint, byte[] buffer, long offset, long size) { ReceiveAsync(); }
        protected override void OnError(SocketError error) { Errors = true; }
    }
}
