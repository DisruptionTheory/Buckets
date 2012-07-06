using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Buckets
{
    public static class StringHashes
    {
        public static int Additive(string key, int size)
        {
            long hash = 0;
            foreach (char k in key.ToCharArray()) hash += k;
            return (int)hash % size;
        }

        public static int ExclusiveOr(string key, int size)
        {
            int hash = 0;
            foreach (char k in key.ToCharArray()) hash ^= k;
            return hash % size;
        }

        public static int SAX(string key, int size)
        {
            int hash = 0;
            foreach (char k in key.ToCharArray()) hash ^= (k << 5) + (k >> 2) + k;
            return hash % size;
        }

        public static int SDMB(string key, int size)
        {
            int hash = 0;
            foreach (char k in key.ToCharArray()) hash = k + (hash << 6) + (hash << 16) - hash;
            return hash % size;
        }

        public static int Bernstein(string key, int size)
        {
            int hash = 0;
            foreach (char k in key.ToCharArray()) hash = ((hash << 5) + hash) + k;
            return hash % size;
        }

        public static int BernsteinModified(string key, int size)
        {
            int hash = 0;
            foreach (char k in key.ToCharArray()) hash = ((hash << 5) + hash) ^ k;
            return hash % size;
        }
    }
}
