using System.Data.Entity.ModelConfiguration;

namespace Northwind.Model.Mapping
{
    public class ShipperMap : EntityTypeConfiguration<Shipper>
    {
        public ShipperMap()
        {
            // Primary Key
            HasKey(t => t.ShipperId);

            // Properties
            this.Property(t => t.CompanyName)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.Phone)
                .HasMaxLength(24);

            // Table & Column Mappings
            this.ToTable("Shippers");
            this.Property(t => t.ShipperId).HasColumnName("ShipperID");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.Phone).HasColumnName("Phone");
        }
    }
}
