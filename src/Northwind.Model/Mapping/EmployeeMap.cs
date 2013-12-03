using System.Data.Entity.ModelConfiguration;

namespace Northwind.Model.Mapping
{
    public class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        public EmployeeMap()
        {
            // Primary Key
            HasKey(t => t.EmployeeId);

            // Properties
            Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(10);

            Property(t => t.Title)
                .HasMaxLength(30);

            Property(t => t.TitleOfCourtesy)
                .HasMaxLength(25);

            Property(t => t.Address.Addr)
                .HasMaxLength(60);

            Property(t => t.Address.City)
                .HasMaxLength(15);

            Property(t => t.Address.Region)
                .HasMaxLength(15);

            Property(t => t.Address.PostalCode)
                .HasMaxLength(10);

            Property(t => t.Address.Country)
                .HasMaxLength(15);

            Property(t => t.HomePhone)
                .HasMaxLength(24);

            Property(t => t.Extension)
                .HasMaxLength(4);

            Property(t => t.PhotoPath)
                .HasMaxLength(255);

            // Table & Column Mappings
            ToTable("Employees");
            Property(t => t.EmployeeId).HasColumnName("EmployeeID");
            Property(t => t.LastName).HasColumnName("LastName");
            Property(t => t.FirstName).HasColumnName("FirstName");
            Property(t => t.Title).HasColumnName("Title");
            Property(t => t.TitleOfCourtesy).HasColumnName("TitleOfCourtesy");
            Property(t => t.BirthDate).HasColumnName("BirthDate");
            Property(t => t.HireDate).HasColumnName("HireDate");
            Property(t => t.Address.Addr).HasColumnName("Address");
            Property(t => t.Address.City).HasColumnName("City");
            Property(t => t.Address.Region).HasColumnName("Region");
            Property(t => t.Address.PostalCode).HasColumnName("PostalCode");
            Property(t => t.Address.Country).HasColumnName("Country");
            Property(t => t.HomePhone).HasColumnName("HomePhone");
            Property(t => t.Extension).HasColumnName("Extension");
            Property(t => t.Photo).HasColumnName("Photo");
            Property(t => t.Notes).HasColumnName("Notes");
            Property(t => t.ThisReportsToEmployeeId).HasColumnName("ReportsTo");
            Property(t => t.PhotoPath).HasColumnName("PhotoPath");

            // Relationships
            HasMany(t => t.Territories)
                .WithMany(t => t.Employees)
                .Map(m =>
                {
                    m.ToTable("EmployeeTerritories");
                    m.MapLeftKey("EmployeeId");
                    m.MapRightKey("TerritoryID");
                });

            HasOptional(t => t.ThisReportsToEmployee)
                .WithMany(t => t.EmployeesReportToThis)
                .HasForeignKey(d => d.ThisReportsToEmployeeId);
        }
    }
}