using SapphireBootWPF.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SapphireBootWPF
{
    public static class BootClient
    {
        public static void StartClient(string sessionId, string serverLobbyAddress, string serverFrontierAddress)
        {
            // TODO: Send the CORRECT version of the client?
            string args = string.Format(
                "DEV.TestSID={0} DEV.UseSqPack=1 DEV.DataPathType=1 " +
                "DEV.LobbyHost01={1} DEV.LobbyPort01=54994 " +
                "DEV.LobbyHost02={1} DEV.LobbyPort02=54994 " +
                "DEV.LobbyHost03={1} DEV.LobbyPort03=54994 " +
                "DEV.LobbyHost04={1} DEV.LobbyPort04=54994 " +
                "DEV.LobbyHost05={1} DEV.LobbyPort05=54994 " +
                "DEV.LobbyHost06={1} DEV.LobbyPort06=54994 " +
                "DEV.LobbyHost07={1} DEV.LobbyPort07=54994 " +
                "DEV.LobbyHost08={1} DEV.LobbyPort08=54994 " +
                "SYS.Region=3 language={2} version=1.0.0.0 DEV.MaxEntitledExpansionID={3} DEV.GMServerHost={4} {5}",
                sessionId, serverLobbyAddress, Settings.Default.SavedLanguage, Settings.Default.ExpansionLevel, serverFrontierAddress, Settings.Default.LaunchParams);

            var startInfo = new ProcessStartInfo
            {
                Arguments = args,
                WorkingDirectory = Path.GetDirectoryName( Settings.Default.ClientPath ),
                FileName = Path.GetFileName( Settings.Default.ClientPath )
            };

            Process proc = Process.Start( startInfo );
        }
    }
}
