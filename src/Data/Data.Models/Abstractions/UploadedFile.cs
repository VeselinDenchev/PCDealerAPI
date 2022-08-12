using Data.Models.Entities;

namespace Data.Models.Abstractions
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Data.Models.Interfaces;

    public abstract class UploadedFile : BaseEntity, IFile
    {
        public string FileName { get; set; }

        public string FileExtension { get; set; }

        [NotMapped]
        public string FullFileName => FileName + FileExtension;

        public string Path { get; set; }
    }
}
