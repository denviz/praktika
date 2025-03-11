using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace praktika.Entities;

public partial class PraktikaContext : DbContext
{
    public PraktikaContext()
    {
    }

    public PraktikaContext(DbContextOptions<PraktikaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdCompany> AdCompanies { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectsStatus> ProjectsStatuses { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Specification> Specifications { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("host=localhost;port=5432;database=praktika;username=postgres;password=qwe123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdCompany>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("ad_companies_pkey");

            entity.ToTable("ad_companies");

            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Budget)
                .HasPrecision(10, 2)
                .HasColumnName("budget");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(100)
                .HasColumnName("company_name");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.Platform)
                .HasMaxLength(50)
                .HasColumnName("platform");
            entity.Property(e => e.ProjectInCompany).HasColumnName("project_in_company");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.ProjectInCompanyNavigation).WithMany(p => p.AdCompanies)
                .HasForeignKey(d => d.ProjectInCompany)
                .HasConstraintName("ad_companies_project_in_company_fkey");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("clients_pkey");

            entity.ToTable("clients");

            entity.HasIndex(e => e.ClientEmail, "clients_client_email_key").IsUnique();

            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.ClientEmail)
                .HasMaxLength(100)
                .HasColumnName("client_email");
            entity.Property(e => e.ClientName)
                .HasMaxLength(100)
                .HasColumnName("client_name");
            entity.Property(e => e.ClientPhone)
                .HasMaxLength(20)
                .HasColumnName("client_phone");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("employees_pkey");

            entity.ToTable("employees");

            entity.HasIndex(e => e.EmpEmail, "employees_emp_email_key").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EmpEmail)
                .HasMaxLength(100)
                .HasColumnName("emp_email");
            entity.Property(e => e.EmpPhone)
                .HasMaxLength(20)
                .HasColumnName("emp_phone");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.Fullname)
                .HasMaxLength(30)
                .HasColumnName("fullname");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(30)
                .HasColumnName("middle_name");
            entity.Property(e => e.Position)
                .HasMaxLength(100)
                .HasColumnName("position");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("projects_pkey");

            entity.ToTable("projects");

            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.ClientInProject).HasColumnName("client_in_project");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(100)
                .HasColumnName("project_name");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.ClientInProjectNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ClientInProject)
                .HasConstraintName("projects_client_in_project_fkey");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.Status)
                .HasConstraintName("projects_status_fkey");
        });

        modelBuilder.Entity<ProjectsStatus>(entity =>
        {
            entity.HasKey(e => e.ProjectStatusId).HasName("projects_status_pkey");

            entity.ToTable("projects_status");

            entity.Property(e => e.ProjectStatusId).HasColumnName("project_status_id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(30)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("reports_pkey");

            entity.ToTable("reports");

            entity.Property(e => e.ReportId).HasColumnName("report_id");
            entity.Property(e => e.GeneratedBy).HasColumnName("generated_by");
            entity.Property(e => e.GenerationDate).HasColumnName("generation_date");
            entity.Property(e => e.ProjectInReport).HasColumnName("project_in_report");
            entity.Property(e => e.ReportName)
                .HasMaxLength(100)
                .HasColumnName("report_name");

            entity.HasOne(d => d.GeneratedByNavigation).WithMany(p => p.Reports)
                .HasForeignKey(d => d.GeneratedBy)
                .HasConstraintName("reports_generated_by_fkey");

            entity.HasOne(d => d.ProjectInReportNavigation).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ProjectInReport)
                .HasConstraintName("reports_project_in_report_fkey");
        });

        modelBuilder.Entity<Specification>(entity =>
        {
            entity.HasKey(e => e.SpecId).HasName("specifications_pkey");

            entity.ToTable("specifications");

            entity.Property(e => e.SpecId).HasColumnName("spec_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.ProjectInSpecification).HasColumnName("project_in_specification");
            entity.Property(e => e.SpecName)
                .HasMaxLength(100)
                .HasColumnName("spec_name");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Specifications)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("specifications_created_by_fkey");

            entity.HasOne(d => d.ProjectInSpecificationNavigation).WithMany(p => p.Specifications)
                .HasForeignKey(d => d.ProjectInSpecification)
                .HasConstraintName("specifications_project_in_specification_fkey");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("tasks_pkey");

            entity.ToTable("tasks");

            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.AssignedTo).HasColumnName("assigned_to");
            entity.Property(e => e.Deadline).HasColumnName("deadline");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ProjectInTask).HasColumnName("project_in_task");
            entity.Property(e => e.TaskName)
                .HasMaxLength(100)
                .HasColumnName("task_name");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.AssignedTo)
                .HasConstraintName("tasks_assigned_to_fkey");

            entity.HasOne(d => d.ProjectInTaskNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectInTask)
                .HasConstraintName("tasks_project_in_task_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
