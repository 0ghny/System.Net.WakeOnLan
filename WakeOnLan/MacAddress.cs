using System;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace WakeOnLan
{
    /// <summary>
    /// Represents a mac address.
    /// This class exists, because i don't like how PhysicalAddress parse macaddress strings,
    /// as example, it doesn't support to create a new PhysicalAddress from "00:00:00:00:00:00".
    ///
    /// This class makes easier to create MacAddresses from different formats.
    /// </summary>
    public class MacAddress : PhysicalAddress
    {
        #region Members
        /// <summary>
        /// Regex expresion used to validate an string containing a MacAddress
        /// </summary>
        /// <exception cref="ArgumentException">Invalid mac address</exception>
        public static readonly string MacAddressRegex = @"^([0-9A-F]{2}[:-]){5}([0-9A-F]{2})$";
        #endregion

        #region Properties
        /// <summary>
        /// Organizationally Unique Identifier
        /// </summary>
        // TODO: We can get them from here: https://code.wireshark.org/review/gitweb?p=wireshark.git;a=blob_plain;f=manuf
        public string OUI => throw new NotImplementedException();
        #endregion

        #region Constructor's
        /// <summary>
        /// Creates a new instance from an string containing a mac address.
        /// </summary>
        /// <param name="address"></param>
        public MacAddress (string address) : base(MacAddress.From(address)) {}
        #endregion

        new public static MacAddress Parse(string address)
        {
            return new MacAddress(address);
        }

        private static byte[] From(string address)
        {
            var result = Regex.Match(address, MacAddressRegex);
            if (!result.Success && String.IsNullOrWhiteSpace(result.Value))
                throw new ArgumentException("Non valid mac address.", nameof(address));

            // Create the mac address using just the numbers
            return PhysicalAddress.Parse(result.Value.Replace(":","").Replace("-","")).GetAddressBytes();
        }
    }
}
