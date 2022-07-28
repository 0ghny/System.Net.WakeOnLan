namespace WakeOnLan.IntegrationTests
{
    using System.Net;
    using System.Threading;
    using FluentAssertions;
    using NUnit.Framework;
    using WakeOnLan.IntegrationTests.Support;

    [TestFixture()]
    public class WakeOnLanTests
    {
        [Test, Timeout(10000)]
        [Category("Integration")]
        public void Wake_SendCorrectUdpMessage_ToUdpServer()
        {
            // Arrange
            var port = 25002;
            string mac = "01:02:03:04:05:06";
            string expectedPayload = new MagicPacket(new MacAddress(mac)).ToString();

            WakeOnLanServer wakeOnLanServer = new WakeOnLanServer(IPAddress.Any, port);
            wakeOnLanServer.Start();
            wakeOnLanServer.IsStarted.Should().BeTrue();
            while (!wakeOnLanServer.IsStarted)
                Thread.Yield();

            WakeOnLanService wakeOnLan = new WakeOnLanService();
            wakeOnLan.Port = port;
            wakeOnLan.BroadcastAddress = BroadcastAddress.Parse(IPAddress.Loopback.ToString());

            // Act
            wakeOnLan.Wake(mac);

            // Let's wait until the server start to process a message
            while (!wakeOnLanServer.ProcessingMessage) { }
            // Let's wait now, until it finish to process the message
            while (wakeOnLanServer.ProcessingMessage) { }
            // Once message is processed we can stop the server
            wakeOnLanServer.Stop();

            // Assert that Payload has been sent to the server
            wakeOnLanServer.ReceivedBuffer.ToString().Should().Contain(expectedPayload);

        }
    }
}
