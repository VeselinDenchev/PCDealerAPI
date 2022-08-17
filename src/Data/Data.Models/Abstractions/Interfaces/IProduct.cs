namespace Data.Models.Abstractions.Interfaces
{
    using System.ComponentModel.DataAnnotations;

    using Data.Models.Abstractions.Interfaces.Base;
    using Data.Models.Entities;

    public interface IProduct : IName, IRating, IQuantity
    {
        public Model Model { get; set; }

        public string Processor { get; set; }

        public string Ram { get; set; }

        public string GPU { get; set; }

        public string Storage { get; set; }

        public string Display { get; set; }

        public decimal Price { get; set; }

        ICollection<ImageFile> Images { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }
    }
}