namespace Data.Models.Interfaces
{
    public interface IFile
    {
        public string FileName { get; set; }

        public string FileExtension { get; set; }

        public string Path { get; set; }

        public string Url => '/' + this.Path;
    }
}
