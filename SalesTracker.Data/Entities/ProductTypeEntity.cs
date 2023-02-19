
using System.ComponentModel.DataAnnotations;

public class ProductTypeEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name {get; set; } 
        public List<ProductEntity> Products { get; set; } = new List<ProductEntity>();
    }
