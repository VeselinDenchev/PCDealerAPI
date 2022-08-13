namespace Data.Services.DtoModels
{
    public class SpecificationDto : BaseDto
    {
        public SpecificationTypeDto Type { get; set; }

        public string Value { get; set; }
    }
}
