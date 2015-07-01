namespace BaseSolution.Hooks
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Threading;

    internal class HookHelper
    {
        public static void Initialise()
        {
            CloseAll("IEDriverServer");
            KillAll("IEDriverServer");
            CloseAllMainWindows("iexplore");
            KillAll("iexplore");
        }

        private static void CloseAllMainWindows(string processName)
        {
            Process.GetProcesses()
                .Where(p => string.CompareOrdinal(p.ProcessName, processName) == 0)
                .ToList()
                .ForEach(p => { try { p.CloseMainWindow(); } catch { } });
        }

        private static void CloseAll(string processName)
        {
            Process.GetProcesses()
                .Where(p => string.CompareOrdinal(p.ProcessName, processName) == 0)
                .ToList()
                .ForEach(p => { try { p.Close(); } catch { } });
        }

        private static void KillAll(string processName)
        {
            Process.GetProcesses()
                .Where(p => string.CompareOrdinal(p.ProcessName, processName) == 0)
                .ToList()
                .ForEach(p => { try { p.Kill(); } catch { } });
        }
    }
}
