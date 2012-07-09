using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace System
{
    public static class ParallelPlus
    {
        private static long cpuCount = Environment.ProcessorCount;

        public static void StridingFor(long from, long to, Action<long> body)
        {
            long stride = (to - from) / cpuCount;
            long blocks = cpuCount;
            IAsyncResult[] tasks = new IAsyncResult[blocks];
            for (long i = 0; i < blocks; i++)
            {
                long low = from + (i * stride);
                long high = low + stride - 1;
                if (to - low < high - low) high = to - low - 1;
                tasks[i] = (new Action(() => {for (long j = low; j <= high; j++) body(j); })).BeginInvoke(null, null);
            }
            for (long i = 0; i < blocks; i++) tasks[i].AsyncWaitHandle.WaitOne();
        }
    }
}



