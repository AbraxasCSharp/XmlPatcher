namespace XmlPatcher
{
  public interface IXmlPatcher
  {
    IXmlPatcher Patch(string xmlPath, string value);

    string Build();
  }
}
