# System.Net.WakeOnLan

Wake on lan utility for System.Net

## Usage

```csharp
var port = 25002;
string mac = "01:02:03:04:05:06";
// Create our WakeOnLan service
WakeOnLanService wakeOnLan = new WakeOnLanService();
wakeOnLan.Port = port;
wakeOnLan.BroadcastAddress = BroadcastAddress.Parse(IPAddress.Loopback.ToString());

// Wake !
wakeOnLan.Wake(mac);
```