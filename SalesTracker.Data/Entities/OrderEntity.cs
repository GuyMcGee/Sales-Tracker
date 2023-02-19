
using System.ComponentModel.DataAnnotations;

public class OrderEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string location { get; set; }

        public List<ProductEntity> Products { get; set; } = new List<ProductEntity>();
        
        public List<TransactionEntity> Transactions { get; set; } = new List<TransactionEntity>();

    }

