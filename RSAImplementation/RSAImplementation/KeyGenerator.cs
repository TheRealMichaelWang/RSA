using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace RSAImplementation
{
    class PublicKey
    { 
        public BigInteger N { get; private set; }
        public BigInteger E { get; private set; }

        public PublicKey(BigInteger n, BigInteger e)
        {
            this.N = n;
            this.E = e;
        }
    }

    class PrivateKey
    {
        public BigInteger N { get; private set; }
        public BigInteger D { get; private set; }

        public PrivateKey(BigInteger n, BigInteger d)
        {
            this.N = n;
            this.D = d;
        }
    }

    class KeyGenerator
    {
        public KeyGenerator()
        {

        }

        public static Tuple<PublicKey,PrivateKey> GenerateKeyPair(int size=1024)
        {
            BigInteger P = RandomPrime(size);
            BigInteger Q = RandomPrime(size);
            BigInteger N = BigInteger.Multiply(P, Q);
            BigInteger phi = BigInteger.Multiply(P - 1, Q - 1);
            BigInteger E;
            do
            {
                Random random = new Random();
                byte[] data = new byte[phi.ToByteArray().Length];
                random.NextBytes(data);
                E = new BigInteger(data);
            }
            while (E < 1 || GCD(E, phi) != 1);
            BigInteger D = ModInverse(E, phi);
            return new Tuple<PublicKey, PrivateKey>(new PublicKey(N, E), new PrivateKey(N, D));
        }

        public static BigInteger ModInverse(BigInteger a, BigInteger b)
        {
            BigInteger temp = b;
            BigInteger y = 0, x = 1;
            if(b == 1)
            {
                return 0;
            }
            while(a > 1)
            {
                BigInteger q = BigInteger.Divide(a, b);
                BigInteger t = b;
                b = BigInteger.ModPow(a, 1, b);
                a = t;
                t = y;
                y = x - q * y;
                x = t;
            }
            if(x<0)
            {
                x += temp;
            }
            return x;
        }

        public static BigInteger RandomPrime(int size)
        {
            BigInteger prime;
            do
            {
                Random random = new Random();
                byte[] data = new byte[size];
                random.NextBytes(data);
                prime = new BigInteger(data);
            }
            while (!IsPrime(prime) || prime <1);
            return prime;
        }

        public static BigInteger GCD(BigInteger a, BigInteger b)
        {
            if(b == 0)
            {
                return a;
            }
            else
            {
                return GCD(b, BigInteger.ModPow(a, 1, b));
            }
        }

        public static bool IsPrime(BigInteger n,int k=10)
        {
            if (n <= 1 || n == 4)
            {
                return false;
            }
            else if( n <= 3)
            {
                return true;
            }

            BigInteger r = BigInteger.Subtract(n, 1);
            while(r.IsEven)
            {
                r = BigInteger.Divide(r, 2);
            }

            for (int i = 0; i < k; i++)
            {
                if(!MillerRabin(r,n))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool MillerRabin(BigInteger r, BigInteger n)
        {
            Random random = new Random();
            int n_size = n.ToByteArray().Length;
            BigInteger a;
            do
            {
                byte[] data = new byte[n_size];
                random.NextBytes(data);
                a = new BigInteger(data) + 2;
            }
            while (a >= n || a<1);

            BigInteger x = BigInteger.ModPow(a, r, n);

            if(x == 1 || x == n-1)
            {
                return true;
            }

            while(r != n-1)
            {
                x = BigInteger.ModPow(x, 2, n);
                r = r * 2;
                if(x == 1)
                {
                    return false;
                }
                else if(x == n-1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
