using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experiments
{
    public class TestMath
    {
        public static int[] Factorize(int targetNum)
        {
            if (targetNum == 1)
            {
                return new[] { 1 };
            }

            int numBuf = targetNum;
            var factors = new List<int>();

            while (1 < numBuf)
            {
                bool isPrime = true;
                for (int dev = 2; dev <= Math.Ceiling(Math.Sqrt(numBuf)); dev++)
                {
                    if (numBuf % dev == 0)
                    {
                        factors.Add(dev);
                        numBuf /= dev;
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    factors.Add(numBuf);
                    break;
                }
            }

            return factors.ToArray();
        }
    }
}