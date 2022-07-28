using System;
using System.Collections.Generic;
using System.Text;

namespace WakeOnLan.Fluent
{
    public class FluentWakeOnLan
    {
        #region Members
        public List<string> _macAddresses;
        public List<BroadcastAddress> _broadcastAdresses;
        public List<int> _ports;
        #endregion

        #region CTOR's
        public FluentWakeOnLan()
        {
            _macAddresses = new List<string>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Send the wake on lan configured package
        /// </summary>
        public void Send()
        {
            var wol = new WakeOnLanService();

            foreach (var broadcastAddress in _broadcastAdresses)
            {
                foreach (var macAddress in _macAddresses)
                {
                    foreach (var port in _ports)
                    {
                        wol.BroadcastAddress = broadcastAddress;
                        wol.Port = port;
                        wol.Wake(macAddress);
                    }
                }
            }
        }
        #endregion
    }
}
