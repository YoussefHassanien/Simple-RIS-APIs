namespace Core.Models
{
    public partial class Patient
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public bool? IsVip { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual Person Person { get; set; } = null!;

        public virtual ICollection<Study> Studies { get; set; } = [];
    }
}


