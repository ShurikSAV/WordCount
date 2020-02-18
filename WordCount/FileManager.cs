using System.IO;
using System.Linq;
using System.Windows.Forms;
using WordCount.Interface;

namespace WordCount
{
    class FileManager : IFileManager
    {
        public string Folder { get; set; } = string.Empty;

        public string FolderSelect()
        {
            var FolderDialog = new FolderBrowserDialog();

            if (FolderDialog.ShowDialog() != DialogResult.OK || string.IsNullOrWhiteSpace(FolderDialog.SelectedPath))
            {
                return string.Empty;
            }

            Folder = FolderDialog.SelectedPath;

            return Folder;
        }

        public string[] GetFiles(string fileMask)
        {
            if (string.IsNullOrEmpty(Folder)) return null;

            return new DirectoryInfo(Folder).GetFiles("*.txt", SearchOption.AllDirectories).Select(x => x.FullName).ToArray();
            
        }

        public StreamReader FileOpen(string fileFullName)
        {
            if (string.IsNullOrWhiteSpace(fileFullName)) return null;

            return new StreamReader(new FileStream(fileFullName, FileMode.Open, FileAccess.Read));
        }
    }
}
