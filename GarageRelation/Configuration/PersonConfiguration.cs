using GarageRelation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarageRelation.Configuration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> entity)
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(40);
            entity.Property(e => e.Age).IsRequired();
            entity.Property(e => e.MainAddress).IsRequired().HasMaxLength(40);
            entity.Property(e => e.Number).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Complement).HasMaxLength(255);
        }
    }
}
