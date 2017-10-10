using System;


namespace BigIntDemo
{
  internal static class Program
  {
    private static void Main()
    {
      Action<string> display = Console.WriteLine;
      display("Big Int demo");


      display("Press any key to exit:");
      Console.ReadKey();
    }
  }
}