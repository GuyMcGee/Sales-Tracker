
using System.ComponentModel.DataAnnotations;

public class CustomerEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FullName { get; set; }

        public List<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
        public List<TransactionEntity> Transactions { get; set; } = new List<TransactionEntity>();

    }
