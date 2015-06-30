using System;
using System.IO;
using System.Xml;

namespace XmlPatcher
{
  public class XmlTextPatcher : IXmlPatcher
  {
    private readonly XmlDocument _xDocument;

    public XmlTextPatcher(string xmlText)
    {
      _xDocument = new XmlDocument();
      _xDocument.LoadXml(xmlText);
    }

    public IXmlPatcher Patch(string xmlPath, string value)
    {
      throw new NotImplementedException();
    }

    public string Build()
    {
      var wr = new StringWriter();
      _xDocument.Save(wr);
      return wr.GetStringBuilder().ToString();
    }
  }
}