using System;
using System.Xml;
using NUnit.Framework;

namespace XmlPatcher.Tests
{
  [TestFixture]
  public class ExceptionTests
  {
    [Test]
    public void ExceptionIsThrownIfStringIsNotXmlConform()
    {
      var ex = Assert.Throws<Exception>(() => new XmlTextPatcher("IllegalXMlContent"));
      Assert.That(ex.Message, Is.EqualTo("No valid xml string was given, see inner exception for more details."));
      Assert.That(ex.InnerException, Is.TypeOf<XmlException>());
    }

    [Test]
    public void ExceptionIsThrownIfXPathForElementIsNotFound()
    {
      const string original = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
                              "<note>" +
                              "<to>Tove</to>" +
                              "<from>Jani</from>" +
                              "<heading>Reminder</heading>" +
                              "<body>Don't forget me this weekend!</body>" +
                              "</note>";
      var patcher = new XmlTextPatcher(original);

      const string notExistingXPath = "//note/too";
      var ex = Assert.Throws<Exception>(() => patcher.Patch(notExistingXPath, "Hans"));
      Assert.That(ex.Message, Is.EqualTo(string.Format("XPath '{0}' can't be found.", notExistingXPath)));
    }

    [Test]
    public void ExceptionIsThrownIfXPathForAttributeIsNotFound()
    {
      const string original = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
                              "<note>" +
                              "<to>Tove</to>" +
                              "<from>Jani</from>" +
                              "<heading>Reminder</heading>" +
                              "<body>Don't forget me this weekend!</body>" +
                              "</note>";
      var patcher = new XmlTextPatcher(original);

      const string notExistingXPath = "//note/@too";
      var ex = Assert.Throws<Exception>(() => patcher.Patch(notExistingXPath, "Hans"));
      Assert.That(ex.Message, Is.EqualTo(string.Format("XPath '{0}' can't be found.", notExistingXPath)));
    }
  }
}
