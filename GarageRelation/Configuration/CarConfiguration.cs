using GarageRelation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarageRelation.Configuration
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> entity)
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Plate).HasMaxLength(8).IsRequired();
            entity.Property(e => e.Brand).IsRequired().HasMaxLength(40);
            entity.Property(e => e.Model).IsRequired().HasMaxLength(40);
            entity.Property(e => e.Year).IsRequired().HasMaxLength(10);
            entity.HasOne(e => e.Person).WithMany(e => e.Cars).HasForeignKey(e => e.PersonId).IsRequired();
        }
    }
}
