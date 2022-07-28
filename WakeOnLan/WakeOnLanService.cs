namespace WakeOnLan
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    /// The magic packet is a broadcast frame containing anywhere within its payload 6 bytes of all 255 (FF FF FF FF FF FF in hexadecimal), 
    /// followed by sixteen repetitions of the target computer's 48-bit MAC address, for a total of 102 bytes.
    /// </summary>
    public class WakeOnLanService
    {
        #region Members
        private UdpClient _udpClient;
        #endregion

        #region Properties
        public int Port { get; set; } = 7;
        public BroadcastAddress BroadcastAddress { get; set; } = BroadcastAddress.Default;
        public UdpClient UdpClient
        {
            get {
                if (_udpClient == null)
                {
                    _udpClient = new UdpClient();
                    _udpClient.EnableBroadcast = true;
                    _udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                }
                return _udpClient;
            }
            set { _udpClient = value; }
        }
        #endregion

        #region Constructor
        #endregion

        #region Methods
        public void Wake(string macAddress)
        {
            Wake(MacAddress.Parse(macAddress));
        }
        public void Wake(MacAddress macAddress)
        {
            // Create the Magic Packet
            var packet = new MagicPacket(macAddress);

            // Send!
            UdpClient.Send(packet.Payload, packet.Payload.Length, new IPEndPoint(BroadcastAddress, Port));
        }
        #endregion
    }
}
