
using System.ComponentModel.DataAnnotations;

public class ProductTypeCreate
    {
        [Required]
        [MinLength(2, ErrorMessage = "{0} must be at least {1} Characters long.")]
        [MaxLength(100, ErrorMessage = "{0} must contain no more than {1} characters.")]
        public string Name { get; set; }
        // public List<ItemEntity> Items { get; set; }
    }