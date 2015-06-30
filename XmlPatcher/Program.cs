using System;
using System.IO;
using System.Linq;

namespace XmlPatcher
{
  public class Program
  {
    private static int Main(string[] args)
    {
      if (args.Count() != 3)
      {
        Console.WriteLine("Expected input: [PathToFile] [xPath] [New value]");
        return 0;
      }

      string file = args[0];
      string xpath = args[1];
      string value = args[2];

      try
      {
        new Program().PatchFile(file, xpath, value);
        return 1;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return -1;
      }
    }

    public void PatchFile(string file, string xpath, string value)
    {
      IXmlPatcher patcher;

      using (var fileStream = new FileStream(file,
        FileMode.Open, FileAccess.Read, FileShare.Read))
      using (var sr = new StreamReader(fileStream))
      {
        string content = sr.ReadToEnd();
        patcher = new XmlTextPatcher(content);
      }

      patcher.Patch(xpath, value);

      using (var fileStream = new FileStream(file,
        FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
      using (var sw = new StreamWriter(fileStream))
      {
        sw.Write(patcher.Build());
      }
    }
  }
}
