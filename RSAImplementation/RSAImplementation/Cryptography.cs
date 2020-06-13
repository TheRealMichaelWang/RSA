using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace RSAImplementation
{
    class Cryptography
    {
        public static byte[] Encrypt(PublicKey publicKey,byte[] data)
        {
            BigInteger toEncrypt = new BigInteger(data);
            BigInteger output = BigInteger.ModPow(toEncrypt, publicKey.E, publicKey.N);
            return output.ToByteArray();
        }

        public static byte[] Decrypt(PrivateKey privateKey, byte[] data)
        {
            BigInteger toDecrypt = new BigInteger(data);
            BigInteger output = BigInteger.ModPow(toDecrypt, privateKey.D, privateKey.N);
            return output.ToByteArray();
        }
    }
}
