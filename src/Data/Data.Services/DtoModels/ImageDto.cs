namespace Data.Services.DtoModels
{
    using Constants;

    using Newtonsoft.Json;

    public class ImageDto : BaseDto
    {
        public ImageDto(string fileName, string fileExtension)
        {
            this.FileName = fileName;
            this.FileExtension = fileExtension;
            this.Path = string.Format(ImageConstant.IMAGES_PATH, this.FullFileName);
        }

        public ImageDto()
        {

        }

        [JsonProperty(PropertyName = JsonConstant.FILE_NAME_PROPERTY)]
        public string FileName { get; set; }

        [JsonProperty(PropertyName = JsonConstant.EXTENSION_PROPERTY)]
        public string FileExtension { get; set; }

        [JsonProperty(PropertyName = JsonConstant.FULL_FILE_NAME_PROPERTY)]
        public string FullFileName => this.FileName + this.FileExtension;

        [JsonProperty(PropertyName = JsonConstant.PATH_PROPERTY)]
        public string Path { get; set; }

        [JsonProperty(PropertyName = JsonConstant.URL_PROPERTY)]
        public string? Url => ImageConstant.API_URL + this.Path;
    }
}
