using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Buckets
{
    public static class Generator
    {
        /// <summary>
        /// The random number generator.
        /// </summary>
        private static readonly Random RandomNumberGenerator = new Random();

        private const string charsAlphaNumeric = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz";

        /// <summary>
        /// Generates a random alpha-numeric string of the specified length.
        /// </summary>
        /// <param name="size">The length of the alpha numeric string to be generated.</param>
        /// <returns>A random alpha numeric string.</returns>
        public static string RandomAlphaNumeric(int size, bool threaded = false)
        {
            if (threaded)
            {
                char[] buffer = new char[size];
                lock (RandomNumberGenerator)
                {
                    for (int i = 0; i < size; i++)
                    {
                        buffer[i] = charsAlphaNumeric[RandomNumberGenerator.Next(charsAlphaNumeric.Length)];
                    }
                }
                return new string(buffer);
            }
            else
            {
                char[] buffer = new char[size];
                for (int i = 0; i < size; i++)
                {
                    buffer[i] = charsAlphaNumeric[RandomNumberGenerator.Next(charsAlphaNumeric.Length)];
                }
                return new string(buffer);
            }
        }

        /// <summary>
        /// Generates a random 32 bit integer.
        /// </summary>
        /// <returns>A random 32 bit integer.</returns>
        public static int RandomInteger()
        {
            lock (RandomNumberGenerator)
            {
                return RandomNumberGenerator.Next(int.MaxValue);
            }
        }
    }
}
