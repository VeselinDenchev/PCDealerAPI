﻿namespace Data.Models.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Data.Models.Abstractions;
    using Data.Models.Abstractions.Interfaces;

    public class Product : BaseEntity, IProduct
    {
        public float Rating { get; set; }

        public IBrand Brand { get; set; }

        public Model Model { get; set; }

        public decimal Price { get; set; }

        public List<UploadedFile> Images { get; set; }

        public string Description { get; set; }

        [Range(0, short.MaxValue, ErrorMessage = "Quantity must be non negative!")]
        public short Quantity { get; set; }

        public ICollection<Specification> Specifications { get; set; }

        public Category Category { get; set; }

        public string Name { get; set; }
    }

}