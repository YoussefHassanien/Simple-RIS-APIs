using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public partial class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public virtual DbSet<Doctor> Doctors { get; set; }

        public virtual DbSet<Patient> Patients { get; set; }

        public virtual DbSet<PatientData> PatientData { get; set; }

        public virtual DbSet<Person> Persons { get; set; }

        public virtual DbSet<Service> Services { get; set; }

        public virtual DbSet<Study> Studies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Doctors__3213E83F08E211BB");

                entity.ToTable(tb => tb.HasTrigger("trg_update_timestamp_doctors"));

                entity.HasIndex(e => e.PersonId, "UQ__Doctors__543848DE684AB303").IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CurrencyCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValue("EGP")
                    .HasColumnName("currency_code");
                entity.Property(e => e.Expertise)
                    .HasMaxLength(500)
                    .HasColumnName("expertise");
                entity.Property(e => e.PersonId).HasColumnName("person_id");
                entity.Property(e => e.Salary)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("salary");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Person).WithOne(p => p.Doctor)
                    .HasForeignKey<Doctor>(d => d.PersonId)
                    .HasConstraintName("FK__Doctors__person___5EBF139D");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Patients__3213E83F644B0C93");

                entity.ToTable(tb => tb.HasTrigger("trg_update_timestamp_patients"));

                entity.HasIndex(e => e.PersonId, "UQ__Patients__543848DE8A503916").IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true)
                    .HasColumnName("is_active");
                entity.Property(e => e.IsVip)
                    .HasDefaultValue(false)
                    .HasColumnName("is_vip");
                entity.Property(e => e.PersonId).HasColumnName("person_id");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Person).WithOne(p => p.Patient)
                    .HasForeignKey<Patient>(d => d.PersonId)
                    .HasConstraintName("FK__Patients__person__4D94879B");
            });

            modelBuilder.Entity<PatientData>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("Patient_Data");

                entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
                entity.Property(e => e.DoctorFirstName)
                    .HasMaxLength(50)
                    .HasColumnName("doctor_first_name");
                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
                entity.Property(e => e.DoctorLastName)
                    .HasMaxLength(50)
                    .HasColumnName("doctor_last_name");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("first_name");
                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("gender");
                entity.Property(e => e.IsActive).HasColumnName("is_active");
                entity.Property(e => e.IsVip).HasColumnName("is_vip");
                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name");
                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("mobile_number");
                entity.Property(e => e.PatientId).HasColumnName("patient_id");
                entity.Property(e => e.PersonId).HasColumnName("person_id");
                entity.Property(e => e.ServiceCost)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("service_cost");
                entity.Property(e => e.ServiceCurrency)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("service_currency");
                entity.Property(e => e.ServiceDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("service_description");
                entity.Property(e => e.ServiceId).HasColumnName("service_id");
                entity.Property(e => e.ServiceType)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("service_type");
                entity.Property(e => e.SocialSecurityNumber)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("social_security_number");
                entity.Property(e => e.StudyCreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("study_created_at");
                entity.Property(e => e.StudyId).HasColumnName("study_id");
                entity.Property(e => e.StudyUpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("study_updated_at");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Persons__3213E83F5CED2655");

                entity.ToTable(tb => tb.HasTrigger("trg_update_timestamp"));

                entity.HasIndex(e => e.MobileNumber, "UQ__Persons__30462B0F8F2D296D").IsUnique();

                entity.HasIndex(e => e.Email, "UQ_persons_email").IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");
                entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
                entity.Property(e => e.Email)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("email");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("first_name");
                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("gender");
                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name");
                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("mobile_number");
                entity.Property(e => e.Password)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("password");
                entity.Property(e => e.Role)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("role");
                entity.Property(e => e.SocialSecurityNumber)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("social_security_number");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Services__3213E83FAA54E505");

                entity.ToTable(tb => tb.HasTrigger("trg_update_timestamp_services"));

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Cost)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("cost");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");
                entity.Property(e => e.CurrencyCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValue("EGP")
                    .HasColumnName("currency_code");
                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("description");
                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("type");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Study>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Studies__3213E83FC41A002A");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");
                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
                entity.Property(e => e.PatientId).HasColumnName("patient_id");
                entity.Property(e => e.ServiceId).HasColumnName("service_id");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Doctor).WithMany(p => p.Studies)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Studies__doctor___73BA3083");

                entity.HasOne(d => d.Patient).WithMany(p => p.Studies)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Studies__patient__72C60C4A");

                entity.HasOne(d => d.Service).WithMany(p => p.Studies)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Studies__service__74AE54BC");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
