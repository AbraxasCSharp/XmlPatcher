using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace XmlPatcher
{
  public class XmlTextPatcher : IXmlPatcher
  {
    private readonly XmlDocument _xDocument;

    public XmlTextPatcher(string xmlText)
    {
      try
      {
        _xDocument = new XmlDocument();
        _xDocument.LoadXml(xmlText);
      }
      catch (Exception ex)
      {
        throw new Exception("No valid xml string was given, see inner exception for more details.", ex);
      }
    }

    private IXmlPatcher PatchAttribute(string xmlPath, string value)
    {
      var xAttribute = _xDocument.SelectSingleNode(xmlPath) as XmlAttribute;
      if (xAttribute != null)
      {
        xAttribute.Value = value;
      }

      return this;
    }

    private IXmlPatcher PatchElement(string xmlPath, string value)
    {
      var xElement = _xDocument.SelectSingleNode(xmlPath) as XmlElement;
      if (xElement != null)
      {
        xElement.InnerText = value;
      }

      return this;
    }

    public IXmlPatcher Patch(string xmlPath, string value)
    {
      var xElement = _xDocument.SelectSingleNode(xmlPath) as XmlElement;
      var xAttribute = _xDocument.SelectSingleNode(xmlPath) as XmlAttribute;

      if (xElement != null)
        return PatchElement(xmlPath, value);
      if (xAttribute != null)
        return PatchAttribute(xmlPath, value);

      throw new Exception(string.Format("XPath '{0}' can't be found.", xmlPath));
    }

    public string Build()
    {
      var wr = new StringWriter();
      _xDocument.Save(wr);
      return wr.GetStringBuilder().ToString();
    }
  }
}