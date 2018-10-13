using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter N:-");

                int N = int.Parse(Console.ReadLine());
                int p = 0, q = 0;
                for (int i = 2; i < N / 2; i++)
                {
                    if (N % i == 0 && isPrime(i) && isPrime(N / i))
                    {
                        p = i;
                        q = N / i;
                        break;
                    }
                }

                Console.WriteLine("P = " + p + ";Q= " + q);
                Console.WriteLine("(P-1)(Q-1) =" + (p - 1) * (q - 1));
                Console.WriteLine(string.Format("Possible e values:-{0}", String.Join(",", findRelativelyPrime((p - 1) * (q - 1)).ToArray())));
                Console.WriteLine("Choose e:-");
                int e = int.Parse(Console.ReadLine());
                int d = findPrivateKey(e, p, q);
                Console.WriteLine("Private key (d)->>  " + d);
                Console.WriteLine("Press E for encrypt & D for decrypt");
                switch (Console.ReadLine())
                {
                    case "E":
                        Console.WriteLine("Enter Plain Text for encryption:-");
                        int M = int.Parse(Console.ReadLine());
                        Console.WriteLine("Encrypted ->>   " + (Math.Pow(M, e)) % (double)N);
                        break;
                    case "D":
                        Console.WriteLine("Enter Cipher Text for decryption:-");
                        int C = int.Parse(Console.ReadLine());
                        Console.WriteLine("Decrypted ->>   " + (Math.Pow(C, d)) % (double)N);
                        break;
                    default:
                        break;
                }

                Console.ReadLine(); 
            }
        }


        public static int findPrivateKey(int e, int p, int q)
        {
            int d = 0;
            //ed = 1 mod (p-1)(q-1)

            for (int i = 2; i < (p - 1) * (q - 1); i++)
            {
                if ((i * e) % ((p - 1) * (q - 1)) == 1)
                {
                    d = i;
                    break;
                }
            }

            return d;
        }

        public static bool isPrime(int x)
        {
            bool result = true;
            for (int i = 2; i < x / 2; i++)
            {
                if (x % i == 0)
                    result = false;
            }
            return result;
        }

        public static List<int> findRelativelyPrime(int x)
        {

            List<int> result = new List<int>();
            for (int i = 2; i < x; i++)
            {
                if (isRelativelyPrime(i, x))
                    result.Add(i);
            }
            return result;
        }

        public static bool isRelativelyPrime(int a, int b)
        {
            bool result = false;
            int t;
            while (b != 0)
            {
                t = a;
                a = b;
                b = t % b;
            }
            if (a == 1)
                result = true;
            return result;

        }
    }
}
