using System;
using System.IO;
using System.Reflection;

namespace XmlPatcher.Tests
{
  /// <summary>
  /// http://stackoverflow.com/questions/864140/write-file-from-assembly-resource-stream-to-disk
  /// </summary>
  public static class ResourceHelper
  {
    private static void CopyStream(Stream input, Stream output)
    {
      if (input == null) throw new ArgumentNullException("input");
      if (output == null) throw new ArgumentNullException("output");

      var buffer = new byte[8192];

      int bytesRead;
      while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
      {
        output.Write(buffer, 0, bytesRead);
      }
    }

    public static void WriteResourceToDisk(string resourceName, string path)
    {
      string directory = Path.GetDirectoryName(path);
      if (!Directory.Exists(directory))
      {
        Directory.CreateDirectory(directory);
      }

      using (Stream input = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
      using (Stream output = File.Create(path))
      {
        CopyStream(input, output);
      }
    }
  }
}
