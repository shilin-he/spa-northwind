using System.Data.Entity.ModelConfiguration;

namespace Northwind.Model.Mapping
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            // Primary Key
            HasKey(t => t.OrderId);

            // Properties
            Property(t => t.CustomerId)
                .IsFixedLength()
                .HasMaxLength(5);

            Property(t => t.ShipName)
                .HasMaxLength(40);

            Property(t => t.ShipAddress.Addr)
                .HasMaxLength(60);

            Property(t => t.ShipAddress.City)
                .HasMaxLength(15);

            Property(t => t.ShipAddress.Region)
                .HasMaxLength(15);

            Property(t => t.ShipAddress.PostalCode)
                .HasMaxLength(10);

            Property(t => t.ShipAddress.Country)
                .HasMaxLength(15);

            // Table & Column Mappings
            ToTable("Orders");
            Property(t => t.OrderId).HasColumnName("OrderId");
            Property(t => t.CustomerId).HasColumnName("CustomerId");
            Property(t => t.EmployeeId).HasColumnName("EmployeeId");
            Property(t => t.OrderDate).HasColumnName("OrderDate");
            Property(t => t.RequiredDate).HasColumnName("RequiredDate");
            Property(t => t.ShippedDate).HasColumnName("ShippedDate");
            Property(t => t.ShipVia).HasColumnName("ShipVia");
            Property(t => t.Freight).HasColumnName("Freight");
            Property(t => t.ShipName).HasColumnName("ShipName");
            Property(t => t.ShipAddress.Addr).HasColumnName("ShipAddress");
            Property(t => t.ShipAddress.City).HasColumnName("ShipCity");
            Property(t => t.ShipAddress.Region).HasColumnName("ShipRegion");
            Property(t => t.ShipAddress.PostalCode).HasColumnName("ShipPostalCode");
            Property(t => t.ShipAddress.Country).HasColumnName("ShipCountry");

            // Relationships
            HasOptional(t => t.Customer)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.CustomerId);
            HasOptional(t => t.Employee)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.EmployeeId);
            HasOptional(t => t.Shipper)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.ShipVia);
        }
    }
}
