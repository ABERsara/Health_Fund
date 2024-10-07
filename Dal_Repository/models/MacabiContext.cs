using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dal_Repository.models;

public partial class MacabiContext : DbContext
{
    public MacabiContext()
    {
    }

    public MacabiContext(DbContextOptions<MacabiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<AppointmentsArchiv> AppointmentsArchivs { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-OMLCUK1;Database=Macabi;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__appointm__3213E83FA0AF13CA");

            entity.ToTable("appointments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppointmentDate).HasColumnName("appointmentDate");
            entity.Property(e => e.Doctor).HasColumnName("doctor");
            entity.Property(e => e.Medicine).HasColumnName("medicine");
            entity.Property(e => e.Patient)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("patient");

            entity.HasOne(d => d.DoctorNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.Doctor)
                .HasConstraintName("FK__appointme__docto__2C3393D0");

            entity.HasOne(d => d.MedicineNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.Medicine)
                .HasConstraintName("FK__appointme__medic__2D27B809");

            entity.HasOne(d => d.PatientNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.Patient)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__appointme__patie__2B3F6F97");
        });

        modelBuilder.Entity<AppointmentsArchiv>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__appointm__3213E83F1AC5DF91");

            entity.ToTable("appointmentsArchiv");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AppointmentDate).HasColumnName("appointmentDate");
            entity.Property(e => e.Doctor).HasColumnName("doctor");
            entity.Property(e => e.Medicine).HasColumnName("medicine");
            entity.Property(e => e.Patient)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("patient");

            entity.HasOne(d => d.DoctorNavigation).WithMany(p => p.AppointmentsArchivs)
                .HasForeignKey(d => d.Doctor)
                .HasConstraintName("FK__appointme__docto__30F848ED");

            entity.HasOne(d => d.MedicineNavigation).WithMany(p => p.AppointmentsArchivs)
                .HasForeignKey(d => d.Medicine)
                .HasConstraintName("FK__appointme__medic__31EC6D26");

            entity.HasOne(d => d.PatientNavigation).WithMany(p => p.AppointmentsArchivs)
                .HasForeignKey(d => d.Patient)
                .HasConstraintName("FK__appointme__patie__300424B4");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__doctors__3213E83F825231EB");

            entity.ToTable("doctors");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Specialty)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("specialty");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__medicine__3213E83FC2FF1030");

            entity.ToTable("medicines");

            entity.HasIndex(e => e.Name, "UQ__medicine__72E12F1B81D3EFFB").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Dosage).HasColumnName("dosage");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__patients__3213E83F5D2D1894");

            entity.ToTable("patients");

            entity.Property(e => e.Id)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.BirthDate).HasColumnName("birthDate");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
