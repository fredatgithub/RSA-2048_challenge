using System;
using System.Collections.Generic;
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

      BigInteger number = BigInteger.Pow(ulong.MaxValue, 3);
      display($"The ulong max value to the power of 3 is {number}");
      // too long and may have an exception
      //number = BigInteger.Pow(ulong.MaxValue, int.MaxValue);
      //display($"The ulong max value to the power of int max value is {number}");

      //searching for factors
      for (BigInteger i = 2; i < 10; i++)
      {
        display($"The factors of {i} are {ListFactors(Factors(i))}");
      }

      //too long to run on a laptop, run it on a VM
      //display($"The factors of RSA2048 are {ListFactors(Factors(rsa2048))}");
      BigInteger b1 = 961748947;
      BigInteger b2 = 961748941;

      BigInteger tmpUlong = b1 * b2;
      display($"{tmpUlong}");
      if (tmpUlong % b2 == 0)
      {
        display($"{tmpUlong} est divisible par {b2} et {tmpUlong / b2}");
      }

      display(string.Empty);
      b1 =  BigInteger.Parse("2519590847565789349402718324004839857142928212620403202777713783604366202070759555626401852588078440691829064124951508218929855914917618450280848912007284499268739280728777673597141834727026189637501497182469116507761337985909570009733045974880842840179742910064245869181719511874612151517265463228221");
        
      b2 = BigInteger.Parse("6869987549182422433637259085141865462043576798423387184774447920739934236584823824281198163815010674810451660377306056201619676256133844143603833904414952634432190114657544454178424020924616515723350778707749817125772467962926386356373289912154831438167899885040445364023527381951378636564391212010397122822120720357");
      BigInteger b3 = b1 * b2;
      display($"{b1} {Environment.NewLine}multiplié par {Environment.NewLine}{b2}{Environment.NewLine} égal {Environment.NewLine}{b3}");
      display(string.Empty);
      display($"La longeur {b1.ToString().Length} multiplié par la longeur {b2.ToString().Length} égal à la longeur {b3.ToString().Length}");

      display(string.Empty);
      bool divisorfound = false;
      for (int i = 1; i < 3000; i = i+2)
      {
        b1 += i;
        if (rsa2048 % b1 == 0)
        {
          display($"{b1} divise RSA 2048 par {rsa2048 / b1}");
          divisorfound = true;
          break;
        }
      }

      if (!divisorfound)
      {
        display($"no divisor was found");
      }

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
        // to debug, uncomment following line
        //Console.WriteLine(i);
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