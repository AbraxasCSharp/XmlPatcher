using System.IO;
using NetBike.XmlUnit.NUnitAdapter;
using NUnit.Framework;

namespace XmlPatcher.Tests
{
  [TestFixture]
  public class FileTests
  {
    [Test]
    public void FileCanBePatched()
    {
      const string path = @".\Resources\Example.xml";
      ResourceHelper.WriteResourceToDisk("XmlPatcher.Tests.Resources.Example.xml", path);

      new Program().PatchFile(path, "//note/to", "Hans");
      const string expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
                              "<note>" +
                              "<to>Hans</to>" +
                              "<from>Jani</from>" +
                              "<heading>Reminder</heading>" +
                              "<body>Don't forget me this weekend!</body>" +
                              "</note>";

      string actual = File.ReadAllText(path);
      Assert.That(actual, IsXml.Equals(expected));
    }
  }
}
