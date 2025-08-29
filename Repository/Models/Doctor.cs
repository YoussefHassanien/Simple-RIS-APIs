namespace Core.Models
{
    public partial class Doctor
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public decimal Salary { get; set; }

        public string CurrencyCode { get; set; } = null!;

        public string? Expertise { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual Person Person { get; set; } = null!;

        public virtual ICollection<Study> Studies { get; set; } = [];
    }

}

