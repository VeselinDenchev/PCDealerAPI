namespace Data.Services.DtoModels
{
    using Newtonsoft.Json;

    public class ImageDto : BaseDto
    {
        const string API_URL = "https://localhost:7282/";

        public ImageDto(string fileName, string fileExtension)
        {
            this.FileName = fileName;
            this.FileExtension = fileExtension;
            this.Path = $"images/{this.FullFileName}";
        }

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
        public string? Url => API_URL + this.Path;
    }
}
