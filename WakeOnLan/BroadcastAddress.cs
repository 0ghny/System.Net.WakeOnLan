using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WakeOnLan
{
    public class BroadcastAddress : IPAddress
    {
        #region Members
        private const string _defaultBroadcastAddress = "255.255.255.255";
        #endregion

        #region Properties
        /// <summary>
        /// Default broadcast address to 255.255.255.255
        /// </summary>
        public static BroadcastAddress Default = new BroadcastAddress(_defaultBroadcastAddress);
        #endregion

        #region Constructor's
        public BroadcastAddress(string address) : base(BroadcastAddress.From(address))
        { }

        public BroadcastAddress(IPAddress address) : base(address.GetAddressBytes())
        { }
        #endregion

        #region Methods
        private static byte[] From(string ipAddress)
        {
            return IPAddress.Parse(ipAddress).GetAddressBytes();
        }
        new public static BroadcastAddress Parse(string address)
        {
            return new BroadcastAddress(address);
        }
        #endregion
    }
}
