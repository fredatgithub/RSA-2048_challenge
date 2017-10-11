using System;
using System.Numerics;

namespace BigIntDemo
{
  internal static class Program
  {
    private static void Main()
    {
      Action<string> display = Console.WriteLine;
      Action<string> displayOneLine = Console.Write;
      display("Big Int demo");
      BigInteger bigNumber = new BigInteger();
      bigNumber = BigInteger.Parse(long.MaxValue.ToString());
      display($"int64 max value has the value of {bigNumber.ToString()}");
      for (BigInteger i = 0; i < 100; i++)
      {
        if (IsPrime(i))
        {
          displayOneLine($"{i.ToString()} ");
        }
      }

      display(string.Empty);
      display("Press any key to exit:");
      Console.ReadKey();
    }

    private static bool IsPrime(BigInteger bigInt)
    {
      bool result = true;
      if (bigInt < 2)
      {
        return false;
      }

      if (bigInt == 2 || bigInt == 3 || bigInt == 5)
      {
        return true;
      }

      if (bigInt % 2 == 0)
      {
        return false;
      }

      for (int i = 3; i < Math.Sqrt((double)bigInt); i = i + 2)
      {
        if (bigInt % i == 0)
        {
          return false;
        }
      }

      return result;
    }
  }
}