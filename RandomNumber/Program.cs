using System;
using System.Security.Cryptography;
using System.Linq;

namespace RandomNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            // When using System.Random, as long as the seed is determined, the sequence is definitive.
            RandBytesWithSeed(10, 5);  // always write out:  205, 173, 195, 47, 63.
            RandIntsWithSeed(10, 5);  // always write out:  2041175501     1613858733     1627583427     1487326767     1548100671
            RandDoubleWithSeed(10, 5);  // always write out: 0.950496411859289     0.7515115354915669     0.7579025941704878     0.6925904972909905     0.7208905516755257

            // Cryptography secure random, not so easy to predict, but still, pseudo random.
            CryptoRand(5);
        }

        static void RandBytesWithSeed(int seed, int length)
        {
            var rnd = new Random(seed);

            var randBytes = new byte[length];
            rnd.NextBytes(randBytes);
            Console.WriteLine("Five random bytes:");
            foreach (var b in randBytes)
            {
                Console.Write($"{b,5}");
            }
            Console.WriteLine();
        }

        static void RandIntsWithSeed(int seed, int length)
        {
            var rnd = new Random(seed);
            Console.WriteLine("Five random ints:");
            for (int i = 0; i < length; i++)
            {
                Console.Write($"{rnd.Next()}     ");
            }
            Console.WriteLine();
        }

        static void RandDoubleWithSeed(int seed, int length)
        {
            var rnd = new Random(seed);
            Console.WriteLine("Five random double:");
            for (int i = 0; i < length; i++)
            {
                Console.Write($"{rnd.NextDouble()}     ");
            }
            Console.WriteLine();
        }

        // Downside of this one is the RNG can only return byte.
        static void CryptoRand(int length)
        {
            using var rng = new RNGCryptoServiceProvider("jeff");
            var result = new byte[length];
            rng.GetBytes(result);
            Console.WriteLine("Crypto secure random bytes:");
            foreach (var b in result)
            {
                Console.Write($"{b,5}");
            }
            Console.WriteLine();
        }
    }
}
