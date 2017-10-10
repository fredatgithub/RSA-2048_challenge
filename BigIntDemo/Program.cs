using System;
using System.Numerics;

namespace BigIntDemo
{
  internal static class Program
  {
    private static void Main()
    {
      Action<string> display = Console.WriteLine;
      display("Big Int demo");
      BigInteger bigNumber = new BigInteger();
      bigNumber = BigInteger.Parse(long.MaxValue.ToString());
      display($"int64 max value has the value of {bigNumber.ToString()}");


      display("Press any key to exit:");
      Console.ReadKey();
    }

    private bool IsPrime(BigInteger big1)
    {
      
    }
  }
}