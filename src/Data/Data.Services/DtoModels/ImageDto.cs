namespace Data.Services.DtoModels
{
    using Newtonsoft.Json;

    public class ImageDto : BaseDto
    {
        // TODO: Constructor for setting properties

        public ImageDto()
        {

        }

        [JsonProperty(PropertyName = "fileName")]
        public string FileName { get; set; }

        [JsonProperty(PropertyName = "extension")]
        public string FileExtension { get; set; }

        [JsonProperty(PropertyName = "fullFileName")]
        public string FullFileName => this.FileName + this.FileExtension;

        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string? Url => "https://localhost:7168/" + this.Path;
    }
}
