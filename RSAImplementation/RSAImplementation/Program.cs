using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace RSAImplementation
{
    class Program
    {
        static void Main(string[] args)
        {
            Tuple<PublicKey, PrivateKey> keyPair = KeyGenerator.GenerateKeyPair(1);
            
        }
    }
}
