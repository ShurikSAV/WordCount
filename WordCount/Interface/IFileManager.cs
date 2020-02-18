using System.IO;

namespace WordCount.Interface
{
    interface IFileManager
    {
        string Folder { get; set; }

        string FolderSelect();
        string[] GetFiles(string fileMask);
        StreamReader FileOpen(string fileFullName);
    }
}
