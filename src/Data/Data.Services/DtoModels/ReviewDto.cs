﻿namespace Data.Services.DtoModels
{
    using Data.Models.Entities;

    using Newtonsoft.Json;

    public class ReviewDto : BaseDto
    {
        [JsonProperty(PropertyName = "user")]
        public User? User { get; set; }

        [JsonProperty(PropertyName = "product")]
        public ProductDto? Product { get; set; }

        [JsonProperty(PropertyName = "rating")]
        public float Rating { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }

        [JsonProperty(PropertyName = "madeOnDate")]
        public string? MadeOnDate =>
            base.CreatedAtUtc.HasValue
            ? DateTime.Parse(base.CreatedAtUtc.ToString()).ToLocalTime().ToString("dd/MM/yyyy HH:mm")
            : null;
    }
}