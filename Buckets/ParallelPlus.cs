using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace System
{
    public static class ParallelPlus
    {
        public static void StridingFor(int from, int to, int stride, Action<int> body)
        {
            int index = from;
            if (stride <= 0) stride = 1;
            int blocks = (to - from) / stride;
            Task[] tasks = new Task[blocks];
            for (int i = 0; i < blocks; i++)
            {
                int low = from + (i * stride);
                int high = low + stride - 1;
                tasks[i] = new Task(delegate { for (int j = low; j <= high; j++) body(j); });
                tasks[i].Start();
            }
            for (int i = 0; i < blocks; i++)
            {
                tasks[i].Wait();
            }
        }
    }
}



