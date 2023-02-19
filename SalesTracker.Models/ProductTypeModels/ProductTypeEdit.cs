
using System.ComponentModel.DataAnnotations;

public class ProductTypeEdit
{
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
}