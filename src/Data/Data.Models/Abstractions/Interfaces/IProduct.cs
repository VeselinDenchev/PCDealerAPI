namespace Data.Models.Abstractions.Interfaces
{
    using System.ComponentModel.DataAnnotations;

    using Data.Models.Abstractions.Interfaces.Base;
    using Data.Models.Entities;

    public interface IProduct : IName, IRating, IQuantity
    {
        public static int count = 0;

        public Brand Brand { get; set; }

        public Model Model { get; set; }

        public decimal Price { get; set; }

        List<UploadedFile> Images { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        public ICollection<Specification> Specifications { get; set; }

        public Category Category { get; set; }
    }
}