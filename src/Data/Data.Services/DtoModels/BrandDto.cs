﻿namespace Data.Services.DtoModels
{
    using Newtonsoft.Json;

    public class BrandDto : BaseDto
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
