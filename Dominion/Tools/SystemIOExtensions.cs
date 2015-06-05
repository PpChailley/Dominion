using System.IO;

namespace org.gbd.Dominion.Tools
{
  public static class SystemIoExtensions
  {
    /// <summary>
    ///   Recursively create directory
    /// </summary>
    /// <param name="dirInfo"> Folder path to create. </param>
    public static void CreateRecursive(this DirectoryInfo dirInfo)
    {
      if (dirInfo.Parent != null) CreateRecursive(dirInfo.Parent);
      if (!dirInfo.Exists) dirInfo.Create();
    }
  }
}