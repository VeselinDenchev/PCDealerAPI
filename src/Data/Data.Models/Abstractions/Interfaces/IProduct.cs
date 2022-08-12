namespace Data.Models.Abstractions.Interfaces
{
    using System.ComponentModel.DataAnnotations;

    using Data.Models.Abstractions.Interfaces.Base;
    using Data.Models.Entities;

    public interface IProduct : IName, IRating
    {
        public static int count = 0;

        public IBrand Brand { get; set; }

        public Model Model { get; set; }

        public decimal Price { get; set; }

        List<UploadedFile> Images { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        [Range(0, short.MaxValue, ErrorMessage = "Rating must be non negative!")]
        public short Quantity { get; set; }

        public ICollection<Specification> Specifications { get; set; }

        public Category Category { get; set; }
    }
}