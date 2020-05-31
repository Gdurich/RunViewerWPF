using System.Windows.Media;

namespace RunViewer.Models
{
    public class ProgramItem
    {
        public ImageSource Icon { get; set; }
        public string FileName { get; set; }
        public string CommandString { get; set; }
        public string FilePath { get; set; }
        public string type { get; set; }
    }
}
