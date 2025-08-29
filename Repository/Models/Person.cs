namespace Core.Models
{
    public partial class Person
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string MobileNumber { get; set; } = null!;

        public  string? Gender { get; set; }

        public  string? SocialSecurityNumber { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public  string? Email { get; set; }

        public  string? Password { get; set; }

        public  string? Role { get; set; }

        public virtual Doctor? Doctor { get; set; }

        public virtual Patient? Patient { get; set; }
    }
}


