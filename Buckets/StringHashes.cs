using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Buckets
{
    public static class StringHashes
    {
        public static uint Additive(string key, uint range)
        {
            long hash = 0;
            foreach (char k in key) hash += k;
            return (uint)hash % range;
        }

        public static uint ExclusiveOr(string key, uint range)
        {
            uint hash = 0;
            foreach (char k in key) hash ^= k;
            return hash % range;
        }

        public static uint SAX(string key, uint range)
        {
            uint hash = 0;
            foreach (char k in key) hash ^= (uint)((k << 5) + (k >> 2) + k);
            return hash % range;
        }

        public static uint SDMB(string key, uint range)
        {
            uint hash = 0;
            foreach (char k in key) hash = k + (hash << 6) + (hash << 16) - hash;
            return hash % range;
        }

        public static uint Bernstein(string key, uint range)
        {
            uint hash = 0;
            foreach (char k in key) hash = ((hash << 5) + hash) + k;
            return hash % range;
        }

        public static uint BernsteinModified(string key, uint range)
        {
            uint hash = 0;
            foreach (char k in key) hash = ((hash << 5) + hash) ^ k;
            return hash % range;
        }

        public static uint JenkinsOneAtATime(string key, uint range)
        {
            uint hash = 0;
            foreach (char k in key)
            {
                hash += k;
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }
            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);
            return hash % range;
        }

        public static uint FNV1(string key, uint range)
        {
            uint hash = 2166136261;
            foreach (char k in key)
            {
                hash *= 16777619;
                hash ^= k;
            }
            return hash % range;
        }

        public static uint FNV1A(string key, uint range)
        {
            uint hash = 2166136261;
            foreach (char k in key)
            {
                hash ^= k;
                hash *= 16777619;  
            }
            return hash % range;
        }
    }
}
