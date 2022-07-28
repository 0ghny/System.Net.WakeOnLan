namespace WakeOnLan
{
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Represents a Magic Packet for Wake-on-Lan protocol
    /// </summary>
    /// <see cref="https://en.wikipedia.org/wiki/Wake-on-LAN"/>
    public class MagicPacket
    {
        #region Members
        /// <summary>
        /// Anywhere within its payload 6 bytes of all 255 (FF FF FF FF FF FF in hexadecimal)
        /// </summary>
        public byte[] Header = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
        public MacAddress _macAddress;
        #endregion

        #region Properties
        public byte[] Payload { get; }
        #endregion

        #region Constructor's
        public MagicPacket(MacAddress macAddress)
        {
            _macAddress = macAddress;

            Payload = ComposePayload();
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            StringBuilder hex = new StringBuilder(Payload.Length * 2);
            foreach (byte b in Payload)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        private byte[] ComposePayload()
        {
            // Makes sure the payload is clean
            int payloadIndex = 0;
            byte[] payload = new byte[17 * 6];
            // Add 6 bytes with value 255 (FF) in our payload
            for (int i = 0; i < 6; i++)
            {
                payload[payloadIndex] = 0xFF;
                payloadIndex++;
            }

            // Repeat the device MAC address sixteen times
            for (int j = 0; j < 16; j++)
            {
                for (int k = 0; k < _macAddress.ToString().Length; k += 2)
                {
                    var s = _macAddress.ToString().Substring(k, 2);
                    payload[payloadIndex] = byte.Parse(s, NumberStyles.HexNumber);
                    payloadIndex++;
                }
            }

            return payload;
        }
        #endregion
    }
}
