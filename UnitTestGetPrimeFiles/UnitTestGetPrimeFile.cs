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
  }
}