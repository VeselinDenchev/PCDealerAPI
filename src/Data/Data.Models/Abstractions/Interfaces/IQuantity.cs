namespace Data.Models.Abstractions.Interfaces
{
    using System.ComponentModel.DataAnnotations;

    public interface IQuantity
    {

        [Range(0, short.MaxValue, ErrorMessage = "Rating must be non negative!")]
        public short Quantity { get; set; }
    }
}
