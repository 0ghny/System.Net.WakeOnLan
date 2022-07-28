namespace WakeOnLan.UnitTests
{
    using FluentAssertions;
    using NUnit.Framework;

    class MagicPacketTests
    {
        [Test]
        public void MagicPacket_WithValidMacAddress_isProperlyFormed()
        {
            // Arrange
            string sampleAddress = "01-00-00-00-00-02";
            string expectedPayload = "FFFFFFFFFFFF010000000002010000000002010000000002010000000002010000000002010000000002010000000002010000000002010000000002010000000002010000000002010000000002010000000002010000000002010000000002010000000002";
            MacAddress macAddress = new MacAddress(sampleAddress);
            // Act
            var magicPacket = new MagicPacket(macAddress);

            // Assert
            magicPacket.ToString().Should().BeEquivalentTo(expectedPayload);
        }
    }
}
