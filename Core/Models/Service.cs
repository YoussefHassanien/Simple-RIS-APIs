namespace Core.Models
{
    public partial class Service
    {
        public int Id { get; set; }

        public string Type { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string CurrencyCode { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public decimal Cost { get; set; }

        public virtual ICollection<Study> Studies { get; set; } = [];
    }
}


