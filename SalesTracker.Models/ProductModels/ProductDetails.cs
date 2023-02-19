
public class ProductDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Cost { get; set; }
        public ProductTypeListItem ProductType { get; set; }
    }