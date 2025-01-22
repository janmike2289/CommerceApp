using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class CreateProductDTO
    {
        
        /// <summary>
        /// Model state validation is handled by the controller but throws a weird error statement. 
        /// Instead of using the field validation used in the entity class, we will use the data annotation validation in the DTO class and create a custom validation error.
        /// </summary>

        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public string Description { get; set; } = string.Empty;
        
        [Range(0.01, double.MaxValue, ErrorMessage ="Price must be greater than 0")]
        public decimal Price { get; set; }
        
        [Required]
        public string PictureURL { get; set; } = string.Empty;
        
        [Required]
        public string Type { get; set; } = string.Empty;
        
        [Required]
        public string Brand { get; set; } = string.Empty;
        
        [Range(1, int.MaxValue, ErrorMessage="Quantity in stock must be at least 1")]
        public int QuantityInStock { get; set; }
    }
}
