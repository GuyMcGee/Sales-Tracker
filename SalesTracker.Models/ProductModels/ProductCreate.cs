using System.ComponentModel.DataAnnotations;

public class ProductCreate
    {
        [Required]
        [MinLength(2, ErrorMessage = "{0} must be at least {1} Characters long.")]
        [MaxLength(100, ErrorMessage = "{0} must contain no more than {1} characters.")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public decimal Cost { get; set; }
        public int ProductTypeId { get; set; }
    }