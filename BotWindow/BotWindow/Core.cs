using System;
using System.Collections.Generic;
using System.Threading;

namespace BotWindow
{
    public static class Core
    {
        private static Dictionary<double, Thread> threadPool = new Dictionary<double, Thread>();
        public static DateTime UnixToDate(long unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        public static Thread RunThread(Action methodName)
        {
            ManualResetEvent syncEvent = new ManualResetEvent(false);
            double unixMilli = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
            Thread newThread = new Thread(
        () =>
        {
            syncEvent.Set();
            methodName();
            syncEvent.WaitOne();
            threadPool.Remove(unixMilli);
        }

    );
            if (threadPool.ContainsKey(unixMilli))
            {
                Thread.Sleep(5);
                threadPool.Add(unixMilli, newThread);
            }

            newThread.Start();
            return newThread;
        }
        public static void CloseAllThreads()
        {
            foreach (double key in threadPool.Keys)
            {
                threadPool[key].Abort();
            }
        }
    }
}
