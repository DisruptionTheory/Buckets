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

        //Murmur Hashes taken from http://blog.kejser.org/murmurhash-in-c/

        public static uint MurmurHash2 (string key, uint range)
        {
	        // 'm' and 'r' are mixing constants generated offline.
	        // They're not really 'magic', they just happen to work well.

	        uint m = 0x5bd1e995;
	        int r = 24;
            uint len = (uint)key.Length;

	        // Initialize the hash to a 'random' value
            // random seed, any integer will work
	        uint h = 33 ^ len;
            string data = key;

	        // Mix 4 bytes at a time into the hash
            int index = 0;
	        while(len >= 4)
	        {
                uint k = (uint)(data[index++]
                 | data[index++] << 8
                 | data[index++] << 16
                 | data[index++] << 24);

		        k *= m;
                k ^= k >> r; 
		        k *= m; 
		
		        h *= m; 
		        h ^= k;

		        data += 4;
		        len -= 4;
	        }
	
	        // Handle the last few bytes of the input array
	        switch(len)
	        {
                case 3:
                    h ^= (UInt16)(data[index++]
                      | data[index++] << 8);
                    h ^= (UInt32)(data[index] << 16);
                    h *= m;
                    break;
                case 2:
                    h ^= (UInt16)(data[index++]
                      | data[index] << 8);
                    h *= m;
                    break;
                case 1:
                    h ^= data[index];
                    h *= m;
                    break;
                default:
                    break;
	        };

	        // Do a few final mixes of the hash to ensure the last few
	        // bytes are well-incorporated.

	        h ^= h >> 13;
	        h *= m;
	        h ^= h >> 15;

	        return h % range;
        }

        public static uint MurmurHash3(string key, uint range)
        {
            uint c1 = 0xcc9e2d51;
            uint c2 = 0x1b873593;

            int curLength = key.Length;    /* Current position in byte array */
            int length = curLength;   /* the const length we need to fix tail */
            uint h1 = 33;
            uint k1 = 0;

            /* body, eat stream a 32-bit int at a time */
            int index = 0;
            while (curLength >= 4)
            {
                /* Get four bytes from the input into an UInt32 */
                k1 = (UInt32)(key[index++]
                  | key[index++] << 8
                  | key[index++] << 16
                  | key[index++] << 24);

                /* bitmagic hash */
                k1 *= c1;
                k1 = rotl32(k1, 15);
                k1 *= c2;

                h1 ^= k1;
                h1 = rotl32(h1, 13);
                h1 = h1 * 5 + 0xe6546b64;
                curLength -= 4;
            }

            /* tail, the reminder bytes that did not make it to a full int */
            /* (this switch is slightly more ugly than the C++ implementation 
             * because we can't fall through) */
            switch (curLength)
            {
                case 3:
                    k1 = (uint)(key[index++]
                      | key[index++] << 8
                      | key[index++] << 16);
                    k1 *= c1;
                    k1 = rotl32(k1, 15);
                    k1 *= c2;
                    h1 ^= k1;
                    break;
                case 2:
                    k1 = (UInt32)(key[index++]
                      | key[index++] << 8);
                    k1 *= c1;
                    k1 = rotl32(k1, 15);
                    k1 *= c2;
                    h1 ^= k1;
                    break;
                case 1:
                    k1 = (UInt32)(key[index++]);
                    k1 *= c1;
                    k1 = rotl32(k1, 15);
                    k1 *= c2;
                    h1 ^= k1;
                    break;
            };

            // finalization, magic chants to wrap it all up
            h1 ^= (UInt32)length;
            h1 = fmix(h1);

            return h1 % range;
        }

        private static uint rotl32(uint x, byte r)
        {
            return (x << r) | (x >> (32 - r));
        }

        private static uint fmix(uint h)
        {
            h ^= h >> 16;
            h *= 0x85ebca6b;
            h ^= h >> 13;
            h *= 0xc2b2ae35;
            h ^= h >> 16;
            return h;
        }

    }
}
