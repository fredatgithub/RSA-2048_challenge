using System;
using System.Collections.Generic;
using System.Linq;
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
      for (BigInteger i = 0; i < 10; i++)
      {
        if (IsPrime(i))
        {
          display($"{i.ToString()} ");
        }
      }
      display("Searching for factors of RSA2048");
      BigInteger rsa2048 = new BigInteger();
      rsa2048 = BigInteger.Parse("25195908475657893494027183240048398571429282126204032027777137836043662020707595556264018525880784406918290641249515082189298559149176184502808489120072844992687392807287776735971418347270261896375014971824691165077613379859095700097330459748808428401797429100642458691817195118746121515172654632282216869987549182422433637259085141865462043576798423387184774447920739934236584823824281198163815010674810451660377306056201619676256133844143603833904414952634432190114657544454178424020924616515723350778707749817125772467962926386356373289912154831438167899885040445364023527381951378636564391212010397122822120720357");
      //if (IsPrime(rsa2048))
      //{
      //  display("there should be an error because RSA2048 should not be a prime");
      //}
      //else
      //{
      //  display("RSA2048 is not a prime because it is the product of two primes");
      //}

      BigInteger n1 = BigInteger.Pow(154382190, 3);
      BigInteger n2 = BigInteger.Multiply(1643590, 166935);
      try
      {
        Console.WriteLine("The greatest common divisor of {0} and {1} is {2}.",
          n1, n2, BigInteger.GreatestCommonDivisor(n1, n2));
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.WriteLine("Unable to calculate the greatest common divisor:");
        Console.WriteLine("   {0} is an invalid value for {1}",
          e.ActualValue, e.ParamName);
      }

      //searching for factors
      for (BigInteger i = 2; i < 10; i++)
      {
        display($"The factors of {i} are {ListFactors(Factors(i))}");
      }

      display($"The factors of RSA2048 are {ListFactors(Factors(rsa2048))}");

      display(string.Empty);
      display("Press any key to exit:");
      Console.ReadKey();
    }

    private static string ListFactors(IEnumerable<BigInteger> numbers)
    {
      string result = "= ";
      foreach (BigInteger number in numbers)
      {
        result += $"{number}, ";
      }

      return result.TrimEnd().TrimEnd(',');
    }

    private static IEnumerable<BigInteger> Factors(BigInteger bigInt)
    {
      List<BigInteger> result = new List<BigInteger>();
      if (bigInt < 2)
      {
        return result;
      }

      // a positive number is always divided by 1
      result.Add(1);
      if (bigInt == 2 || bigInt == 3 || bigInt == 5)
      {
        result.Add(bigInt);
        return result;
      }

      if (bigInt % 2 == 0)
      {
        result.Add(2);
      }

      for (BigInteger i = 3; i <= bigInt; i++)
      {
        Console.WriteLine(i);
        if (bigInt % i == 0)
        {
          result.Add(i);
        }
      }

      return result;
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