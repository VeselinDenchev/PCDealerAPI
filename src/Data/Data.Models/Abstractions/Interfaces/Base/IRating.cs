namespace Data.Models.Abstractions.Interfaces.Base
{
    using System.ComponentModel.DataAnnotations;

    public interface IRating
    {
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5!")]
        public float? Rating { get; set; }
    }
}
