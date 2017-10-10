using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestGetPrimeFiles
{
  [TestClass]
  public class UnitTestGetPrimeFile
  {
    [TestMethod]
    public void TestMethodTrimApostrophe()
    {
      const string source = ",1,1,";
      const string expected = "1,1";
      string result = source.Trim(',');
      Assert.AreEqual(result, expected);
    }

    [TestMethod()]
    public void TestMethod_Add_To_Name()
    {
      PrivateType privateTypeObject = new PrivateType(typeof(GetPrimeFiles.FormMain));
      const string source = "primes1.txt";
      const string expected = "primes1-Lined.txt";
      object obj = privateTypeObject.InvokeStatic("AddToName", source);
      Assert.AreEqual(expected, (string)obj);
    }
  }
}