using NetBike.XmlUnit.NUnitAdapter;
using NUnit.Framework;

namespace XmlPatcher.Tests
{
  [TestFixture]
  public class BasicTests
  {
    [Test]
    public void CanReplaceTextInUniqueElement()
    {
      const string original = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
                              "<note>" +
                              "<to>Tove</to>" +
                              "<from>Jani</from>" +
                              "<heading>Reminder</heading>" +
                              "<body>Don't forget me this weekend!</body>" +
                              "</note>";
      var patcher = new XmlTextPatcher(original);

      const string expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
                              "<note>" +
                              "<to>Hans</to>" +
                              "<from>Jani</from>" +
                              "<heading>Reminder</heading>" +
                              "<body>Don't forget me this weekend!</body>" +
                              "</note>";

      string actual = patcher.Patch("//note/to", "Hans").Build();
      Assert.That(actual, IsXml.Equals(expected));
    }

    [Test]
    public void CanReplaceTextOfOneItemInListOfElements()
    {
      const string original = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
                              "<inventory>" +
                              "<date year=\"2006\" month=\"8\" day=\"27\" />" +
                              "<items>" +
                              "<item quantity=\"5\" productCode=\"01\" description=\"PHP\"  unitCost=\"9.95\" />" +
                              "<item quantity=\"3\" productCode=\"02\" description=\"Perl\" unitCost=\"4.95\" />" +
                              "</items>" +
                              "</inventory>";
      var patcher = new XmlTextPatcher(original);

      const string expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
                              "<inventory>" +
                              "<date year=\"2006\" month=\"8\" day=\"27\" />" +
                              "<items>" +
                              "<item quantity=\"5\" productCode=\"01\" description=\"PHP\"  unitCost=\"9.95\">" +
                              "Codename Hans" +
                              "</item>" +
                              "<item quantity=\"3\" productCode=\"02\" description=\"Perl\" unitCost=\"4.95\">" +
                              "Codename Werner" +
                              "</item>" +
                              "</items>" +
                              "</inventory>";

      string actual = patcher
        .Patch("//items/item[@productCode='01']", "Codename Hans")
        .Patch("//items/item[@productCode='02']", "Codename Werner")
        .Build();
      Assert.That(actual, IsXml.Equals(expected));
    }

    [Test]
    public void CanReplaceTextInUniqueAttribute()
    {
      const string original = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
                              "<inventory>" +
                              "<date year=\"2006\" month=\"8\" day=\"27\" />" +
                              "<items>" +
                              "<item quantity=\"5\" productCode=\"01\" description=\"PHP\"  unitCost=\"9.95\" />" +
                              "<item quantity=\"3\" productCode=\"02\" description=\"Perl\" unitCost=\"4.95\" />" +
                              "</items>" +
                              "</inventory>";
      var patcher = new XmlTextPatcher(original);

      const string expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
                              "<inventory>" +
                              "<date year=\"2015\" month=\"8\" day=\"27\" />" +
                              "<items>" +
                              "<item quantity=\"5\" productCode=\"01\" description=\"PHP\"  unitCost=\"9.95\" />" +
                              "<item quantity=\"3\" productCode=\"02\" description=\"Perl\" unitCost=\"4.95\" />" +
                              "</items>" +
                              "</inventory>";
      string actual = patcher.Patch("//inventory/date/@year", "2015").Build();
      Assert.That(actual, IsXml.Equals(expected));
    }

    [Test]
    public void CanReplaceTextOfOneItemInListOfAttributes()
    {
      const string original = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
                              "<inventory>" +
                              "<date year=\"2006\" month=\"8\" day=\"27\" />" +
                              "<items>" +
                              "<item quantity=\"5\" productCode=\"01\" description=\"PHP\"  unitCost=\"9.95\" />" +
                              "<item quantity=\"3\" productCode=\"02\" description=\"Perl\" unitCost=\"4.95\" />" +
                              "</items>" +
                              "</inventory>";
      var patcher = new XmlTextPatcher(original);

      const string expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
                              "<inventory>" +
                              "<date year=\"2006\" month=\"8\" day=\"27\" />" +
                              "<items>" +
                              "<item quantity=\"5\" productCode=\"01\" description=\"C#\"  unitCost=\"9.95\" />" +
                              "<item quantity=\"3\" productCode=\"02\" description=\"F#\" unitCost=\"4.95\" />" +
                              "</items>" +
                              "</inventory>";
      string actual = patcher
        .Patch("//items/item[@productCode='01']/@description", "C#")
        .Patch("//items/item[@productCode='02']/@description", "F#")
        .Build();
      Assert.That(actual, IsXml.Equals(expected));
    }
  }
}
