namespace Data.Models.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Constants;

    using Data.Models.Abstractions;
    using Data.Models.Abstractions.Interfaces;

    public class Product : BaseEntity, IProduct
    {
        public string Name { get; set; }

        public string Processor { get; set; }

        public string Ram { get; set; }

        public string GPU { get; set; }

        public string Storage { get; set; }

        public string Display { get; set; }

        public float? Rating { get; set; }

        public Model Model { get; set; }

        public decimal Price { get; set; }

        public ICollection<ImageFile> Images { get; set; }

        public string Description { get; set; }

        [Range(0, short.MaxValue, ErrorMessage = ErrorMessage.NEGATIVE_QUANTITY_MESSAGE)]
        public short Quantity { get; set; }
    }
}